using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public int width;
    public int height;

    public Maze maze;

    public GameObject wallPrefab;
    public float cellSize;

    public LevelController(string parmaetrts)
    {

    }
    
    void Awake()
    {
        createMaze();
    }

    void createMaze()
    {
        GameObject mazeContainer = new GameObject("Maze");
        mazeContainer.AddComponent<Maze>();
        maze = mazeContainer.GetComponent<Maze>();
        maze.width = width;
        maze.height = height;
        maze.wallPrefab = wallPrefab;
        maze.cellWidth = cellSize;
        maze.Ready();
    }
}
