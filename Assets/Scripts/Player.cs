using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : GridObject
{

    void Update()
    {
        checkKeys();
    }

    void checkKeys()
    {
        if (Input.GetKeyDown("up")) move(1);
        if (Input.GetKeyDown("left")) rotate("left");
        if (Input.GetKeyDown("right")) rotate("right");

        //The following is a TESTING SCRIPT
        if (Input.GetKeyDown("n")) GameObject.FindGameObjectsWithTag("GameManager")[0].GetComponent<GameSceneManager>().NextScene();
    }
}
