using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventCallthrough : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    public void StrikeConnect()
    {
        Debug.Log("Callthrough Connection");
        player.GetComponent<ParentCombatController>().StrikeConnect();
    }

    // Update is called once per frame
    public void Complete(string peram)
    {
        player.GetComponent<ParentCombatController>().Complete(peram);
    }

    

}
