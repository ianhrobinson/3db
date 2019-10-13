using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_simple : MonoBehaviour
{
    public int speed = 500;
    public float delta = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey("Button.DpadUp")) {
            transform.position += new Vector3(delta, 0, delta);
        } else if (Input.GetKey("Button.DpadDown")) {
            transform.position -= new Vector3(delta, 0, delta);
        }

        if (Input.GetKey("Button.DpadLeft")) {
            // transform.RotateAround (new Vector3(0, 0, 0), Vector3.up, -delta);
            transform.position += new Vector3(-delta, 0, delta);
        } else if (Input.GetKey("Button.DpadRight")) {
            // transform.RotateAround (new Vector3(0, 0, 0), Vector3.up, delta);
            transform.position -= new Vector3(-delta, 0, delta);
        }

        float moveHorizontal = Input.GetAxis("Oculus_GearVR_DpadX");
        float moveVertical = Input.GetAxis("Oculus_GearVR_DpadY");

        /*Vector3 position = transform.position;
        position.x += moveHorizontal * speed * Time.deltaTime;
        position.z += moveVertical * speed * Time.deltaTime;
        transform.position = position;*/
    }
}
