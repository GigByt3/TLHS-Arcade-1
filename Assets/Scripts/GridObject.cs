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

    public List<Vector2Int> visibleNorthCells()
    {
        List<Vector2Int> visibleCells = new List<Vector2Int>();

        Vector2Int currentCell = new Vector2Int(gridCoords.x, gridCoords.y);

        while (!maze.getWallFromDirection(currentCell.x, currentCell.y, 0))
        {
            currentCell.y--;
            visibleCells.Add(currentCell);
        }

        return visibleCells;
    }
    public List<Vector2Int> visibleEastCells()
    {
        List<Vector2Int> visibleCells = new List<Vector2Int>();

        Vector2Int currentCell = new Vector2Int(gridCoords.x, gridCoords.y);

        while (!maze.getWallFromDirection(currentCell.x, currentCell.y, 1))
        {
            currentCell.x++;
            visibleCells.Add(currentCell);
        }

        return visibleCells;
    }
    public List<Vector2Int> visibleSouthCells()
    {
        List<Vector2Int> visibleCells = new List<Vector2Int>();

        Vector2Int currentCell = new Vector2Int(gridCoords.x, gridCoords.y);

        while (!maze.getWallFromDirection(currentCell.x, currentCell.y, 2))
        {
            currentCell.y++;
            visibleCells.Add(currentCell);
        }

        return visibleCells;
    }
    public List<Vector2Int> visibleWestCells()
    {
        List<Vector2Int> visibleCells = new List<Vector2Int>();

        Vector2Int currentCell = new Vector2Int(gridCoords.x, gridCoords.y);

        while (!maze.getWallFromDirection(currentCell.x, currentCell.y, 3))
        {
            currentCell.x--;
            visibleCells.Add(currentCell);
        }

        return visibleCells;
    }

    public void move(int distance)
    {
        maze.moveObject(this, distance);
    }

    public bool canMoveForwards()
    {
        return !maze.getWallFromDirection(gridCoords.x, gridCoords.y, gridCoords.z);
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
