using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : ParentCombatController
{
    new int damage = 2;
    new int id = 0;
    

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

    private void Start()
    {
        strike(actionHeight.HIGH, strikeSide.RIGHT);
    }

    protected void HandleSetEnemy(int _id)
    {
        enemyId = _id;
    }

    public override void wasHit(actionHeight _strikeHeight, strikeSide _strikeSide, ParentCombatController hitBy, int _id)
    {
        if(isDodging != dodgeDir.NONE)
        {
            if ((short)isDodging == (short)_strikeSide)
            {
                health -= (int) (hitBy.damage * 1.2);
                //oof. you walked into that one.
            } else
            {
                return;
            }
        } else if(isBlocking != actionHeight.NONE)
        {
            if ((short)isBlocking == (short)_strikeSide)
            {
                health -= (int)(hitBy.damage * 0.2 * blockCombo);
                //block succeeds!
            } else
            {
                health -= hitBy.damage;
            }
        } else
        {
            health -= hitBy.damage;
        }
    }

    public delegate void projection(bool striking, dodgeDir dodging, actionHeight blocking, actionHeight attackHeight, strikeSide attackSide);

    public static event projection _projection;

    protected void combatAction(string code)
    {
        actionHeight attackHeight = actionHeight.NONE;
        strikeSide attackSide = strikeSide.NONE;

        Debug.Log("Player taking combat action " + code);

        switch (code)
        {
            case "up":
                isDodging = dodgeDir.BACK;
                // Preform Animation

                // up
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
                isDodging = dodgeDir.DOWN;
                // Preform Animation

                // down
                break;

            case "q":
                isStriking = true;
                // Preform Animation
                strike(actionHeight.HIGH, strikeSide.LEFT);

                // q
                break;
            case "w":
                isBlocking = actionHeight.HIGH;
                // Preform Animation

                // w
                break;
            case "e":
                isStriking = true;
                // Preform Animation
                strike(actionHeight.LOW, strikeSide.LEFT);

                // e
                break;

            case "a":
                isStriking = true;
                // Preform Animation
                strike(actionHeight.HIGH, strikeSide.RIGHT);

                // a
                break;
            case "s":
                isBlocking = actionHeight.LOW;
                // Preform Animation


                // s
                break;
            case "d":
                isStriking = true;
                // Preform Animation
                strike(actionHeight.LOW, strikeSide.RIGHT);

                // d
                break;
        }
        Debug.Log("Invoke Projection");
        _projection?.Invoke(isStriking, isDodging, isBlocking, attackHeight, attackSide);
    }

    public void strikeConnect()
    {

    }
}
