using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    // FEILDS ==============================================================================================

    public GameSoundManager _soundManager;

    public Maze maze;

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
     * 3.5 - Victory Transtion
     * 402 - Death Sceen
     * Default - Victory Transtion
     */

    // SETUP ==============================================================================================

    //All the Game Management Objects live in DontDestroyOnLoad!
    void Awake()
    {
        Debug.Log("Scene Manager Awake.");
        DontDestroyOnLoad(transform.gameObject);
    }

    //Subscribing Setup
    void OnEnable()
    {
        SceneManager.sceneLoaded += Setup;
        Debug.Log("OnEnable");
    }

    //Unsubscribing Setup
    void OnDisable()
    {
        SceneManager.sceneLoaded -= Setup;
        Debug.Log("OnDisable");
    }

    //Checking Position and thus what Sub-Setup Class to call
    private void Setup(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene Manager New Scene Loaded.");
        switch (position)
        {
            case 0.5f:
                SetUpTransition("Wellcome To ZORK! READY TO BEGIN LEVEL 1 IN 4 SECONDS", false);
                break;
            case 1:
                SetUpGame(18, 0, 1);
                break;
            case 1.5f:
                SetUpTransition("READY TO BEGIN LEVEL 2 IN 4 SECONDS", false);
                break;
            case 2:
                SetUpGame(18, 2, 3);
                break;
            case 2.5f:
                SetUpTransition("READY TO BEGIN LEVEL 3 IN 4 SECONDS", false);
                break;
            case 3:
                SetUpGame(24, 4, 4);
                break;
            case 3.5f:
                SetUpTransition("Victory Screen", true);
                break;
            case 402:
                SetUpTransition(PoemGenerator(), true);
                break;
            default:
                // DO NOTHING HERE OR ALL HELL WILL BREAK LOSE AND WREAK TERRIBLE VENGENCE UPON AN UNSUSPECTING EARTH
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
        floor.transform.localScale = new Vector3(mazeSize * (cellWidth / 10), 1.0f, mazeSize * (cellWidth / 10));

        GameObject ceiling = Instantiate(floorPrefab, new Vector3(-(mazeSize - 1) * (cellWidth / 2), cellWidth, (mazeSize - 1) * (cellWidth / 2)), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        ceiling.transform.localScale = new Vector3(mazeSize * (cellWidth / 10), 1.0f, mazeSize * (cellWidth / 10));

        //Maze Generation
        GameObject mazeContainer = new GameObject("Maze");
        mazeContainer.AddComponent<MeshFilter>();
        mazeContainer.AddComponent<MeshRenderer>();
        maze = mazeContainer.AddComponent<Maze>();
        GameObject[] enemyPrefabs = {Resources.Load<GameObject>("Zombie")};
        maze.MazeConstructor(mazeSize, mazeSize, Resources.Load<GameObject>("Player"), enemyPrefabs, Resources.Load<Material>("Wall"), cellWidth, 5, 1.0f);
        maze.Ready();
    }

    //Check if Dead & Give Story Byte
    private void SetUpTransition(string Transition, bool isGameOver)
    {
        StartCoroutine(SwitchOutOfTransition(4, isGameOver));
        TransitionText = GameObject.FindGameObjectsWithTag("TransitionTextOne");
        TransitionText[0].GetComponent<UnityEngine.UI.Text>().text = Transition;
    }

    // PUBLIC METHODS ==========================================================================================

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

    //This Method Should Not Be Called Internaly!
    public void Death()
    {
        position = 402;
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

    private IEnumerator SwitchOutOfTransition(int pauseTime, bool isGameOver)
    {
        Debug.Log("In Transition, Starting " + pauseTime + " second wait. [TIME: " + Time.deltaTime + "]");
        yield return new WaitForSeconds(pauseTime);
        Debug.Log("In Transition, Proscess Complete. [TIME: " + Time.deltaTime + "]");
        if (isGameOver)
        {
            BackToTitle();
        } else {
            NextScene();
        }
    }
}