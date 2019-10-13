using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TMPro;

public class DataManager : MonoBehaviour
{
    
    public GameObject primitivePrefab;
    public GameObject listPrefab;
    int yOffset = 0;

    List<GameObject> cur_stack = new List<GameObject>();



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    GameObject CreatePrimitiveObject(string keyIn, string valueIn) {
        GameObject primitiveObj = Instantiate(primitivePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        primitiveObj.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().SetText(keyIn);
        primitiveObj.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().SetText(valueIn);
        return primitiveObj;
    }
    GameObject CreateListObject(string keyIn, List<GameObject> listIn) {
        GameObject listObj = Instantiate(listPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        float len = listIn.Count;
        float maxTextOffset = 0.0f;
        // First pass through to determine count
        for (int i = 0; i < listIn.Count; ++i) {
            if (listIn[i].name.Contains("Primitive")) {
                float thisOffset = listIn[i].transform.GetChild(0).gameObject.transform.position.y;
                if (thisOffset > maxTextOffset) {
                    maxTextOffset = thisOffset;
                }
            } else if (listIn[i].name.Contains("List")) {
                len += listIn[i].transform.localScale.x - 1.0f;
                float thisOffset = listIn[i].transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.position.y;
                if (thisOffset > maxTextOffset) {
                    maxTextOffset = thisOffset;
                }
            }
        }
        listObj.transform.localScale = new Vector3(len, 1, 1);
        listObj.transform.GetChild(0).gameObject.transform.localScale = new Vector3(1.0f/len, 1.0f, 1.0f);
        listObj.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.position = new Vector3(0.0f, maxTextOffset + 0.3f, 0.0f);
        listObj.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().SetText(keyIn);
        // Second pass through to properly scale everything
        float off = 0;
        for (int i = 0; i < listIn.Count; ++i) {
            GameObject mem = listIn[i];
            float this_len = mem.transform.localScale.x;
            Debug.Log(this_len);
            mem.transform.SetParent(listObj.transform);
            if (mem.name.Contains("Primitive")) {
                mem.transform.localScale = new Vector3(0.8f / len, 0.8f, 0.8f);
                mem.transform.position = new Vector3(-(len - 1)/2 + off, 0, 0);
            } else if (mem.name.Contains("List")) {
                mem.transform.localScale = new Vector3(0.8f * this_len / len, 0.8f, 0.8f);
                mem.transform.position = new Vector3(-(len - 1)/2 + off + (this_len - 1)/2, 0, 0);
            }
            off += this_len;
            Debug.Log(this_len);
        }
        return listObj;
    }

    GameObject RecursivelyParseObject(string name, JObject obj) {
        List<GameObject> our_mems = new List<GameObject>();
        foreach(var x in obj) {
            string key = x.Key;
            JToken value = x.Value;
            if (value.Type == JTokenType.Object) {
                // Object / Dictionary
                our_mems.Add(RecursivelyParseObject(key, (JObject) value));
            } else if (value.Type == JTokenType.Array) {
                // Array
                our_mems.Add(RecursivelyParseArray(key, (JArray) value));
            } else {
                // Primitive
                our_mems.Add(CreatePrimitiveObject(key, value.ToObject<string>()));
            }
        }
        return CreateListObject(name, our_mems);
    }
     GameObject RecursivelyParseArray(string name, JArray obj) {
        List<GameObject> our_mems = new List<GameObject>();
        foreach(JToken x in obj) {
            if (x.Type == JTokenType.Object) {
                // Object / Dictionary
                our_mems.Add(RecursivelyParseObject("", (JObject) x));
            } else if (x.Type == JTokenType.Array) {
                // Array
                our_mems.Add(RecursivelyParseArray("", (JArray) x));
            } else {
                // Primitive
                our_mems.Add(CreatePrimitiveObject("", x.ToObject<string>()));
            }
        }
        return CreateListObject(name, our_mems);
    }
    void UpdateInfo(string text)
    {
        foreach (var mem in cur_stack) {
            Destroy(mem);
        }
        cur_stack.Clear();
        int zOffset = 0;
        Debug.Log(text);
        JObject jsonData = JObject.Parse(text); 
        Debug.Log(jsonData);
        transform.GetChild(1).GetComponent<CodeDisplayer>().current_line = jsonData["current_line"].ToObject<int>();
        transform.GetChild(2).GetChild(0).GetComponent<TextMeshPro>().SetText("Current stack frame: " + jsonData["stack"].ToObject<string>() + "\n" + jsonData["variables"].ToObject<string>());
        string variables_string = jsonData["variables"].ToObject<string>();
        if (variables_string.Contains(">")) {
            variables_string = "{" + variables_string.Split('>')[1].Substring(1);
        } else {
            variables_string = "{'" + variables_string.Split('{')[2].Substring(1);
        }
        Debug.Log(variables_string);
        JObject variables = JObject.Parse(variables_string);
        foreach(var x in variables) {
            string name = x.Key;
            JToken value = x.Value;
            if (value.Type == JTokenType.Object) {
                // Object / Dictionary
                GameObject newObj  = RecursivelyParseObject(name, (JObject) value);
                newObj.transform.position += new Vector3(0, -5, -zOffset * 2);
                cur_stack.Add(newObj);
            } else if (value.Type == JTokenType.Array) {
                // Array
                GameObject newArr  = RecursivelyParseArray(name, (JArray) value);
                newArr.transform.position += new Vector3(0, -5, -zOffset * 2);
                cur_stack.Add(newArr);
            } else {
                // Primitive
                GameObject newPrim = CreatePrimitiveObject(name, value.ToObject<string>());
                newPrim.transform.position += new Vector3(0, -5, -zOffset * 2);
                cur_stack.Add(newPrim);
            }
            zOffset += 1;
        }
        yOffset += 1;
    }
}
