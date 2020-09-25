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

    private Dictionary<Vector3, GridObject> gridObjectDict;

    public Maze(int mWidth, int mHeight)
    {
        width = mWidth;
        height = mHeight;
    }
    
    // Start is called before the first frame update
    void Awake()
    {
        //cellWidth = 1;

        walls = new bool[width + 1, height + 1, 2];

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
        Stack<Vector2> previousCellStack = new Stack<Vector2>();
        Stack<Vector2> visitedCellStack = new Stack<Vector2>();

        int currentCellX = UnityEngine.Random.Range(1, width - 2);
        int currentCellY = UnityEngine.Random.Range(1, height - 2);

        //previousCellStack.Push(new Vector2(currentCellX, currentCellY));
        //visitedCellStack.Push(new Vector2(currentCellX, currentCellY));

        Debug.Log("Starting at Cell " + currentCellX + ", " + currentCellY);
        Debug.Log("walls has " + walls.Length);


        while (visitedCellStack.Count < width * height)
        {
            int faceToBreak = chooseAdjacentWallToBreak(currentCellX, currentCellY, visitedCellStack);

            Debug.Log("Trying to break face " + faceToBreak + " from Cell " + currentCellX + ", " + currentCellY);

            if (faceToBreak == -1)
            {
                //go back
                Debug.Log("No face found to break from Cell " + currentCellX + ", " + currentCellY);

                //add cell to list of visited cells if it isnt already
                if (!visitedCellStack.Contains(new Vector2(currentCellX, currentCellY))) visitedCellStack.Push(new Vector2(currentCellX, currentCellY));

                currentCellX = (int)previousCellStack.Peek().x;
                currentCellY = (int)previousCellStack.Pop().y;

                Debug.Log("Going back to Cell " + currentCellX + ", " + currentCellY);
            }
            else
            {
                Debug.Log("Face " + faceToBreak + " broken successfully!");

                //break wall
                setWallFromDirection(currentCellX, currentCellY, faceToBreak, false);

                //add cell to list of previous cells
                previousCellStack.Push(new Vector2(currentCellX, currentCellY));

                //add cell to list of visited cells if it isnt already
                if (!visitedCellStack.Contains(new Vector2(currentCellX, currentCellY))) visitedCellStack.Push(new Vector2(currentCellX, currentCellY));

                //move through

                int oldCellX = currentCellX;
                int oldCellY = currentCellY;

                currentCellX = (int)getNewThroughWallCoords(oldCellX, oldCellY, faceToBreak).x;
                currentCellY = (int)getNewThroughWallCoords(oldCellX, oldCellY, faceToBreak).y;

                Debug.Log("Moved from currentCell into Cell " + currentCellX + ", " + currentCellY);
            }

            Debug.Log("previousCellStack has " + previousCellStack.Count);
            Debug.Log("visitedCellStack has " + visitedCellStack.Count);
        } 
    }

    int chooseAdjacentWallToBreak(int x, int y, Stack<Vector2> visitedCellStack)
    {
        ArrayList availableWalls = new ArrayList();

        if (y > 0 && getWallFromDirection(x, y, 0) && !visitedCellStack.Contains(getNewThroughWallCoords(x, y, 0))) availableWalls.Add("NORTH");
        if (x < width - 1 && getWallFromDirection(x, y, 1) && !visitedCellStack.Contains(getNewThroughWallCoords(x, y, 1))) availableWalls.Add("EAST");
        if (y < height - 1 && getWallFromDirection(x, y, 2) && !visitedCellStack.Contains(getNewThroughWallCoords(x, y, 2))) availableWalls.Add("SOUTH");
        if (x > 0 && getWallFromDirection(x, y, 3) && !visitedCellStack.Contains(getNewThroughWallCoords(x, y, 3))) availableWalls.Add("WEST");

        if (availableWalls.Count <= 0) return -1;

        String pickedWall = (String) availableWalls[UnityEngine.Random.Range(0, availableWalls.Count - 1)];

        Debug.Log("Selected wall " + pickedWall + " from " + availableWalls.Count + " choices...");

        switch (pickedWall)
        {
            case "NORTH":
                return 0;
            case "EAST":
                return 1;
            case "SOUTH":
                return 2;
            case "WEST":
                return 3;
            default:
                return -1;
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
        
        for (int i = 0; i < width; i++)
        {
            GameObject southWall = Instantiate(wallPrefab);
            southWall.name = i + " South Wall";
            southWall.transform.parent = southWalls.transform;
            southWall.transform.position = cellCoordsToGlobalCoords(i, height, 0, -0.5f);
            southWall.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }

        GameObject westWalls = new GameObject("West Walls");
        westWalls.transform.parent = this.transform;

        for (int j = 0; j < width; j++)
        {
            GameObject westWall = Instantiate(wallPrefab);
            westWall.name = j + " West Wall";
            westWall.transform.parent = westWalls.transform;
            westWall.transform.position = cellCoordsToGlobalCoords(0, j, 0.5f, 0);
            westWall.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
        }

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
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

    Vector3 cellCoordsToGlobalCoords(float x, float y, float xMod, float yMod)
    {
        return new Vector3((-x * cellWidth) + xMod, 0, (y * cellWidth) + yMod);
    }

    public bool getWallFromCoords(int x, int y, int side)
    {
        return walls[x, y, side];
    }

    public Vector2 getNewThroughWallCoords(int x, int y, int side)
    {
        switch (side)
        {
            case 0:
                return new Vector2(x, y - 1.0f);
            case 1:
                return new Vector2(x + 1.0f, y);
            case 2:
                return new Vector2(x, y + 1.0f);
            case 3:
                return new Vector2(x - 1.0f, y);
            default:
                return new Vector2(x, y);
        }
    }

    public void setWallFromDirection(int x, int y, int side, bool newValue)
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
                if (x < width - 1)
                {
                    walls[x, y, side] = newValue;
                }
                break;
            case 2:
                if (y < height  -1)
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
                    return walls[x, y, side];
                }
            case 1:
                if (x >= width)
                {
                    return true;
                }
                else
                {
                    return walls[x, y, side];
                }
            case 2:
                if (y >= height)
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
    }
}
