using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Generator : MonoBehaviour
{
    public GameObject roomSeed, seedContainer;
    // Start is called before the first frame update
    void Start()
    {
        for (float x = -22.5f; x < 25; x += 5)
        {
            for (float y = -22.5f; y < 25; y += 5)
            {
                Debug.Log("Checking " + x + " , " + y + "...");
                this.transform.position = new Vector3(x, 1, y);
                findRooms();
            }
        }
    }

    void findRooms()
    {
        RaycastHit hit = new RaycastHit();
        if (!Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(-3, 0, -3)), out hit, 3.5f) ||
            !Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(3, 0, -3)), out hit, 3.5f) ||
            !Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(-3, 0, 3)), out hit, 3.5f) ||
            !Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(3, 0, 3)), out hit, 3.5f))
        {
            Instantiate(roomSeed, this.transform.position, this.transform.rotation, seedContainer.transform);
            Debug.Log("Left a Room-Seed");
        }
    }
}
