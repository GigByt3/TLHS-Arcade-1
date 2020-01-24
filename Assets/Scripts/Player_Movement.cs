using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float speed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        checkKeys();
    }

    void checkKeys()
    {
        if (Input.GetKey("up"))
        {
            transform.Translate(Vector3.forward * speed);
        }

        if (Input.GetKey("down"))
        {
            transform.Translate(Vector3.back * speed);
        }

        if (Input.GetKey("left"))
        {
            transform.Rotate(0, -5, 0, Space.Self);
        }

        if (Input.GetKey("right"))
        {
            transform.Rotate(0, 5, 0, Space.Self);
        }
    }
}
