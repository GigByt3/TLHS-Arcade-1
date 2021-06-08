using UnityEngine;

public class StrengthPotion : Potion
{
    public StrengthPotion() : base()
    {
        name = "Strength Potion";
        value = 17;
    }

    public override void onConsume()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombatController>().damageModifier += 10;

        //if(player.health > 100) {
        //player.health = 100;
        //}

    }
}
