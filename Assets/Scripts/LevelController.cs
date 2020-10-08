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

    public GameObject playerObject, playerPrefab;

    void Awake()
    {
        createMaze();
        placePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        //in the future, should be only run on move event
        updateGridObjectPositions();
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

    void placePlayer()
    {
        playerObject = Instantiate(playerPrefab);
        playerObject.name = "Player";
        playerObject.GetComponent<Player>().Ready();

        Vector3Int playerStartCoords = new Vector3Int(0, 0, 2);

        playerObject.GetComponent<Player>().gridCoords = playerStartCoords;
        maze.gridObjectDict = new Dictionary<Vector3Int, GridObject>();
        maze.gridObjectDict.Add(playerStartCoords, playerObject.GetComponent<Player>());
    }

    void updateGridObjectPositions()
    {
        foreach (KeyValuePair<Vector3Int, GridObject> kvp in maze.gridObjectDict)
        {
            kvp.Value.transform.position = maze.cellCoordsToGlobalCoords(kvp.Key.x, kvp.Key.y, 0, 0);
            switch (kvp.Key.z)
            {
                case 0:
                    kvp.Value.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                    break;
                case 1:
                    kvp.Value.transform.rotation = Quaternion.Euler(0.0f, 270.0f, 0.0f);
                    break;
                case 2:
                    kvp.Value.transform.rotation = Quaternion.identity;
                    break;
                case 3:
                    kvp.Value.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
                    break;
                default:
                    kvp.Value.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                    break;
            }
        }
    }
}
