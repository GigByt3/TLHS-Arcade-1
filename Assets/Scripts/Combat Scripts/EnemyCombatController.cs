using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyCombatController : ParentCombatController
{
    // Percentage out of 100
    protected int difficulty = 30;
    private static int idCounter = 1;

    public delegate void death();
    public static event death _death;

    void OnEnable()
    {
        id = idCounter;
        idCounter++;
        PlayerCombatController._attack += wasHit;
        PlayerCombatController._projection += react;
    }

    void OnDisable()
    {
        PlayerCombatController._attack -= wasHit;
        PlayerCombatController._projection -= react;
    }

    public override void wasHit(actionHeight _strikeHeight, strikeSide _strikeSide, ParentCombatController hitBy, int _id)
    {
        if (_id != id) return;

        if (!canAct())
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

            if (health <= 0)
            {
                _death();
                gameObject.GetComponent<Enemy>().maze.removeObject(gameObject.GetComponent<Enemy>());
            }
        }

        Debug.Log("Enemy id: " + id + " was hit. Current health:" + health);
    }

    protected void react(bool striking, dodgeDir dodging, actionHeight blocking, actionHeight attackHeight, strikeSide attackSide)
    {
        if (Random.Range(0.0f, 100.0f) > difficulty) return;

        if (blocking != actionHeight.NONE && dodging != dodgeDir.NONE && canStrike)
        {
            //  strike
        }

        if (striking)
        {
            //  block or dodge
        }
    }

}
