using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System;
using Newtonsoft.Json;

public class CodeDisplayer : MonoBehaviour
{
    public Material LineHighlighted;
    public Material LineNormal;

    public Code runningCode = null;

    int lastRunLine = 0;
    public int current_line = 1;

    public GameObject LinePrefab;
    List<GameObject> lines = new List<GameObject>();
    const float LINE_HEIGHT = -.6f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetRequest("http://localhost:8000/api/initialize/"));
        
    }

    // Update is called once per frame
    void Update()
    {
        if(lastRunLine != current_line && lines.Count > 0)
        {
            Debug.Log("Line Changed: " + current_line);
            Debug.Log(lines.Count);
            lines[current_line-1].GetComponent<Renderer>().material = LineHighlighted;
            if(lastRunLine > 0)
                lines[lastRunLine-1].GetComponent<Renderer>().material = LineNormal;
            lastRunLine = current_line;
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
            string jsonReturn = uwr.downloadHandler.text;
            runningCode = JsonConvert.DeserializeObject<Code>(jsonReturn);

            for(int i = 0; i < runningCode.code.Count; ++i)
            {
                GameObject line = Instantiate(LinePrefab, this.gameObject.transform.position + new Vector3(0,i*LINE_HEIGHT,0), Quaternion.identity);
                line.transform.Find("TextOverlay").gameObject.GetComponent<TextMeshPro>().SetText(runningCode.code[i]);
                line.transform.Find("LineNumber").gameObject.GetComponent<TextMeshPro>().SetText((i+1).ToString());
                line.transform.Find("Breakpoint").gameObject.GetComponent<BreakpointHandler>().lineNumber = i + 1;
                line.transform.parent = this.gameObject.transform;
                lines.Add(line);
            }   
        }
    }

    void HandleClick()
    {
        
    }

    void HandleMouseOn()
    {
        Debug.Log("Mouse On");
    }

    [System.Serializable]
    public class Code
    {
        public List<string> code;
    }

}
