using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieCombatController : EnemyCombatController
{
    void OnEnable()
    {
        PlayerCombatController._attack += wasHit;
        PlayerCombatController._projection += react;
    }

    void OnDisable()
    {
        PlayerCombatController._attack -= wasHit;
        PlayerCombatController._projection -= react;
    }

    private void Update()
    {

    }

    public void blockOrDoge()
    {

        float random = Random.Range(0.0f, 100.0f);
        //Block
        if (random < 50.0f)
        {
            if (canBlock) {
                float rando = Random.Range(0.0f, 100.0f);
                //action height
                if (rando < 50.0f)
                {
                    isBlocking = actionHeight.HIGH;
                }
                else
                {
                    isBlocking = actionHeight.LOW;
                }
            }
        } else  //dodge
        {
            if (canBlock)
            {
                float rand = Random.Range(0.0f, 100.0f);
                if (random < 20.0f)
                {
                    isDodging = dodgeDir.RIGHT;
                }
                else if (random < 40.0f)
                {
                    isDodging = dodgeDir.LEFT;
                }
                else if (random < 60.0f)
                {
                    isDodging = dodgeDir.DOWN;
                }
                else
                {
                    isDodging = dodgeDir.BACK;
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

        wasHit(attackHeight, attackSide, strikePower.NORMAL, this, id);

    }

    /*
     * Defense Progression
     * If player is attacking 
     */
}
