using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//A super helpful script that hides an object if it's not the correc tab
//its connected to HiderPrefab; the code is self-explanitory
public class HideScript : MonoBehaviour
{
    public GameObject myself, variables;
    public int displayTab;
    public float scaleTo = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (variables.GetComponent<MainVariables>().whichTab == displayTab){
    		Vector3 temp = myself.GetComponent<RectTransform>().anchoredPosition;
    		myself.GetComponent<RectTransform>().localScale = new Vector3(scaleTo, scaleTo, 1);
    	}
    	else {
    		Vector3 temp = myself.GetComponent<RectTransform>().anchoredPosition;
    		myself.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
    	}
    }
}
