using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : GridObject
{
    private bool inCombat;

    void Update()
    {
        if(inCombat)
        {
            checkKeysCombat();
        } else
        {
            checkKeysMove();
        }
        
    }

    private void checkKeysMove()
    {
        if (Input.GetKeyDown("up")) move(1);
        if (Input.GetKeyDown("left")) rotate("left");
        if (Input.GetKeyDown("right")) rotate("right");

        //The following is a TESTING SCRIPT
        if (Input.GetKeyDown("n")) GameObject.FindGameObjectsWithTag("GameManager")[0].GetComponent<GameSceneManager>().NextScene();
    }

    private void checkKeysCombat()
    {
        //Joystick positions
        if (Input.GetKeyDown("up")) _sendKey("up");
        if (Input.GetKeyDown("left")) _sendKey("left");
        if (Input.GetKeyDown("right")) _sendKey("right");
        if (Input.GetKeyDown("down")) _sendKey("down");

        //Top row buttons
        if (Input.GetKeyDown("q")) _sendKey("q");
        if (Input.GetKeyDown("w")) _sendKey("w");
        if (Input.GetKeyDown("e")) _sendKey("e");

        //Bottom row buttons
        if (Input.GetKeyDown("a")) _sendKey("a");
        if (Input.GetKeyDown("s")) _sendKey("s");
        if (Input.GetKeyDown("d")) _sendKey("d");
    }

    public delegate void sendKey(string code);

    public static event sendKey _sendKey;
}
