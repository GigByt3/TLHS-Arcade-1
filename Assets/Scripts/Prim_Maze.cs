﻿using System;
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

    void populateMazeArray()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                for (int k = 0; k < 2; k++)
                {
                    walls[i,j,k] = true;
                }
            }
        }

        List<Vector2Int> visitedCells = new List<Vector2Int>();
        List<Vector3Int> wallList = new List<Vector3Int>();

        Vector2Int initialCell = new Vector2Int((int) UnityEngine.Random.Range(1, width), UnityEngine.Random.Range(1, height));
        visitedCells.Add(initialCell);

        wallList.AddRange(getNeighboringWalls(initialCell));

        while(wallList.Count != 0)
        {
            Vector3Int activeWall = wallList[(int) UnityEngine.Random.Range(0, wallList.Count - 1)];
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
                Debug.Log("unvisitedCells was 1");
            }
            
            wallList.Remove(activeWall);
        }

        /*foreach (bool wall in walls)
        {
            Debug.Log("[" + wall + "] ");
        } */
        
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

    List<Vector3Int> getNeighboringWalls(Vector2Int cellCoords)
    {
        List<Vector3Int> neighboringWalls = new List<Vector3Int>();
        List<Vector3Int> wallsThatDontExist = new List<Vector3Int>();

        neighboringWalls.Add(new Vector3Int(cellCoords.x, cellCoords.y, 0));
        neighboringWalls.Add(new Vector3Int(cellCoords.x, cellCoords.y, 1));
        neighboringWalls.Add(new Vector3Int(cellCoords.x - 1, cellCoords.y, 0));
        neighboringWalls.Add(new Vector3Int(cellCoords.x, cellCoords.y - 1, 1));

        foreach(Vector3Int wall in neighboringWalls)
        {
            if (!doesWallExist(wall)) wallsThatDontExist.Add(wall);
        }

        foreach(Vector3Int wall in wallsThatDontExist)
        {
            neighboringWalls.Remove(wall);
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

                Vector2Int southCell = new Vector2Int(wallX, wallY);
                if(doesCellExist(southCell)) neighboringCells.Add(southCell);
                
                break;
            default:
                Debug.Log("Input Wall direction was (" + wallDir + "). Why TF is this happening");
                break;
        }

        return neighboringCells;
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

    bool doesCellExist(Vector2Int cell)
    {
        return !((cell.x < 0) || (cell.x >= width) || (cell.y < 0) || (cell.y >= height));
    }

    bool doesWallExist(Vector3Int wall)
    {
        if (!doesCellExist(new Vector2Int(wall.x, wall.y)) || wall.z > 1) return false;
        return walls[wall.x, wall.y, wall.z];
    }
}