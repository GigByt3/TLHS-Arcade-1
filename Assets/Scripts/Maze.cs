using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class Maze : MonoBehaviour
{
    public int mazeWidth, mazeHeight;

    public GameObject wallPrefab;

    public float cellWidth;

    private bool[,,] walls;

    private Dictionary<Vector3Int, GridObject> gridObjectDict;

    public Maze(int mWidth, int mHeight)
    {
        mazeWidth = mWidth;
        mazeHeight = mHeight;
    }
    
    // Start is called before the first frame update
    void Awake()
    {
        //cellWidth = 1;

        walls = new bool[mazeWidth + 1, mazeHeight + 1, 2];

        populateMazeArray();

        generateMazeMesh();

        populateGridObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* void populateMazeArray()
    {
        //set all walls true
        for (int i = 0; i < mazeWidth; i++)
        {
            for (int j = 0; j < mazeHeight; j++)
            {
                walls[i, j, 0] = true;
                walls[i, j, 1] = true;
            }
        }

        //define stack of previous cells
        Stack<Vector2Int> previousCellStack = new Stack<Vector2Int>();
        Stack<Vector2Int> visitedCellStack = new Stack<Vector2Int>();

        int currentCellX = UnityEngine.Random.Range(1, mazeWidth - 2);
        int currentCellY = UnityEngine.Random.Range(1, mazeHeight - 2);

        //previousCellStack.Push(new Vector2Int(currentCellX, currentCellY));
        //visitedCellStack.Push(new Vector2Int(currentCellX, currentCellY));

        Debug.Log("Starting at Cell " + currentCellX + ", " + currentCellY);
        Debug.Log("walls has " + walls.Length);

        while (visitedCellStack.Count < mazeWidth * mazeHeight)
        {
            int faceToBreak = chooseAdjacentWallToBreak(currentCellX, currentCellY, previousCellStack, visitedCellStack);

            Debug.Log("Trying to break face " + faceToBreak + " from Cell " + currentCellX + ", " + currentCellY);

            if (faceToBreak == -1)
            {
                //go back
                Debug.Log("No face found to break from Cell " + currentCellX + ", " + currentCellY);

                //Add cell to list of visited cells if it isnt already
                if (!visitedCellStack.Contains(new Vector2Int(currentCellX, currentCellY))) visitedCellStack.Push(new Vector2Int(currentCellX, currentCellY));

                currentCellX = previousCellStack.Peek().x;
                currentCellY = previousCellStack.Pop().y;

                Debug.Log("Going back to Cell " + currentCellX + ", " + currentCellY);
            }
            else
            {
                Debug.Log("Face " + faceToBreak + " broken successfully!");

                //break wall
                setWallFromDirection(currentCellX, currentCellY, faceToBreak, false);

                //Add cell to list of previous cells
                previousCellStack.Push(new Vector2Int(currentCellX, currentCellY));

                //Add cell to list of visited cells if it isnt already
                if (!visitedCellStack.Contains(new Vector2Int(currentCellX, currentCellY))) visitedCellStack.Push(new Vector2Int(currentCellX, currentCellY));

                //move through

                int oldCellX = currentCellX;
                int oldCellY = currentCellY;

                currentCellX = getNewThroughWallCoords(oldCellX, oldCellY, faceToBreak).x;
                currentCellY = getNewThroughWallCoords(oldCellX, oldCellY, faceToBreak).y;

                Debug.Log("Moved from currentCell into Cell " + currentCellX + ", " + currentCellY);
            }

            Debug.Log("previousCellStack has " + previousCellStack.Count);
            Debug.Log("visitedCellStack has " + visitedCellStack.Count);
        } 
    }

    int chooseAdjacentWallToBreak(int x, int y, Stack<Vector2Int> previousCellStack, Stack<Vector2Int> visitedCellStack)
    {
        ArrayList availableWalls = new ArrayList();

        int lastFaceBroken = -1;

        if (previousCellStack.Count > 0)
        {
            int previousCellX = previousCellStack.Peek().x;
            int previousCellY = previousCellStack.Peek().y;

            if (previousCellX - x == 0 && previousCellY - y == 0)
            {
                //idfk clearly something went wrong
            }
            else if (previousCellX - x >= 1 && previousCellY - y == 0)
            {
                lastFaceBroken = 1;
            }
            else if (previousCellX - x <= -1 && previousCellY - y == 0)
            {
                lastFaceBroken = 3;
            }
            else if (previousCellX - x == 0 && previousCellY - y >= 1)
            {
                lastFaceBroken = 0;
            }
            else if (previousCellX - x == 0 && previousCellY - y <= -1)
            {
                lastFaceBroken = 2;
            }
        }

        if (y > 0 && getWallFromDirection(x, y, 0) && !visitedCellStack.Contains(getNewThroughWallCoords(x, y, 0))) availableWalls.Add(0);
        if (x < mazeWidth - 1 && getWallFromDirection(x, y, 1) && !visitedCellStack.Contains(getNewThroughWallCoords(x, y, 1))) availableWalls.Add(1);
        if (y < mazeHeight - 1 && getWallFromDirection(x, y, 2) && !visitedCellStack.Contains(getNewThroughWallCoords(x, y, 2))) availableWalls.Add(2);
        if (x > 0 && getWallFromDirection(x, y, 3) && !visitedCellStack.Contains(getNewThroughWallCoords(x, y, 3))) availableWalls.Add(3);

        if (availableWalls.Contains(lastFaceBroken)) availableWalls.Remove(lastFaceBroken);

        if (availableWalls.Count <= 0) /*availableWalls.Add(lastFaceBroken)*//* return -1;

        int pickedWall = (int) availableWalls[UnityEngine.Random.Range(0, availableWalls.Count - 1)];

        Debug.Log("Selected wall " + pickedWall + " from " + availableWalls.Count + " choices...");

        return pickedWall;
    } */

    void populateMazeArray()
    {
        for (int i = 0; i < mazeWidth; i++)
        {
            for (int j = 0; j < mazeHeight; j++)
            {
                for (int k = 0; k < 2; k++)
                {
                    if (doesWallExist(new Vector3Int(i,j,k)))
                    {
                        walls[i,j,k] = true;
                    }
                }
            }
        }

        List<Vector2Int> visitedCells = new List<Vector2Int>();
        List<Vector3Int> wallList = new List<Vector3Int>();

        Vector2Int initialCell = new Vector2Int(UnityEngine.Random.Range(1, mazeWidth), UnityEngine.Random.Range(1, mazeHeight));

        visitedCells.Add(initialCell);
        wallList.AddRange(getNeighboringWalls(initialCell));

        while(wallList.Count != 0)
        {
            Vector3Int activeWall = wallList.Item[UnityEngine.Random.Range(0, wallList.Count - 1)];
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

    }

    void generateMazeMesh()
    {
        //ALL CODE IN HERE WILL BE REWRITTEN ENTIRELY
        //JUST TEMPORARY FOR MAKING MAZE GEN ALGORITHM
        
        GameObject southWalls = new GameObject("South Walls");
        southWalls.transform.parent = this.transform;
        
        for (int i = 0; i < mazeWidth; i++)
        {
            GameObject southWall = Instantiate(wallPrefab);
            southWall.name = i + " South Wall";
            southWall.transform.parent = southWalls.transform;
            southWall.transform.position = cellCoordsToGlobalCoords(i, mazeHeight, 0, -0.5f);
            southWall.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }

        GameObject westWalls = new GameObject("West Walls");
        westWalls.transform.parent = this.transform;

        for (int j = 0; j < mazeWidth; j++)
        {
            GameObject westWall = Instantiate(wallPrefab);
            westWall.name = j + " West Wall";
            westWall.transform.parent = westWalls.transform;
            westWall.transform.position = cellCoordsToGlobalCoords(0, j, 0.5f, 0);
            westWall.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
        }

        for (int i = 0; i < mazeWidth; i++)
        {
            for (int j = 0; j < mazeHeight; j++)
            {
                GameObject cell = new GameObject("Cell " + i + ", " + j);
                cell.transform.position = cellCoordsToGlobalCoords(i, j, 0, 0);
                cell.transform.parent = this.transform;

                if (walls[i, j, 0])
                {
                    GameObject northWall = Instantiate(wallPrefab);
                    northWall.name = i + ", " + j + " North Wall";
                    northWall.transform.parent = cell.transform;
                    northWall.transform.position = cellCoordsToGlobalCoords(i, j, 0, -0.5f);
                    northWall.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                }

                if (walls[i, j, 1])
                {
                    GameObject eastWall = Instantiate(wallPrefab);
                    eastWall.name = i + ", " + j + " East Wall";
                    eastWall.transform.parent = cell.transform;
                    eastWall.transform.position = cellCoordsToGlobalCoords(i, j, -0.5f, 0);
                    eastWall.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
                }
            }
        }
    }

    List<Vector3Int> getNeighboringWalls(Vector2Int cellCoords)
    {
        List<Vector3Int> neighboringWalls = new List<Vector3Int>();
       neighboringWalls.Add(new Vector3Int(cellCoords.x, cellCoords.y, 0));
       neighboringWalls.Add(new Vector3Int(cellCoords.x, cellCoords.y, 1));
       neighboringWalls.Add(new Vector3Int(cellCoords.x - 1, cellCoords.y, 0));
       neighboringWalls.Add(new Vector3Int(cellCoords.x, cellCoords.y - 1, 1));

        foreach(Vector3Int wall in neighboringWalls)
        {
            if (!doesWallExist(wall))neighboringWalls.Remove(wall);
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
                Vector2Int northCell = new Vector2Int(wallX, wallY - 1);
                if(doesCellExist(northCell)) neighboringCells.Add(northCell);

                Vector2Int southCell = new Vector2Int(wallX + 1, wallY);
                if(doesCellExist(southCell)) neighboringCells.Add(southCell);
                
                break;
            default:
                Debug.Log("Input Wall direction was (" + wallDir + "). Why TF is this happening");
                break;
        }

        return neighboringCells;
    }

    void teleportObject(GridObject objectToMove, int x, int y)
    {
        //update hash table
    }

    void moveObject(GridObject objectToMove, int direction, int distance)
    {
        int tilesMoved = 0;
        while (tilesMoved < distance)
        {
            //check if wall in front
                //if not, continue
                //if yes, canMove = false
        }
    }

    Vector3 cellCoordsToGlobalCoords(int x, int y, float xMod, float yMod)
    {
        return new Vector3((-x * cellWidth) + xMod, 0, (y * cellWidth) + yMod);
    }

    public bool getWallFromCoords(int x, int y, int side)
    {
        return walls[x, y, side];
    }

    public Vector2Int getNewThroughWallCoords(int x, int y, int side)
    {
        switch (side)
        {
            case 0:
                return new Vector2Int(x, y - 1);
            case 1:
                return new Vector2Int(x + 1, y);
            case 2:
                return new Vector2Int(x, y + 1);
            case 3:
                return new Vector2Int(x - 1, y);
            default:
                return new Vector2Int(x, y);
        }
    }

    void setWallFromDirection(int x, int y, int side, bool newValue)
    {
        switch (side)
        {
            case 0:
                if (y > 0)
                {
                    walls[x, y, side] = newValue;
                }
                break;
            case 1:
                if (x < mazeWidth - 1)
                {
                    walls[x, y, side] = newValue;
                }
                break;
            case 2:
                if (y < mazeHeight  -1)
                {
                    walls[x, y + 1, 0] = newValue;
                }
                break;
            case 3:
                if (x > 0)
                {
                    walls[x - 1, y, 1] = newValue;
                }
                break;
        }
    }

    /*public bool getWallFromDirection(int x, int y, int side)
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
                    return walls[x, y, side];
                }
            case 1:
                if (x >= mazeWidth)
                {
                    return true;
                }
                else
                {
                    return walls[x, y, side];
                }
            case 2:
                if (y >= mazeHeight)
                {
                    return true;
                }
                else
                {
                    return walls[x, y + 1, 0];
                }
            case 3:
                if (x <= 0)
                {
                    return true;
                }
                else
                {
                    return walls[x - 1, y, 1];
                }
            default:
                return true;
        }
    } */

    bool doesCellExist(Vector2Int cell)
    {
        return (cell.x <= 0) || (cell.x > mazeWidth) || (cell.y <= 0) || (cell.y > mazeHeight);
    }

    bool doesWallExist(Vector3Int wall)
    {
        if (!doesCellExist(new Vector2Int(wall.x, wall.y))) return false;
        if (wall.x == 0 && wall.y == 0) return false;

        if (wall.y == 0 && (1 <= wall.x && wall.x <= mazeWidth))
        {
            switch (wall.z)
            {
                case 0:
                    return false;

                case 1:
                    return true;

                default:
                    Debug.Log("wall direction index isn't 0 or 1");
                    return false;
            }
        } else if (wall.x == 0 && (1 <= wall.y && wall.y <= mazeHeight))
        {
            switch (wall.z)
            {
                case 0:
                    return true;

                case 1:
                    return false;

                default:
                    Debug.Log("wall direction index isn't 0 or 1");
                    return false;
            }
        } else 
        {
            return true;
        }
    }
}
