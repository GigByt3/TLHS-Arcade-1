using System.Collections.Generic;
using UnityEngine;

public class Maze_Generator : MonoBehaviour
{
    [System.Serializable]
    public class Cell
    {
        public GameObject north, east, south, west;

        public bool visited;
    }

    public GameObject wall, wallContainer;
    public int xSize, ySize;
    public float wallLength;

    private Vector3 initialPos;
    public Cell[] cells;
    private int currentCell;

    private int visitedCells, backingUp;
    private List<int> lastCells;

    private bool startedBuilding;

    private int wallToBreak;

    public void Start()
    {
        CreateWalls();
        CreateCells();
        CreateMaze();
    }

    public void CreateWalls()
    {
        startedBuilding = false;
        
        //Define temporary wall to be instantiated later
        GameObject tempWall;

        //Define inital position to be map center
        initialPos = new Vector3((-xSize / 2) + (wallLength / 2), 0.0f, (-ySize / 2) + (wallLength / 2));

        //Define current generator position, to iterate through map
        Vector3 currentGenPos = initialPos;

        //X axis walls for loop
        for (int i = 0; i < ySize; i++)
        {
            for (int j = 0; j <= xSize; j++)
            {
                //Define currentGenPos to be at the current position in the maze, based on the for loops
                currentGenPos = new Vector3(initialPos.x + (j * wallLength) - wallLength / 2, 0.0f, initialPos.z + (i * wallLength) - wallLength / 2);

                //Instantiate new wall
                tempWall = Instantiate(wall, currentGenPos, Quaternion.identity) as GameObject;
                tempWall.transform.parent = wallContainer.transform;
            }
        }

        //Y axis walls for loop
        for (int i = 0; i <= ySize; i++)
        {
            for (int j = 0; j < xSize; j++)
            {
                //Define currentGenPos to be at the current position in the maze, based on the for loops
                currentGenPos = new Vector3(initialPos.x + (j * wallLength), 0.0f, initialPos.z + (i * wallLength) - wallLength);

                //Instantiate new wall
                tempWall = Instantiate(wall, currentGenPos, Quaternion.Euler(0.0f, 90.0f, 0.0f)) as GameObject;
                tempWall.transform.parent = wallContainer.transform;
            }
        }
    }

    public void CreateCells()
    {
        lastCells = new List<int>();
        lastCells.Clear();
        GameObject[] walls;
        int numOfWalls = wallContainer.transform.childCount;
        walls = new GameObject[numOfWalls];
        cells = new Cell[xSize * ySize];

        int eastWestProcess = 0, termCount = 0;

        //Gets all walls
        for (int i = 0; i < numOfWalls; i++)
        {
            walls[i] = wallContainer.transform.GetChild(i).gameObject;
        }

        //Assigns walls to cells
        for (int cellProcess = 0; cellProcess < cells.Length; cellProcess++)
        {
            cells[cellProcess] = new Cell();

            cells[cellProcess].west = walls[eastWestProcess];
            cells[cellProcess].south = walls[cellProcess + (xSize + 1) * ySize];

            if (termCount == xSize)
            {
                eastWestProcess += 2;
                termCount = 0;
            }
            else
            {
                eastWestProcess++;
            }

            termCount++;

            cells[cellProcess].east = walls[eastWestProcess];
            cells[cellProcess].north = walls[(cellProcess + (xSize + 1) * ySize) + xSize];
        }
    }

    public void CreateMaze()
    {      
        for (int i = 0; i < 20; i++)
        {
            Debug.Log("startedBuilding" + startedBuilding);
            if (startedBuilding)
            {
                Debug.Log("currentcell" + currentCell);
                int currentNeighbour = GetNeighbour();
                if (!cells[currentNeighbour].visited && cells[currentCell].visited)
                {
                    BreakWall();
                    cells[currentNeighbour].visited = true;
                    visitedCells++;
                    lastCells.Add(currentCell);
                    currentCell = currentNeighbour;
                    if (lastCells.Count > 0)
                    {
                        backingUp = lastCells.Count - 1;
                    }
                }
            }
            else
            {
                //currentCell = Random.Range(0, cells.Length);
                currentCell = 12;
                cells[currentCell].visited = true;
                visitedCells++;
                startedBuilding = true;
            }
        }
    }

    public void BreakWall()
    {
        Debug.Log("break" + wallToBreak);
        switch (wallToBreak)
        {
            case 1:
                Destroy(cells[currentCell].north);
                Debug.Log("break north");
                break;
            case 2:
                Destroy(cells[currentCell].east);
                Debug.Log("break east");
                break;
            case 3:
                Destroy(cells[currentCell].south);
                Debug.Log("break south");
                break;
            case 4:
                Destroy(cells[currentCell].west);
                Debug.Log("break west");
                break;
            case 0:
                Debug.Log("not work");
                break;
            default:
                Debug.Log("i hate you");
                break;
        }
    }

    public int GetNeighbour()
    {
        int length = 0;
        int[] neighbours = new int[4];
        int[] connectingWall = new int[4];

        //East
        if ((currentCell + 1 < cells.Length && (currentCell + 1) % xSize != 0) || currentCell == 0)
        {
            if (!cells[currentCell + 1].visited)
            {
                neighbours[length] = currentCell + 1;
                connectingWall[length] = 2;
                length++;
            }
        }

        //West
        if (currentCell != 0 && (currentCell) % xSize != 0)
        {
            if (!cells[currentCell - 1].visited)
            {
                neighbours[length] = currentCell - 1;
                connectingWall[length] = 4;
                length++;
            }
        }

        //North
        if (currentCell + xSize < cells.Length)
        {
            if (!cells[currentCell + xSize].visited)
            {
                neighbours[length] = currentCell + xSize;
                connectingWall[length] = 1;
                length++;
            }
        }

        //South
        if (currentCell - xSize >= 0)
        {
            if (!cells[currentCell - xSize].visited)
            {
                neighbours[length] = currentCell - xSize;
                connectingWall[length] = 3;
                length++;
            }
        }

        if (length != 0)
        {
            int chosenNeighbour = Random.Range(0, length);
            Debug.Log("length" + length);
            Debug.Log("chosenNeighbour" + chosenNeighbour);
            //wallToBreak = neighbours[chosenNeighbour];
            wallToBreak = connectingWall[chosenNeighbour];
            if (backingUp > 0)
            {
                currentCell = lastCells[backingUp];
                backingUp--;
            }
            Debug.Log("currentNeighbour" + neighbours[chosenNeighbour]);
            return neighbours[chosenNeighbour];
        }
        else
        {
            return 0;
        }
    }
}
