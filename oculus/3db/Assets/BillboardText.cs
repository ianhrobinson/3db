using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    static Transform tCam = null;
    void Update()
    {
        if(!tCam)
        {
            if(!Camera.main)
            {
                return;
            }
            tCam = Camera.main.transform;
        }
        transform.LookAt(2*transform.position-tCam.position, Vector3.up);
    }
}
