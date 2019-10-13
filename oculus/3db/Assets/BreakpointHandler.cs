using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BreakpointHandler : MonoBehaviour
{

    bool isSet = false;

    public int lineNumber = 0; //Filled in by CodeDisplayer

    public Renderer MaterialRender;

    public Material BreakNormal;
    public Material BreakHighlight;
    public Material BreakActive;

    // Start is called before the first frame update
    void Start()
    {
        MaterialRender.material = BreakNormal;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GetRequest(string uri)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(uri);
        yield return uwr.SendWebRequest();

        if(uwr.isNetworkError)
        {
            Debug.Log("Error: " + uwr.error);
        }
        else
        {
            string text;
            Debug.Log("Received: " + uwr.downloadHandler.text);
        }
    }

    void HandleClick()
    {
        if(isSet) 
        {   //Remove breakpoint
            StartCoroutine(GetRequest("http://localhost:8000/api/execution/remove_breakpoint/"+lineNumber));
            isSet = false;
            MaterialRender.material = BreakNormal;
        }
        else
        {   //Set breakpoint
            StartCoroutine(GetRequest("http://localhost:8000/api/execution/set_breakpoint/"+lineNumber));
            isSet = true;
            MaterialRender.material = BreakActive;
        }
    }

    void HandleMouseOver()
    {
        MaterialRender.material = BreakHighlight;
    }

    void HandleMouseOff()
    {
        if(isSet)
        {
            MaterialRender.material = BreakActive;
        }
        else {
            MaterialRender.material = BreakNormal;
        }
    }
}

