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

        Item itemToGive; //change this to other/random item(s) in the future
        switch(Random.Range(0, 30))
        {
            case 1:
                itemToGive = new HolySword();
                break;
            case 2:
                itemToGive = new HolyShield();
                break;
            case 3:
            case 4:
                itemToGive = new SteelSword();
                break;
            case 5:
            case 6:
                itemToGive = new SteelShield();
                break;
            case 7:
            case 8:
                itemToGive = new StrengthPotion();
                break;
            case 9:
            case 10:
                itemToGive = new IornSkinPotion();
                break;
            case 11:
            case 12:
                itemToGive = new Poison();
                break;
            case 13:
            case 14:
                itemToGive = new SpeedPotion();
                break;
            case 15:
                itemToGive = new GreaterSpeedPotion();
                break;
            case 16:
                itemToGive = new GreaterStrengthPotion();
                break;
            case 17:
                itemToGive = new GreaterIornSkinPotion();
                break;
            case 18:
                itemToGive = new GreaterPoison();
                break;
            case 19:
                itemToGive = new GreaterHealthPotion();
                break;
            default:
                itemToGive = new HealthPotion();
                break;
        }

        Debug.Log("You have aquired a " + itemToGive.name);

        maze.player.inventory.AddItem(itemToGive);
        
        maze.removeObject(this);
    }
}
