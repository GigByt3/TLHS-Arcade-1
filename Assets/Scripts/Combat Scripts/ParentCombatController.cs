using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ParentCombatController : MonoBehaviour
{
    private int dodgeCooldown = 2;
    private int blockCooldown = 1;
    private int strikeCooldown = 1;

    public int id;
    public int enemyId = -1;
    protected int health = 100;
    protected int stamina = 10;
    public int damage = 20;
    public float defense = 0.2f;


    protected int blockCombo = 0;
    protected int strikeCombo = 0;

    protected bool canStrike = true;
    protected bool canDodge = true;
    protected bool canBlock = true;

    protected actionHeight isBlocking = actionHeight.NONE;
    protected dodgeDir isDodging = dodgeDir.NONE;
    protected bool isStriking = false;

    protected actionHeight strikeHeightSTORE;
    protected strikeSide strikeSideSTORE;
    protected strikePower strikePowerSTORE;

    public enum actionHeight : short
    {
        HIGH,
        LOW,
        NONE = 4
    }

    public enum strikeSide : short
    {
        LEFT = 0,
        RIGHT = 1,
        NONE = 4
    }

    public enum strikePower : short
    {
        LIGHT,
        NORMAL,
        HEAVY,
        NONE = 4
    }

    public enum dodgeDir : short
    {
        LEFT = 0,
        RIGHT = 1,
        BACK = 2, //(up arrow)
        DOWN = 3, //(back arrow)
        NONE = 4
    }

    public abstract void wasHit(actionHeight _strikeHeight, strikeSide _strikeSide, strikePower _strikePower, ParentCombatController hitter, int hittee);

    public delegate void attack(actionHeight _strikeHeight, strikeSide _strikeSide, strikePower _strikePower, ParentCombatController hitter, int hittee);

    public static event attack _attack;

    public void strike(actionHeight _strikeHeight, strikeSide _strikeSide, strikePower _strikePower)
    {
        if (!canAct()) return;

        isStriking = true;
        blockCombo = 0;
        strikeHeightSTORE = _strikeHeight;
        strikeSideSTORE = _strikeSide;
        strikePowerSTORE = _strikePower;
        if (_strikeHeight == actionHeight.HIGH && _strikeSide == strikeSide.LEFT)
        {
            AnimStart(1);
        }
        else if (_strikeHeight == actionHeight.HIGH && _strikeSide == strikeSide.RIGHT)
        {
            AnimStart(2);
        }
        else if (_strikeHeight == actionHeight.LOW && _strikeSide == strikeSide.LEFT)
        {
            AnimStart(3);
        }
        else if (_strikeHeight == actionHeight.LOW && _strikeSide == strikeSide.RIGHT)
        {
            AnimStart(4);
        }
    }

    //Checks if Player is in the Middle of an Action
    protected bool canAct()
    {
        if (isStriking || isDodging != dodgeDir.NONE || isBlocking != actionHeight.NONE)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public abstract int REMOVE();

    //Called by Animator Event on Height of Punch just calls the attack event. ()
    public void StrikeConnect()
    {
        _attack(strikeHeightSTORE, strikeSideSTORE, strikePowerSTORE, this, enemyId);
    }

    //Called by AnimatorEvent when Animation is done
    public void Complete(string type)
    {
        AnimReset();
        StartCoroutine(ActionComplete(type));
    }


    //Called by AnimatorEvent when Animation is done
    public IEnumerator ActionComplete(string type)
    {
        //Debug.Log("ActionComplete called with action: " + type);
        switch(type)
        {
            case "dodge":
                isDodging = dodgeDir.NONE;
                isStriking = false;
                yield return new WaitForSeconds(dodgeCooldown);
                break;
            case "block":
                isBlocking = actionHeight.NONE;
                isStriking = false;
                yield return new WaitForSeconds(blockCooldown);
                break;
            case "light_strike":
                isStriking = false;
                yield return new WaitForSeconds(strikeCooldown * 0.33f);
                break;
            case "normal_strike":
                isStriking = false;
                yield return new WaitForSeconds(strikeCooldown);
                break;
            case "heavy_strike":
                isStriking = false;
                yield return new WaitForSeconds(strikeCooldown * 3);
                break;
            default:
                isStriking = false;
                yield return new WaitForSeconds(2);
                break;
        }

        switch (type)
        {
            case "dodge":
                canDodge = true;
                canStrike = true;
                break;
            case "block":
                canBlock = true;
                canStrike = true;
                break;
            case "light_strike":
                canStrike = true;
                break;  
            case "normal_strike":
                canStrike = true;
                break;
            case "heavy_strike":
                canStrike = true;
                break;
            default:
                canDodge = true;
                canBlock = true;
                canStrike = true;
                break;
        }

    }

    public abstract void AnimStart(int number);

    public abstract void AnimReset();
}