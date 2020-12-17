using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieCombatController : EnemyCombatController
{
    public ZombieCombatController()
    {
        id = 1;
    }

    public PlayerCombatController playerCombatController;

    protected void react(bool striking, dodgeDir dodging, actionHeight blocking, actionHeight attackHeight, strikeSide attackSide)
    {
        if (Random.Range(0.0f, 100.0f) > difficulty) return;

        if (blocking != actionHeight.NONE && dodging != dodgeDir.NONE && canStrike)
        {
            if (Random.Range(0.0f, 100.0f) > 50.0f)
            {
                strike();
            }
        }

        if (striking)
        {
            blockOrDoge();
        }
    }

    public void blockOrDoge()
    {
        dodgeDir dodgeDirection;

        float random = Random.Range(0.0f, 100.0f);
        //Block
        actionHeight blockHeight;
        if (random < 50.0f)
        {
            if (canBlock) 
            {
                float rando = Random.Range(0.0f, 100.0f);
                //action height
                if (rando < 50.0f)
                {
                    blockHeight = actionHeight.HIGH;
                }
                else
                {
                    blockHeight = actionHeight.LOW;
                }
            }
        } else  //dodge
        {
            if (canBlock)
            {
                float rand = Random.Range(0.0f, 100.0f);
                if (random < 20.0f)
                {
                    dodgeDirection = dodgeDir.RIGHT;
                }
                else if (random < 40.0f)
                {
                    dodgeDirection = dodgeDir.LEFT;
                }
                else if (random < 60.0f)
                {
                    dodgeDirection = dodgeDir.DOWN;
                }
                else
                {
                    dodgeDirection = dodgeDir.BACK;
                }
            }
        }
    }

    public void strike()
    {
        actionHeight attackHeight;
        strikeSide attackSide;


        float random = Random.Range(0.0f, 100.0f);
        //action height
        if (random < 50.0f)
        {
            attackHeight = actionHeight.HIGH;
        } else
        {
            attackHeight = actionHeight.LOW;
        }

        random = Random.Range(0.0f, 100.0f);
        //strike height
        if (random < 50.0f)
        {
            attackSide = strikeSide.LEFT;
        }
        else
        {
            attackSide = strikeSide.RIGHT;
        }

        playerCombatController.wasHit(attackHeight, attackSide, this, id);


    }



}
