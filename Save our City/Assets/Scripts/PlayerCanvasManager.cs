using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvasManager : MonoBehaviour
{
    public GameObject bars, map;
    public GameObject actionTracker;
    public GameObject variables;
    public bool trackActions = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (trackActions){
        	bars.GetComponent<RectTransform>().localScale = new Vector3(0.75f, 0.75f, 1);
        	bars.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 50, 0);
        	map.GetComponent<Transform>().localScale = new Vector3(0.75f, 0.75f, 1);
        	map.GetComponent<Transform>().position = new Vector3(-0.5f, 1002.5f, 0);
        	actionTracker.SetActive(true);
        }
        else {
        	bars.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        	bars.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -28, 0);
        	map.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
        	map.GetComponent<Transform>().position = new Vector3(-0.5f, 1000.5f, 0);
        	actionTracker.SetActive(false);
        }
    }
}
