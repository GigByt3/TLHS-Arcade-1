using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GridObject : MonoBehaviour
{
    public Vector3Int gridCoords;

    public Maze maze;

    public void Ready()
    {
        maze = GameObject.Find("Game Manager").GetComponent<GameSceneManager>().maze;

        if (gridCoords.x < 0) gridCoords.x = 0;
        if (gridCoords.x >= maze.width) gridCoords.x = maze.width - 1;
        if (gridCoords.y < 0) gridCoords.y = 0;
        if (gridCoords.y >= maze.height) gridCoords.y = maze.height - 1;

        gridCoords.z %= 4;
    }

    void Update()
    {
        
    }

    void updatePosition()
    {

    }

    public void move(int distance)
    {
        maze.moveObject(this, distance);
    }

    public void rotate(string direction)
    {
        int newdir = gridCoords.z;
        switch (direction)
        {
            case "left":
                newdir--;
                break;
            case "right":
                newdir++;
                break;
        }

        if (newdir < 0) newdir = 3;
        newdir %= 4;

        maze.setObjectRotation(this, newdir);

        gridCoords.z = newdir;
    }

    public void faceDirection(string direction)
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
