using UnityEngine;

public class GreaterSpeedPotion : Potion
{
    public GreaterSpeedPotion() : base()
    {
        name = "Greater Speed Potion";
        value = 26;
    }

    public override void onConsume()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<ParentCombatController>().coolDownModifier *= 0.85f;

        //if(player.health > 100) {
        //player.health = 100;
        //}

    }
}
