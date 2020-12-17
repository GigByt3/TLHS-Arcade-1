using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager
{

    public static List<Item> items = new List<Item>();

    public void init()
    {
        // \/ \/ \/  ! Add All Items Here ! \/ \/ \/         OLD

        //id 1 - 20
        //items.Add(new WoodenShield());

        //21 - 40
        //items.Add(new WoodenShield());


        //id  41 - 60
        //items.Add(new HealthPotion());

        //items.Add(new OkDagger());   Example


        //------------------------------------------        OLD


        // Items are added to item List in the Item supersuperinnit

        foreach (Item item in items)
        {
            //child sub sub class
            item.init();

            //parent sub class
            item.superinit();

            //parent class
            item.supersuperinit();
        }
    }

    public Item getItemFromID(int id)
    {
        foreach (Item item in items)
        {
            if(item.id == id)
            {
                return item;
            }
        }

        return null;
    }


}
