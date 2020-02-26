using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Seed_Network : MonoBehaviour
{
    public ArrayList Network = new ArrayList();
    private string[] RoomTypes = { "Cult_Den", "Spider_Lair", "Crypt_Room" };
    private string tagOfThisRoom;
    // Start is called before the first frame update
    void Start()
    {
        findNetwork();
    }

    void findNetwork()
    {
        Vector3 origin = Vector3.zero;
        Vector3 findNetwork = new Vector3(0, 0, -6);
        RaycastHit hit;
        if (Physics.Raycast(origin, findNetwork, out hit))
        {
            if (hit.collider.gameObject.tag == "Room-Seed") { LinkUp(hit.collider); }
        }
        findNetwork = new Vector3(6, 0, -6);
        if (Physics.Raycast(origin, findNetwork, out hit))
        {
            if (hit.collider.gameObject.tag == "Room-Seed") { LinkUp(hit.collider); }
            if (hit.collider.gameObject.tag == "Wall") { FinishRoom();  }
        }
    }

    void LinkUp(Collider hit)
    {
        hit.gameObject.GetComponent<Room_Seed_Network>().Network.Add(this.gameObject);
        this.Network = hit.gameObject.GetComponent<Room_Seed_Network>().Network;
        foreach (GameObject link in Network)
        {
            link.GetComponent<Room_Seed_Network>().Network = this.Network;
        }
        Debug.Log("--------------------------------LINKED UP!");
    }

    void FinishRoom()
    {
        string roomTags = RoomTypes[Random.Range(0, 2)];
        foreach (GameObject link in Network)
        {
            link.gameObject.GetComponent<Room_Seed_Network>().tagOfThisRoom = roomTags;
        }
    }
}
