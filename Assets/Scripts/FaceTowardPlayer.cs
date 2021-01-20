using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceTowardPlayer : MonoBehaviour
{
    Player player;
    
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {
        switch (player.gridCoords.z)
        {
            case 0:
                transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                break;
            case 1:
                transform.rotation = Quaternion.Euler(0.0f, 270.0f, 0.0f);
                break;
            case 2:
                transform.rotation = Quaternion.identity;
                break;
            case 3:
                transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
                break;
            default:
                transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                break;
        }
    }
}
