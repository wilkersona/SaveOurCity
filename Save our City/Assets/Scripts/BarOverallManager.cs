using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarOverallManager : MonoBehaviour
{
    public GameObject[] bars;
    public GameObject variables; 
    public GameObject tabManager;
    public GameObject popupText, buttonText;
    bool editMode;
    int everySoOften = 0;
    public int[] CHM_Values = {0, 0, 0, 0, 0, 0, 0, 0, 0};
    public int[] CHM_Graph_Reveals = {0, 0, 0, 0, 0, 0, 0, 0, 0};
    public bool[] CHM_Caps = {false, false, false, false, false, false, false, false, false};
    public string[] CHM_Names = {"Infections", "Hospitalizations", "Deaths", "Anxiety and Depression", "Misinformation and Mistrust", 
    	"Health Disparities", "R & D Expenses", "Medical Supply Shortages", "Barriers to Healthcare Access"};
    public int whichTab = 0;

    // Start is called before the first frame update
    void Start()
    {
        setInitialVariables();
    }

    // Update is called once per frame
    void Update()
    {
        if (variables.GetComponent<MainVariables>().popupSignalLive && variables.GetComponent<MainVariables>().popupSignalNum==2){
        	variables.GetComponent<MainVariables>().popupSignalLive = false;
        	if (editMode){
    			tabManager.GetComponent<TabManagement>().hideTabs = false;
    			editMode = false;
    			for (int i=0; i<9; i++){
    				bars[i].GetComponent<BarHandler>().editing = false;
    				variables.GetComponent<MainVariables>().CHM_Graph_Reveals[i] = CHM_Graph_Reveals[i];
    				variables.GetComponent<MainVariables>().CHM_Caps[i] = CHM_Caps[i];
    				variables.GetComponent<MainVariables>().CHM_Values[i] = CHM_Values[i];
    			}
        		popupText.GetComponent<PopupManager>().textBubbles[2] = "Do you want to make your desired changes on the map?";
        		buttonText.GetComponent<Text>().text = "Edit Graph";
        	}
        	else {
        		setInitialVariables();
    			tabManager.GetComponent<TabManagement>().hideTabs = true;
    			editMode = true;
    			for (int i=0; i<9; i++){
    				bars[i].GetComponent<BarHandler>().editing = true;
    				bars[i].GetComponent<BarHandler>().dropdown.GetComponent<Dropdown>().value = variables.GetComponent<MainVariables>().CHM_Graph_Reveals[i];
    				bars[i].GetComponent<BarHandler>().inputField.GetComponent<InputField>().text = ""+variables.GetComponent<MainVariables>().CHM_Values[i];
    				bars[i].GetComponent<BarHandler>().toggle.GetComponent<Toggle>().isOn = variables.GetComponent<MainVariables>().CHM_Caps[i];
    			}
        		popupText.GetComponent<PopupManager>().textBubbles[2] = "Do you want to select portions of the graph to reveal/change?";
        		buttonText.GetComponent<Text>().text = "Confirm Edits";
        	}
        }
        else if (!editMode){
        	whichTab = variables.GetComponent<MainVariables>().whichTab;
        	everySoOften++;
        	if (everySoOften%20==0) setInitialVariables();
        }
    }

    void setInitialVariables() {
        whichTab = variables.GetComponent<MainVariables>().whichTab;
    	for (int i=0; i<9; i++){
    		CHM_Values[i] = variables.GetComponent<MainVariables>().CHM_Values[i];
    		CHM_Graph_Reveals[i] = variables.GetComponent<MainVariables>().CHM_Graph_Reveals[i];
    		CHM_Caps[i] = variables.GetComponent<MainVariables>().CHM_Caps[i];
    	}
    }
}
