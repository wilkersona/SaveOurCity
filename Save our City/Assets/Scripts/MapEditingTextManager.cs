using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapEditingTextManager : MonoBehaviour
{
    public GameObject editingMap;
    public GameObject buttonText, text, popupText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (editingMap.GetComponent<MapManagement>().editingClouds){
        	buttonText.GetComponent<Text>().text = "Confirm Reveal";
        	text.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        	popupText.GetComponent<PopupManager>().textBubbles[1] = "Do you want to reveal the selected tiles?";
        }
        else {
        	buttonText.GetComponent<Text>().text = "Edit Clouds";
        	text.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
        	popupText.GetComponent<PopupManager>().textBubbles[1] = "Do you want to select portions of the map to reveal?";
        }
    }
}
