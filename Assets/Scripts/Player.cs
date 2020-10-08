using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : GridObject
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkKeys();
    }

    void checkKeys()
    {
        if (Input.GetKeyDown("up")) move(1);
        if (Input.GetKeyDown("down")) move(-1);
        if (Input.GetKeyDown("left")) rotate("left");
        if (Input.GetKeyDown("right")) rotate("right");
    }
}
