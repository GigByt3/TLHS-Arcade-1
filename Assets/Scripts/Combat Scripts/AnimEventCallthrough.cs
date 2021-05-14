using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventCallthrough : MonoBehaviour
{
    public GameObject player;
    
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Start is called before the first frame update
    public void StrikeConnectCallthrough()
    {
        Debug.Log("At AnimEvent StrikeConnectCallthrough, hitter.damage: " + player.GetComponent<PlayerCombatController>().damage);
        player.GetComponent<PlayerCombatController>().StrikeConnect();
    }

    // Update is called once per frame
    public void CompleteCallthrough(string peram)
    {
        player.GetComponent<ParentCombatController>().Complete(peram);
    }

    

}
