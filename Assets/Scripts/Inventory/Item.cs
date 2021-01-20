using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Item
{
    
    public int id;

    public string name;

    public ItemType type;

    public int value;

    public virtual void init(){} // super.innt doesnt work so i have to make a init for each sub class and parent

    public virtual void superinit() {}


}
