using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//script to manage the player's view of things
//yeah its pretty tame
public class PlayerCanvasManager : MonoBehaviour
{
    public GameObject bars, map;
    public GameObject actionTracker;
    public GameObject variables;
    public GameObject mapLabels;
    public bool trackActions = false;
    public GameObject im1, im2, tx1, tx2;
    string[] subphaseLetters = {"A", "B"};

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (trackActions){
        	bars.GetComponent<RectTransform>().localScale = new Vector3(0.75f, 0.75f, 1);
        	bars.GetComponent<RectTransform>().anchoredPosition = new Vector3(-120, 45, 0);
        	mapLabels.GetComponent<RectTransform>().localScale = new Vector3(1.925f*0.75f, 1.925f*0.75f, 1);
        	mapLabels.GetComponent<RectTransform>().anchoredPosition = new Vector3(-390, 215, 0);
        	map.GetComponent<Transform>().localScale = new Vector3(0.75f, 0.75f, 1);
        	map.GetComponent<Transform>().position = new Vector3(-3f, 1002.25f, 0);
        	im1.GetComponent<RectTransform>().anchoredPosition = new Vector3(290, 200, 0);
        	im2.GetComponent<RectTransform>().anchoredPosition = new Vector3(290, 170, 0);
        	tx1.GetComponent<RectTransform>().anchoredPosition = new Vector3(290, 200, 0);
        	tx2.GetComponent<RectTransform>().anchoredPosition = new Vector3(290, 170, 0);
    		tx1.GetComponent<Text>().text = "Phase " + (variables.GetComponent<MainVariables>().phase+1);
    		tx2.GetComponent<Text>().text = "Subphase " + subphaseLetters[variables.GetComponent<MainVariables>().subphase%2];
        	actionTracker.SetActive(true);
        }
        else {
        	bars.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        	bars.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -60, 0);
        	mapLabels.GetComponent<RectTransform>().localScale = new Vector3(1.925f, 1.95f, 1);
        	mapLabels.GetComponent<RectTransform>().anchoredPosition = new Vector3(-385, 195, 0);
        	map.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
        	map.GetComponent<Transform>().position = new Vector3(-0.5f, 1000.5f, 0);
        	im1.GetComponent<RectTransform>().anchoredPosition = new Vector3(140, 200, 0);
        	im2.GetComponent<RectTransform>().anchoredPosition = new Vector3(290, 200, 0);
        	tx1.GetComponent<RectTransform>().anchoredPosition = new Vector3(140, 200, 0);
        	tx2.GetComponent<RectTransform>().anchoredPosition = new Vector3(290, 200, 0);
    		tx1.GetComponent<Text>().text = "Phase " + (variables.GetComponent<MainVariables>().phase+1);
    		tx2.GetComponent<Text>().text = "Subphase " + subphaseLetters[variables.GetComponent<MainVariables>().subphase%2];
        	actionTracker.SetActive(false);
        }
    }
}
