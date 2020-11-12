using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ParentCombatController : MonoBehaviour
{
    protected int id;

    protected int health = 100;
    protected int stamina = 10;
    public int damage = 20;

    protected int blockCombo = 0;
    protected int strikeCombo = 0;

    protected bool canStrike;
    protected bool canDodge;
    protected bool canBlock;

    protected actionHeight isBlocking;
    protected dodgeDir isDodging;
    protected bool isStriking;

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

    public enum dodgeDir : short
    {
        LEFT = 0,
        RIGHT = 1,
        BACK = 2, //(up arrow)
        DOWN = 3, //(back arrow)
        NONE = 4
    }

    protected abstract void wasHit(actionHeight _strikeHeight, strikeSide _strikeSide, ParentCombatController hitBy, int _id);

    public delegate void attack(actionHeight _strikeHeight, strikeSide _strikeSide, ParentCombatController hitBy, int _id);

    public static event attack _attack;

    public void strike(actionHeight _strikeHeight, strikeSide _strikeSide)
    {
        if (canAct()) return;
        blockCombo = 0;
        _attack(_strikeHeight, _strikeSide, this, id);
    }

    protected bool canAct()
    {
        if (isStriking || isDodging != dodgeDir.NONE || isBlocking != actionHeight.NONE)
        {
            return true;
        }
        else return false;
    }
}