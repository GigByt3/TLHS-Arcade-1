using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class Maze : MonoBehaviour
{
    public int width, height;

    public GameObject wallPrefab;

    public float cellWidth;

    private bool[,,] walls;

    public Dictionary<Vector3Int, GridObject> gridObjectDict;
    
    public void Ready()
    {
        walls = new bool[width + 1, height + 1, 2];

        Debug.Log("Made new maze of size " + width + ", " + height);

        populateMazeArray();

        generateMazeMesh();

        populateGridObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void populateMazeArray()
    {
        //set all walls true
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                walls[i, j, 0] = true;
                walls[i, j, 1] = true;
            }
        }

        //define stack of previous cells
        Stack<Vector2Int> previousCellStack = new Stack<Vector2Int>();
        HashSet<Vector2Int> visitedCellStack = new HashSet<Vector2Int>();

        int currentCellX = UnityEngine.Random.Range(1, width - 2);
        int currentCellY = UnityEngine.Random.Range(1, height - 2);

        Debug.Log("Starting at Cell " + currentCellX + ", " + currentCellY);

        while (visitedCellStack.Count < width * height)
        {
            int faceToBreak = chooseAdjacentWallToBreak(currentCellX, currentCellY, previousCellStack, visitedCellStack);

            if (faceToBreak == -1)
            {
                //add cell to list of visited cells if it isnt already
                if (!visitedCellStack.Contains(new Vector2Int(currentCellX, currentCellY))) visitedCellStack.Add(new Vector2Int(currentCellX, currentCellY));

                currentCellX = previousCellStack.Peek().x;
                currentCellY = previousCellStack.Pop().y;
            }
            else
            {
                //break wall
                setWallFromDirection(currentCellX, currentCellY, faceToBreak, false);

                //add cell to list of previous cells
                previousCellStack.Push(new Vector2Int(currentCellX, currentCellY));

                //add cell to list of visited cells if it isnt already
                if (!visitedCellStack.Contains(new Vector2Int(currentCellX, currentCellY))) visitedCellStack.Add(new Vector2Int(currentCellX, currentCellY));

                //move through

                int oldCellX = currentCellX;
                int oldCellY = currentCellY;

                currentCellX = getNewThroughWallCoords(oldCellX, oldCellY, faceToBreak).x;
                currentCellY = getNewThroughWallCoords(oldCellX, oldCellY, faceToBreak).y;
            }
        }
        Debug.Log("Maze generation finished!");
    }

    int chooseAdjacentWallToBreak(int x, int y, Stack<Vector2Int> previousCellStack, HashSet<Vector2Int> visitedCellStack)
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
        if (x < width - 1 && getWallFromDirection(x, y, 1) && !visitedCellStack.Contains(getNewThroughWallCoords(x, y, 1))) availableWalls.Add(1);
        if (y < height - 1 && getWallFromDirection(x, y, 2) && !visitedCellStack.Contains(getNewThroughWallCoords(x, y, 2))) availableWalls.Add(2);
        if (x > 0 && getWallFromDirection(x, y, 3) && !visitedCellStack.Contains(getNewThroughWallCoords(x, y, 3))) availableWalls.Add(3);

        bool lastFaceRemoved = false;
        if (availableWalls.Contains(lastFaceBroken) && UnityEngine.Random.Range(0, 1) <= 0.1)
        {
            availableWalls.Remove(lastFaceBroken);
            lastFaceRemoved = true;
        }

        if (availableWalls.Count <= 0 && lastFaceRemoved)
        {
            availableWalls.Add(lastFaceBroken);
        }
        else if (availableWalls.Count <= 0)
        {
            return -1;
        }

        int pickedWall = (int) availableWalls[UnityEngine.Random.Range(0, availableWalls.Count - 1)];

        return pickedWall;
    }

    void populateGridObjects()
    {

    }

    void generateMazeMesh()
    {
        //ALL CODE IN HERE WILL BE REWRITTEN ENTIRELY
        //JUST TEMPORARY FOR MAKING MAZE GEN ALGORITHM
        
        GameObject northWalls = new GameObject("North Walls");
        northWalls.transform.parent = this.transform;
        
        for (int i = 0; i < width; i++)
        {
            GameObject northWall = Instantiate(wallPrefab);
            northWall.name = i + " North Wall";
            northWall.transform.parent = northWalls.transform;
            northWall.transform.position = cellCoordsToGlobalCoords(i, 0, 0, -0.5f * cellWidth);
            northWall.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }

        GameObject westWalls = new GameObject("West Walls");
        westWalls.transform.parent = this.transform;

        for (int j = 0; j < width; j++)
        {
            GameObject westWall = Instantiate(wallPrefab);
            westWall.name = j + " West Wall";
            westWall.transform.parent = westWalls.transform;
            westWall.transform.position = cellCoordsToGlobalCoords(0, j, 0.5f * cellWidth, 0);
            westWall.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
        }

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GameObject cell = new GameObject("Cell " + i + ", " + j);
                cell.transform.position = cellCoordsToGlobalCoords(i, j, 0, 0);
                cell.transform.parent = transform;

                if (walls[i, j, 0])
                {
                    GameObject eastWall = Instantiate(wallPrefab);
                    eastWall.name = i + ", " + j + " East Wall";
                    eastWall.transform.parent = cell.transform;
                    eastWall.transform.position = cellCoordsToGlobalCoords(i, j, -0.5f * cellWidth, 0);
                    eastWall.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
                }

                if (walls[i, j, 1])
                {
                    GameObject southWall = Instantiate(wallPrefab);
                    southWall.name = i + ", " + j + " South Wall";
                    southWall.transform.parent = cell.transform;
                    southWall.transform.position = cellCoordsToGlobalCoords(i, j, 0, 0.5f * cellWidth);
                    southWall.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                }
            }
        }
    }

    public void teleportObject(GridObject objectToMove, int x, int y)
    {
        if (gridObjectDict.ContainsKey(new Vector3Int(objectToMove.gridCoords.x, objectToMove.gridCoords.y, objectToMove.gridCoords.z)))
            gridObjectDict.Remove(new Vector3Int(objectToMove.gridCoords.x, objectToMove.gridCoords.y, objectToMove.gridCoords.z));
        gridObjectDict.Add(new Vector3Int(x, y, objectToMove.gridCoords.z), objectToMove);
    }

    public void moveObject(GridObject objectToMove, int distance)
    {
        int tilesMoved = 0;
        int moveMod = 1;
        if (distance < 0)
        {
            moveMod = -1;
            distance = Math.Abs(distance);
        }
        while (tilesMoved < distance)
        {
            int faceToCheck = 0;
            if (moveMod == -1)
            {
                faceToCheck += 2;
                faceToCheck %= 4;
            }
            else
            {
                faceToCheck = objectToMove.gridCoords.z;
            }
            
            if (!getWallFromDirection(objectToMove.gridCoords.x, objectToMove.gridCoords.y, faceToCheck))
            {
                switch (faceToCheck)
                {
                    case 0:
                        if (objectToMove.gridCoords.y > 0)
                        {
                            if (gridObjectDict.ContainsKey(new Vector3Int(objectToMove.gridCoords.x, objectToMove.gridCoords.y, objectToMove.gridCoords.z)))
                                gridObjectDict.Remove(new Vector3Int(objectToMove.gridCoords.x, objectToMove.gridCoords.y, objectToMove.gridCoords.z));
                            gridObjectDict.Add(new Vector3Int(objectToMove.gridCoords.x, objectToMove.gridCoords.y - moveMod, objectToMove.gridCoords.z), objectToMove);
                            objectToMove.gridCoords.y = objectToMove.gridCoords.y - (moveMod);
                        }
                        break;
                    case 1:
                        if (objectToMove.gridCoords.x + 1 < width)
                        {
                            if (gridObjectDict.ContainsKey(new Vector3Int(objectToMove.gridCoords.x, objectToMove.gridCoords.y, objectToMove.gridCoords.z)))
                                gridObjectDict.Remove(new Vector3Int(objectToMove.gridCoords.x, objectToMove.gridCoords.y, objectToMove.gridCoords.z));
                            gridObjectDict.Add(new Vector3Int(objectToMove.gridCoords.x + moveMod, objectToMove.gridCoords.y, objectToMove.gridCoords.z), objectToMove);
                            objectToMove.gridCoords.x = objectToMove.gridCoords.x + (moveMod);
                        }
                        break;
                    case 2:
                        if (objectToMove.gridCoords.y + 1 < height)
                        {
                            if (gridObjectDict.ContainsKey(new Vector3Int(objectToMove.gridCoords.x, objectToMove.gridCoords.y, objectToMove.gridCoords.z)))
                                gridObjectDict.Remove(new Vector3Int(objectToMove.gridCoords.x, objectToMove.gridCoords.y, objectToMove.gridCoords.z));
                            gridObjectDict.Add(new Vector3Int(objectToMove.gridCoords.x, objectToMove.gridCoords.y + moveMod, objectToMove.gridCoords.z), objectToMove);
                            objectToMove.gridCoords.y = objectToMove.gridCoords.y + (moveMod);
                        }
                        break;
                    case 3:
                        if (objectToMove.gridCoords.x > 0)
                        {
                            if (gridObjectDict.ContainsKey(new Vector3Int(objectToMove.gridCoords.x, objectToMove.gridCoords.y, objectToMove.gridCoords.z)))
                                gridObjectDict.Remove(new Vector3Int(objectToMove.gridCoords.x, objectToMove.gridCoords.y, objectToMove.gridCoords.z));
                            gridObjectDict.Add(new Vector3Int(objectToMove.gridCoords.x - moveMod, objectToMove.gridCoords.y, objectToMove.gridCoords.z), objectToMove);
                            objectToMove.gridCoords.x = objectToMove.gridCoords.x - (moveMod);
                        }
                        break;
                    default:
                        return;
                }
                tilesMoved++;
            }
            else
            {
                return;
            }
        }
    }

    public void setObjectRotation(GridObject objectToRotate, int direction)
    {
        if (gridObjectDict.ContainsKey(new Vector3Int(objectToRotate.gridCoords.x, objectToRotate.gridCoords.y, objectToRotate.gridCoords.z)))
            gridObjectDict.Remove(new Vector3Int(objectToRotate.gridCoords.x, objectToRotate.gridCoords.y, objectToRotate.gridCoords.z));
        gridObjectDict.Add(new Vector3Int(objectToRotate.gridCoords.x, objectToRotate.gridCoords.y, direction), objectToRotate);
    }

    public Vector3 cellCoordsToGlobalCoords(int x, int y, float xMod, float yMod)
    {
        return new Vector3((-x * cellWidth) + xMod, cellWidth / 2.0f, (y * cellWidth) + yMod);
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
}
