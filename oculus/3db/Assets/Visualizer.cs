using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System;

public class Visualizer : MonoBehaviour
{

    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject primitivePrefab;
    public GameObject listPrefab;

    float delta = 0.1f;
    List<GameObject> listMems = new List<GameObject>();
    int objectCount = 0;

    // Start is called before the first frame update
    void Start()
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

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown("q")) {
            char[] test = "Hello".ToCharArray();
            char[] test2 = "World".ToCharArray();
            char[] test3 = {'T'};
            List<GameObject> list_test = new List<GameObject>();
            foreach (var mem in test) {
                list_test.Add(CreatePrimitiveObject("",mem.ToString()));
            }
            List<GameObject> list_test2 = new List<GameObject>();
            foreach (var mem in test2) {
                list_test2.Add(CreatePrimitiveObject("",mem.ToString()));
            }
            List<GameObject> list_test3 = new List<GameObject>();
            foreach (var mem in test3) {
                list_test3.Add(CreatePrimitiveObject("blah",mem.ToString()));
            }

            List<GameObject> list_test4 = new List<GameObject>(){CreateListObject("myList", list_test3), CreatePrimitiveObject("test","0"), 
                CreateListObject("", list_test), CreatePrimitiveObject("hello","0"), CreateListObject("", list_test2), CreatePrimitiveObject("","0")};
            GameObject listObj = CreateListObject("My Huge List", list_test4);
            
            /* 
            GameObject listObj = CreateListObject("test", new List<GameObject>(){
                CreateListObject("test", new List<GameObject>(){
                    CreateListObject("test", new List<GameObject>(){
                        CreateListObject("test", new List<GameObject>(){
                            CreateListObject("test", new List<GameObject>(){
                                CreateListObject("test", new List<GameObject>(){
                                    CreatePrimitiveObject("test", "Hi")
                                })
                            })
                        })
                    })
                })
            });
            */

            listObj.transform.position += new Vector3(0, 0, objectCount * 2);
            objectCount += 1;
        }
        
    }
}
