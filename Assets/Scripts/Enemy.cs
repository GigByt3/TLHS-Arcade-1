using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : GridObject
{
    public EnemyType type;
    
    Player player;

    private int actionsPerTurn;

    //Method for assigning all the various fields of an enemy, in lieu of a Java-esque "constructor" in C#
    public void EnemyConstructor(EnemyType _type)
    {
        type = _type;

        switch (type)
        {
            case EnemyType.Zombie:
                actionsPerTurn = 1;
                break;
            case EnemyType.Skeleton:
                actionsPerTurn = 2;
                break;
            default:
                actionsPerTurn = 1;
                break;
        }
    }
    
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

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
        for (int i = 0; i < actionsPerTurn; i++)
        {
            if (isPlayerAdjacent())
            {
                player.enterCombat(this);
                GetComponent<EnemyCombatController>().inCombat = true;
                return;
            }

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
                bool hasMoved = false;
                while (!hasMoved)
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
                    if (canMoveForwards())
                    {
                        move(1);
                        hasMoved = true;
                    }
                    else
                    {
                        hasMoved = false;
                        continue;
                    }
                }
            }
        }
    }

    public enum EnemyType
    {
        Zombie, Skeleton
    }
}
