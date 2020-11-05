﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : ParentCombatController
{
    new public int damage = 2;
    new private int id = 0;

    void OnEnable()
    {
        EnemyCombatController._attack += wasHit;
    }

    void OnDisable()
    {
        EnemyCombatController._attack -= wasHit;
    }

    private void Start()
    {
        strike(actionHeight.HIGH, strikeSide.RIGHT);
    }

    protected override void wasHit(actionHeight _strikeHeight, strikeSide _strikeSide, ParentCombatController hitBy, int _id)
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
        } else if(isBlocking != strikeSide.NONE)
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
}
