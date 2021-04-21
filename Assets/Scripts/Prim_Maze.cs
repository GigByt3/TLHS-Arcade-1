using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//using UnityEngine.XR.WSA.Input;

[RequireComponent(typeof(MeshFilter))]

public class Maze : MonoBehaviour
{
    public int width, height;

    public float cellWidth;
    public float cellHeight;

    public GameObject playerPrefab, exitPrefab;
    public GameObject[] enemyPrefabs;

    public int numberOfStartingEnemies;
    public float enemyDifficulty;
    public Player player;
    public ExitDoor exitDoor;

    public Dictionary<Vector3Int, GridObject> gridObjectDict;

    private bool[,,] walls;
    private List<Vector2Int> deadEndCells;

    private Material material;

    private float torchDensity;
    private int numOfTorches;
    private GameObject torchPrefab;

    //Method for assigning all the various fields of a maze, in lieu of a Java-esque "constructor" in C#
    public void MazeConstructor(int _width, int _height, GameObject _playerPrefab, GameObject _exitPrefab, GameObject[] _enemyPrefabs, Material _material, float _cellWidth, int _numberOfStartingEnemies, float _enemyDifficulty, float _torchDensity)
    {
        width = _width;
        height = _height;
        playerPrefab = _playerPrefab;
        exitPrefab = _exitPrefab;
        material = _material;
        enemyDifficulty = _enemyDifficulty;
        cellWidth = _cellWidth;
        cellHeight = _cellWidth;
        numberOfStartingEnemies = _numberOfStartingEnemies;
        enemyDifficulty = _enemyDifficulty;
        torchDensity = _torchDensity;
    }

    //Method to tell the maze when to actually start generating things, usually run after MazeConstructor
    public void Ready()
    {
        walls = new bool[width + 1, height + 1, 2];
        numOfTorches = (int) (width * height * torchDensity);
        torchPrefab = Resources.Load<GameObject>("Torch");

        Debug.Log("Made new maze of size " + width + ", " + height);

        populateMazeArray();

        generateMazeMesh();

        markDeadEndCells();

        populateGridObjects();

        placeTorches();
    }

    //Generates the maze data from a Randomized Prim's Algorithm
    //https://en.wikipedia.org/wiki/Maze_generation_algorithm
    void populateMazeArray()
    {
        //Sets all possible wall positions true
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                for (int k = 0; k < 2; k++)
                {
                    walls[i,j,k] = true;
                }
            }
        }

        //Do funky maze gen stuff
        List<Vector2Int> visitedCells = new List<Vector2Int>();
        List<Vector3Int> wallList = new List<Vector3Int>();

        Vector2Int initialCell = new Vector2Int((int) UnityEngine.Random.Range(1, width), UnityEngine.Random.Range(1, height));
        visitedCells.Add(initialCell);

        wallList.AddRange(getNeighboringWalls(initialCell));

        while(wallList.Count != 0)
        {
            Vector3Int activeWall = wallList[(int) UnityEngine.Random.Range(0, wallList.Count - 1)];
            List<Vector2Int> dividedCells = getNeighboringCells(activeWall);
            List<Vector2Int> unvisitedCells = new List<Vector2Int>();

            foreach (Vector2Int cell in dividedCells)
            {
                if (!visitedCells.Contains(cell)) unvisitedCells.Add(cell);
            }

            if (unvisitedCells.Count == 1)
            {
                walls[activeWall.x, activeWall.y, activeWall.z] = false;
                visitedCells.Add(unvisitedCells[0]);

                wallList.AddRange(getNeighboringWalls(unvisitedCells[0]));
            }

            wallList.Remove(activeWall);
        }
    }

    //Places various GridObjects in the maze, such as the player, enemies, and exit.
    void populateGridObjects()
    {
        //Place Player
        GameObject playerObject = Instantiate(playerPrefab);
        playerObject.name = "Player";
        player = playerObject.GetComponent<Player>();
        player.Ready();

        Vector3Int playerStartCoords = new Vector3Int(UnityEngine.Random.Range(0, width), UnityEngine.Random.Range(0, height), UnityEngine.Random.Range(0, 4));

        player.gridCoords = playerStartCoords;
        gridObjectDict = new Dictionary<Vector3Int, GridObject>();
        gridObjectDict.Add(playerStartCoords, player);
        
        //Place enemies
        for (int i = 0; i < numberOfStartingEnemies; i++)
        {
            bool startCoordsFound = false;
            Vector3Int possibleStartCoords = new Vector3Int(UnityEngine.Random.Range(0, width), UnityEngine.Random.Range(0, height), UnityEngine.Random.Range(0, 4));
            while (!startCoordsFound)
            {
                if (!isObjectAtCoords(possibleStartCoords.x, possibleStartCoords.y))
                {
                    startCoordsFound = true;
                }
                else
                {
                    possibleStartCoords = new Vector3Int(UnityEngine.Random.Range(0, width), UnityEngine.Random.Range(0, height), UnityEngine.Random.Range(0, 4));
                }
            }

            GameObject newZombie = Instantiate(Resources.Load<GameObject>("Zombie"), cellCoordsToGlobalCoords(possibleStartCoords.x, possibleStartCoords.y), Quaternion.identity);
            newZombie.GetComponent<Enemy>().EnemyConstructor(Enemy.EnemyType.Zombie);
            newZombie.GetComponent<Enemy>().gridCoords = possibleStartCoords;
            newZombie.GetComponent<Enemy>().Ready();
            gridObjectDict.Add(new Vector3Int(possibleStartCoords.x, possibleStartCoords.y, possibleStartCoords.z), newZombie.GetComponent<Enemy>());
        }

        //Place exit
        bool exitCoordsFound = false;
        Vector3Int possibleExitCoords = new Vector3Int();
        while (!exitCoordsFound)
        {
            int deadEndCellIndex = UnityEngine.Random.Range(0, deadEndCells.Count);
            Vector2Int chosenDeadEndCell = deadEndCells[deadEndCellIndex];
            if (!isObjectAtCoords(chosenDeadEndCell.x ,chosenDeadEndCell.y))
            {
                int openSide = 0;
                if (!getWallFromDirection(chosenDeadEndCell.x, chosenDeadEndCell.y, 0)) openSide = 0;
                if (!getWallFromDirection(chosenDeadEndCell.x, chosenDeadEndCell.y, 1)) openSide = 1;
                if (!getWallFromDirection(chosenDeadEndCell.x, chosenDeadEndCell.y, 2)) openSide = 2;
                if (!getWallFromDirection(chosenDeadEndCell.x, chosenDeadEndCell.y, 3)) openSide = 3;

                possibleExitCoords = new Vector3Int(chosenDeadEndCell.x, chosenDeadEndCell.y, openSide);

                exitCoordsFound = true;
            }
        }

        GameObject exitDoorObject = Instantiate(exitPrefab);
        exitDoor = exitDoorObject.GetComponent<ExitDoor>();
        exitDoor.ExitDoorConstructor(cellWidth, cellHeight);
        exitDoor.Ready();

        exitDoor.gridCoords = possibleExitCoords;
        gridObjectDict.Add(possibleExitCoords, exitDoor);
    }

    //Loops through all the cells, and adds all dead end cells to the deadEndCells list
    void markDeadEndCells()
    {
        deadEndCells = new List<Vector2Int>();

        for (int i = 0; i < width - 1; i++)
        {
            for (int j = 0; j < height - 1; j++)
            {
                if (getNeighboringWalls(new Vector2Int(i ,j)).Count == 3)
                {
                    deadEndCells.Add(new Vector2Int(i, j));
                }
            }
        }
    }

    //Places torches around the maze
    void placeTorches()
    {
        for (int i = 0; i < numOfTorches; i++)
        {
            int x = 0;
            int y = 0;
            List<int> adjacentWalls = new List<int>();

            while (adjacentWalls.Count <= 0)
            {
                x = UnityEngine.Random.Range(0, width - 1);
                y = UnityEngine.Random.Range(0, height - 1);
                adjacentWalls = getNeighboringWallsLocal(new Vector2Int(x, y));
            }

            int z = adjacentWalls[UnityEngine.Random.Range(0, adjacentWalls.Count - 1)];

            Vector3 torchOffset;

            switch (z)
            {
                case 0:
                    torchOffset = new Vector3(0, 0, -cellWidth / 2 + 0.5f);
                    break;

                case 1:
                    torchOffset = new Vector3(-cellWidth / 2 - 0.5f, 0, 0);
                    break;

                case 2:
                    torchOffset = new Vector3(0, 0, cellWidth / 2 - 0.5f);
                    break;

                case 3:
                    torchOffset = new Vector3(cellWidth / 2 + 0.5f, 0, 0);
                    break;

                default:
                    torchOffset = new Vector3(0, 0, 0);
                    break;
            }
            GameObject torch = Instantiate(torchPrefab, cellCoordsToGlobalCoords(x, y) + torchOffset + Vector3.up, Quaternion.identity);
            torch.transform.localScale = new Vector3(2, 2, 2);
            torch.transform.parent = gameObject.transform;
        }
    }

    //Generates the maze mesh based on the maze wall data
    void generateMazeMesh()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uv = new List<Vector2>();
        List<Vector3> normals = new List<Vector3>();

        Mesh currentWallSegment;
        int currentVertCount;

        for (int i = 0; i < width; i++)
        {
            currentWallSegment = generateWallSegment(new Vector3Int(i, -1, 1));
            currentVertCount = vertices.Count;

            vertices.AddRange(currentWallSegment.vertices);
            uv.AddRange(currentWallSegment.uv);
            normals.AddRange(currentWallSegment.normals);
            foreach (int triangleVert in currentWallSegment.triangles)
            {
                triangles.Add(triangleVert + currentVertCount);
            }

            for (int j = 0; j < height; j++)
            {
                currentWallSegment = generateWallSegment(new Vector3Int(-1, j, 0));
                currentVertCount = vertices.Count;

                vertices.AddRange(currentWallSegment.vertices);
                uv.AddRange(currentWallSegment.uv);
                normals.AddRange(currentWallSegment.normals);
                foreach (int triangleVert in currentWallSegment.triangles)
                {
                    triangles.Add(triangleVert + currentVertCount);
                }

                for (int k = 0; k < 2; k++)
                {
                    if (walls[i, j, k])
                    {
                        currentWallSegment = generateWallSegment(new Vector3Int(i, j, k));
                        currentVertCount = vertices.Count;

                        vertices.AddRange(currentWallSegment.vertices);
                        uv.AddRange(currentWallSegment.uv);
                        normals.AddRange(currentWallSegment.normals);
                        foreach (int triangleVert in currentWallSegment.triangles)
                        {
                            triangles.Add(triangleVert + currentVertCount);
                        }
                    }
                }
            }
        }
        mesh.vertices = vertices.ToArray();
        mesh.uv = uv.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.normals = normals.ToArray();

        GetComponent<MeshRenderer>().material = material;
    }

    //Generates the mesh for a given wall segment using its position
    Mesh generateWallSegment(Vector3Int wall)
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[8];
        int[] triangles = { 0, 1, 2, 2, 3, 0, 6, 5, 4, 4, 7, 6 };

        Vector2[] uv = new Vector2[8];
        Vector3[] normals = new Vector3[8];

        Vector3 correction = new Vector3(0, -0.5f * cellWidth, 0);

        Vector2 textureOffset = new Vector2(0.0f, 0.0f);
        if (UnityEngine.Random.Range(0, 6) == 0)
        {
            textureOffset.x = UnityEngine.Random.Range(0, 5) * 0.2f;
        }

        switch (wall.z)
        {
            case 0:
                for (int i = 0; i < 8; i++)
                {
                    if (i % 4 == 0)
                    {
                        vertices[i + 0] = correction + cellCoordsToGlobalCoords(wall.x + 0.5f, wall.y - 0.5f);
                        vertices[i + 1] = correction + cellCoordsToGlobalCoords(wall.x + 0.5f, wall.y - 0.5f) + Vector3.up * cellHeight;
                        vertices[i + 2] = correction + cellCoordsToGlobalCoords(wall.x + 0.5f, wall.y + 0.5f) + Vector3.up * cellHeight;
                        vertices[i + 3] = correction + cellCoordsToGlobalCoords(wall.x + 0.5f, wall.y + 0.5f);
                    }
                    normals[i] = (i >= 4) ? Vector3.left : Vector3.right;
                }
                break;

            case 1:
                for (int i = 0; i < 8; i++)
                {
                    if (i % 4 == 0)
                    {
                        vertices[i + 0] = correction + cellCoordsToGlobalCoords(wall.x - 0.5f, wall.y + 0.5f);
                        vertices[i + 1] = correction + cellCoordsToGlobalCoords(wall.x - 0.5f, wall.y + 0.5f) + Vector3.up * cellHeight;
                        vertices[i + 2] = correction + cellCoordsToGlobalCoords(wall.x + 0.5f, wall.y + 0.5f) + Vector3.up * cellHeight;
                        vertices[i + 3] = correction + cellCoordsToGlobalCoords(wall.x + 0.5f, wall.y + 0.5f);
                    }
                    normals[i] = (i >= 4) ? Vector3.back : Vector3.forward;
                }
                break;
        }

        for (int i = 0; i < 2; i++)
        {
            uv[i * 4 + 0] = new Vector2(0, 0) + textureOffset;
            uv[i * 4 + 1] = new Vector2(0, 1) + textureOffset;
            uv[i * 4 + 2] = new Vector2(0.2f, 1) + textureOffset;
            uv[i * 4 + 3] = new Vector2(0.2f, 0) + textureOffset;
        }
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        mesh.normals = normals;

        return mesh;
    }

    //Returns a list of which walls are around a given cell
    List<int> getNeighboringWallsLocal(Vector2Int cellCoords)
    {
        List<int> neighboringWalls = new List<int>();

        if (cellCoords.x > 0 && cellCoords.y > 0)
        {
            if (walls[cellCoords.x, cellCoords.y - 1, 1]) neighboringWalls.Add(0);
            if (walls[cellCoords.x, cellCoords.y, 0]) neighboringWalls.Add(1);
            if (walls[cellCoords.x, cellCoords.y, 1]) neighboringWalls.Add(2);
            if (walls[cellCoords.x - 1, cellCoords.y, 0]) neighboringWalls.Add(3);
        }
        else if (cellCoords.x == 0 && cellCoords.y > 0)
        {
            if (walls[cellCoords.x, cellCoords.y - 1, 1]) neighboringWalls.Add(0);
            if (walls[cellCoords.x, cellCoords.y, 0]) neighboringWalls.Add(1);
            if (walls[cellCoords.x, cellCoords.y, 1]) neighboringWalls.Add(2);
            neighboringWalls.Add(3);
        }
        else if (cellCoords.x > 0 && cellCoords.y == 0)
        {
            neighboringWalls.Add(0);
            if (walls[cellCoords.x, cellCoords.y, 0]) neighboringWalls.Add(1);
            if (walls[cellCoords.x, cellCoords.y, 1]) neighboringWalls.Add(2);
            if (walls[cellCoords.x - 1, cellCoords.y, 0]) neighboringWalls.Add(3);
        }
        return neighboringWalls;
    }

    //Returns a list of the coords of which walls are around a given cell
    List<Vector3Int> getNeighboringWalls(Vector2Int cellCoords)
    {
        List<Vector3Int> neighboringWalls = new List<Vector3Int>();
        List<Vector3Int> wallsThatDontExist = new List<Vector3Int>();

        neighboringWalls.Add(new Vector3Int(cellCoords.x, cellCoords.y, 0));
        neighboringWalls.Add(new Vector3Int(cellCoords.x, cellCoords.y, 1));
        neighboringWalls.Add(new Vector3Int(cellCoords.x - 1, cellCoords.y, 0));
        neighboringWalls.Add(new Vector3Int(cellCoords.x, cellCoords.y - 1, 1));

        foreach (Vector3Int wall in neighboringWalls)
        {
            if (!doesWallExist(wall)) wallsThatDontExist.Add(wall);
        }

        foreach (Vector3Int wall in wallsThatDontExist)
        {
            neighboringWalls.Remove(wall);
        }

        return neighboringWalls;
    }

    //Returns a list of all cells on either sides of a given wall
    List<Vector2Int> getNeighboringCells(Vector3Int wallCoords)
    {
        List<Vector2Int> neighboringCells = new List<Vector2Int>();
        int wallX = wallCoords.x;
        int wallY = wallCoords.y;
        int wallDir = wallCoords.z;

        switch(wallDir)
        {
            case 0:
                Vector2Int westCell = new Vector2Int(wallX, wallY);
                if(doesCellExist(westCell)) neighboringCells.Add(westCell);

                Vector2Int eastCell = new Vector2Int(wallX + 1, wallY);
                if(doesCellExist(eastCell)) neighboringCells.Add(eastCell);

                break;
            case 1:
                Vector2Int northCell = new Vector2Int(wallX, wallY);
                if(doesCellExist(northCell)) neighboringCells.Add(northCell);

                Vector2Int southCell = new Vector2Int(wallX, wallY + 1);
                if(doesCellExist(southCell)) neighboringCells.Add(southCell);

                break;
            default:
                Debug.Log("Input Wall direction was (" + wallDir + "). Why TF is this happening");
                break;
        }

        return neighboringCells;
    }

    //Teleports the given GridObject to the given coordinates
    //WARNING: DOES NOT CHECK IF COORDS ARE INSIDE THE MAZE
    public void teleportObject(GridObject objectToMove, int x, int y)
    {
        if (gridObjectDict.ContainsKey(new Vector3Int(objectToMove.gridCoords.x, objectToMove.gridCoords.y, objectToMove.gridCoords.z)))
            gridObjectDict.Remove(new Vector3Int(objectToMove.gridCoords.x, objectToMove.gridCoords.y, objectToMove.gridCoords.z));
        gridObjectDict.Add(new Vector3Int(x, y, objectToMove.gridCoords.z), objectToMove);

        objectToMove.handleMove(new Vector2Int(x, y));
    }

    //Attempts to remove a given GridObject from the dictionary, does nothing if it's not there to begin with
    public void removeObject(GridObject objectToRemove)
    {
        if (gridObjectDict.ContainsKey(objectToRemove.gridCoords))
            gridObjectDict.Remove(objectToRemove.gridCoords);
        Destroy(objectToRemove.gameObject);
    }

    //Returns whether or not there is a GridObject at the given coords
    public bool isObjectAtCoords(int x, int y)
    {
        for (int i = 0; i < 4; i++)
        {
            if (gridObjectDict.ContainsKey(new Vector3Int(x, y, i))) return true;
        }
        return false;
    }

    //Returns whether or not the player is at the given coords
    public bool isPlayerAtCoords(int x, int y)
    {
        for (int i = 0; i < 4; i++)
        {
            if (gridObjectDict.ContainsKey(new Vector3Int(x, y, i)))
            {
                if (gridObjectDict[new Vector3Int(x, y, i)] == player) return true;
            }
        }
        return false;
    }

    //Returns whether or not the maze exit is at the given coords
    public bool isExitAtCoords(int x, int y)
    {
        for (int i = 0; i < 4; i++)
        {
            if (gridObjectDict.ContainsKey(new Vector3Int(x, y, i)))
            {
                if (gridObjectDict[new Vector3Int(x, y, i)] == exitDoor) return true;
            }
        }
        return false;
    }

    //Moves the given GridObject forward the given spaces, as long as there are not walls in the way
    public void moveObject(GridObject objectToMove, int distance)
    {
        int tilesMoved = 0;
        while (tilesMoved < distance)
        {
            if (!getWallFromDirection(objectToMove.gridCoords.x, objectToMove.gridCoords.y, objectToMove.gridCoords.z))
            {
                switch (objectToMove.gridCoords.z)
                {
                    case 0:
                        if (objectToMove.gridCoords.y > 0)
                        {
                            int xToMoveTo = objectToMove.gridCoords.x;
                            int yToMoveTo = objectToMove.gridCoords.y - 1;

                            if (!isObjectAtCoords(xToMoveTo, yToMoveTo))
                            {
                                for (int i = 0; i < 4; i++)
                                    if (gridObjectDict.ContainsKey(new Vector3Int(objectToMove.gridCoords.x, objectToMove.gridCoords.y, i)))
                                        gridObjectDict.Remove(new Vector3Int(objectToMove.gridCoords.x, objectToMove.gridCoords.y, i));
                                objectToMove.gridCoords.y--;
                                gridObjectDict.Add(objectToMove.gridCoords, objectToMove);

                                objectToMove.handleMove(new Vector2Int(xToMoveTo, yToMoveTo));
                            }
                        }
                        break;
                    case 1:
                        if (objectToMove.gridCoords.x + 1 < width)
                        {
                            int xToMoveTo = objectToMove.gridCoords.x + 1;
                            int yToMoveTo = objectToMove.gridCoords.y;

                            if (!isObjectAtCoords(xToMoveTo, yToMoveTo))
                            {
                                for (int i = 0; i < 4; i++)
                                    if (gridObjectDict.ContainsKey(new Vector3Int(objectToMove.gridCoords.x, objectToMove.gridCoords.y, i)))
                                        gridObjectDict.Remove(new Vector3Int(objectToMove.gridCoords.x, objectToMove.gridCoords.y, i));
                                objectToMove.gridCoords.x++;
                                gridObjectDict.Add(objectToMove.gridCoords, objectToMove);

                                objectToMove.handleMove(new Vector2Int(xToMoveTo, yToMoveTo));
                            }
                        }
                        break;
                    case 2:
                        if (objectToMove.gridCoords.y + 1 < height)
                        {
                            int xToMoveTo = objectToMove.gridCoords.x;
                            int yToMoveTo = objectToMove.gridCoords.y + 1;

                            if (!isObjectAtCoords(xToMoveTo, yToMoveTo))
                            {
                                for (int i = 0; i < 4; i++)
                                    if (gridObjectDict.ContainsKey(new Vector3Int(objectToMove.gridCoords.x, objectToMove.gridCoords.y, i)))
                                        gridObjectDict.Remove(new Vector3Int(objectToMove.gridCoords.x, objectToMove.gridCoords.y, i));
                                objectToMove.gridCoords.y++;
                                gridObjectDict.Add(objectToMove.gridCoords, objectToMove);

                                objectToMove.handleMove(new Vector2Int(xToMoveTo, yToMoveTo));
                            }
                        }
                        break;
                    case 3:
                        if (objectToMove.gridCoords.x > 0)
                        {
                            int xToMoveTo = objectToMove.gridCoords.x - 1;
                            int yToMoveTo = objectToMove.gridCoords.y;

                            if (!isObjectAtCoords(xToMoveTo, yToMoveTo))
                            {
                                for (int i = 0; i < 4; i++)
                                    if (gridObjectDict.ContainsKey(new Vector3Int(objectToMove.gridCoords.x, objectToMove.gridCoords.y, i)))
                                        gridObjectDict.Remove(new Vector3Int(objectToMove.gridCoords.x, objectToMove.gridCoords.y, i));
                                objectToMove.gridCoords.x--;
                                gridObjectDict.Add(objectToMove.gridCoords, objectToMove);

                                objectToMove.handleMove(new Vector2Int(xToMoveTo, yToMoveTo));
                            }
                        }
                        break;
                    default:
                        return;
                }
                tilesMoved++;
            }
            else return;
        }
    }

    //Sets the given GridObject's rotation to the given direction
    public void setObjectRotation(GridObject objectToRotate, int direction)
    {
        if (gridObjectDict.ContainsKey(new Vector3Int(objectToRotate.gridCoords.x, objectToRotate.gridCoords.y, objectToRotate.gridCoords.z)))
            gridObjectDict.Remove(new Vector3Int(objectToRotate.gridCoords.x, objectToRotate.gridCoords.y, objectToRotate.gridCoords.z));
        gridObjectDict.Add(new Vector3Int(objectToRotate.gridCoords.x, objectToRotate.gridCoords.y, direction), objectToRotate);

        objectToRotate.handleRotation(direction);
    }

    /*//Updates the in-scene positions of all GridObjects in the dictionary based on the dictionary
    public void updateGridObjectPositions()
    {
        foreach (KeyValuePair<Vector3Int, GridObject> kvp in gridObjectDict)
        {
            kvp.Value.transform.position = cellCoordsToGlobalCoords(kvp.Key.x, kvp.Key.y);
            switch (kvp.Key.z)
            {
                case 0:
                    kvp.Value.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                    break;
                case 1:
                    kvp.Value.transform.rotation = Quaternion.Euler(0.0f, 270.0f, 0.0f);
                    break;
                case 2:
                    kvp.Value.transform.rotation = Quaternion.identity;
                    break;
                case 3:
                    kvp.Value.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
                    break;
                default:
                    kvp.Value.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                    break;
            }
        }
    }*/

    //Converts given float x and y coords to a Vector3 with height cellWidth / 2
    public Vector3 cellCoordsToGlobalCoords(float x, float y)
    {
        return new Vector3((-x * cellWidth), cellWidth / 2.0f, (y * cellWidth));
    }

    public Quaternion cellDirectionToGlobalRotation(int direction)
    {
        switch (direction)
        {
            case 0:
                return Quaternion.Euler(0.0f, 180.0f, 0.0f);
            case 1:
                return Quaternion.Euler(0.0f, 270.0f, 0.0f);
            case 2:
                return Quaternion.identity;
            case 3:
                return Quaternion.Euler(0.0f, 90.0f, 0.0f);
            default:
                return Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }
    }

    //Returns the raw wall data at a given coords and side
    //WARNING: DO NOT ATTEMPT TO ASK FOR A SIDE LARGER THAN 1
    public bool getWallFromCoords(int x, int y, int side)
    {
        return walls[x, y, side];
    }

    //Sets a given wall to the given bool value
    public void setWallFromDirection(int x, int y, int side, bool newValue)
    {
        switch (side)
        {
            case 0:
                if (y > 0)
                {
                    walls[x, y - 1, 1] = newValue;
                }
                break;
            case 1:
                if (x + 1 < width)
                {
                    walls[x, y, 0] = newValue;
                }
                break;
            case 2:
                if (y + 1 < height)
                {
                    walls[x, y, 1] = newValue;
                }
                break;
            case 3:
                if (x > 0)
                {
                    walls[x - 1, y, 0] = newValue;
                }
                break;
        }
    }

    //Returns the value of a given wall
    public bool getWallFromDirection(int x, int y, int side)
    {
        switch (side)
        {
            case 0:
                if (y <= 0)
                {
                    return true;
                }
                else
                {
                    return walls[x, y - 1, 1];
                }
            case 1:
                if (x + 1 >= width)
                {
                    return true;
                }
                else
                {
                    return walls[x, y, 0];
                }
            case 2:
                if (y + 1 >= height)
                {
                    return true;
                }
                else
                {
                    return walls[x, y, 1];
                }
            case 3:
                if (x <= 0)
                {
                    return true;
                }
                else
                {
                    return walls[x - 1, y, 0];
                }
            default:
                return true;
        }
    }

    //Returns whether or not the given cell is in the maze
    bool doesCellExist(Vector2Int cell)
    {
        return !((cell.x < 0) || (cell.x >= width) || (cell.y < 0) || (cell.y >= height));
    }

    //Returns whether or not a given wall exists in the maze
    bool doesWallExist(Vector3Int wall)
    {
        if (!doesCellExist(new Vector2Int(wall.x, wall.y)) || wall.z > 1) return false;
        return walls[wall.x, wall.y, wall.z];
    }
}
