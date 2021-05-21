using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextUpdate : MonoBehaviour
{
    public string[] quotes;
    public TextMeshProUGUI displayText;
    private int randomIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        randomIndex = Random.Range(0, quotes.Length);
    }

    public string UpdateText()
    {
        
//        Debug.Log(randomIndex);
        return quotes[randomIndex];
    }
}
