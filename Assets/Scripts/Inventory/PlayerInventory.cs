using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory
{
    private Potion[] potions = new Potion[4]; // 4 represents the potion inventory size

    private Item rightHand;
    private Item leftHand;

    //Starter item kit for the player, gives an iron sword, wooden shield, and one health potion.
    public void StarterKit()
    {
        rightHand = new IronSword();
        leftHand = new WoodenShield();
        potions[0] = new HealthPotion();
    }

    //Attempts to add an item if it is better than the item the player currently has
    public void AddItem(Item itemToAdd)
    {
        if (itemToAdd != null)
        {
            if (itemToAdd.type == ItemType.POTION)
            {
                ComparePotions((Potion) itemToAdd);
            }
            else if (itemToAdd.type == ItemType.COMBAT)
            {
                rightHand = CompareItems(itemToAdd, rightHand);
            }
            else if (itemToAdd.type == ItemType.SHIELD)
            {
                leftHand = CompareItems(itemToAdd, rightHand);
            }
        }
    }

    //Returns the higher value item of the 2
    public Item CompareItems(Item item1, Item item2) 
    {
        return (item1.value > item2.value) ? item1 : item2;
    }

    //Checks to see if the given potion is better than any currently carried potions, and if so replaces it, then sorts the potions
    public void ComparePotions(Potion newPotion)
    {
        for (int i = 0; i < potions.Length; i++)
        {
            if (potions[i] == null || potions[i].value <= newPotion.value)
            {
                potions[i] = newPotion;
                break;
            }
        }

        SortPotions();
    }

    //Uses the PotionComparer to sort the potions, pushing nulls to the back
    public void SortPotions()
    {
        //Cool comparer sorting w/ help from Owen
        IComparer potComparer = new PotionComparer();
        Array.Sort(potions, 0, potions.Length, potComparer);
    }

    //Returns player attack damage based on the weapon in the right hand
    public float GetDamage()
    {
        if (rightHand.type == ItemType.COMBAT)
        {
            return ((Weapon) rightHand).damage;
        }
        else return 0.0f;
    }

    //Returns player defense based on the shield in the left hand
    public float GetDefense()
    {
        if (leftHand.type == ItemType.SHIELD)
        {
            return ((Shield) leftHand).defense;
        }
        else return 0.0f;
    }


}
