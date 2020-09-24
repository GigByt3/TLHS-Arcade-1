using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    private int width, height;

    private bool[,,] walls;

    private Dictionary<Vector3, GridObject> gridObjectDict;

    public Maze(int mWidth, int mHeight)
    {
        width = mWidth;
        height = mHeight;

        walls = new bool[width + 1, height + 1, 2];
    }
    
    // Start is called before the first frame update
    void Start()
    {
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

    }

    void populateGridObjects()
    {

    }

    void generateMazeMesh()
    {

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
