using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventCallthrough : MonoBehaviour
{
    public GameObject player;

    public void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }
    // Start is called before the first frame update
    public void StrikeConnect()
    {
        player.GetComponent<ParentCombatController>().StrikeConnect();
    }

    // Update is called once per frame
    public void Complete(string peram)
    {
        player.GetComponent<ParentCombatController>().Complete(peram);
    }

    

}
