﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : GridObject
{
    private float width, height;

    private GameObject doorWall;
    
    public void ExitDoorConstructor(float _width, float _height)
    {
        width = _width;
        height = _height;
    }
    
    void Start()
    {
        doorWall = transform.GetChild(0).gameObject;
        doorWall.transform.localScale = new Vector3(width / 10, height / 10, width / 10);
        doorWall.transform.localPosition = new Vector3(0, 0, width / 2);
    }
}
