using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class CodeDisplayer : MonoBehaviour
{
    string text;
    int hasNewText;

    TextMeshPro displaytext;

    // Start is called before the first frame update
    void Start()
    {
        displaytext = GetComponentInChildren<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hasNewText == 1)
        {
            displaytext.SetText(text);
        }   
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
            Debug.Log("Received: " + uwr.downloadHandler.text);
            text = uwr.downloadHandler.text;
            hasNewText = 1;
        }
    }

    void HandleClick()
    {
        StartCoroutine(GetRequest("http://localhost:8000/api/v1"));
    }

    void HandleMouseOn()
    {
        Debug.Log("Mouse On");
    }
}
