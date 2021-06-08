using UnityEngine;

public class IornSkinPotion : Potion
{
    public IornSkinPotion() : base()
    {
        name = "Iorn Skin Potion";
        value = 15;
    }

    public override void onConsume()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombatController>().defenseModifier += 10;

        //if(player.health > 100) {
        //player.health = 100;
        //}

    }
}
