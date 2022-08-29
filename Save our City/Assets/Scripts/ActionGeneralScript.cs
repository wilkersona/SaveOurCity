using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This is very similar to ActionSpecificScript
//except it only deals with the actions common to all players
//still not fun and also pretty buggy
public class ActionGeneralScript : MonoBehaviour
{
    public GameObject variables;
    public GameObject playerDropdown, actionObj;
    public GameObject background, textWindow, lobbyInput, tableHeader;
    public GameObject cancelButton, confirmButton, enhancementButton;
    public GameObject[] shareObj, SynergyButton;
    public int windowLevel=0;

    //who participates in which synergies
	public int[][] participants = {
    	new int[] {0, 1, 7},
    	new int[] {0, 1, 3},
    	new int[] {1, 5, 6},
    	new int[] {2, 4, 6},
    	new int[] {5, 6, 7}
	};
	public int[] costs = {3, 4, 3, 4, 4};

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

    //theres a few options for the tab pulled up; this just determines which is displayed
    void Display(){
    	if (windowLevel==0){//none
    		background.SetActive(false);
    		textWindow.SetActive(false);
    		for (int i=0; i<8; i++){
    			shareObj[i].GetComponent<PlusMinusScript>().curVal = 0;
    			shareObj[i].SetActive(false);
    		}
    		for (int i=0; i<5; i++){
    			SynergyButton[i].SetActive(false);
    		}
    		tableHeader.SetActive(false);
    		lobbyInput.SetActive(false);
    		cancelButton.SetActive(false);
    		confirmButton.SetActive(false);
    		enhancementButton.SetActive(false);
    	}
    	else if (windowLevel==1){//share
    		background.SetActive(true);
    		textWindow.SetActive(true);
    		int sum = 0;
    		for (int i=0; i<8; i++){
    			shareObj[i].SetActive(true);
    			shareObj[i].GetComponent<PlusMinusScript>().whichGiver = playerDropdown.GetComponent<Dropdown>().value;
    			sum+=(shareObj[i].GetComponent<PlusMinusScript>().curVal*shareObj[i].GetComponent<PlusMinusScript>().giveNums[playerDropdown.GetComponent<Dropdown>().value][i]);
    		}
    		textWindow.GetComponent<Text>().text = "Sharing AP from the " + variables.GetComponent<MainVariables>().Role_Names[playerDropdown.GetComponent<Dropdown>().value] +
    				" (" + (variables.GetComponent<MainVariables>().player_AP[playerDropdown.GetComponent<Dropdown>().value]-sum) + " AP left)";
    		for (int i=0; i<5; i++){
    			SynergyButton[i].SetActive(false);
    		}
    		tableHeader.SetActive(false);
    		lobbyInput.SetActive(false);
    		cancelButton.SetActive(true);
    		confirmButton.SetActive(sum<=(variables.GetComponent<MainVariables>().player_AP[playerDropdown.GetComponent<Dropdown>().value]));
    		confirmButton.GetComponent<RectTransform>().GetChild(0).GetComponent<Text>().text = "Use " + sum + " AP";
    		enhancementButton.SetActive(false);
    	}
    	else if (windowLevel==2){//lobby
    		background.SetActive(true);
    		textWindow.SetActive(true);
    		textWindow.GetComponent<Text>().text = "Input the " + variables.GetComponent<MainVariables>().Role_Names[playerDropdown.GetComponent<Dropdown>().value] + "'s Lobby action:";
    		for (int i=0; i<8; i++){
    			shareObj[i].SetActive(false);
    		}
    		for (int i=0; i<5; i++){
    			SynergyButton[i].SetActive(false);
    		}
    		tableHeader.SetActive(false);
    		lobbyInput.SetActive(true);
    		cancelButton.SetActive(true);
    		confirmButton.SetActive(true);
    		confirmButton.GetComponent<RectTransform>().GetChild(0).GetComponent<Text>().text = 
    			"Use " + Mathf.Max(1, 3 - variables.GetComponent<MainVariables>().empoweredAmounts[playerDropdown.GetComponent<Dropdown>().value]) + " AP";
    		enhancementButton.SetActive(false);
    	}
    	else if (windowLevel==3){//synergy
    		background.SetActive(true);
    		textWindow.SetActive(true);
    		textWindow.GetComponent<Text>().text = "Choose which synergy you would like to perform";
    		for (int i=0; i<8; i++){
    			shareObj[i].SetActive(false);
    		}
    		for (int i=0; i<5; i++){
    			SynergyButton[i].SetActive(true);
    		}
    		tableHeader.SetActive(true);
    		lobbyInput.SetActive(false);
    		cancelButton.SetActive(true);
    		confirmButton.SetActive(false);
    	}
    	else if (windowLevel==4){//skip
    		background.SetActive(true);
    		textWindow.SetActive(true);
    		textWindow.GetComponent<Text>().text = "Skip the " + variables.GetComponent<MainVariables>().Role_Names[playerDropdown.GetComponent<Dropdown>().value] + "'s turn?";
    		for (int i=0; i<8; i++){
    			shareObj[i].SetActive(false);
    		}
    		for (int i=0; i<5; i++){
    			SynergyButton[i].SetActive(false);
    		}
    		tableHeader.SetActive(false);
    		lobbyInput.SetActive(false);
    		cancelButton.SetActive(true);
    		confirmButton.SetActive(true);
    		enhancementButton.SetActive(false);
    	}
    	else if (windowLevel==5){//synergy a
    		background.SetActive(true);
    		textWindow.SetActive(true);
    		textWindow.GetComponent<Text>().text = "Pool AP from The Epidemiologist, The Health Behavioralist, and The Clinician (3 AP from each) to reduce CHM Anxiety & Depression by 12 PU?";
    		for (int i=0; i<8; i++){
    			shareObj[i].SetActive(false);
    		}
    		for (int i=0; i<5; i++){
    			SynergyButton[i].SetActive(false);
    		}
    		tableHeader.SetActive(false);
    		lobbyInput.SetActive(false);
    		cancelButton.SetActive(true);
    		confirmButton.SetActive(variables.GetComponent<MainVariables>().CHM_Values[3]<35 && canTake(0));
    		confirmButton.GetComponent<RectTransform>().GetChild(0).GetComponent<Text>().text = 
    			"Use " + calculateAP(0) + " AP";
    		enhancementButton.SetActive(false);
    	}
    	else if (windowLevel==6){//synergy b
    		background.SetActive(true);
    		textWindow.SetActive(true);
    		textWindow.GetComponent<Text>().text = "Pool AP from The Epidemiologist, The Laboratory Diagnostician, and The Clinician (4 AP from each) to reduce CHM Infection by 6 PU, reduce CHM Death by 6 PU. CHM Hospitalizations is capped for the current phase.?";
    		for (int i=0; i<8; i++){
    			shareObj[i].SetActive(false);
    		}
    		for (int i=0; i<5; i++){
    			SynergyButton[i].SetActive(false);
    		}
    		tableHeader.SetActive(false);
    		lobbyInput.SetActive(false);
    		cancelButton.SetActive(true);
    		confirmButton.SetActive(variables.GetComponent<MainVariables>().actionTakenGame[0][0]!=0 && canTake(1));
    		confirmButton.GetComponent<RectTransform>().GetChild(0).GetComponent<Text>().text = 
    			"Use " + calculateAP(1) + " AP";
    		enhancementButton.SetActive(variables.GetComponent<MainVariables>().actionTakenGame[0][0]!=0 && variables.GetComponent<MainVariables>().actionTakenGame[2][5]!=0 && canTake(1));
    	}
    	else if (windowLevel==7){//synergy c
    		background.SetActive(true);
    		textWindow.SetActive(true);
    		textWindow.GetComponent<Text>().text = "Pool AP from The Epidemiologist, The Politician, and The Media Campaigner (3 AP from each) to reduce CHM Misinformation & Mistrust by 12 PU?";
    		for (int i=0; i<8; i++){
    			shareObj[i].SetActive(false);
    		}
    		for (int i=0; i<5; i++){
    			SynergyButton[i].SetActive(false);
    		}
    		tableHeader.SetActive(false);
    		lobbyInput.SetActive(false);
    		cancelButton.SetActive(true);
    		confirmButton.SetActive(variables.GetComponent<MainVariables>().actionTakenGame[5][2]!=0 && canTake(2));
    		confirmButton.GetComponent<RectTransform>().GetChild(0).GetComponent<Text>().text = 
    			"Use " + calculateAP(2) + " AP";
    		enhancementButton.SetActive(false);
    	}
    	else if (windowLevel==8){//synergy d
    		background.SetActive(true);
    		textWindow.SetActive(true);
    		textWindow.GetComponent<Text>().text = "Pool AP from The Politician, The Virologist, and The Drug Developer (4 AP from each) to Reduce CHM Research & Development Expenses by 6 PU, and reduce CHM Medical Supply Shortages by 6 PU. Cap CHM Health Disparities for current phase.?";
    		for (int i=0; i<8; i++){
    			shareObj[i].SetActive(false);
    		}
    		for (int i=0; i<5; i++){
    			SynergyButton[i].SetActive(false);
    		}
    		tableHeader.SetActive(false);
    		lobbyInput.SetActive(false);
    		cancelButton.SetActive(true);
    		confirmButton.SetActive(canTake(3));
    		confirmButton.GetComponent<RectTransform>().GetChild(0).GetComponent<Text>().text = 
    			"Use " + calculateAP(3) + " AP";
    		enhancementButton.SetActive(variables.GetComponent<MainVariables>().actionTakenGame[5][4]!=0 && canTake(3));
    	}
    	else if (windowLevel==9){//synergy e
    		background.SetActive(true);
    		textWindow.SetActive(true);
    		textWindow.GetComponent<Text>().text = "Pool AP from The Politician, The Health Behaviorist, and The Media Campaigner (4 AP from each) to Reduce CHM Barriers to Healthcare Access by 6 PU, and reduce CHM Health Disparities by 6 PU. Cap CHM Misinformation & Mistrust for current phase.";
    		for (int i=0; i<8; i++){
    			shareObj[i].SetActive(false);
    		}
    		for (int i=0; i<5; i++){
    			SynergyButton[i].SetActive(false);
    		}
    		tableHeader.SetActive(false);
    		lobbyInput.SetActive(false);
    		cancelButton.SetActive(true);
    		confirmButton.SetActive(variables.GetComponent<MainVariables>().actionTakenGame[5][4]!=0 && canTake(4));
    		confirmButton.GetComponent<RectTransform>().GetChild(0).GetComponent<Text>().text = 
    			"Use " + calculateAP(4) + " AP";
    		enhancementButton.SetActive(variables.GetComponent<MainVariables>().actionTakenGame[5][4]!=0 && variables.GetComponent<MainVariables>().actionTakenGame[7][3]!=0 && canTake(4));
    	}
    }

    //checks if a synergy can be taken
    bool canTake(int which){
    	for(int i=0; i<3; i++){
        	if (variables.GetComponent<MainVariables>().turnTaken[participants[which][i]] || 
        			(costs[which] - variables.GetComponent<MainVariables>().empoweredAmounts[participants[which][i]]) > variables.GetComponent<MainVariables>().player_AP[participants[which][i]]) {
        		return false;
        	}
        }
        return true;
    }

    //calculates the ap of a synergy
    int calculateAP(int which){
    	int sum = 0;
    	for (int i=0; i<3; i++){
    		sum+=(costs[which] - variables.GetComponent<MainVariables>().empoweredAmounts[participants[which][i]]);
    	}
    	return sum;
    }

    //sets which window you're on
    public void setWindowLevel(int which){
    	windowLevel = which;
    	return;
    }

    //perform the tangible bits of a synergy
    public void confirm (int signal=0){
    	int ddev_beginning_ap = variables.GetComponent<MainVariables>().player_AP[2];
    	int whichPlayer = playerDropdown.GetComponent<Dropdown>().value;
    	if (windowLevel==1){
    		int sum = 0;
    		string printed = "Share Amounts: ";
    		for (int i=0; i<8; i++){
    			sum+=(shareObj[i].GetComponent<PlusMinusScript>().curVal*shareObj[i].GetComponent<PlusMinusScript>().giveNums[whichPlayer][i]);
    			variables.GetComponent<MainVariables>().player_AP[i] += (shareObj[i].GetComponent<PlusMinusScript>().curVal*shareObj[i].GetComponent<PlusMinusScript>().getNums[whichPlayer][i]);
    			printed += variables.GetComponent<MainVariables>().Role_Names[i] + ": " + (shareObj[i].GetComponent<PlusMinusScript>().curVal*shareObj[i].GetComponent<PlusMinusScript>().getNums[whichPlayer][i]) + " AP; ";
    		}
    		variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= sum;
			variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
			variables.GetComponent<MainVariables>().saveAction(whichPlayer, 6, printed);

    	}
    	if (windowLevel==2){
    		variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= Mathf.Max(1, 3 - variables.GetComponent<MainVariables>().empoweredAmounts[whichPlayer]);
			variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
			variables.GetComponent<MainVariables>().saveAction(whichPlayer, 7, lobbyInput.GetComponent<InputField>().text);
    	}
    	if (windowLevel==4){
			variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
			variables.GetComponent<MainVariables>().saveAction(whichPlayer, 8, "Skipped Turn");

    	}
    	if (windowLevel==5){
	    	for (int i=0; i<3; i++){
	    		variables.GetComponent<MainVariables>().player_AP[participants[0][i]] -= Mathf.Max(1, costs[0] - variables.GetComponent<MainVariables>().empoweredAmounts[participants[0][i]]);
				variables.GetComponent<MainVariables>().turnTaken[participants[0][i]] = true;
    		}
			variables.GetComponent<MainVariables>().CHM_Values[3] -= 12;
			variables.GetComponent<MainVariables>().saveAction(9, 0, "Option: "+ signal);
    	}
    	if (windowLevel==6){
	    	for (int i=0; i<3; i++){
	    		variables.GetComponent<MainVariables>().player_AP[participants[1][i]] -= Mathf.Max(1, costs[1] - variables.GetComponent<MainVariables>().empoweredAmounts[participants[1][i]]);
				variables.GetComponent<MainVariables>().turnTaken[participants[1][i]] = true;
    		}
			variables.GetComponent<MainVariables>().CHM_Values[0] -= 6;
			variables.GetComponent<MainVariables>().CHM_Values[2] -= 6;
			variables.GetComponent<MainVariables>().CHM_Caps[1] = true;
			variables.GetComponent<MainVariables>().CHM_Cap_Durations[1] = 1+signal;
			variables.GetComponent<MainVariables>().saveAction(9, 1, "Option: "+ signal);
    	}
    	if (windowLevel==7){
	    	for (int i=0; i<3; i++){
	    		variables.GetComponent<MainVariables>().player_AP[participants[2][i]] -= Mathf.Max(1, costs[2] - variables.GetComponent<MainVariables>().empoweredAmounts[participants[2][i]]);
				variables.GetComponent<MainVariables>().turnTaken[participants[2][i]] = true;
    		}
			variables.GetComponent<MainVariables>().CHM_Values[4] -= 12;
			variables.GetComponent<MainVariables>().saveAction(9, 2, "Option: "+ signal);
    	}
    	if (windowLevel==8){
	    	for (int i=0; i<3; i++){
	    		variables.GetComponent<MainVariables>().player_AP[participants[3][i]] -= Mathf.Max(1, costs[3] - variables.GetComponent<MainVariables>().empoweredAmounts[participants[3][i]]);
				variables.GetComponent<MainVariables>().turnTaken[participants[3][i]] = true;
    		}
			variables.GetComponent<MainVariables>().CHM_Values[6] -= 6;
			variables.GetComponent<MainVariables>().CHM_Values[7] -= 6;
			variables.GetComponent<MainVariables>().CHM_Caps[5] = true;
			variables.GetComponent<MainVariables>().CHM_Cap_Durations[5] = 1+signal;
			variables.GetComponent<MainVariables>().saveAction(9, 3, "Option: "+ signal);
    	}
    	if (windowLevel==9){
	    	for (int i=0; i<3; i++){
	    		variables.GetComponent<MainVariables>().player_AP[participants[4][i]] -= Mathf.Max(1, costs[4] - variables.GetComponent<MainVariables>().empoweredAmounts[participants[4][i]]);
				variables.GetComponent<MainVariables>().turnTaken[participants[4][i]] = true;
    		}
			variables.GetComponent<MainVariables>().CHM_Values[5] -= 6;
			variables.GetComponent<MainVariables>().CHM_Values[8] -= 6;
			variables.GetComponent<MainVariables>().CHM_Caps[4] = true;
			variables.GetComponent<MainVariables>().CHM_Cap_Durations[4] = 1+signal;
			variables.GetComponent<MainVariables>().saveAction(9, 4, "Option: "+ signal);
    	}
    	variables.GetComponent<MainVariables>().ddev_ap_spent += Mathf.Max(0, (ddev_beginning_ap - variables.GetComponent<MainVariables>().player_AP[2]));
    	actionObj.GetComponent<ActionSpecificsScript>().recentlyTakenAction = 10;
    	actionObj.GetComponent<ActionSpecificsScript>().playerChart.GetComponent<ActionTrackerManager>().cancelled();
    	windowLevel=0;
    	return;
    }
}

/*
check for
	ap
	enhancement ap
	show ap that confirming will spend
	share show ap left
subtract ap

	turn taken
take turn

action history entry

ddev ap spent
*/
