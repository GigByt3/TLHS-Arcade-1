using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    // FEILDS ==============================================================================================

    public GameSoundManager _soundManager;

    public Maze maze;

    private GameObject StartMenu;

    private GameObject[] TransitionText;

    static float position = 0;
    /*
     * SCENE DIRECTORY
     * 0 - Main Menu
     * 0.5 - First Transition
     * 1 - First Level
     * 1.5 - Second Transition
     * 2 - Second Level
     * 2.5 - Third Transition
     * 3 - Third Level
     * 3.5 - Boss Transtion
     * 4 - Boss Level
     * 4.5 - Victory Transition
     * 402 - Death Sceen
     * Default - Victory Transtion
     */

    // SETUP ==============================================================================================

    //All the Game Management Objects live in DontDestroyOnLoad!
    void Awake()
    {
        if(GameObject.FindGameObjectsWithTag("GameManager").Length > 1)
        {
            Destroy(transform.gameObject);
        } else
        {
            StartMenu = GameObject.FindGameObjectWithTag("Canvas");
            DontDestroyOnLoad(transform.gameObject);
            DontDestroyOnLoad(StartMenu);

        }
        Debug.Log("Scene Manager Awake.");
    }

    //Subscribing Setup
    void OnEnable()
    {
        SceneManager.sceneLoaded += Setup;
        Debug.Log("Scene Manager OnEnable");
    }
    
    //Unsubscribing Setup
    void OnDisable()
    {
        SceneManager.sceneLoaded -= Setup;
        Debug.Log("Scene Manager OnDisable");
    }

    //Checking Position and thus what Sub-Setup Class to call
    private void Setup(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene Manager New Scene Loaded.");
        string TransitionText = TransitionTextGen();
        StartMenu.SetActive(false);
        switch (position)
        {
            case 0:
                Debug.Log("On the main menu.");
                StartMenu.SetActive(true);
                break;
            case 0.5f:
                SetUpTransition(TransitionText, false);
                break;
            case 1:
                Debug.Log("ENTERING THE FIRST LAYER");
                SetUpGame(18, 0, 1);
                break;
            case 1.5f:
                SetUpTransition(TransitionText, false);
                break;
            case 2:
                SetUpGame(18, 2, 3);
                break;
            case 2.5f:
                SetUpTransition(TransitionText, false);
                break;
            case 3:
                SetUpGame(24, 4, 4);
                break;
            case 3.5f:
                SetUpTransition(TransitionText, false);
                break;
            case 4:
                SetUpBoss(4, 4, 4);
                break;
            case 4.5f:
                SetUpTransition("MFW Winner :confetti:", true);
                break;
            case 402:
                SetUpTransition(PoemGenerator(), true);
                break;
            default:
                // DO NOTHING HERE OR ALL HELL WILL BREAK LOSE AND WREAK TERRIBLE VENGENCE UPON AN UNSUSPECTING EARTH
                Debug.Log(":)");
                break;
        }
    }

    //Set Game Music & Generate Maze
    private void SetUpGame(int mazeSize, int INTROindex, int LOOPindex)
    {
        _soundManager.MusicTransition(INTROindex, LOOPindex);

        float cellWidth = 4.0f;

        //Generate floor & ceiling
        GameObject floorPrefab = Resources.Load<GameObject>("Floor");
        GameObject floor = Instantiate(floorPrefab, new Vector3(-(mazeSize - 1) * (cellWidth / 2), 0.0f, (mazeSize - 1) * (cellWidth / 2)), Quaternion.identity);
        floor.name = "Floor";
        floor.transform.localScale = new Vector3(mazeSize * (cellWidth / 10), 1.0f, mazeSize * (cellWidth / 10));

        GameObject ceiling = Instantiate(floorPrefab, new Vector3(-(mazeSize - 1) * (cellWidth / 2), cellWidth, (mazeSize - 1) * (cellWidth / 2)), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        ceiling.transform.localScale = new Vector3(mazeSize * (cellWidth / 10), 1.0f, mazeSize * (cellWidth / 10));
        ceiling.name = "Ceiling";

        //Maze Generation
        GameObject mazeContainer = new GameObject("Maze");
        mazeContainer.AddComponent<MeshFilter>();
        mazeContainer.AddComponent<MeshRenderer>();
        maze = mazeContainer.AddComponent<Maze>();
        GameObject[] enemyPrefabs = {Resources.Load<GameObject>("Zombie")};
        maze.MazeConstructor(mazeSize, mazeSize, Resources.Load<GameObject>("Player"), Resources.Load<GameObject>("ExitDoor"), enemyPrefabs, Resources.Load<Material>("Wall"), cellWidth, 30, 1.0f, 0.2f);
        maze.Ready();
        StartCoroutine(GameObject.FindGameObjectWithTag("RetroCanvas").GetComponent<CameraFade>().FadeUp(
            () => {
                Debug.Log("Fade Completed");
            }
        ));
    }

    //Check if Dead & Give Story Byte
    private void SetUpTransition(string Transition, bool isGameOver)
    {
        try
        {
            StartCoroutine(SwitchOutOfTransition(4, isGameOver));
            TransitionText = GameObject.FindGameObjectsWithTag("TransitionTextOne");
            TransitionText[0].GetComponent<TextMeshProUGUI>().text = Transition;
        }
        catch(Exception e)
        {
            Debug.Log(e);
        }
    }

    private void SetUpBoss(int mazeSize, int INTROindex, int LOOPindex)
    {
        _soundManager.MusicTransition(INTROindex, LOOPindex);

        float cellWidth = 4.0f;

        //Generate floor & ceiling
        GameObject floorPrefab = Resources.Load<GameObject>("Floor");
        GameObject floor = Instantiate(floorPrefab, new Vector3(-(mazeSize - 1) * (cellWidth / 2), 0.0f, (mazeSize - 1) * (cellWidth / 2)), Quaternion.identity);
        floor.name = "Floor";
        floor.transform.localScale = new Vector3(mazeSize * (cellWidth / 10), 1.0f, mazeSize * (cellWidth / 10));

        GameObject ceiling = Instantiate(floorPrefab, new Vector3(-(mazeSize - 1) * (cellWidth / 2), cellWidth, (mazeSize - 1) * (cellWidth / 2)), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        ceiling.transform.localScale = new Vector3(mazeSize * (cellWidth / 10), 1.0f, mazeSize * (cellWidth / 10));
        ceiling.name = "Ceiling";

        //Maze Generation
        GameObject mazeContainer = new GameObject("Maze");
        mazeContainer.AddComponent<MeshFilter>();
        mazeContainer.AddComponent<MeshRenderer>();
        maze = mazeContainer.AddComponent<Maze>();
        GameObject bossPrefab = Resources.Load<GameObject>("BlackKnight"); 
        maze.BossConstructor(mazeSize, mazeSize, Resources.Load<GameObject>("Player"), bossPrefab, Resources.Load<Material>("Wall"), cellWidth);
        maze.Ready();
        StartCoroutine(GameObject.FindGameObjectWithTag("RetroCanvas").GetComponent<CameraFade>().FadeUp(
            () => {
                Debug.Log("Fade Completed");
            }
        ));
    }

    // PUBLIC METHODS ==========================================================================================

    public void NextScene()
    {
        position += 0.5f;

        if (Mathf.Floor(position) != position)
        {
            try
            {
                StartCoroutine(GameObject.FindGameObjectWithTag("RetroCanvas").GetComponent<CameraFade>().FadeToBlack(
                () => {
                    Debug.Log("Fade Completed");
                    SceneManager.LoadScene("Transition_Area");
                }
                ));
            }
            catch(Exception e)
            {
                SceneManager.LoadScene("Transition_Area");
            }
        }
        else
        {
            SceneManager.LoadScene("Game_Scene");
        }
    }

    //This Method Should Not Be Called Internaly!
    public void Death()
    {
        position = 402;

        StartCoroutine(GameObject.FindGameObjectWithTag("RetroCanvas").GetComponent<CameraFade>().FadeToRed(
            () => {
                Debug.Log("Fade Completed");
                _DeathCall();
            }
        ));
    }

    private void _DeathCall()
    {
        SceneManager.LoadScene("Transition_Area");
    }

    // PRIVATE METHODS (CLASS TOOLS) ====================================================================================

    private void BackToTitle()
    {
        position = 0;
        SceneManager.LoadScene("Title_Scene");
        //Any other system reset goes here like clearing inventory or experience.
    }

    private string PoemGenerator()
    {
        //find a haiku? Or summink... idk? This is where that script goes.
        return "You may be wondering how you got here. You died. Congratulations.";
    }

    private string TransitionTextGen()
    {
        string[] quotes =
            {
                "Carinsti, the arch mage rules over the northern kingdom of Terengar.",
                "Remember to dodge and block.",
                "Death is a process of advancement.",
                "Attack with <attack_keys>",
                "Nestled in the Caraglan mountains is a mysterious dungeon constructed by an ancient deity.",
                "Dragons lurk in the swamps in the south.",
                "Powerful necromancers sometimes leave skeletons just for the fun of it.",
                "Peace was never an option.",
                "Death is an escape to this eternal labyrinth.",
                "Resolve in the light of the torch.",
                "Skeletons lurk on the lower floors.",
                "Left to time, mountains erode and gods fall.",
                "The fire warms the soul, and warms the drums of battle.",
                "Death is only a setback, keep moving.",
                "The fire blinds everything, your fears and your mercy.",
                "Remember to pay attention to the height of your attacks.",
                "Blocking is an essential part of not meeting your end.",
                "Dodging an attack can turn the flow of combat.",
                "Peace rests on a rope in the lands of Palengar.",
                "The southern isles of Isma course with jungles and river of acid.",
                "The frigid course of the mountains leave many at the mercy of spirits.",
                "You remember the stark difference in each floor, from the entrance to this mendacious layer."
            };


        return quotes[UnityEngine.Random.Range(0, quotes.Length)];
    }

    private IEnumerator SwitchOutOfTransition(int pauseTime, bool isGameOver)
    {
        Debug.Log("In Transition, Starting " + pauseTime + " second wait. [TIME: " + Time.deltaTime + "]");
        yield return new WaitForSeconds(pauseTime);
        Debug.Log("In Transition, Process Complete. [TIME: " + Time.deltaTime + "]");
        if (isGameOver)
        {
            BackToTitle();
        } else {
            NextScene();
        }
    }
}