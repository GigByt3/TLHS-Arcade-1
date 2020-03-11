using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch_Script : MonoBehaviour
{
    void OnEnable()
    {
        PlayerMovement.onTurn += TurnTorch;
    }


    void OnDisable()
    {
        PlayerMovement.onTurn -= TurnTorch;
    }

    void TurnTorch()
    {

        //this.transform.Rotate(90, 0, 0); //turns 90


        if (hasTurned)
        {
            this.transform.Rotate(90, 0, 0);
            hasTurned = false;
        }
        else
        {
            this.transform.Rotate(-90, 0, 0);
        }
    }
}

