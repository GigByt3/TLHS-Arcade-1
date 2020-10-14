using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public SoundManager _soundManager;
    
    private GameObject[] TransitionText;

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

    void Awake()
    {
        Debug.Log("Scene Manager Awake.");
        DontDestroyOnLoad(transform.gameObject);
    }

    //OnEnable comes first
    void OnEnable()
    {
        SceneManager.sceneLoaded += Setup;
        Debug.Log("OnEnable");
    }

    //OnDisable last.
    void OnDisable()
    {
        SceneManager.sceneLoaded -= Setup;
        Debug.Log("OnDisable");
    }

    private void Setup(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene Manager New Scene Loaded.");
        switch (position)
        {
            case 0.5f:
                SetUpTransition("Wellcome To ZORK! READY TO BEGIN LEVEL 1 IN 4 SECONDS");
                break;
            case 1:
                SetUpGame();
                break;
            case 1.5f:
                SetUpTransition("READY TO BEGIN LEVEL 2 IN 4 SECONDS");
                break;
            case 2:
                SetUpGame();
                break;
            case 2.5f:
                SetUpTransition("READY TO BEGIN LEVEL 3 IN 4 SECONDS");
                break;
            case 3:
                SetUpGame();
                break;
            case 402:
                //call a method.
                break;
            default:
                //generate victory-scene probably? (That'll probably just be a transition)
                break;
        }
    }

    private void SetUpGame()
    {
        _soundManager.MusicTransition(1, 2);
        GenerateMaze(); //Call this presumably
    }

    private void SetUpTransition(string Transition)
    {
        StartCoroutine(SwitchOutOfTransition(4));
        TransitionText = GameObject.FindGameObjectsWithTag("TransitionTextOne");
        TransitionText[0].GetComponent<UnityEngine.UI.Text>().text = Transition;
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
        //Fire Maze Generation
    }

    private IEnumerator SwitchOutOfTransition(int pauseTime)
    {
        Debug.Log("In Transition, Starting " + pauseTime + " second wait. [TIME: " + Time.deltaTime + "]");
        yield return new WaitForSeconds(pauseTime);
        Debug.Log("In Transition, Proscess Complete. [TIME: " + Time.deltaTime + "]");
        NextScene();
    }
}