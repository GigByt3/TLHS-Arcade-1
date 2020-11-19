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

    /*
    public List<Vector2Int> visibleCells(Vector2Int cell)
    {
        List<Vector2Int> visibleCells = new List<Vector2Int>();

        Vector2Int currentCell = new Vector2Int(gridCoords.x, gridCoords.y);
        //NORTH
        List<Vector2Int> visibleNorthCells = new List<Vector2Int>();
        List<Vector2Int> visibleNorthEastCells = new List<Vector2Int>();
        List<Vector2Int> visibleNorthWestCells = new List<Vector2Int>();
        while (!maze.getWallFromDirection(currentCell.x, currentCell.y, 0))
        {
            currentCell.y--;
            visibleNorthCells.Add(currentCell);
        }
        foreach (Vector2Int cellToCheck in visibleNorthCells)
        {
            //East Corridor 1
            if (!maze.getWallFromDirection(cellToCheck.x, cellToCheck.y, 1))
            {
                int distTravelled = gridCoords.y - cellToCheck.y;
                currentCell.x = cellToCheck.x + 1;
                currentCell.y = cellToCheck.y;
                visibleNorthEastCells.Add(currentCell);
                while (!maze.getWallFromDirection(currentCell.x, currentCell.y, 0) && cellToCheck.y - currentCell.y < (distTravelled * 2 + 1))
                {
                    currentCell.y--;
                    distTravelled++;
                    visibleNorthEastCells.Add(currentCell);
                }
            }

            //West Corridor 1
            if (!maze.getWallFromDirection(cellToCheck.x, cellToCheck.y, 3))
            {
                int distTravelled = gridCoords.y - cellToCheck.y;
                currentCell.x = cellToCheck.x - 1;
                currentCell.y = cellToCheck.y;
                visibleNorthWestCells.Add(currentCell);
                while (!maze.getWallFromDirection(currentCell.x, currentCell.y, 0) && cellToCheck.y - currentCell.y < (distTravelled * 2 + 1))
                {
                    currentCell.y--;
                    distTravelled++;
                    visibleNorthWestCells.Add(currentCell);
                }
            }
        }
        foreach (Vector2Int cellToAdd in visibleNorthEastCells) visibleNorthCells.Add(cellToAdd);
        foreach (Vector2Int cellToAdd in visibleNorthWestCells) visibleNorthCells.Add(cellToAdd);

        currentCell = new Vector2Int(gridCoords.x, gridCoords.y);

        //EAST
        List<Vector2Int> visibleEastCells = new List<Vector2Int>();
        List<Vector2Int> visibleEastSouthCells = new List<Vector2Int>();
        List<Vector2Int> visibleEastNorthCells = new List<Vector2Int>();
        while (!maze.getWallFromDirection(currentCell.x, currentCell.y, 1))
        {
            currentCell.x++;
            visibleEastCells.Add(currentCell);
        }
        foreach (Vector2Int cellToCheck in visibleEastCells)
        {
            //South Corridor 1
            if (!maze.getWallFromDirection(cellToCheck.x, cellToCheck.y, 2))
            {
                int distTravelled = cellToCheck.x - gridCoords.x;
                currentCell.x = cellToCheck.x;
                currentCell.y = cellToCheck.y + 1;
                visibleEastSouthCells.Add(currentCell);
                while (!maze.getWallFromDirection(currentCell.x, currentCell.y, 1) && currentCell.x - cellToCheck.x < (distTravelled * 2 + 1))
                {
                    currentCell.x++;
                    distTravelled++;
                    visibleEastSouthCells.Add(currentCell);
                }
            }

            //North Corridor 1
            if (!maze.getWallFromDirection(cellToCheck.x, cellToCheck.y, 0))
            {
                int distTravelled = cellToCheck.x - gridCoords.x;
                currentCell.x = cellToCheck.x;
                currentCell.y = cellToCheck.y - 1;
                visibleEastNorthCells.Add(currentCell);
                while (!maze.getWallFromDirection(currentCell.x, currentCell.y, 1) && currentCell.x - cellToCheck.x < (distTravelled * 2 + 1))
                {
                    currentCell.x++;
                    distTravelled++;
                    visibleEastSouthCells.Add(currentCell);
                }
            }
        }
        foreach (Vector2Int cellToAdd in visibleEastSouthCells) visibleEastCells.Add(cellToAdd);
        foreach (Vector2Int cellToAdd in visibleEastNorthCells) visibleEastCells.Add(cellToAdd);

        currentCell = new Vector2Int(gridCoords.x, gridCoords.y);

        //SOUTH
        List<Vector2Int> visibleSouthCells = new List<Vector2Int>();
        List<Vector2Int> visibleSouthWestCells = new List<Vector2Int>();
        List<Vector2Int> visibleSouthEastCells = new List<Vector2Int>();
        while (!maze.getWallFromDirection(currentCell.x, currentCell.y, 2))
        {
            currentCell.y++;
            visibleSouthCells.Add(currentCell);
        }
        foreach (Vector2Int cellToCheck in visibleSouthCells)
        {
            //West Corridor 1
            if (!maze.getWallFromDirection(cellToCheck.x, cellToCheck.y, 3))
            {
                int distTravelled = cellToCheck.y - gridCoords.y;
                currentCell.x = cellToCheck.x - 1;
                currentCell.y = cellToCheck.y;
                visibleSouthWestCells.Add(currentCell);
                while (!maze.getWallFromDirection(currentCell.x, currentCell.y, 2) && currentCell.y - cellToCheck.y < (distTravelled * 2 + 1))
                {
                    currentCell.y++;
                    distTravelled++;
                    visibleSouthWestCells.Add(currentCell);
                }
            }

            //East Corridor 1
            if (!maze.getWallFromDirection(cellToCheck.x, cellToCheck.y, 1))
            {
                int distTravelled = cellToCheck.y - gridCoords.y;
                currentCell.x = cellToCheck.x + 1;
                currentCell.y = cellToCheck.y;
                visibleSouthEastCells.Add(currentCell);
                while (!maze.getWallFromDirection(currentCell.x, currentCell.y, 2) && currentCell.y - cellToCheck.y < (distTravelled * 2 + 1))
                {
                    currentCell.y++;
                    distTravelled++;
                    visibleSouthEastCells.Add(currentCell);
                }
            }
        }
        foreach (Vector2Int cellToAdd in visibleSouthWestCells) visibleSouthCells.Add(cellToAdd);
        foreach (Vector2Int cellToAdd in visibleSouthEastCells) visibleSouthCells.Add(cellToAdd);

        currentCell = new Vector2Int(gridCoords.x, gridCoords.y);
        //WEST
        List<Vector2Int> visibleWestCells = new List<Vector2Int>();
        List<Vector2Int> visibleWestNorthCells = new List<Vector2Int>();
        List<Vector2Int> visibleWestSouthCells = new List<Vector2Int>();
        while (!maze.getWallFromDirection(currentCell.x, currentCell.y, 3))
        {
            currentCell.x--;
            visibleWestCells.Add(currentCell);
        }
        foreach (Vector2Int cellToCheck in visibleWestCells)
        {
            //North Corridor 1
            if (!maze.getWallFromDirection(cellToCheck.x, cellToCheck.y, 0))
            {
                int distTravelled = gridCoords.x - cellToCheck.x;
                currentCell.x = cellToCheck.x;
                currentCell.y = cellToCheck.y - 1;
                visibleWestNorthCells.Add(currentCell);
                while (!maze.getWallFromDirection(currentCell.x, currentCell.y, 3) && cellToCheck.x - currentCell.x < (distTravelled * 2 + 1))
                {
                    currentCell.x--;
                    distTravelled++;
                    visibleWestNorthCells.Add(currentCell);
                }
            }

            //South Corridor 1
            if (!maze.getWallFromDirection(cellToCheck.x, cellToCheck.y, 2))
            {
                int distTravelled = gridCoords.x - cellToCheck.x;
                currentCell.x = cellToCheck.x;
                currentCell.y = cellToCheck.y + 1;
                visibleWestSouthCells.Add(currentCell);
                while (!maze.getWallFromDirection(currentCell.x, currentCell.y, 3) && cellToCheck.x - currentCell.x < (distTravelled * 2 + 1))
                {
                    currentCell.x--;
                    distTravelled++;
                    visibleWestSouthCells.Add(currentCell);
                }
            }
        }
        foreach (Vector2Int cellToAdd in visibleWestNorthCells) visibleWestCells.Add(cellToAdd);
        foreach (Vector2Int cellToAdd in visibleWestSouthCells) visibleWestCells.Add(cellToAdd);

        foreach (Vector2Int cellToAdd in visibleNorthCells) visibleCells.Add(cellToAdd);
        foreach (Vector2Int cellToAdd in visibleEastCells) visibleCells.Add(cellToAdd);
        foreach (Vector2Int cellToAdd in visibleSouthCells) visibleCells.Add(cellToAdd);
        foreach (Vector2Int cellToAdd in visibleWestCells) visibleCells.Add(cellToAdd);

        return visibleCells;
    }
    */
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
