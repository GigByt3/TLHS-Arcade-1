using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{

    static float position = 0;
    /*
     * SCENE DIRECTORY
     * 0 - Main Menu
     * 0.5 - First Transition
     * 1 - First Level
     * 1.5 Second Transition
     * ...
     * x - End Sceen
     * 402 - Death Sceen
     */

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        switch (position)
        {
            case 1;
                break;
            case 402:
                //call a method.
                break;
            default:
                //generate a maze ig.
                break;
        }
    }

    public void NextScene()
    {
        position += 0.5f;
        if (Mathf.Floor(position) != position)
        {
            SceneManager.LoadScene("Transition_Area");
        }
        else
        {
            SceneManager.LoadScene("Game_Scene");
        }
    }

    public void Death()
    {
        position = 402;
        SceneManager.LoadScene("Transition_Area");
    }

    //Tools

    public void GenerateMaze()
    {

    }
}