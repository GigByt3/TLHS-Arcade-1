using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GridObject : MonoBehaviour
{
    public Vector3Int gridCoords;

    public Maze maze;

    //Method to be run when a GridObject is created
    public void Ready()
    {
        maze = GameObject.Find("Game Manager").GetComponent<GameSceneManager>().maze;

        if (gridCoords.x < 0) gridCoords.x = 0;
        if (gridCoords.x >= maze.width) gridCoords.x = maze.width - 1;
        if (gridCoords.y < 0) gridCoords.y = 0;
        if (gridCoords.y >= maze.height) gridCoords.y = maze.height - 1;

        gridCoords.z %= 4;

        forcePosition();
    }

    //Returns all cells this GridObject can see to the north, obstructed by walls
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

    //Returns all cells this GridObject can see to the east, obstructed by walls
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

    //Returns all cells this GridObject can see to the south, obstructed by walls
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

    //Returns all cells this GridObject can see to the west, obstructed by walls
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

    //Returns whether or not the object is in the given cell list
    public bool isInCell(List<Vector2Int> cells)
    {
        return cells.Contains(new Vector2Int(gridCoords.x, gridCoords.y));
    }

    //Attempts to move the GridObject forward by the given distance, obstructed by walls
    public void move(int distance)
    {
        maze.moveObject(this, distance);
    }

    //Returns whether or not the GridObject can move forward by one space
    public bool canMoveForwards()
    {
        return !maze.getWallFromDirection(gridCoords.x, gridCoords.y, gridCoords.z);
    }

    //Returns whether or not the exit is in front of this GridObject
    public void interactInFront()
    {
        if (!maze.getWallFromDirection(gridCoords.x, gridCoords.y, gridCoords.z))
        {
            GridObject objectInFront = new DummyObject();
            switch (gridCoords.z)
            {
                case 0:
                    for (int i = 0; i < 4; i++)
                    {
                        Vector3Int keyToCheck = new Vector3Int(gridCoords.x, gridCoords.y - 1, i);
                        if (maze.gridObjectDict.ContainsKey(keyToCheck)) objectInFront = maze.gridObjectDict[keyToCheck];
                    }
                    break;
                case 1:
                    for (int i = 0; i < 4; i++)
                    {
                        Vector3Int keyToCheck = new Vector3Int(gridCoords.x + 1, gridCoords.y, i);
                        if (maze.gridObjectDict.ContainsKey(keyToCheck)) objectInFront = maze.gridObjectDict[keyToCheck];
                    }
                    break;
                case 2:
                    for (int i = 0; i < 4; i++)
                    {
                        Vector3Int keyToCheck = new Vector3Int(gridCoords.x, gridCoords.y - 1, i);
                        if (maze.gridObjectDict.ContainsKey(keyToCheck)) objectInFront = maze.gridObjectDict[keyToCheck];
                    }
                    break;
                case 3:
                    for (int i = 0; i < 4; i++)
                    {
                        Vector3Int keyToCheck = new Vector3Int(gridCoords.x - 1, gridCoords.y, i);
                        if (maze.gridObjectDict.ContainsKey(keyToCheck)) objectInFront = maze.gridObjectDict[keyToCheck];
                    }
                    break;
            }
            if (objectInFront is DummyObject) return;
            if (objectInFront is Interactible interactibleInFront) interactibleInFront.onInteract();
        }
    }

    //Returns whether or not the player is adjacent to this GridObject
    public bool isPlayerAdjacent()
    {
        if (maze.isPlayerAtCoords(gridCoords.x, gridCoords.y - 1) && !maze.getWallFromDirection(gridCoords.x, gridCoords.y, 0)) return true;
        if (maze.isPlayerAtCoords(gridCoords.x + 1, gridCoords.y) && !maze.getWallFromDirection(gridCoords.x, gridCoords.y, 1)) return true;
        if (maze.isPlayerAtCoords(gridCoords.x, gridCoords.y + 1) && !maze.getWallFromDirection(gridCoords.x, gridCoords.y, 2)) return true;
        if (maze.isPlayerAtCoords(gridCoords.x - 1, gridCoords.y) && !maze.getWallFromDirection(gridCoords.x, gridCoords.y, 3)) return true;
        return false;
    }

    //Rotates the GridObject 'left' or 'right' by 90 degrees
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

    //Sets the GridObject to face 'north', 'east', 'south', or 'west'
    public void faceDirection(string direction)
    {
        switch (direction)
        {
            case "north":
                maze.setObjectRotation(this, 0);
                gridCoords.z = 0;
                break;
            case "east":
                maze.setObjectRotation(this, 1);
                gridCoords.z = 1;
                break;
            case "south":
                maze.setObjectRotation(this, 2);
                gridCoords.z = 2;
                break;
            case "west":
                maze.setObjectRotation(this, 3);
                gridCoords.z = 3;
                break;
        }
    }

    //Forces the object's position to it's correct position in the scene based on gridCoords
    void forcePosition()
    {
        transform.position = maze.cellCoordsToGlobalCoords(gridCoords.x, gridCoords.y);
        transform.rotation = maze.cellDirectionToGlobalRotation(gridCoords.z);
    }

    public virtual void handleMove(Vector2Int destination)
    {
        gameObject.transform.position = maze.cellCoordsToGlobalCoords(destination.x, destination.y);
    }

    public virtual void handleRotation(int destination)
    {
        gameObject.transform.rotation = maze.cellDirectionToGlobalRotation(destination);
    }
}
