using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : ParentCombatController
{
    public GameObject swordCanvas;
    public GameObject sheildCanvas;
    public int damageModifier = 0;
    public int defenseModifier = 0;

    public delegate void projection(bool striking, dodgeDir dodging, actionHeight blocking, actionHeight attackHeight, strikeSide attackSide, strikePower attackPower, int hittee_id);
    public static event projection _projection;

    public delegate void enemyExitCombat();
    public static event enemyExitCombat _enemyExitCombat;

    void OnEnable()
    {
        id = 0;

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

    public void UpdatePlayerWeapons()
    {
        damage = (int)GetComponent<Player>().inventory.GetDamage();
        defense = GetComponent<Player>().inventory.GetDefense();
        switch(gameObject.GetComponent<Player>().inventory.leftHand.name)
        {
            case "Wooden Shield":
                sheildCanvas.GetComponent<Animator>().runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("Resources/WoodenSheildAnims", typeof(RuntimeAnimatorController));
                break;
            case "Steel Shield":
                sheildCanvas.GetComponent<Animator>().runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("Resources/SteelSheildAnims.controller", typeof(RuntimeAnimatorController));
                break;
            case "Holy Shield":
                sheildCanvas.GetComponent<Animator>().runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("Resources/HolySheildAnims.controller", typeof(RuntimeAnimatorController));
                break;
        }
        switch (gameObject.GetComponent<Player>().inventory.rightHand.name)
        {
            case "Rusted Sword":
                swordCanvas.GetComponent<Animator>().runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("Resources/RustySwordAnims.controller", typeof(RuntimeAnimatorController));
                break;
            case "Steel Sword":
                swordCanvas.GetComponent<Animator>().runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("Resources/SteelSwordAnims.controller", typeof(RuntimeAnimatorController));
                break;
            case "Holy Sword":
                swordCanvas.GetComponent<Animator>().runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("Resources/HolySwordAnims", typeof(RuntimeAnimatorController));
                break;
        }
    }

    private void HandleSetEnemy(int _id, Enemy.EnemyType _enemyType)
    {
        Debug.Log(this + " runs HandleSetEnemy, takes in " + _id + "and type " + _enemyType);
        enemyId = _id;
        enemyType = _enemyType;
    }

    public override void wasHit(actionHeight _strikeHeight, strikeSide _strikeSide, strikePower strikePower, ParentCombatController hitter, int hittee_id)
    {
        if (hitter.id == 0) return;

        int damageDealt = (int) Random.Range(10.0f, hitter.damage);

        if (isDodging != dodgeDir.NONE && (short)isDodging == (short)_strikeSide)
        {
            health -= (int) (damageDealt * 1.2);
            //oof. you walked into that one
        }
        else if(isBlocking != actionHeight.NONE && (short)isBlocking == (short)_strikeHeight)
        {
            health -= (int)(damageDealt - (defense * Mathf.Pow(0.9f, blockCombo)));
        }
        else if(isDodging == dodgeDir.NONE && isBlocking == actionHeight.NONE)
        {
            health -= damageDealt;
        }

        if (health <= 0)
        {
            GameObject.FindGameObjectsWithTag("GameManager")[0].GetComponent<GameSceneManager>().Death();
            GetComponent<Player>().die();
            _enemyExitCombat?.Invoke();
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
                if (!canAct()) break;
                isDodging = dodgeDir.RIGHT;
                GameObject.FindGameObjectsWithTag("MainCamera")[0].GetComponent<Animator>().SetInteger("DodgePos", 2);
                // Preform Animation

                // right
                break;
            case "left":
                if (!canAct()) break;
                isDodging = dodgeDir.LEFT;
                GameObject.FindGameObjectsWithTag("MainCamera")[0].GetComponent<Animator>().SetInteger("DodgePos", 1);
                // Preform Animation

                // left
                break;
            case "down":
                this.gameObject.GetComponent<Player>().inventory.DrinkPotion();
                // Drink Potion
                break;

            case "q":
                if (!canAct()) break;
                strike(actionHeight.HIGH, strikeSide.LEFT, strikePower.NORMAL);

                // q    
                break;
            case "w":
                if (!canAct()) break;
                isBlocking = actionHeight.HIGH;
                sheildCanvas.GetComponent<Animator>().SetInteger("sheildNum", 1);
                blockCombo++;
                // Preform Animation

                // w
                break;
            case "e":
                if (!canAct()) break;
                strike(actionHeight.LOW, strikeSide.LEFT, strikePower.NORMAL);

                // e
                break;

            case "a":
                if (!canAct()) break;
                strike(actionHeight.HIGH, strikeSide.RIGHT, strikePower.NORMAL);

                // a
                break;
            case "s":
                if (!canAct()) break;
                isBlocking = actionHeight.LOW;
                sheildCanvas.GetComponent<Animator>().SetInteger("sheildNum", 2);
                blockCombo++;
                // Preform Animation

                // s
                break;
            case "d":
                if (!canAct()) break;
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
        swordCanvas.GetComponent<Animator>().SetInteger("AttackIndex", 0);
        sheildCanvas.GetComponent<Animator>().SetInteger("sheildNum", 0);
        GameObject.FindGameObjectsWithTag("MainCamera")[0].GetComponent<Animator>().SetInteger("DodgePos", 0);
    }
}