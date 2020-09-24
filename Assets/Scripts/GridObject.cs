using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GridObject : MonoBehaviour
{
    private Vector3 gridCoords;

    private Maze maze;

    public GridObject(int x, int y, int facing)
    {
        //define maze here

        //clamp x & y here

        facing %= 4;

        gridCoords = new Vector3(x, y, facing);
    }

    void Start()
    {

    }

    void Update()
    {
        
    }

    void move()
    {
        
    }

    void rotate(string direction)
    {
        switch (direction)
        {
            case "left":
                gridCoords.z--;
                break;
            case "right":
                gridCoords.z++;
                break;
        }

        gridCoords.z %= 4;
    }

    void faceDirection(string direction)
    {
        switch (direction)
        {
            case "north":
                gridCoords.z = 0;
                break;
            case "east":
                gridCoords.z = 1;
                break;
            case "south":
                gridCoords.z = 2;
                break;
            case "west":
                gridCoords.z = 3;
                break;
        }
    }
}
