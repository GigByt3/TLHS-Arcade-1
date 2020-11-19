using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : GridObject
{
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        Player.moveEvent += AI;
    }

    void OnDisable()
    {
        Player.moveEvent -= AI;
    }

    public void AI()
    {
        if (player.isInCell(visibleNorthCells()))
        {
            faceDirection("north");
            move(1);
        }
        else if (player.isInCell(visibleEastCells()))
        {
            faceDirection("east");
            move(1);
        } 
        else if (player.isInCell(visibleSouthCells()))
        {
            faceDirection("south");
            move(1);
        }
        else if (player.isInCell(visibleWestCells()))
        {
            faceDirection("west");
            move(1);
        }
        else
        {
            switch (UnityEngine.Random.Range(0, 4))
            {
                case 0:
                    faceDirection("north");
                    break;
                case 1:
                    faceDirection("east");
                    break;
                case 2:
                    faceDirection("south");
                    break;
                case 3:
                    faceDirection("west");
                    break;
            }
            move(1);
        }
    }
}
