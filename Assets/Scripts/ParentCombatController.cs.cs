using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ParentCombatController : MonoBehaviour
{
    protected int id;

    protected int health = 10;
    protected int stamina = 10;
    public int damage = 2;

    protected int blockCombo = 0;
    protected int strikeCombo = 0;

    protected bool canStrike;
    protected bool canDodge;
    protected bool canBlock;

    protected enum actionHeight
    {
        HIGH,
        LOW
    }

    protected enum strikeSide
    {
        LEFT,
        RIGHT
    }

    protected enum dodgeDir
    {
        LEFT,
        RIGHT,
        BACK, //(up arrow)
        DOWN //(back arrow)
    }

    protected abstract void wasHit(ParentCombatController hitBy, int _id);

    protected abstract void attack(actionHeight _strikeHeight, strikeSide _strikeSide);
}