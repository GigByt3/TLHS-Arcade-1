using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyCombatController : ParentCombatController
{


    void OnEnable()
    {
        PlayerCombatController._attack += wasHit;
    }

    void OnDisable()
    {
        PlayerCombatController._attack -= wasHit;
    }

    protected override void wasHit(actionHeight _strikeHeight, strikeSide _strikeSide, ParentCombatController hitBy, int _id)
    {
        if (_id != id) return;
        if(!canAct())
        {
            //enemy is invulnerable while acting
            return;
        }

        if(canDodge && stamina - 1 >= 0)
        {
            stamina -= 1;
            //dodge animation
        } else if(canBlock && blockCombo < 5)
        {
            blockCombo++;
            health -= (int) (hitBy.damage * 0.2 * blockCombo);
            //block animation
        } else
        {
            health -= hitBy.damage;
        }
    }
}
