using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float speed;

    public bool canMoveUp;
    public bool canMoveRight;
    public bool canMoveDown;
    public bool canMoveLeft;
    
    void FixedUpdate()
    {
        checkKeys();
    }

    void checkKeys()
    {

        if (Input.GetKey("up"))
        {
            if (canMoveUp)
            {
                transform.Translate(Vector3.forward * speed);
                canMoveUp = false;
            }
        }

        if (!Input.GetKey("up"))
        {
            canMoveUp = true;
        }

        if (Input.GetKey("down"))
        {
            if (canMoveDown)
            {
                transform.Translate(Vector3.back * speed);
                canMoveDown = false;
            }
        }

        if (!Input.GetKey("down"))
        {
            canMoveDown = true;
        }

        if (Input.GetKey("left"))
        {
            if (canMoveLeft)
            {
                transform.Rotate(0, -90, 0, Space.Self);
                canMoveLeft = false;
            }
        }

        if (!Input.GetKey("left"))
        {
            canMoveLeft = true;
        }

        if (Input.GetKey("right"))
        {
            if (canMoveRight)
            {
                transform.Rotate(0, 90, 0, Space.Self);
                canMoveRight = false;
            }
        }

        if (!Input.GetKey("right"))
        {
            canMoveRight = true;
        }
    }
}
