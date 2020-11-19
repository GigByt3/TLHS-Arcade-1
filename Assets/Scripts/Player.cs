using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : GridObject
{
    public delegate void MoveDelegate();
    public static event MoveDelegate moveEvent;
    
    void Update()
    {
        checkKeys();
    }

    void checkKeys()
    {
        if (Input.GetKeyDown("up"))
        {
            move(1);
            moveEvent?.Invoke();
        }
        if (Input.GetKeyDown("left"))
        {
            rotate("left");
            moveEvent?.Invoke();
        }
        if (Input.GetKeyDown("right"))
        {
            rotate("right");
            moveEvent?.Invoke();
        }

        //The following is a TESTING SCRIPT
        if (Input.GetKeyDown("n")) GameObject.FindGameObjectsWithTag("GameManager")[0].GetComponent<GameSceneManager>().NextScene();
    }

    public bool isInCell(List<Vector2Int> cells)
    {
        return cells.Contains(new Vector2Int(gridCoords.x, gridCoords.y));
    }
}
