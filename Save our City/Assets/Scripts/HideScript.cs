using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideScript : MonoBehaviour
{
    public GameObject myself, variables;
    public int displayTab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (variables.GetComponent<MainVariables>().whichTab == displayTab){
    		Vector3 temp = myself.GetComponent<RectTransform>().anchoredPosition;
    		myself.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    	}
    	else {
    		Vector3 temp = myself.GetComponent<RectTransform>().anchoredPosition;
    		myself.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
    	}
    }
}
