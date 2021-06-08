using UnityEngine;

public class Poison : Potion
{
    public Poison() : base()
    {
        name = "Poison";
        value = 13;
    }

    public override void onConsume()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if(enemy.GetComponent<EnemyCombatController>().id == GameObject.FindGameObjectWithTag("Player").GetComponent<ParentCombatController>().enemyId)
            {
                enemy.GetComponent<EnemyCombatController>().health -= 30;
            }
        }

        //if(player.health > 100) {
        //player.health = 100;
        //}

    }
}
