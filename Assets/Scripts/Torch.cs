using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Torch : MonoBehaviour
{
    private Quaternion mBaseRotation;
    private GameObject torchStem;

    public Torch(Quaternion baseRotation)
    {
        mBaseRotation = baseRotation;
        torchStem = this.transform.GetChild(0).gameObject;
    }

    private void Start()
    {
        this.transform.rotation = mBaseRotation;

        
    }

    void Update()
    {
        if (this.transform.rotation == mBaseRotation)
        {
            //set to side torch
        } else
        {
            //set to front torch
        }
    }
}
