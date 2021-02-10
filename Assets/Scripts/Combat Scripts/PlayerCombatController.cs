using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : ParentCombatController
{
    new int damage = 2;
    new int id = 0;

    public GameObject canvas;

    public delegate void projection(bool striking, dodgeDir dodging, actionHeight blocking, actionHeight attackHeight, strikeSide attackSide, strikePower attackPower);
    public static event projection _projection;

    void OnEnable()
    {
        EnemyCombatController._attack += wasHit;
        Player._sendKey += combatAction;
        Player._setEnemy += HandleSetEnemy;
    }

    void OnDisable()
    {
        EnemyCombatController._attack -= wasHit;
        Player._sendKey -= combatAction;
        Player._setEnemy -= HandleSetEnemy;
    }

    protected void HandleSetEnemy(int _id)
    {
        enemyId = _id;
    }

    public override void wasHit(actionHeight _strikeHeight, strikeSide _strikeSide, strikePower strikePower, ParentCombatController hitter, int hittee_id)
    {
        if(isDodging != dodgeDir.NONE)
        {
            //dodging....

            if ((short)isDodging == (short)_strikeSide)
            {
                health -= (int) (hitter.damage * 1.2);
                //oof. you walked into that one.
            } else
            {
                //you dodged
                return;
            }
        }
        else if(isBlocking != actionHeight.NONE)
        {
            if ((short)isBlocking == (short)_strikeHeight)
            {
                health -= (int)(hitter.damage * 0.2 * blockCombo);
                //block succeeds!
            } else
            {
                health -= hitter.damage;
                //block fails
            }
        } else
        {
            health -= hitter.damage;
        }
    }

    protected void combatAction(string code)
    {
        actionHeight attackHeight = actionHeight.NONE;
        strikeSide attackSide = strikeSide.NONE;
        strikePower attackPower = strikePower.NONE;

        Debug.Log("Player taking combat action " + code);

        switch (code)
        {
            case "up":

                // Heavy Attack
                break;
            case "right":
                isDodging = dodgeDir.RIGHT;
                // Preform Animation

                // right
                break;
            case "left":
                isDodging = dodgeDir.LEFT;
                // Preform Animation

                // left
                break;
            case "down":

                // Drink Potion
                break;

            case "q":
                attackHeight = actionHeight.HIGH;
                attackSide = strikeSide.LEFT;
                attackPower = strikePower.NORMAL;
                canvas.GetComponent<Animator>().SetBool("isAttacking", true);
                strike(actionHeight.HIGH, strikeSide.LEFT, strikePower.NORMAL);

                // q    
                break;
            case "w":
                isBlocking = actionHeight.HIGH;
                blockCombo++;
                // Preform Animation

                // w
                break;
            case "e":
                attackHeight = actionHeight.LOW;
                attackSide = strikeSide.LEFT;
                attackPower = strikePower.NORMAL;
                canvas.GetComponent<Animator>().SetBool("isAttacking", true);
                strike(actionHeight.LOW, strikeSide.LEFT, strikePower.NORMAL);

                // e
                break;

            case "a":
                attackHeight = actionHeight.HIGH;
                attackSide = strikeSide.RIGHT;
                attackPower = strikePower.NORMAL;
                canvas.GetComponent<Animator>().SetBool("isAttacking", true);
                strike(actionHeight.HIGH, strikeSide.RIGHT, strikePower.NORMAL);

                // a
                break;
            case "s":
                isBlocking = actionHeight.LOW;
                blockCombo++;
                // Preform Animation


                // s
                break;
            case "d":
                attackHeight = actionHeight.LOW;
                attackSide = strikeSide.RIGHT;
                attackPower = strikePower.NORMAL;
                canvas.GetComponent<Animator>().SetBool("isAttacking", true);
                strike(actionHeight.LOW, strikeSide.RIGHT, strikePower.NORMAL);
                    
                // d
                break;
        }
        _projection?.Invoke(isStriking, isDodging, isBlocking, attackHeight, attackSide, attackPower);
    }

    protected override void AnimReset()
    {
        GetComponent<Animator>().SetBool("isAttacking", false);
    }
}
