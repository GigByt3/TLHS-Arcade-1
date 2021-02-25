using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private Potion[] potions = new Potion[4]; // 4 represents the potion inventory size;

    private Item rightHand;
    private Item leftHand;

    public void StarterKit()
    {
        rightHand = new IronSword();
        leftHand = new WoodenShield();
        potions[0] = new HealthPotion();
    }

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

    public Item CompareItems(Item item1, Item item2) // Returns if the first item is better 
    {
        return (item1.value > item2.value) ? item1 : item2;
    }

    public void ComparePotions(Potion newPotion)
    {
        for (int i = 0; i < potions.Length; i++)
        {
            if (potions[i].value <= newPotion.value)
            {
                potions[i] = newPotion;
                break;
            }
        }

        SortPotions();
    }

    public void SortPotions()
    {
        bool isSorted = false;
        while (!isSorted)
        {
            int prevPotValue = 0;
            isSorted = true;
            foreach (Potion potion in potions)
            {
                if (prevPotValue > potion.value)
                {
                    isSorted = false;
                    break;
                }
            }
            if (isSorted) break;

            for (int i = 0; i < (potions.Length - 1); i++)
            {
                if (potions[i].value < potions[i + 1].value)
                {
                    Potion firstPotion = potions[i];
                    potions[i] = potions[i + 1];
                    potions[i + 1] = firstPotion;
                }
            }
        }
    }

    public float GetDamage()
    {
        if (rightHand.type == ItemType.COMBAT)
        {
            return ((Weapon) rightHand).damage;
        }
        else return 0.0f;
    }

    public float GetDefense()
    {
        if (leftHand.type == ItemType.SHIELD)
        {
            return ((Shield) leftHand).defense;
        }
        else return 0.0f;
    }


}
