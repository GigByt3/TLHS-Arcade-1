using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    // FIELDS ==============================================================================================

    public GameSoundManager _soundManager;

    public Maze maze;

    private GameObject StartMenu;

    private GameObject[] TransitionText;
    private GameObject[] DeathText;

    private Maze.PlayerData transitionalPlayerData;

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

        //Set starting player data
        transitionalPlayerData = new Maze.PlayerData();
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
        StartMenu.SetActive(false);
        if (SceneManager.GetActiveScene().name == "Credits" || SceneManager.GetActiveScene().name == "Controls") { return; }
        string TransitionText = TransitionTextGen();
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
                transitionalPlayerData = new Maze.PlayerData();
                SetUpGame(18, 0, 1);
                break;
            case 1.5f:
                SetUpTransition(TransitionText, false);
                break;
            case 2:
                SetUpGame(20, 2, 3);
                break;
            case 2.5f:
                SetUpTransition(TransitionText, false);
                break;
            case 3:
                SetUpGame(22, 4, 4);
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
                SetUpDeath(PoemGenerator(), true);
                break;
            default:
                // DO NOTHING HERE OR ALL HELL WILL BREAK LOSE AND WREAK TERRIBLE VENGENCE UPON AN UNSUSPECTING EARTH
                // I HAVE ADDED THIS COMMENT. YOUR SILLY FALLIBLE CODE STRUCTURE SHALL FALL TO MY ENORMOUS POWER OF DEATH AND DESTRUCTION.
                // LET THE DEMONS FLY, LET THE CHILDREN DIE, EARTH WILL FALL.
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
        float difficulty = 1.0f;
        switch(mazeSize)
        {
            case 18:
                difficulty = 3.0f;
                break;
            case 20:
                difficulty = 4.0f;
                break;
            case 22:
                difficulty = 5.0f;
                break;
            default:
                difficulty = 7.0f;
                break;
        }
        maze.MazeConstructor(mazeSize, mazeSize, Resources.Load<GameObject>("Player"), transitionalPlayerData, Resources.Load<GameObject>("ExitDoor"), enemyPrefabs, Resources.Load<Material>("Wall"), cellWidth, 30, difficulty, 0.2f);
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

    //Check if Dead & Give Story Byte
    private void SetUpDeath(string Death, bool isGameOver)
    {
        try
        {
            StartCoroutine(SwitchOutOfTransition(4, isGameOver));
            DeathText = GameObject.FindGameObjectsWithTag("DeathText");
            DeathText[0].GetComponent<TextMeshProUGUI>().text = Death;
        }
        catch (Exception e)
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
        maze.BossConstructor(mazeSize, mazeSize, Resources.Load<GameObject>("Player"), transitionalPlayerData, bossPrefab, Resources.Load<Material>("Wall"), cellWidth);
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
        if (position != 0 && Mathf.Approximately(position, Mathf.Floor(position))) transitionalPlayerData = maze.player.getPlayerData();
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
        transitionalPlayerData = maze.player.getPlayerData();
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
        SceneManager.LoadScene("Death_Scene");
    }

    // PRIVATE METHODS (CLASS TOOLS) ====================================================================================

    private void BackToTitle()
    {
        position = 0;
        SceneManager.LoadScene("Title_Scene");
        //Any other system reset goes here like clearing inventory or experience.
    }

    //Death Poems
    private string PoemGenerator()
    {
        string[] selectedPoem;
        Vector2Int range = new Vector2Int();
        Item shield = transitionalPlayerData.inventory.leftHand;
        Item sword = transitionalPlayerData.inventory.rightHand;

        string[] zombiePoems =
        {
            "Fallen, an undead crafted thy tomb\nSword, broken, a gift, and your doom",
            "Death, at the hands of those you fear\ncrawling zombies, thy blade to clear",
            "Your glorious sword, was not enough\nThe zombies slay, deep in the rough",
            "Your rusty shield would not protect\nThe zombies slay, they won’t respect",
            "Thy ornate shield has broken through\nThe zombie horde, too great for you",
            "Holy protector could not stay their hands\nZombies bloodied fingers, scour the lands"
        };

        string[] skeletonPoems =
        {
            "Fallen, an undead crafted thy tomb\nSword, broken, a gift, and your doom",
            "Death, at the hands of those you fear\nThe crawling Skeletons, thy blade to clear",
            "Your glorious sword, was not enough\nThe Skeletons slay, deep in the rough",
            "Your rusty shield would not protect\nThe Skeletons slay, they won’t respect",
            "Thy ornate shield has broken through\nThe skeleton horde, too great for you",
            "Holy protector could not stay their hands\nskeleton’s bloodied swords, scour the lands",
        };

        //Demons do not exist and thus this string array is currently useless.
        string[] demonPoems =
        {
            "Fought thus the last ounce, the demon ahead,\nstrength alone cannot slay, rusty sword fished from the dead",
            "Feet, disrupt the ground above, the demon gravels\nside pierced, blade drops, ending thy travels",
            "Cheek against the stone, a thud, sprayed blood\nWarm, senseless, symbol holy thrown, sky with blackness flood",
            "Entrenched stone, a fiend over head,\nA simple defense your flaw instead",
            "Callous walls, staring with that malignant beast\nShielding only body nay mind or soul, deceased",
            "Souls lost, a perpetual abyss, but ended by the fiend\nEven the adorned bulwark unable to protect a dream",
        };

        //Phantoms do not exist and thus this string array is currently useless.
        string[] phantomPoems =
        {
            "Foolish courage, facing odds without metal to match\nDreaming soldiers called limbs detached",
            "Soon in bloody battle did ensue\nPhantom limbs ripped the fair sword subdued",
            "Phantasmal limbs, pierce thou body\nHoly relic fallen to the floor, disembodied",
            "Entrenched stone, a fiend over head,\nA simple defense your flaw instead",
            "Callous walls, staring with that malignant beast\nShielding only body nay mind or soul, deceased",
            "Relic Light fading in the stretching abyss\nProtecting naught against the flailing limbs, a hiss",
        };

        switch (transitionalPlayerData.lastCombatant)
        {
            case Enemy.EnemyType.ZOMBIE:
                selectedPoem = zombiePoems;
                break;

            case Enemy.EnemyType.SKELETON:
                selectedPoem = skeletonPoems;
                break;

            case Enemy.EnemyType.BLACKKNIGHT:
                selectedPoem = demonPoems;
                break;
            default:
                selectedPoem = phantomPoems;
                Debug.Log("Enemy type is null; no random poem returned.");
                break;
        }

        if (sword is RustedSword)
        {
            range.x = 0;
        }            
        else if (sword is SteelSword)
        {
            range.x = 1;
        }
        else if (sword is HolySword)
        {
            range.x = 2;
        }

        if (shield is WoodenShield)
        {
            range.y = 3;
        }
        else if (shield is SteelShield)
        {
            range.y = 4;
        }
        else if (shield is HolyShield)
        {
            range.y = 5;
        }

        if (UnityEngine.Random.Range(0, 2) == 1)
        {
            return selectedPoem[range.x];
        }
        else
        {
            return selectedPoem[range.y];
        }
    } //0-2 swords: rusted, steel, holy, 3-5 shields: wooden, steel, holy
   

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