using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : GridObject
{
    public delegate void MoveDelegate();
    public static event MoveDelegate moveEvent;

    private bool inCombat;

    private const float PLAYER_MOVE_COOLDOWN = 0.5f;
    private float playerMoveCooldownCount;
    
    void Update()
    {
        if (inCombat)
        {
            checkKeysCombat();
        }
        else
        {
            checkKeysMove();
        }
    }

    void checkKeysMove()
    {
        playerMoveCooldownCount++;
        if (Input.GetKeyDown("up") && playerMoveCooldownCount * Time.deltaTime > PLAYER_MOVE_COOLDOWN)
        {
            move(1);
            moveEvent?.Invoke();
            playerMoveCooldownCount = 0;
        }
        if (Input.GetKeyDown("left"))
        {
            rotate("left");
            //moveEvent?.Invoke();
        }
        if (Input.GetKeyDown("right"))
        {
            rotate("right");
            //moveEvent?.Invoke();
        }

        //The following is a TESTING SCRIPT
        if (Input.GetKeyDown("n")) GameObject.FindGameObjectsWithTag("GameManager")[0].GetComponent<GameSceneManager>().NextScene();
    }

    public void enterCombat(Enemy enemy)
    {
        int enemyXPosDif = enemy.gridCoords.x - gridCoords.x;
        int enemyYPosDif = enemy.gridCoords.y - gridCoords.y;

        if (enemyXPosDif == 0 && enemyYPosDif == -1) faceDirection("north");
        if (enemyXPosDif == 1 && enemyYPosDif == 0) faceDirection("east");
        if (enemyXPosDif == 0 && enemyYPosDif == 1) faceDirection("south");
        if (enemyXPosDif == -1 && enemyYPosDif == 0) faceDirection("west");

        Debug.Log(enemy + " is entering combat with the player!");

        inCombat = true;
    }

    public bool isInCell(List<Vector2Int> cells)
    {
        return cells.Contains(new Vector2Int(gridCoords.x, gridCoords.y));
    }

    public delegate void sendKey(string code);

    public static event sendKey _sendKey;

    private void checkKeysCombat()
    {
        //Joystick positions
        if (Input.GetKeyDown("up")) _sendKey?.Invoke("up");
        if (Input.GetKeyDown("left")) _sendKey?.Invoke("left");
        if (Input.GetKeyDown("right")) _sendKey?.Invoke("right");
        if (Input.GetKeyDown("down")) _sendKey?.Invoke("down");

        //Top row buttons
        if (Input.GetKeyDown("q")) _sendKey?.Invoke("q");
        if (Input.GetKeyDown("w")) _sendKey?.Invoke("w");
        if (Input.GetKeyDown("e")) _sendKey?.Invoke("e");

        //Bottom row buttons
        if (Input.GetKeyDown("a")) _sendKey?.Invoke("a");
        if (Input.GetKeyDown("s")) _sendKey?.Invoke("s");
        if (Input.GetKeyDown("d")) _sendKey?.Invoke("d");
    }

    
}
