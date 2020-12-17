using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    private ItemManager itemManager = new ItemManager(); // Manages all the items(Not in the inventory), check class for functions

    private int[] potions = new int[4]; // 4 represents the potion inventory size;

    private int rightHand;
    private int leftHand;


    public void start()
    {
        itemManager.init();

        for (int i = 0; i < potions.Length; i++)
        {
            potions[i] = -1;
        }

    }

    public void starterKit()
    {
        rightHand = 1;
        leftHand = 21;
        potions[0] = 41;
    }

    public void addItem(int id)
    {
        Item itemToAdd = itemManager.getItemFromID(id);

        addItem(itemToAdd);
    }

    public void addItem(Item itemToAdd)
    {
        if (itemToAdd != null)
        {
            if (itemToAdd.type == ItemType.POTION)
            {
                int replaceTo = comparePotions(itemToAdd.value);
                if (replaceTo == -1)
                {
                    return;
                }
                potions[replaceTo] = itemToAdd.id;
            }
            else if (itemToAdd.type == ItemType.COMBAT)
            {
                bool replace = compareItems(itemToAdd.id, rightHand);
                if (replace)
                {
                    rightHand = itemToAdd.id;
                }
            }
            else if (itemToAdd.type == ItemType.SHIELD)
            {
                bool replace = compareItems(itemToAdd.id, leftHand);
                if (replace)
                {
                    leftHand = itemToAdd.id;
                }
            }
        }
    }

    public bool compareItems(int item1ID, int item2ID) // Returns if the first item is better 
    {
        if (itemManager.getItemFromID(item1ID).value > itemManager.getItemFromID(item2ID).value)
        {
            return true;
        }
        return false;
    }


    public int comparePotions(int itemValue)
    {

        List<Item> items = new List<Item>();


        int index = 0;
        foreach (Item item in items)
        {
            if (item.value < itemValue)
            {
                return index;
            }
            index++;
        }

        return -1; // Potion has a value less or the same as the current potions
    }


    public List<Item> getPotions()
    {
        List<Item> items = new List<Item>();

        for (int i = 0; i < potions.Length; i++)
        {
            items.Add(itemManager.getItemFromID(potions[i]));
        }
        return items;
    }

    public Item getRightHandItem()
    {
        return itemManager.getItemFromID(rightHand);
    }

    public Item getLeftHandItem()
    {
        return itemManager.getItemFromID(leftHand);
    }

    public float getDamage()
    {
        return ((Weapon)itemManager.getItemFromID(rightHand)).damage;
    }

    public float getDefense()
    {
        return ((Shield)itemManager.getItemFromID(leftHand)).defense;
    }


}
