using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//manages the graphics on the action table of a single action
//mainly for color and text and stuff
public class SingleActionManagement : MonoBehaviour
{
    public GameObject buttonText, overview, description, enhancements, limitations;
    public GameObject arrow1, arrow2, arrow3;
    public GameObject parent, actionPopup;
    public int buttonNum;

    // Start is called before the first frame update
    void Start()
    {
        Display();
    }

    // Update is called once per frame
    void Update()
    {
        Display();
    }

    public void Display() {
    	int which = parent.GetComponent<ActionDescriptions>().playerDropdown.GetComponent<Dropdown>().value;
    	buttonText.GetComponent<Text>().text = "" + (which+1) + "." + (buttonNum+1) + ": " + parent.GetComponent<ActionDescriptions>().Names[which][buttonNum];
    	overview.GetComponent<Text>().text = formOverview(which);
    	bool coloredRed = (parent.GetComponent<ActionDescriptions>().AP_Costs[which][buttonNum] > parent.GetComponent<ActionDescriptions>().variables.GetComponent<MainVariables>().player_AP[which]);
    	if (coloredRed || parent.GetComponent<ActionDescriptions>().variables.GetComponent<MainVariables>().turnTaken[which]) {
    		overview.GetComponent<Text>().color = new Color(255-64, 0, 0);
    		arrow1.GetComponent<RectTransform>().localScale = new Vector3(1, -1, 1);
    	}
    	else if (parent.GetComponent<ActionDescriptions>().variables.GetComponent<MainVariables>().empowered[which]) {
    		overview.GetComponent<Text>().color = new Color(0, 255-64, 0);
    		arrow1.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    	}
    	else {
    		overview.GetComponent<Text>().color = new Color(255, 255, 255);
    		arrow1.GetComponent<RectTransform>().localScale = new Vector3(1, 0, 1);
    	}
    	description.GetComponent<Text>().text = parent.GetComponent<ActionDescriptions>().Descriptions[which][buttonNum];
    	enhancements.GetComponent<Text>().text = parent.GetComponent<ActionDescriptions>().Enhancements[which][buttonNum];
    	int colorNum = Mathf.Max(actionPopup.GetComponent<ActionSpecificsScript>().isEnhanced[which][buttonNum], 0);
    	enhancements.GetComponent<Text>().color = new Color(255*(1-colorNum), 255-(64*colorNum), 255*(1-colorNum));
    	arrow2.GetComponent<RectTransform>().localScale = new Vector3(1, colorNum, 1);
    	limitations.GetComponent<Text>().text = parent.GetComponent<ActionDescriptions>().Limitations[which][buttonNum];
    	colorNum = Mathf.Max(actionPopup.GetComponent<ActionSpecificsScript>().isLimited[which][buttonNum], 0);
    	limitations.GetComponent<Text>().color = new Color(255-(64*colorNum), 255*(1-colorNum), 255*(1-colorNum));
    	arrow3.GetComponent<RectTransform>().localScale = new Vector3(1, -1*colorNum, 1);
    }

    public string formOverview(int which){
    	string result = "Cost: ";
    	result = result + parent.GetComponent<ActionDescriptions>().AP_Costs[which][buttonNum];
    	result = result + " AP\nLevel ";
    	int level = 1 + (parent.GetComponent<ActionDescriptions>().Action_Categories[which][buttonNum]) % 2;
    	result = result + level + "\n";
    	if (parent.GetComponent<ActionDescriptions>().Action_Categories[which][buttonNum] < 2){
    		result = result + "Graph Reveal";
    	}
    	else if (parent.GetComponent<ActionDescriptions>().Action_Categories[which][buttonNum] < 4) {
    		result = result + "Reduce";
    	}
    	else if (parent.GetComponent<ActionDescriptions>().Action_Categories[which][buttonNum] < 6) {
    		result = result + "Cap";
    	}
    	else if (parent.GetComponent<ActionDescriptions>().Action_Categories[which][buttonNum] < 8) {
    		result = result + "Empower";
    	}
    	else if (parent.GetComponent<ActionDescriptions>().Action_Categories[which][buttonNum] < 10) {
    		result = result + "Map Reveal";
    	}
    	return result;
    }

    public void buttonPushed(){

    }
}
