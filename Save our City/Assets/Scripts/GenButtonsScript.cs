using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//a script for the share/lobby/synergy/skip buttons
//mostly to hide them if they aren't useable
public class GenButtonsScript : MonoBehaviour
{
	public GameObject variables;
	public GameObject share, lobby, synergy, skip;
	public GameObject playerDropdown;
	public int[][] general_AP_Costs = {
		new int[] {3, 3, 0},
		new int[] {3, 3, 0},
		new int[] {3, 3, 0},
		new int[] {3, 3, 0},
		new int[] {1, 3, 0},
		new int[] {3, 3, 0},
		new int[] {1, 3, 0},
		new int[] {3, 3, 0}
	};
	public int[] synergy_AP_Costs = {3, 4, 3, 4, 4};
    // Start is called before the first frame update
    void Start()
    {
    	
    }

    // Update is called once per frame
    void Update()
    {
        Display();
    }

    void Display(){
    	int whichPlayer = playerDropdown.GetComponent<Dropdown>().value;
    	showButton(share, !variables.GetComponent<MainVariables>().turnTaken[whichPlayer] && 
    			Mathf.Max(1, general_AP_Costs[whichPlayer][0]/* - variables.GetComponent<MainVariables>().empoweredAmounts[whichPlayer]*/) <= variables.GetComponent<MainVariables>().player_AP[whichPlayer]);
    	showButton(lobby, !variables.GetComponent<MainVariables>().turnTaken[whichPlayer] && 
    			Mathf.Max(1, general_AP_Costs[whichPlayer][1] - variables.GetComponent<MainVariables>().empoweredAmounts[whichPlayer]) <= variables.GetComponent<MainVariables>().player_AP[whichPlayer]);
    	showButton(synergy, !variables.GetComponent<MainVariables>().turnTaken[whichPlayer]);
    	showButton(skip, !variables.GetComponent<MainVariables>().turnTaken[whichPlayer]);
    }

    void showButton(GameObject button, bool active){
    	if (!active) button.GetComponent<RectTransform>().GetChild(0).GetComponent<Text>().color = new Color(255, 0, 0, 1);
    	else button.GetComponent<RectTransform>().GetChild(0).GetComponent<Text>().color = new Color(0, 0, 0, 1);
    	button.GetComponent<Button>().interactable = active;
    }
}
