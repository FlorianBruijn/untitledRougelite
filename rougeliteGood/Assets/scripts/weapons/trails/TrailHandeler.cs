using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailHandeler : MonoBehaviour
{
    bool destroy;
    public Vector3 moveLocation;
    TrailRenderer tr;
    void Start() 
    {
        tr = GetComponent<TrailRenderer>();
    }
    void Update()
    {
        if (!destroy)
        {
            transform.position = moveLocation;
        }
        else if (!tr.autodestruct)
        {
            tr.autodestruct = true;
        }
    }
}
