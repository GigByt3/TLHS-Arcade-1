using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class Maze : MonoBehaviour
{
    public int width, height;

    public GameObject wallPrefab;

    private float cellWidth;

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
        cellWidth = 1;

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
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                walls[i, j, 0] = true;
                walls[i, j, 1] = true;
            }
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

    public bool getWallFromDirection(int x, int y, int side)
    {
        if (side == 2)
        {
            return walls[x, y + 1, 0];
        } else if (side == 3)
        {
            return walls[x - 1, y, 1];
        } else
        {
            return walls[x, y, side];
        }
    }
}
