using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonCombatController : EnemyCombatController
{
    private void OnEnable()
    {
        PlayerCombatController._attack += wasHit;
        PlayerCombatController._projection += react;
        difficulty = 50;
    }

    private void OnDisable()
    {
        PlayerCombatController._attack -= wasHit;
        PlayerCombatController._projection -= react;
    }

    public void blockOrDoge()
    {

        float random = Random.Range(0.0f, 100.0f);
        //Block
        if (random < 50.0f)
        {
            if (canBlock)
            {
                float rando = Random.Range(0.0f, 100.0f);
                //action height
                if (rando < 50.0f)
                {
                    isBlocking = actionHeight.HIGH;
                    GetComponent<Animator>().SetInteger("CurrentAction", 5);
                }
                else
                {
                    isBlocking = actionHeight.LOW;
                    GetComponent<Animator>().SetInteger("CurrentAction", 6);
                }
            }
        }
        else  //dodge
        {
            if (canDodge)
            {
                float rand = Random.Range(0.0f, 100.0f);
                if (random < 50.0f)
                {
                    isDodging = dodgeDir.RIGHT;
                    GetComponent<Animator>().SetInteger("CurrentAction", 8);
                }
                else
                {
                    isDodging = dodgeDir.LEFT;
                    GetComponent<Animator>().SetInteger("CurrentAction", 7);
                }
            }
        }
    }

    public override void enemyStrike()
    {
        actionHeight attackHeightSTORE;
        strikeSide attackSideSTORE;



        float random = Random.Range(0.0f, 100.0f);
        //action height
        if (random < 50.0f)
        {
            attackHeightSTORE = actionHeight.HIGH;
        }
        else
        {
            attackHeightSTORE = actionHeight.LOW;
        }

        random = Random.Range(0.0f, 100.0f);
        //strike height
        if (random < 50.0f)
        {
            attackSideSTORE = strikeSide.LEFT;
        }
        else
        {
            attackSideSTORE = strikeSide.RIGHT;
        }

        strike(attackHeightSTORE, attackSideSTORE, strikePower.NORMAL);
    }
}
