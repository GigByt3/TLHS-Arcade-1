using UnityEngine;

public class GreaterIornSkinPotion : Potion
{
    public GreaterIornSkinPotion() : base()
    {
        name = "Greater Iorn Skin Potion";
        value = 20;
    }

    public override void onConsume()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombatController>().defenseModifier += 10;

        //if(player.health > 100) {
        //player.health = 100;
        //}

    }
}
