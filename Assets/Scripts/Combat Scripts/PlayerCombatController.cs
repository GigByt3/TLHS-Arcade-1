using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : ParentCombatController
{
    new int damage;
    new float defense;
    private new int id = 0;

    public GameObject swordCanvas;
    public GameObject sheildCanvas;

    public delegate void projection(bool striking, dodgeDir dodging, actionHeight blocking, actionHeight attackHeight, strikeSide attackSide, strikePower attackPower, int hittee_id);
    public static event projection _projection;

    void OnEnable()
    {
        damage = (int) GetComponent<Player>().inventory.GetDamage();
        defense = GetComponent<Player>().inventory.GetDefense();
        isDodging = dodgeDir.NONE;
        isBlocking = actionHeight.NONE;

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
                health -= (int)(hitter.damage * defense * blockCombo);
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
        strikeHeightSTORE = actionHeight.NONE;
        strikeSideSTORE = strikeSide.NONE;
        strikePowerSTORE = strikePower.NONE;

        Debug.Log("Player taking combat action " + code);

        switch (code)
        {
            case "up":

                // Heavy Attack
                break;
            case "right":
                isDodging = dodgeDir.RIGHT;
                GetComponent<Animator>().SetInteger("DodgePos", 2);
                // Preform Animation

                // right
                break;
            case "left":
                isDodging = dodgeDir.LEFT;
                GetComponent<Animator>().SetInteger("DodgePos", 1);
                // Preform Animation

                // left
                break;
            case "down":

                // Drink Potion
                break;

            case "q":
                strike(actionHeight.HIGH, strikeSide.LEFT, strikePower.NORMAL);

                // q    
                break;
            case "w":
                isBlocking = actionHeight.HIGH;
                sheildCanvas.GetComponent<Animator>().SetInteger("sheildNum", 1);
                blockCombo++;
                // Preform Animation

                // w
                break;
            case "e":
                strike(actionHeight.LOW, strikeSide.LEFT, strikePower.NORMAL);

                // e
                break;

            case "a":
                strike(actionHeight.HIGH, strikeSide.RIGHT, strikePower.NORMAL);

                // a
                break;
            case "s":
                isBlocking = actionHeight.LOW;
                sheildCanvas.GetComponent<Animator>().SetInteger("sheildNum", 2);
                blockCombo++;
                // Preform Animation

                // s
                break;
            case "d":
                strike(actionHeight.LOW, strikeSide.RIGHT, strikePower.NORMAL);
                    
                // d
                break;
        }
        _projection?.Invoke(isStriking, isDodging, isBlocking, strikeHeightSTORE, strikeSideSTORE, strikePowerSTORE, enemyId);
    }

    protected override void AnimStart(int number)
    {
        swordCanvas.GetComponent<Animator>().SetInteger("AttackIndex", number); 
    }

    protected override void AnimReset()
    {
        Debug.Log("ANIM RESET PLAYER");
        swordCanvas.GetComponent<Animator>().SetInteger("AttackIndex", 0);
        sheildCanvas.GetComponent<Animator>().SetInteger("sheildNum", 0);
        GetComponent<Animator>().SetInteger("DodgePos", 0);
        isDodging = dodgeDir.NONE;
        isBlocking = actionHeight.NONE;
    }
}
