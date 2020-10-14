using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CompleteTheDifficultLevel());
    }

    private IEnumerator CompleteTheDifficultLevel()
    {
        Debug.Log("Yay! I'm playing a game level!");
        yield return new WaitForSeconds(2);
        Debug.Log("I won the level!");
        GameObject.FindGameObjectsWithTag("GameManager")[0].GetComponent<sceneManager>().NextScene();
    }
}
