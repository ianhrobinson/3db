using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ContinueButton : MonoBehaviour
{
    public Material Material1;
    public Material Material2;

    public Renderer MaterialRender;

    // Start is called before the first frame update
    void Start()
    {
        MaterialRender.material = Material1;
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
            text = uwr.downloadHandler.text;
            gameObject.transform.parent.parent.gameObject.SendMessage("UpdateInfo",text);
        }
    }

    void HandleClick()
    {
        StartCoroutine(GetRequest("http://localhost:8000/api/execution/continue/"));
    }

    void HandleMouseOver()
    {
        MaterialRender.material = Material2;
        Debug.Log("Material 2");
    }

    void HandleMouseOff()
    {
        MaterialRender.material = Material1;
        Debug.Log("Material 1");
    }
}
