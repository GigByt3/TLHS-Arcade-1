using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float speed;

    public Camera playerCamera;

    private bool canMoveUp;
    private bool canMoveDown;
    private bool canMoveLeft;
    private bool canMoveRight;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.GetComponent<Room_Seed_Network>().tagOfThisRoom);
    }

    void FixedUpdate()
    {
        checkKeys();
    }

    void checkKeys()
    {

        if (Input.GetKey("up"))
        {
            castRay("forward");

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
            castRay("back");
            
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
                transform.Rotate(0, -90, 0, Space.World);
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
                transform.Rotate(0, 90, 0, Space.World);
                canMoveRight = false;
            }
        }

        if (!Input.GetKey("right"))
        {
            canMoveRight = true;
        }
    }

    void castRay(string direction)
    {
        RaycastHit hit = new RaycastHit();
        switch (direction)
        {
            case "forward":

                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 2.5f, Color.yellow);

                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 2.5f))
                {
                    canMoveUp = false;
                }
                break;

            case "back":
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * 2.5f, Color.yellow);
                
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, 2.5f))
                {
                    canMoveDown = false;
                }
                break;
        }
    }
}
