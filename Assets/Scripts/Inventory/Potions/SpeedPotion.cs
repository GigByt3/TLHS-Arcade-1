using UnityEngine;

public class SpeedPotion : Potion
{
    public SpeedPotion() : base()
    {
        name = "Speed Potion";
        value = 21;
    }

    public override void onConsume()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<ParentCombatController>().coolDownModifier *= 0.85f;

        //if(player.health > 100) {
        //player.health = 100;
        //}

    }
}
