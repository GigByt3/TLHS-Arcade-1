using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyCombatController : ParentCombatController
{
    /*
     * 0 - walk
     * 1 - right low
     * 2 - left low
     * 3 - right high
     * 4 - left high
     * 5 - low block
     * 6 - high block
     * 7 - left dodge
     * 8 - right dodge
     */

    // Percentage out of 100
    protected int difficulty = 30;
    private static int idCounter = 1;
    public Animator anim;

    public delegate void death();
    public static event death _death;

    void OnEnable()
    {
        PlayerCombatController._attack += wasHit;
        PlayerCombatController._projection += react;
    }

    void OnDisable()
    {
        PlayerCombatController._attack -= wasHit;
        PlayerCombatController._projection -= react;
    }

    private void Start()
    {
        id = idCounter;
        idCounter++;
        anim = GetComponent<Animator>();
        Debug.Log(id);
    }

    //Enemy reaction
    protected void react(bool striking, dodgeDir dodging, actionHeight blocking, actionHeight attackHeight, strikeSide attackSide, strikePower attackPower)
    {


        if (canDodge && stamina - 1 >= 0)
        {
            stamina -= 1;
            switch (attackSide)
            {
                case strikeSide.LEFT:
                    isDodging = dodgeDir.LEFT;
                    GetComponent<Animator>().SetInteger("CurrentAction", 7);
                    break;
                case strikeSide.RIGHT:
                    isDodging = dodgeDir.RIGHT;
                    GetComponent<Animator>().SetInteger("CurrentAction", 8);
                    break;
                default:
                    isDodging = dodgeDir.NONE;
                    break;
            }
        }
        else if (canBlock && blockCombo < 5)
        {
            blockCombo++;
            isBlocking = attackHeight;
            switch(attackHeight)
            {
                case actionHeight.HIGH:
                    GetComponent<Animator>().SetInteger("CurrentAction", 5);
                    break;
                case actionHeight.LOW:
                    GetComponent<Animator>().SetInteger("CurrentAction", 6);
                    break;
            }
        }
    }

    public override void wasHit(actionHeight _strikeHeight, strikeSide _strikeSide, strikePower _attackPower, ParentCombatController hitter, int hittee_id)
    {
        if (hittee_id != id) return;

        if (isDodging != dodgeDir.NONE)
        {
            //dodging....

            if ((short)isDodging == (short)_strikeSide)
            {
                health -= (int)(hitter.damage * 1.2);
                //oof. you walked into that one.
            }
            else
            {
                //you dodged
                return;
            }
        }
        else if (isBlocking != actionHeight.NONE)
        {
            if ((short)isBlocking == (short)_strikeHeight)
            {
                health -= (int)(hitter.damage * 0.2 * blockCombo);
                //block succeeds!
            }
            else
            {
                health -= hitter.damage;
                //block fails
            }
        }
        else
        {
            health -= hitter.damage;
        }

        Debug.Log("Enemy id: " + id + " was hit. Current health:" + health);

        if (health <= 0)
        {
            _death();
            gameObject.GetComponent<Enemy>().maze.removeObject(gameObject.GetComponent<Enemy>());
            Debug.Log("Enemy id: " + id + " has died");
        }
    }

    protected override void AnimReset()
    {
        GetComponent<Animator>().SetInteger("CurrentAction", -1);
    }

}
