using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNormalsWork : MonoBehaviour
{
    MeshFilter filter;
    Mesh fuck;

    // Start is called before the first frame update
    void Start()
    {
        filter = GetComponent<MeshFilter>();
        fuck = filter.mesh;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < fuck.normals.Length; i++)
        {
            Debug.DrawRay(fuck.vertices[i], fuck.normals[i], (i % 8 < 4) ? Color.red : Color.blue, Time.deltaTime);
        }
    }
}
