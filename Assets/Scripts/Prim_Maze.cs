using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

[RequireComponent(typeof(MeshFilter))]

public class Maze : MonoBehaviour
{
    public int width, height;

    public float cellWidth;
    public float cellHeight;

    public GameObject wallPrefab, playerPrefab;
    public GameObject[] enemyPrefabs;

    public int numberOfStartingEnemies;
    public float enemyDifficulty;
    public Player player;
    
    public Dictionary<Vector3Int, GridObject> gridObjectDict;

    private bool[,,] walls;
    private List<Vector2Int> deadEndCells;
    
    public void MazeConstructor(int _width, int _height, GameObject _wallPrefab, GameObject _playerPrefab, GameObject[] _enemyPrefabs, float _cellWidth, int _numberOfStartingEnemies, float _enemyDifficulty)
    {
        width = _width;
        height = _height;
        wallPrefab = _wallPrefab;
        playerPrefab = _playerPrefab;
        enemyDifficulty = _enemyDifficulty;
        cellWidth = _cellWidth;
        cellHeight = _cellWidth;
        numberOfStartingEnemies = _numberOfStartingEnemies;
        enemyDifficulty = _enemyDifficulty;
    }

    public void Ready()
    {
        walls = new bool[width + 1, height + 1, 2];

        Debug.Log("Made new maze of size " + width + ", " + height);

        populateMazeArray();

        generateMazeMesh();

        markDeadEndCells();

        populateGridObjects();
    }

    void Update()
    {
        updateGridObjectPositions();
    }

    void populateMazeArray()
    {
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

    void populateGridObjects()
    {
        GameObject playerObject = Instantiate(playerPrefab);
        playerObject.name = "Player";
        player = playerObject.GetComponent<Player>();
        player.Ready();

        Vector3Int playerStartCoords = new Vector3Int(UnityEngine.Random.Range(0, width), UnityEngine.Random.Range(0, height), UnityEngine.Random.Range(0, 4));

        player.gridCoords = playerStartCoords;
        gridObjectDict = new Dictionary<Vector3Int, GridObject>();
        gridObjectDict.Add(playerStartCoords, player);

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

            Debug.Log("Placing enemy at " + possibleStartCoords.x + ", " + possibleStartCoords.y + " with rotation " + possibleStartCoords.z);
            GameObject newZombie = Instantiate(Resources.Load<GameObject>("Zombie"), cellCoordsToGlobalCoords(possibleStartCoords.x, possibleStartCoords.y), Quaternion.identity);
            newZombie.GetComponent<Enemy>().EnemyConstructor(Enemy.EnemyType.Zombie);
            newZombie.GetComponent<Enemy>().gridCoords = possibleStartCoords;
            newZombie.GetComponent<Enemy>().Ready();
            gridObjectDict.Add(new Vector3Int(possibleStartCoords.x, possibleStartCoords.y, possibleStartCoords.z), newZombie.GetComponent<Enemy>());
        }
    }

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
    }

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
                    normals[i] = Vector3.right;
                    //normals[i] = (i >= 4) ? Vector3.left : Vector3.right;
                    //Debug.Log(i + " " + normals[i]);
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
                    normals[i] = Vector3.back;
                    //normals[i] = (i >= 4) ? Vector3.forward : Vector3.back;
                    //Debug.Log(i + " " + normals[i]);
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

    List<Vector3Int> getNeighboringWalls(Vector2Int cellCoords)
    {
        List<Vector3Int> neighboringWalls = new List<Vector3Int>();
        List<Vector3Int> wallsThatDontExist = new List<Vector3Int>();

        neighboringWalls.Add(new Vector3Int(cellCoords.x, cellCoords.y, 0));
        neighboringWalls.Add(new Vector3Int(cellCoords.x, cellCoords.y, 1));
        neighboringWalls.Add(new Vector3Int(cellCoords.x - 1, cellCoords.y, 0));
        neighboringWalls.Add(new Vector3Int(cellCoords.x, cellCoords.y - 1, 1));

        foreach(Vector3Int wall in neighboringWalls)
        {
            if (!doesWallExist(wall)) wallsThatDontExist.Add(wall);
        }

        foreach(Vector3Int wall in wallsThatDontExist)
        {
            neighboringWalls.Remove(wall);
        }

        return neighboringWalls;
    }

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
    public void teleportObject(GridObject objectToMove, int x, int y)
    {
        if (gridObjectDict.ContainsKey(new Vector3Int(objectToMove.gridCoords.x, objectToMove.gridCoords.y, objectToMove.gridCoords.z)))
            gridObjectDict.Remove(new Vector3Int(objectToMove.gridCoords.x, objectToMove.gridCoords.y, objectToMove.gridCoords.z));
        gridObjectDict.Add(new Vector3Int(x, y, objectToMove.gridCoords.z), objectToMove);
    }

    public bool isObjectAtCoords(int x, int y)
    {
        for (int i = 0; i < 4; i++)
        {
            if (gridObjectDict.ContainsKey(new Vector3Int(x, y, i))) return true;
        }
        return false;
    }

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
    
    public void setObjectRotation(GridObject objectToRotate, int direction)
    {
        if (gridObjectDict.ContainsKey(new Vector3Int(objectToRotate.gridCoords.x, objectToRotate.gridCoords.y, objectToRotate.gridCoords.z)))
            gridObjectDict.Remove(new Vector3Int(objectToRotate.gridCoords.x, objectToRotate.gridCoords.y, objectToRotate.gridCoords.z));
        gridObjectDict.Add(new Vector3Int(objectToRotate.gridCoords.x, objectToRotate.gridCoords.y, direction), objectToRotate);
    }
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
    }

    public Vector3 cellCoordsToGlobalCoords(float x, float y)
    {
        return new Vector3((-x * cellWidth), cellWidth / 2.0f, (y * cellWidth));
    }

    public bool getWallFromCoords(int x, int y, int side)
    {
        return walls[x, y, side];
    }

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

    bool doesCellExist(Vector2Int cell)
    {
        return !((cell.x < 0) || (cell.x >= width) || (cell.y < 0) || (cell.y >= height));
    }

    bool doesWallExist(Vector3Int wall)
    {
        if (!doesCellExist(new Vector2Int(wall.x, wall.y)) || wall.z > 1) return false;
        return walls[wall.x, wall.y, wall.z];
    }
}
