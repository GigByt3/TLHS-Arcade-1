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
        Debug.Log("-------------PLAYER WAS HIT");

        if(isDodging != dodgeDir.NONE)
        {
            //dodging....

            if ((short)isDodging == (short)_strikeSide)
            {
                health -= (int) (hitter.damage * 1.2);
                //oof. you walked into that one.
            } else
            {
                Debug.Log("-------------PLAYER DODGE" + health);
                //you dodged
                return;
            }
        }
        else if(isBlocking != actionHeight.NONE)
        {
            if ((short)isBlocking == (short)_strikeHeight)
            {
                health -= (int)(hitter.damage * defense * blockCombo);
                Debug.Log("-------------PLAYER BLOCK" + health);
                //block succeeds!
            } else
            {
                health -= hitter.damage;
                //block fails
            }
        } else
        {
            Debug.Log("-------------PLAYER HURT " + health);
            health -= hitter.damage;
            if(health <= 0)
            {
                GameObject.FindGameObjectsWithTag("GameManager")[0].GetComponent<GameSceneManager>().Death();
            }
        }
    }

    protected void combatAction(string code)
    {
        strikeHeightSTORE = actionHeight.NONE;
        strikeSideSTORE = strikeSide.NONE;
        strikePowerSTORE = strikePower.NONE;

        switch (code)
        {
            case "up":

                // Heavy Attack
                break;
            case "right":
                if (isDodging != dodgeDir.NONE) break;
                isDodging = dodgeDir.RIGHT;
                GameObject.FindGameObjectsWithTag("MainCamera")[0].GetComponent<Animator>().SetInteger("DodgePos", 2);
                // Preform Animation

                // right
                break;
            case "left":
                if (isDodging != dodgeDir.NONE) break;
                isDodging = dodgeDir.LEFT;
                GameObject.FindGameObjectsWithTag("MainCamera")[0].GetComponent<Animator>().SetInteger("DodgePos", 1);
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
                if (isBlocking != actionHeight.NONE) break;
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
                if (isBlocking != actionHeight.NONE) break;
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

    public override void AnimStart(int number)
    {
        swordCanvas.GetComponent<Animator>().SetInteger("AttackIndex", number); 
    }

    public override void AnimReset()
    {
        Debug.Log("ANIM RESET PLAYER");
        swordCanvas.GetComponent<Animator>().SetInteger("AttackIndex", 0);
        sheildCanvas.GetComponent<Animator>().SetInteger("sheildNum", 0);
        GameObject.FindGameObjectsWithTag("MainCamera")[0].GetComponent<Animator>().SetInteger("DodgePos", 0);
    }
}
