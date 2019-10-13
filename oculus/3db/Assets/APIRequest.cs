using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class APIRequest : MonoBehaviour
{
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
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetRequest("http://localhost:8000/api/v1"));
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("r"))
        {
            StartCoroutine(GetRequest("http://localhost:8000/api/v1"));
        }
    }
}
