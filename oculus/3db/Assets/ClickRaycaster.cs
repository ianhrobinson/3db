using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickRaycaster : MonoBehaviour
{
    public GameObject crosshairs;
    GameObject lastHitGameObject;

    // Start is called before the first frame update
    void Start()
    {
        lastHitGameObject = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        	
        if(Camera.main != null) //Make sure you have a camera
        {
            //Vector3 screenMiddle = new Vector3(Screen.width / 2, Screen.height / 2, 0); //Set raycast position to center of the screen
            //Ray ray = Camera.main.ScreenPointToRay(screenMiddle); //Create the ray vector
            RaycastHit hit; //Variable for storing what the raycast hit
                    
            if (Physics.Raycast(crosshairs.transform.position, crosshairs.transform.TransformDirection(Vector3.forward), out hit, 100.0f)) //Do the raycast, fire the ray 2meters
            {	
                GameObject hitGameObject = hit.collider.gameObject; //Grap the gameobject that was hit
                if(Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space)) //Mouse button 0 is pressed
                {   
                    hitGameObject.SendMessage("HandleClick",null,SendMessageOptions.DontRequireReceiver);
                }
                if(hitGameObject != lastHitGameObject)
                {
                    Debug.Log("Raycast Changed");
                    hitGameObject.SendMessage("HandleMouseOver",null,SendMessageOptions.DontRequireReceiver);
                    lastHitGameObject.SendMessage("HandleMouseOff", null, SendMessageOptions.DontRequireReceiver);
                    lastHitGameObject = hitGameObject;
                }
            }
        }
	}
}
