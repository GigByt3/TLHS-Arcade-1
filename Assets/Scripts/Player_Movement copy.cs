using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    protected Rigidbody rg;
    public float mSpeed;
    private float horizontalInput, verticalInput;

    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody>();
        mSpeed = 10f; 
    }

    // Update is called once per frame
    void Update()
    {
        //The verticalInput should probably be in an if statement because of collision checking. 

        horizontalInput = Input.GetAxis("Horizontal");
        Camera.main.transform.Rotate(new Vector3(90, 0, 0));

        verticalInput = Input.GetAxis("Vertical");
        //check direction
        Camera.main.transform.Translate(Vector3.forward * Time.deltaTime * mSpeed);
    }
}
