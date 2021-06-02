using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : GridObject, Interactible
{
    private float width, height;

    //This isn't technically necesary cuz constructors do exist in C#, but unity big dumb fuck fuck fuck please stop
    public void ChestConstructor(float _width, float _height)
    {
        width = _width;
        height = _height;
    }
    
    void Start()
    {
        transform.localScale = new Vector3(width, height, width);
    }

    public void onInteract()
    {
        GetComponent<Animator>().SetBool("isOpened", true);
        GetComponent<Animator>().Play("chestOpen");
    }

    public void AnimCallthroughFinishOpening()
    {
        Maze maze = GameObject.Find("Maze").GetComponent<Maze>();

        Item itemToGive = new HealthPotion(); //change this to other/random item(s) in the future
        maze.player.inventory.AddItem(itemToGive);
        
        maze.removeObject(this);
    }
}
