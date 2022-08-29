using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

//this script is very buggy and needs tons of work
//it is a series of case switch statemnts that manually go over each action
//theres also quite a bit of redundancy sorry
//theres so many actions to do that its a pain in the [script acronym]
public class ActionSpecificsScript : MonoBehaviour
{
    public GameObject variables, actionVariables, mapManager;
    public GameObject playerDropdown, playerChart, prereqWaiver;
    public GameObject[] options;
    public GameObject[] optionsText;
    public GameObject cancel;
    public GameObject inputField;
    public GameObject infoText;
    public GameObject background;
    public bool confirming = false;
    public int whichPlayer, whichAction, whichChoice;
    public int recentlyTakenAction;
    private System.Random randomizer = new System.Random();
    int oncePerHundred = 0;
    

    //tracks whether actions are currently enhanced/limited
    //0 = false; 1 = true (i.e. it IS enhanced/limited); 2+ = it's non-boolean (I haven't used this yet?)
	public int[][] isEnhanced = {
		new int[] {-1, -1, 0, -1, 0, -1},
		new int[] {-1, 0, -1, 0, -1, 0},
		new int[] {0, 0, 0, 0, -1, -1},
		new int[] {-1, 0, 0, 0, -1, -1},
		new int[] {-1, -1, 0, 0, 0, -1},
		new int[] {-1, -1, 0, 0, 0, 0},
		new int[] {-1, -1, 0, 0, 0, 0},
		new int[] {0, -1, 0, -1, 0, 0}
	};
	public int[][] isLimited = {
		new int[] {-1, -1, 0, -1, -1, -1},
		new int[] {-1, -1, 0, -1, 0, -1},
		new int[] {-1, 0, 0, 0, -1, 0},
		new int[] {-1, -1, 0, 0, -1, -1},
		new int[] {-1, -1, 0, 0, 0, -1},
		new int[] {-1, 0, -1, -1, 0, -1},
		new int[] {-1, -1, 0, -1, 0, -1},
		new int[] {-1, -1, -1, -1, 0, -1}
	};
    // Start is called before the first frame update
    void Start()
    {
        setActivity();
    }

    // Update is called once per frame
    void Update()
    {
    	if (recentlyTakenAction>0) recentlyTakenAction--;

    	oncePerHundred++;
    	if (oncePerHundred%100==0){
    		UpdateEnhancementsAndPrerequisites();
    	}
    }

    //big function to check on the availability of certain options, to display to GM
    void UpdateEnhancementsAndPrerequisites() {
    	for (int i=0; i<8; i++){
    		for (int j=0; j<6; j++){
		    	switch (i*10 + j) 
		    	{
		    		case 2:
		    			setMe(isEnhanced, i, j, variables.GetComponent<MainVariables>().ddev_ap_spent>=5);
		    			setMe(isLimited, i, j, variables.GetComponent<MainVariables>().CHM_Values[6]>=35);
		    			break;
		    		case 4:
		    			setMe(isEnhanced, i, j, variables.GetComponent<MainVariables>().actionTakenGame[7][2]>0);
		    			setMe(isLimited, i, j, false);
		    			break;
		    		case 11:
		    			setMe(isEnhanced, i, j, variables.GetComponent<MainVariables>().actionTakenGame[0][0] != 0);
		    			setMe(isLimited, i, j, false);
		    			break;
		    		case 12:
		    			setMe(isEnhanced, i, j, false);
		    			setMe(isLimited, i, j, variables.GetComponent<MainVariables>().CHM_Values[4]>=35);
		    			break;
		    		case 13:
		    			setMe(isEnhanced, i, j, variables.GetComponent<MainVariables>().player_AP[6] >= 1);
		    			setMe(isLimited, i, j, false);
		    			break;
		    		case 14:
		    			setMe(isEnhanced, i, j, false);
		    			setMe(isLimited, i, j, variables.GetComponent<MainVariables>().CHM_Values[6]>=35);
		    			break;
		    		case 15:
		    			setMe(isEnhanced, i, j, variables.GetComponent<MainVariables>().actionTakenGame[7][2]!=0);
		    			setMe(isLimited, i, j, false);
		    			break;
		    		case 20:
		    			setMe(isEnhanced, i, j, variables.GetComponent<MainVariables>().player_AP[2] >= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[i][j]+1);
		    			setMe(isLimited, i, j, false);
		    			break;
		    		case 21:
		    			setMe(isEnhanced, i, j, variables.GetComponent<MainVariables>().minigameAttemptsSubphase[2][4] == 0);
		    			setMe(isLimited, i, j, variables.GetComponent<MainVariables>().minigameAttemptsSubphase[2][4] != 0);
		    			break;
		    		case 22:
		    			setMe(isEnhanced, i, j, variables.GetComponent<MainVariables>().minigameAttemptsSubphase[2][4] == 0);
		    			setMe(isLimited, i, j, variables.GetComponent<MainVariables>().minigameAttemptsSubphase[2][4] != 0);
		    			break;
		    		case 23:
		    			setMe(isEnhanced, i, j, variables.GetComponent<MainVariables>().phase==0);
		    			setMe(isLimited, i, j, variables.GetComponent<MainVariables>().quizScores[2]<50);
		    			break;
		    		case 31:
		    			setMe(isEnhanced, i, j, variables.GetComponent<MainVariables>().actionTakenPhase[6][3]>0);
		    			setMe(isLimited, i, j, false);
		    			break;
		    		case 32:
		    			setMe(isEnhanced, i, j, variables.GetComponent<MainVariables>().player_AP[3] >= (actionVariables.GetComponent<ActionDescriptions>().AP_Costs[i][j]+1));
		    			setMe(isLimited, i, j, variables.GetComponent<MainVariables>().minigameAttemptsSubphase[3][2] != 0);
		    			break;
		    		case 33:
		    			setMe(isEnhanced, i, j, false);
		    			setMe(isLimited, i, j, variables.GetComponent<MainVariables>().empowered[0]);
		    			break;
		    		case 35:
		    			setMe(isEnhanced, i, j, variables.GetComponent<MainVariables>().actionTakenPhase[5][5]!=0);
		    			setMe(isLimited, i, j, false);
		    			break;
		    		case 42:
		    			setMe(isEnhanced, i, j, true);
		    			setMe(isLimited, i, j, variables.GetComponent<MainVariables>().quizScores[4]<50);
		    			break;
		    		case 43:
		    			setMe(isEnhanced, i, j, variables.GetComponent<MainVariables>().player_AP[4] >= (actionVariables.GetComponent<ActionDescriptions>().AP_Costs[i][j]+1));
		    			setMe(isLimited, i, j, variables.GetComponent<MainVariables>().empowered[2] || variables.GetComponent<MainVariables>().minigameAttemptsPhase[4][2] != 0);
		    			break;
		    		case 44:
		    			setMe(isEnhanced, i, j, variables.GetComponent<MainVariables>().player_AP[4] >= (actionVariables.GetComponent<ActionDescriptions>().AP_Costs[i][j]+1));
		    			setMe(isLimited, i, j, variables.GetComponent<MainVariables>().empowered[2] || variables.GetComponent<MainVariables>().minigameAttemptsPhase[4][2] != 0);
		    			break;
		    		case 51:
		    			setMe(isEnhanced, i, j, false);
		    			setMe(isLimited, i, j, variables.GetComponent<MainVariables>().quizScores[5]<50);
		    			break;
		    		case 52:
		    			setMe(isEnhanced, i, j, variables.GetComponent<MainVariables>().player_AP[6] >= 2);
		    			setMe(isLimited, i, j, false);
		    			break;
		    		case 53:
		    			setMe(isEnhanced, i, j, variables.GetComponent<MainVariables>().quizScores[7]>=50);
		    			setMe(isLimited, i, j, false);
		    			break;
		    		case 54:
		    			setMe(isEnhanced, i, j, variables.GetComponent<MainVariables>().minigameAttemptsSubphase[5][1] == 0);
		    			setMe(isLimited, i, j, variables.GetComponent<MainVariables>().CHM_Values[4]>=35 || variables.GetComponent<MainVariables>().minigameAttemptsSubphase[5][1] != 0);
		    			break;
		    		case 55:
		    			setMe(isEnhanced, i, j, variables.GetComponent<MainVariables>().actionTakenPhase[3][5]!=0);
		    			setMe(isLimited, i, j, false);
		    			break;
		    		case 62:
		    			setMe(isEnhanced, i, j, variables.GetComponent<MainVariables>().player_AP[0] >= 1);
		    			setMe(isLimited, i, j, variables.GetComponent<MainVariables>().minigameAttemptsPhase[6][1] >= 2);
		    			break;
		    		case 63:
		    			setMe(isEnhanced, i, j, variables.GetComponent<MainVariables>().player_AP[2] >= 1);
		    			setMe(isLimited, i, j, false);
		    			break;
		    		case 64:
		    			setMe(isEnhanced, i, j, variables.GetComponent<MainVariables>().minigameAttemptsPhase[6][1] < 2);
		    			setMe(isLimited, i, j, variables.GetComponent<MainVariables>().empowered[7] || variables.GetComponent<MainVariables>().minigameAttemptsPhase[6][1] >= 2);
		    			break;
		    		case 65:
		    			setMe(isEnhanced, i, j, variables.GetComponent<MainVariables>().actionTakenPhase[7][5] != 0);
		    			setMe(isLimited, i, j, false);
		    			break;
		    		case 70:
		    			setMe(isEnhanced, i, j, variables.GetComponent<MainVariables>().player_AP[1] >= 1);
		    			setMe(isLimited, i, j, false);
		    			break;
		    		case 72:
		    			setMe(isEnhanced, i, j, variables.GetComponent<MainVariables>().phase==0);
		    			setMe(isLimited, i, j, false);
		    			break;
		    		case 74:
		    			setMe(isEnhanced, i, j, true);
		    			setMe(isLimited, i, j, variables.GetComponent<MainVariables>().empowered[0]);
		    			break;
		    		case 75:
		    			setMe(isEnhanced, i, j, variables.GetComponent<MainVariables>().actionTakenPhase[6][5]!=0);
		    			setMe(isLimited, i, j, false);
		    			break;
		    		default:
		    			setMe(isEnhanced, i, j, false);
		    			setMe(isLimited, i, j, false);
		    			break;

		    	}
    		}
    	}

    }

    //helper function for UpdateEnhancementsAndPrerequisites
    void setMe(int[][] arr, int i, int j, bool toOne){
    	if (arr[i][j]==-1) return;
    	if (toOne) arr[i][j] = 1;
    	else arr[i][j] = 0;
    	return;
    }

    //if you should display the popup window
    public void displayActionConfirmation(int whichButton) {
    	confirming = true;
    	whichPlayer = playerDropdown.GetComponent<Dropdown>().value;
    	whichAction = whichButton;
    	setActivity();
        displayOptions();
    }

    //prereqs waived
    public void waiveLimits(){
    	variables.GetComponent<MainVariables>().prereqsWaived = prereqWaiver.GetComponent<Toggle>().isOn;
    }

    //cancel button pressed
    public void cancelled() {
    	confirming = false;
    	setActivity();
    }

    //confirm button pressed
    public void confirmed(int whichButton){
    	confirming = false;
    	whichChoice = whichButton;
    	setActivity();
    	performAction();
    }

    //checks if the popup should be being displayed
    void setActivity(){

        for (int i=0; i<6; i++){
        	options[i].SetActive(confirming);
        }
        cancel.SetActive(confirming);
        inputField.SetActive(confirming);
        infoText.SetActive(confirming);
        background.SetActive(confirming);
    }

    //more specifics on what buttons/text to display on the popup window
    //every action needs one of this
    void displayOptions() {
    	if (variables.GetComponent<MainVariables>().turnTaken[whichPlayer]) {
	        for (int i=0; i<6; i++){
	        	options[i].SetActive(false);
	        }
	        inputField.SetActive(false);
	        infoText.GetComponent<Text>().text = "This player has already taken their turn.";
	        return;
    	}
    	bool prereqsWaived = variables.GetComponent<MainVariables>().prereqsWaived;
    	if (prereqsWaived){
	        for (int i=0; i<6; i++){
	        	optionsText[i].GetComponent<Text>().text = "Backup Button; Do Not Press";
	        }
    	}
    	int ap;
    	switch (whichPlayer*10 + whichAction) 
    	{
			case 0:
				ap = actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				options[0].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[0].GetComponent<Text>().text = "Use "+ap+" AP";
		        for (int i=1; i<6; i++){
		        	options[i].SetActive(false);
		        }
		        inputField.SetActive(false);
		        infoText.GetComponent<Text>().text = actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction] + "?";
				break;
			case 1:
				ap = actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				options[0].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[0].GetComponent<Text>().text = "Use "+ap+" AP";
		        for (int i=1; i<6; i++){
		        	options[i].SetActive(false);
		        }
		        inputField.SetActive(false);
		        infoText.GetComponent<Text>().text = actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction] + "?";
				break;
	
			case 2:
				ap = actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				options[0].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().CHM_Values[6]<35);
				optionsText[0].GetComponent<Text>().text = "Use "+ap+" AP";
				options[1].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().ddev_ap_spent>=5 && variables.GetComponent<MainVariables>().CHM_Values[6]<35);
				optionsText[1].GetComponent<Text>().text = "Use "+ap+" AP (Enhancement)";
		        for (int i=2; i<6; i++){
		        	options[i].SetActive(false);
		        }
		        inputField.SetActive(false);
		        infoText.GetComponent<Text>().text = actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction] + "? (D6 result(s) shown in Action History)";
				break;
			case 3:
				ap = actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				options[0].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[0].GetComponent<Text>().text = "Use "+ap+" AP";
		        for (int i=1; i<6; i++){
		        	options[i].SetActive(false);
		        }
		        inputField.SetActive(false);
		        infoText.GetComponent<Text>().text = actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction] + "?";
				break;
			case 4:
				ap = actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				options[0].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[0].GetComponent<Text>().text = "Use "+ap+" AP";
				options[1].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().actionTakenGame[7][2]>0);
				optionsText[1].GetComponent<Text>().text = "Use "+ap+" AP (Enhancement)";
		        for (int i=2; i<6; i++){
		        	options[i].SetActive(false);
		        }
		        inputField.SetActive(false);
		        infoText.GetComponent<Text>().text = actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction] + "?";
				break;
			case 5:
				ap = actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				options[0].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[0].GetComponent<Text>().text = ""+ap+" AP: Cap Hospitalizations";
				options[1].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[1].GetComponent<Text>().text = ""+ap+" AP: Cap Deaths";
				options[2].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[2].GetComponent<Text>().text = ""+ap+" AP: Cap Anxiety & Depression";
		        for (int i=3; i<6; i++){
		        	options[i].SetActive(false);
		        }
		        inputField.SetActive(false);
		        infoText.GetComponent<Text>().text = actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction] + "?";
				break;
    		case 10:
				ap = actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				options[0].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[0].GetComponent<Text>().text = ""+ap+" AP: Reveal Infections";
				options[1].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[1].GetComponent<Text>().text = ""+ap+" AP: Reveal Hospitalizations";
				options[2].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[2].GetComponent<Text>().text = ""+ap+" AP: Reveal Deaths";
				options[3].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[3].GetComponent<Text>().text = ""+ap+" AP: Reveal Anxiety & Depression";
				options[4].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[4].GetComponent<Text>().text = ""+ap+" AP: Reveal Health Disparities";
				options[5].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[5].GetComponent<Text>().text = ""+ap+" AP: Reveal Barriers to Healthcare Access";
		        inputField.SetActive(false);
		        infoText.GetComponent<Text>().text = actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction] + "?";
				break;
			case 11://warning warning Enhancement is not optional ---------------------------------------------------------------------------------------------- warning warning
				string tail = "";
				if (variables.GetComponent<MainVariables>().actionTakenGame[0][0] != 0) tail = " + Reduce";
				ap = actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				options[0].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[0].GetComponent<Text>().text = ""+ap+" AP: Reveal Infections"+tail;
				options[1].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[1].GetComponent<Text>().text = ""+ap+" AP: Reveal Hospitalizations"+tail;
				options[2].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[2].GetComponent<Text>().text = ""+ap+" AP: Reveal Deaths"+tail;
				options[3].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[3].GetComponent<Text>().text = ""+ap+" AP: Reveal Anxiety & Depression"+tail;
				options[4].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[4].GetComponent<Text>().text = ""+ap+" AP: Reveal Health Disparities"+tail;
				options[5].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[5].GetComponent<Text>().text = ""+ap+" AP: Reveal Barriers to Healthcare Access"+tail;
		        inputField.SetActive(false);
		        infoText.GetComponent<Text>().text = actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction] + "? (2D6 result shown in Action History)";
				break;
			case 12:
				ap = actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				options[0].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().CHM_Values[4]<35);
				optionsText[0].GetComponent<Text>().text = "Use "+ap+" AP (Input Reveal tile above; e.g. E5)";
		        for (int i=1; i<6; i++){
		        	options[i].SetActive(false);
		        }
		        inputField.SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().CHM_Values[4]<35);
		        inputField.GetComponent<InputField>().text = "E5";
		        infoText.GetComponent<Text>().text = actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction] + "?";
				break;
			case 13:
				ap = actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				options[0].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[0].GetComponent<Text>().text = ""+ap+" AP; Input tiles above (e.g. E5)";
				options[1].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().player_AP[6] >= 1);
				optionsText[1].GetComponent<Text>().text = ""+ap+" AP; Politician: 1 AP; Input tiles above (e.g. E5)";
		        for (int i=2; i<6; i++){
		        	options[i].SetActive(false);
		        }
		        inputField.SetActive(true);
		        inputField.GetComponent<InputField>().text = "E5, E5, E5, E5, E5";
		        infoText.GetComponent<Text>().text = actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction] + "?";
				break;
			case 14:
				ap = actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				options[0].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().CHM_Values[6]<35);
				optionsText[0].GetComponent<Text>().text = ""+ap+" AP: Reveal Infections";
				options[1].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().CHM_Values[6]<35);
				optionsText[1].GetComponent<Text>().text = ""+ap+" AP: Reveal Hospitalizations";
				options[2].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().CHM_Values[6]<35);
				optionsText[2].GetComponent<Text>().text = ""+ap+" AP: Reveal Deaths";
				options[3].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().CHM_Values[6]<35);
				optionsText[3].GetComponent<Text>().text = ""+ap+" AP: Reveal Anxiety & Depression";
				options[4].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().CHM_Values[6]<35);
				optionsText[4].GetComponent<Text>().text = ""+ap+" AP: Reveal Health Disparities";
				options[5].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().CHM_Values[6]<35);
				optionsText[5].GetComponent<Text>().text = ""+ap+" AP: Reveal Barriers to Healthcare Access";
		        inputField.SetActive(false);
		        infoText.GetComponent<Text>().text = actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction] + "?";
				break;
			case 15://warning warning Enhancement is not optional ---------------------------------------------------------------------------------------------- warning warning
				ap = actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				options[0].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[0].GetComponent<Text>().text = ""+ap+" AP: Reveal & Cap Infections";
				options[1].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[1].GetComponent<Text>().text = ""+ap+" AP: Reveal & Cap Hospitalizations";
				options[2].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[2].GetComponent<Text>().text = ""+ap+" AP: Reveal & Cap Deaths";
				options[3].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[3].GetComponent<Text>().text = ""+ap+" AP: Reveal & Cap Anxiety & Depression";
				options[4].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[4].GetComponent<Text>().text = ""+ap+" AP: Reveal & Cap Health Disparities";
				options[5].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[5].GetComponent<Text>().text = ""+ap+" AP: Reveal & Cap Barriers to Healthcare Access";
		        inputField.SetActive(false);
		        infoText.GetComponent<Text>().text = actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction] + "?";
				break;
    		case 20:
				ap = actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				options[0].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[0].GetComponent<Text>().text = "Use "+ap+" AP";
				options[1].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap+1);
				optionsText[1].GetComponent<Text>().text = "Use "+(ap+1)+" AP (enhanced)";
		        for (int i=2; i<6; i++){
		        	options[i].SetActive(false);
		        }
		        inputField.SetActive(false);
		        infoText.GetComponent<Text>().text = actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction] + "? (D6 result shown in Action History)";
				break;
			case 21:
				ap = actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				options[0].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[0].GetComponent<Text>().text = "Use "+ap+" AP";
				options[1].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().minigameAttemptsSubphase[whichPlayer][4] == 0);
				optionsText[1].GetComponent<Text>().text = ""+ap+" AP; Minigame Lost";
				options[2].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().minigameAttemptsSubphase[whichPlayer][4] == 0);
				optionsText[2].GetComponent<Text>().text = ""+ap+" AP; Minigame Won (Input amount above)";
		        for (int i=3; i<6; i++){
		        	options[i].SetActive(false);
		        }
		        inputField.SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().minigameAttemptsSubphase[whichPlayer][4] == 0);
		        inputField.GetComponent<InputField>().text = "0";
		        infoText.GetComponent<Text>().text = actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction] + "?";
				break;
			case 22:
				ap = actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				options[0].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[0].GetComponent<Text>().text = "Use "+ap+" AP";
				options[1].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().minigameAttemptsSubphase[whichPlayer][4] == 0);
				optionsText[1].GetComponent<Text>().text = ""+ap+" AP; Minigame Lost";
				options[2].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().minigameAttemptsSubphase[whichPlayer][4] == 0);
				optionsText[2].GetComponent<Text>().text = ""+ap+" AP; Minigame Won (Input amount above)";
		        for (int i=3; i<6; i++){
		        	options[i].SetActive(false);
		        }
		        inputField.SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().minigameAttemptsSubphase[whichPlayer][4] == 0);
		        inputField.GetComponent<InputField>().text = "0";
		        infoText.GetComponent<Text>().text = actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction] + "?";
				break;
			case 23:
				ap = actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				int ap2 = 3;
				if (variables.GetComponent<MainVariables>().empowered[2]){
					ap2 = Mathf.Max(1, ap2 - variables.GetComponent<MainVariables>().empoweredAmounts[2]);
				}
				options[0].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().quizScores[2]>=50);
				optionsText[0].GetComponent<Text>().text = "Use "+ap+" AP";
				options[1].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().quizScores[2]>=50 && variables.GetComponent<MainVariables>().phase==0);
				optionsText[1].GetComponent<Text>().text = ""+ap+" AP; Drug Developer: Add " + ap2 +" AP";
		        for (int i=2; i<6; i++){
		        	options[i].SetActive(false);
		        }
		        inputField.SetActive(false);
		        infoText.GetComponent<Text>().text = actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction] + "? (D6 roll result shown in Action History)";
				break;
			case 24:
				ap = actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				options[0].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && !variables.GetComponent<MainVariables>().empowered[0]);
				optionsText[0].GetComponent<Text>().text = "Use "+ap+" AP";
		        for (int i=1; i<6; i++){
		        	options[i].SetActive(false);
		        }
		        inputField.SetActive(false);
		        infoText.GetComponent<Text>().text = actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction] + "?";
				break;
			case 25:
				ap = actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				options[0].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[0].GetComponent<Text>().text = ""+ap+" AP: Cap Hospitalizations";
				options[1].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[1].GetComponent<Text>().text = ""+ap+" AP: Cap Deaths";
				options[2].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[2].GetComponent<Text>().text = ""+ap+" AP: Cap Research & Development Expenses";
				options[3].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[3].GetComponent<Text>().text = ""+ap+" AP: Cap Medical Supply Shortages";
				options[4].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[4].GetComponent<Text>().text = ""+ap+" AP: Budget Line did not appear Causative";
		        for (int i=5; i<6; i++){
		        	options[i].SetActive(false);
		        }
		        inputField.SetActive(false);
		        infoText.GetComponent<Text>().text = actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction] + "?";
				break;
			case 30:
				ap = actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				options[0].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[0].GetComponent<Text>().text = ""+ap+" AP; Graph Reveal";
				options[1].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[1].GetComponent<Text>().text = ""+ap+" AP; Map Reveal";
		        for (int i=2; i<6; i++){
		        	options[i].SetActive(false);
		        }
		        inputField.SetActive(false);
		        infoText.GetComponent<Text>().text = actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction] + "?";
				break;
			case 31:
				ap = actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				options[0].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[0].GetComponent<Text>().text = "Use "+ap+" AP";
				options[1].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().actionTakenPhase[6][3]>0);
				optionsText[1].GetComponent<Text>().text = "Use "+ap+" AP (Enhancement)";
		        for (int i=2; i<6; i++){
		        	options[i].SetActive(false);
		        }
		        inputField.SetActive(false);
		        infoText.GetComponent<Text>().text = actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction] + "?";
				break;
			case 32:
				ap = actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				options[0].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[0].GetComponent<Text>().text = "Use "+ap+" AP";
				options[1].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().minigameAttemptsSubphase[whichPlayer][2] == 0);
				optionsText[1].GetComponent<Text>().text = ""+ap+" AP; Minigame Won";
				options[2].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().minigameAttemptsSubphase[whichPlayer][2] == 0);
				optionsText[2].GetComponent<Text>().text = ""+ap+" AP; Minigame Lost";
				options[3].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= (ap+1) && variables.GetComponent<MainVariables>().minigameAttemptsSubphase[whichPlayer][2] == 0);
				optionsText[3].GetComponent<Text>().text = ""+ap+" AP; Laboratory Diagnostician: 1 AP; Minigame Won";
				options[4].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= (ap+1) && variables.GetComponent<MainVariables>().minigameAttemptsSubphase[whichPlayer][2] == 0);
				optionsText[4].GetComponent<Text>().text = ""+ap+" AP; Laboratory Diagnostician: 1 AP; Minigame Lost";
		        for (int i=5; i<6; i++){
		        	options[i].SetActive(false);
		        }
		        inputField.SetActive(false);
		        infoText.GetComponent<Text>().text = actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction] + "?";
				break;
			case 33:
				ap = actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				options[0].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && !variables.GetComponent<MainVariables>().empowered[0]);
				optionsText[0].GetComponent<Text>().text = "Use "+ap+" AP";
		        for (int i=1; i<6; i++){
		        	options[i].SetActive(false);
		        }
		        inputField.SetActive(false);
		        infoText.GetComponent<Text>().text = actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction] + "?";
				break;
			case 34:
				break;
			case 35:
				ap = actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				options[0].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[0].GetComponent<Text>().text = ""+ap+" AP; Cap Infections";
				options[1].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[1].GetComponent<Text>().text = ""+ap+" AP; Cap Research & Development Expenses";
				options[2].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[2].GetComponent<Text>().text = ""+ap+" AP; Cap Medical Supply Shortages";
				options[3].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().actionTakenPhase[5][5]!=0);
				optionsText[3].GetComponent<Text>().text = ""+ap+" AP; Cap Infections + Reduce";
				options[4].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().actionTakenPhase[5][5]!=0);
				optionsText[4].GetComponent<Text>().text = ""+ap+" AP; Cap Research & Development Expenses + Reduce";
				options[5].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().actionTakenPhase[5][5]!=0);
				optionsText[5].GetComponent<Text>().text = ""+ap+" AP; Cap Medical Supply Shortages + Reduce";
		        inputField.SetActive(false);
		        infoText.GetComponent<Text>().text = actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction] + "? (D6 result shown in Action History)";
				break;
			case 40:
				ap = actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				options[0].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[0].GetComponent<Text>().text = "Use "+ap+" AP";
		        for (int i=1; i<6; i++){
		        	options[i].SetActive(false);
		        }
		        inputField.SetActive(false);
		        infoText.GetComponent<Text>().text = actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction] + "?";
				break;
			case 41:
				ap = actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				options[0].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[0].GetComponent<Text>().text = "Use "+ap+" AP";
		        for (int i=1; i<6; i++){
		        	options[i].SetActive(false);
		        }
		        inputField.SetActive(false);
		        infoText.GetComponent<Text>().text = actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction] + "?";
				break;
			case 42:
				ap = actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				options[0].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().quizScores[4]>=50);
				optionsText[0].GetComponent<Text>().text = "Use "+ap+" AP";
				options[1].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().quizScores[4]>=50);
				optionsText[1].GetComponent<Text>().text = ""+ap+" AP; Minigame Lost";
				options[2].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().quizScores[4]>=50);
				optionsText[2].GetComponent<Text>().text = ""+ap+" AP; Minigame Won (Input amount above)";
		        for (int i=3; i<6; i++){
		        	options[i].SetActive(false);
		        }
		        inputField.SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().minigameAttemptsSubphase[whichPlayer][4] == 0);
		        inputField.GetComponent<InputField>().text = "0";
		        infoText.GetComponent<Text>().text = actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction] + "?";
				break;
			case 43:
				ap = actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				options[0].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && !variables.GetComponent<MainVariables>().empowered[2]);
				optionsText[0].GetComponent<Text>().text = "Use "+ap+" AP";
				options[1].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && !variables.GetComponent<MainVariables>().empowered[2] && variables.GetComponent<MainVariables>().minigameAttemptsPhase[whichPlayer][2] == 0);
				optionsText[1].GetComponent<Text>().text = ""+ap+" AP; Minigame Won";
				options[2].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && !variables.GetComponent<MainVariables>().empowered[2] && variables.GetComponent<MainVariables>().minigameAttemptsPhase[whichPlayer][2] == 0);
				optionsText[2].GetComponent<Text>().text = ""+ap+" AP; Minigame Lost";
				options[3].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= (ap+1) && !variables.GetComponent<MainVariables>().empowered[2] && variables.GetComponent<MainVariables>().minigameAttemptsPhase[whichPlayer][2] == 0);
				optionsText[3].GetComponent<Text>().text = ""+ap+" AP; Virologist: 1 AP; Minigame Won";
				options[4].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= (ap+1) && !variables.GetComponent<MainVariables>().empowered[2] && variables.GetComponent<MainVariables>().minigameAttemptsPhase[whichPlayer][2] == 0);
				optionsText[4].GetComponent<Text>().text = ""+ap+" AP; Virologist: 1 AP; Minigame Lost";
		        for (int i=5; i<6; i++){
		        	options[i].SetActive(false);
		        }
		        inputField.SetActive(false);
		        infoText.GetComponent<Text>().text = actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction] + "?";
				break;
			case 44:
				ap = actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				options[0].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && !variables.GetComponent<MainVariables>().empowered[3]);
				optionsText[0].GetComponent<Text>().text = "Use "+ap+" AP";
				options[1].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && !variables.GetComponent<MainVariables>().empowered[3] && variables.GetComponent<MainVariables>().minigameAttemptsPhase[whichPlayer][2] == 0);
				optionsText[1].GetComponent<Text>().text = ""+ap+" AP; Minigame Won";
				options[2].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && !variables.GetComponent<MainVariables>().empowered[3] && variables.GetComponent<MainVariables>().minigameAttemptsPhase[whichPlayer][2] == 0);
				optionsText[2].GetComponent<Text>().text = ""+ap+" AP; Minigame Lost";
				options[3].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= (ap+1) && !variables.GetComponent<MainVariables>().empowered[3] && variables.GetComponent<MainVariables>().minigameAttemptsPhase[whichPlayer][2] == 0);
				optionsText[3].GetComponent<Text>().text = ""+ap+" AP; Virologist: 1 AP; Minigame Won";
				options[4].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= (ap+1) && !variables.GetComponent<MainVariables>().empowered[3] && variables.GetComponent<MainVariables>().minigameAttemptsPhase[whichPlayer][2] == 0);
				optionsText[4].GetComponent<Text>().text = ""+ap+" AP; Virologist: 1 AP; Minigame Lost";
		        for (int i=5; i<6; i++){
		        	options[i].SetActive(false);
		        }
		        inputField.SetActive(false);
		        infoText.GetComponent<Text>().text = actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction] + "?";
				break;
			case 45:
				ap = actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				options[0].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[0].GetComponent<Text>().text = ""+ap+" AP: Cap Infections";
				options[1].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[1].GetComponent<Text>().text = ""+ap+" AP: Cap research & Development Expenses";
				options[2].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[2].GetComponent<Text>().text = ""+ap+" AP: Cap Medical Supply Shortages";
		        for (int i=3; i<6; i++){
		        	options[i].SetActive(false);
		        }
		        inputField.SetActive(false);
		        infoText.GetComponent<Text>().text = actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction] + "?";
				break;
			case 50:
				ap = actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				options[0].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[0].GetComponent<Text>().text = "Use "+ap+" AP";
		        for (int i=1; i<6; i++){
		        	options[i].SetActive(false);
		        }
		        inputField.SetActive(false);
		        infoText.GetComponent<Text>().text = actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction] + "?";
				break;
			case 51:
				ap = actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				options[0].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().quizScores[5]>=50);
				optionsText[0].GetComponent<Text>().text = "Use "+ap+" AP";
		        for (int i=1; i<6; i++){
		        	options[i].SetActive(false);
		        }
		        inputField.SetActive(false);
		        infoText.GetComponent<Text>().text = actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction] + "?";
				break;
			case 52:
				ap = actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				ap2 = 2;
				if (variables.GetComponent<MainVariables>().empowered[6]){
					ap2 = Mathf.Max(1, ap2 - variables.GetComponent<MainVariables>().empoweredAmounts[6]);
				}
				options[0].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[0].GetComponent<Text>().text = "Use "+ap+" AP";
				options[1].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().player_AP[6] >= ap2);
				optionsText[1].GetComponent<Text>().text = "" + ap + " AP; Politician: Add "+ap2+" AP; Minigame Lost";
				options[2].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().player_AP[6] >= ap2);
				optionsText[2].GetComponent<Text>().text = "" + ap + " AP; Politician: Add "+ap2+" AP; Minigame Won";
		        for (int i=3; i<6; i++){
		        	options[i].SetActive(false);
		        }
		        inputField.SetActive(false);
		        infoText.GetComponent<Text>().text = actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction] + "?";
				break;
			case 53:
				ap = actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				options[0].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[0].GetComponent<Text>().text = "Use "+ap+" AP";
				options[1].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().quizScores[7]>=50);
				optionsText[1].GetComponent<Text>().text = ""+ap+" AP; Enhanced version";
		        for (int i=2; i<6; i++){
		        	options[i].SetActive(false);
		        }
		        inputField.SetActive(false);
		        infoText.GetComponent<Text>().text = actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction] + "?";
				break;
			case 54:
				ap = actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				options[0].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().CHM_Values[4]<35);
				optionsText[0].GetComponent<Text>().text = "Use "+ap+" AP";
				options[1].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().CHM_Values[4]<35 && variables.GetComponent<MainVariables>().minigameAttemptsSubphase[whichPlayer][1] == 0);
				optionsText[1].GetComponent<Text>().text = ""+ap+" AP; Minigame Lost";
				options[2].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().CHM_Values[4]<35 && variables.GetComponent<MainVariables>().minigameAttemptsSubphase[whichPlayer][1] == 0);
				optionsText[2].GetComponent<Text>().text = ""+ap+" AP; Minigame Won (Input amount (additive) above)";
		        for (int i=3; i<6; i++){
		        	options[i].SetActive(false);
		        }
		        inputField.SetActive(true);
		        inputField.GetComponent<InputField>().text = "0";
		        infoText.GetComponent<Text>().text = actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction] + "?";
				break;
			case 55:
				ap = actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				options[0].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[0].GetComponent<Text>().text = ""+ap+" AP; Cap Infections";
				options[1].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[1].GetComponent<Text>().text = ""+ap+" AP; Cap Research & Development Expenses";
				options[2].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[2].GetComponent<Text>().text = ""+ap+" AP; Cap Medical Supply Shortages";
				options[3].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().actionTakenPhase[3][5]!=0);
				optionsText[3].GetComponent<Text>().text = ""+ap+" AP; Cap Infections + Reduce";
				options[4].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().actionTakenPhase[3][5]!=0);
				optionsText[4].GetComponent<Text>().text = ""+ap+" AP; Cap Research & Development Expenses + Reduce";
				options[5].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().actionTakenPhase[3][5]!=0);
				optionsText[5].GetComponent<Text>().text = ""+ap+" AP; Cap Medical Supply Shortages + Reduce";
		        inputField.SetActive(false);
		        infoText.GetComponent<Text>().text = actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction] + "? (D6 result shown in Action History)";
				break;
			case 60:
				ap = actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				options[0].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[0].GetComponent<Text>().text = ""+ap+" AP; Input tiles above (e.g. E5)";
		        for (int i=1; i<6; i++){
		        	options[i].SetActive(false);
		        }
		        inputField.SetActive(true);
		        inputField.GetComponent<InputField>().text = "E5, E5, E5, E5, E5";
		        infoText.GetComponent<Text>().text = actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction] + "?";
				break;
			case 61:
				ap = actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				options[0].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[0].GetComponent<Text>().text = "Use "+ap+" AP: Community Nonprofit Sector";
				options[1].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[1].GetComponent<Text>().text = "Use "+ap+" AP: Government Sector";
				options[2].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[2].GetComponent<Text>().text = "Use "+ap+" AP: Industrial Sector";
				options[3].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[3].GetComponent<Text>().text = "Use "+ap+" AP: Medical Sector";
		        for (int i=4; i<6; i++){
		        	options[i].SetActive(false);
		        }
		        inputField.SetActive(false);
		        infoText.GetComponent<Text>().text = actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction] + "?";
				break;
			case 62:
				ap = actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				options[0].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[0].GetComponent<Text>().text = "Use "+ap+" AP";
				options[1].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().player_AP[0] >= 1 && variables.GetComponent<MainVariables>().minigameAttemptsPhase[whichPlayer][1] < 2);
				optionsText[1].GetComponent<Text>().text = ""+ap+" AP; Clinician: 1 AP; Minigame Lost";
				options[2].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().player_AP[0] >= 1 && variables.GetComponent<MainVariables>().minigameAttemptsPhase[whichPlayer][1] < 2);
				optionsText[2].GetComponent<Text>().text = ""+ap+" AP; Clinician: 1 AP; Minigame Won (Input amount (additive) above)";
		        for (int i=3; i<6; i++){
		        	options[i].SetActive(false);
		        }
		        inputField.SetActive(true);
		        inputField.GetComponent<InputField>().text = "0";
		        infoText.GetComponent<Text>().text = actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction] + "?";
				break;
			case 63:
				ap = actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				ap2 = 1;//can't be empowered lower anyways
				options[0].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[0].GetComponent<Text>().text = "Use "+ap+" AP";
				options[1].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().player_AP[2] >= ap2);
				optionsText[1].GetComponent<Text>().text = "Drug Developer: Add "+ap2+" AP; Minigame Lost";
				options[2].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().player_AP[2] >= ap2);
				optionsText[2].GetComponent<Text>().text = "Drug Developer: Add "+ap2+" AP; Minigame Won";
		        for (int i=3; i<6; i++){
		        	options[i].SetActive(false);
		        }
		        inputField.SetActive(false);
		        infoText.GetComponent<Text>().text = actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction] + "?";
				break;
			case 64:
				ap = actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				options[0].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && !variables.GetComponent<MainVariables>().empowered[7]);
				optionsText[0].GetComponent<Text>().text = "Use "+ap+" AP";
				options[1].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && !variables.GetComponent<MainVariables>().empowered[7] && variables.GetComponent<MainVariables>().minigameAttemptsPhase[whichPlayer][1] < 2);
				optionsText[1].GetComponent<Text>().text = ""+ap+" AP; Health Behaviorist: 1 AP; Minigame Lost";
				options[2].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && !variables.GetComponent<MainVariables>().empowered[7] && variables.GetComponent<MainVariables>().minigameAttemptsPhase[whichPlayer][1] < 2);
				optionsText[2].GetComponent<Text>().text = ""+ap+" AP; Health Behaviorist: 1 AP; Minigame Won";
		        for (int i=3; i<6; i++){
		        	options[i].SetActive(false);
		        }
		        inputField.SetActive(false);
		        infoText.GetComponent<Text>().text = actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction] + "?";
				break;
			case 65://warning warning Enhancement is not optional ---------------------------------------------------------------------------------------------- warning warning
				tail = "";
				if (variables.GetComponent<MainVariables>().actionTakenPhase[7][5] != 0) tail = " + Reduce";
				ap = actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				options[0].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[0].GetComponent<Text>().text = ""+ap+" AP: Cap Misinformation & Mistrust" + tail;
				options[1].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[1].GetComponent<Text>().text = ""+ap+" AP: Cap Health Disparities" + tail;
				options[2].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[2].GetComponent<Text>().text = ""+ap+" AP: Cap Research & Development Expenses" + tail;
				options[3].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[3].GetComponent<Text>().text = ""+ap+" AP: Cap Medical Supply Shortages" + tail;
				options[4].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[4].GetComponent<Text>().text = ""+ap+" AP: Cap Barriers to Healthcare Access" + tail;
		        for (int i=5; i<6; i++){
		        	options[i].SetActive(false);
		        }
		        inputField.SetActive(false);
		        infoText.GetComponent<Text>().text = actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction] + "?";
				break;
			case 70:
				ap = actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				options[0].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[0].GetComponent<Text>().text = "Use "+ap+" AP";
				options[1].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().player_AP[1] >= 1);
				optionsText[1].GetComponent<Text>().text = ""+ap+" AP; Epidemiologist: 1 AP";
		        for (int i=2; i<6; i++){
		        	options[i].SetActive(false);
		        }
		        inputField.SetActive(false);
		        infoText.GetComponent<Text>().text = actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction] + "?";
				break;
			case 71:
				ap = actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				options[0].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[0].GetComponent<Text>().text = "Use "+ap+" AP";
		        for (int i=1; i<6; i++){
		        	options[i].SetActive(false);
		        }
		        inputField.SetActive(false);
		        infoText.GetComponent<Text>().text = actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction] + "?";
				break;
			case 72:
				ap = actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				ap2 = 2;
				if (variables.GetComponent<MainVariables>().empowered[0]){
					ap2 = Mathf.Max(1, ap2 - variables.GetComponent<MainVariables>().empoweredAmounts[0]);
				}
				options[0].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[0].GetComponent<Text>().text = "Use "+ap+" AP";
				options[1].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().phase==0);
				optionsText[1].GetComponent<Text>().text = ""+ap+" AP; Clinician: Add " + ap2 +" AP";
		        for (int i=2; i<6; i++){
		        	options[i].SetActive(false);
		        }
		        inputField.SetActive(false);
		        infoText.GetComponent<Text>().text = actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction] + "? (D6 roll result shown in Action History)";
				break;
			case 73:
				ap = actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				options[0].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[0].GetComponent<Text>().text = "Use "+ap+" AP";
		        for (int i=1; i<6; i++){
		        	options[i].SetActive(false);
		        }
		        inputField.SetActive(false);
		        infoText.GetComponent<Text>().text = actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction] + "?";
				break;
			case 74:
				ap = actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				options[0].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && !variables.GetComponent<MainVariables>().empowered[0]);
				optionsText[0].GetComponent<Text>().text = ""+ap+" AP; Minigame Lost";
				options[1].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && !variables.GetComponent<MainVariables>().empowered[0]);
				optionsText[1].GetComponent<Text>().text = ""+ap+" AP; Minigame Won";
		        for (int i=2; i<6; i++){
		        	options[i].SetActive(false);
		        }
		        inputField.SetActive(false);
		        infoText.GetComponent<Text>().text = actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction] + "?";
				break;
			case 75:
				ap = actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				options[0].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[0].GetComponent<Text>().text = "Use "+ap+" AP: Deaths";
				options[1].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[1].GetComponent<Text>().text = "Use "+ap+" AP: Anxiety & Depression";
				options[2].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[2].GetComponent<Text>().text = "Use "+ap+" AP: Misinformation & Mistrust";
				options[3].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().actionTakenPhase[6][5]!=0);
				optionsText[3].GetComponent<Text>().text = "Use "+ap+" AP: Deaths + Reduce";
				options[4].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().actionTakenPhase[6][5]!=0);
				optionsText[4].GetComponent<Text>().text = "Use "+ap+" AP: Anxiety & Depression + Reduce";
				options[5].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap && variables.GetComponent<MainVariables>().actionTakenPhase[6][5]!=0);
				optionsText[5].GetComponent<Text>().text = "Use "+ap+" AP: Misinformation & Mistrust + Reduce";
		        inputField.SetActive(false);
		        infoText.GetComponent<Text>().text = actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction] + "? (D6 roll result shown in Action History)";
				break;	
    		default:
				ap = actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				options[0].SetActive(variables.GetComponent<MainVariables>().player_AP[whichPlayer] >= ap);
				optionsText[0].GetComponent<Text>().text = "Use "+ap+" AP";
		        for (int i=1; i<6; i++){
		        	options[i].SetActive(false);
		        }
		        inputField.SetActive(false);
		        infoText.GetComponent<Text>().text = actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction] + "?";
    			break;
    	}

    	if (prereqsWaived){
	        for (int i=0; i<6; i++){
		        options[i].SetActive(true);
	        }
    	}
    }

    //actually implementing the effects of an action if a confirm button is pressed
    //obviously each action needs this as well
    void performAction() {
    	int ddev_beginning_ap = variables.GetComponent<MainVariables>().player_AP[2];
    	switch (whichPlayer*10 + whichAction) {
    		case 0:
				variables.GetComponent<MainVariables>().CHM_Graph_Reveals[1] = Mathf.Max(1, variables.GetComponent<MainVariables>().CHM_Graph_Reveals[1]);
				variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];;
				variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
				variables.GetComponent<MainVariables>().saveAction(whichPlayer, whichAction, "Option: "+whichChoice + " "+actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction]);
				break;
			case 1:
				variables.GetComponent<MainVariables>().CHM_Values[1] -= 3;
				variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];;
				variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
				variables.GetComponent<MainVariables>().saveAction(whichPlayer, whichAction, "Option: "+whichChoice + " "+actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction]);
				break;
			case 2:
				int roll1 = 0;
				int roll2 = 0;
				int roll3 = 0;
				roll1 = rollDie(6);
				if (whichChoice==1) {
					roll2 = rollDie(6);
					roll3 = rollDie(6);
				}
				variables.GetComponent<MainVariables>().CHM_Values[2] -= (roll1 + roll2 + roll3);
				variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];;
				variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
				variables.GetComponent<MainVariables>().saveAction(whichPlayer, whichAction, "Option: "+whichChoice + " Roll: "+roll1 + "+"+roll2 + "+"+roll3 + " "+actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction]);
				break;
			case 3:
				variables.GetComponent<MainVariables>().CHM_Values[5] -= 1;
				variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];;
				variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
				variables.GetComponent<MainVariables>().saveAction(whichPlayer, whichAction, "Option: "+whichChoice + " "+actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction]);
				break;
			case 4:
				variables.GetComponent<MainVariables>().CHM_Values[3] -= 2;
				if (whichChoice==1){
					variables.GetComponent<MainVariables>().CHM_Values[3] -= 4;
				}
				variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];;
				variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
				variables.GetComponent<MainVariables>().saveAction(whichPlayer, whichAction, "Option: "+whichChoice + " "+actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction]);
				break;
			case 5:
				variables.GetComponent<MainVariables>().CHM_Caps[1+whichChoice] = true;
				variables.GetComponent<MainVariables>().CHM_Cap_Durations[1+whichChoice] = 1;
				variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];;
				variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
				variables.GetComponent<MainVariables>().saveAction(whichPlayer, whichAction, "Option: "+whichChoice + " "+actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction]);
				break;
    		case 10:
				int[] my_choice = new int[] {0, 1, 2, 3, 5, 8};
				variables.GetComponent<MainVariables>().CHM_Graph_Reveals[my_choice[whichChoice]] = Mathf.Max(1, variables.GetComponent<MainVariables>().CHM_Graph_Reveals[my_choice[whichChoice]]);
				variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];;
				variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
				variables.GetComponent<MainVariables>().saveAction(whichPlayer, whichAction, "Option: "+whichChoice + " "+actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction]);
				break;
			case 11:
				roll1 = 0;
				roll2 = 0;
				if (variables.GetComponent<MainVariables>().actionTakenGame[0][0] != 0) {
					roll1 = rollDie(6);
					roll2 = rollDie(6);
				}
				my_choice = new int[] {0, 1, 2, 3, 5, 8};
				variables.GetComponent<MainVariables>().CHM_Graph_Reveals[my_choice[whichChoice]] = 2;
				variables.GetComponent<MainVariables>().CHM_Values[my_choice[whichChoice]] -= (roll1 + roll2);
				variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];;
				variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
				variables.GetComponent<MainVariables>().saveAction(whichPlayer, whichAction, "Option: "+whichChoice + " Roll: "+roll1 + "+"+roll2+" "+actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction]);
				break;
			case 12:
				revealTiles(inputField.GetComponent<InputField>().text, 1, true);
				variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];;
				variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
				variables.GetComponent<MainVariables>().saveAction(whichPlayer, whichAction, "Option: "+whichChoice + " Input: "+inputField.GetComponent<InputField>().text+" " + " "+actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction]);
				break;
			case 13:
				revealTiles(inputField.GetComponent<InputField>().text, 5);
				if (whichChoice==1) {
					variables.GetComponent<MainVariables>().player_AP[6] -= 1;
				}
				variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];;
				variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
				variables.GetComponent<MainVariables>().saveAction(whichPlayer, whichAction, "Option: "+whichChoice + " Input: "+inputField.GetComponent<InputField>().text+" " + " "+actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction]);
				break;
			case 14:
				my_choice = new int[] {0, 1, 2, 3, 5, 8};
				for (int i=0; i<9; i++){
					for (int j=0; j<9; j++){
						if (variables.GetComponent<MainVariables>().Buildings[i][j]==my_choice[whichChoice])
							variables.GetComponent<MainVariables>().Map_Revealed[i][j] = true;
					}
				}
				variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];;
				variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
				variables.GetComponent<MainVariables>().saveAction(whichPlayer, whichAction, "Option: "+whichChoice + " "+actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction]);
				break;
			case 15:
				my_choice = new int[] {0, 1, 2, 3, 5, 8};
				variables.GetComponent<MainVariables>().CHM_Graph_Reveals[my_choice[whichChoice]] = Mathf.Max(1, variables.GetComponent<MainVariables>().CHM_Graph_Reveals[my_choice[whichChoice]]);
				variables.GetComponent<MainVariables>().CHM_Caps[my_choice[whichChoice]] = true;
				if (variables.GetComponent<MainVariables>().actionTakenGame[7][2]==0){
					variables.GetComponent<MainVariables>().CHM_Cap_Durations[my_choice[whichChoice]] = 1;
				}
				else {
					variables.GetComponent<MainVariables>().CHM_Cap_Durations[my_choice[whichChoice]] = 3;
				}
				variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];;
				variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
				variables.GetComponent<MainVariables>().saveAction(whichPlayer, whichAction, "Option: "+whichChoice + " "+actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction]);
				break;
    		case 20:
				roll1 = 0;
				if (whichChoice==1) {
					roll1 = rollDie(6);
				}
				variables.GetComponent<MainVariables>().CHM_Graph_Reveals[6] = Mathf.Max(1, variables.GetComponent<MainVariables>().CHM_Graph_Reveals[6]);
				if (roll1>3) variables.GetComponent<MainVariables>().CHM_Graph_Reveals[6] = 2;
				variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];;
				variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
				variables.GetComponent<MainVariables>().saveAction(whichPlayer, whichAction, "Option: "+whichChoice + " Roll: "+roll1 + " "+actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction]);
				break;
			case 21:
				if (whichChoice!=0){
					variables.GetComponent<MainVariables>().minigameAttemptsPhase[whichPlayer][4]++;
					variables.GetComponent<MainVariables>().minigameAttemptsSubphase[whichPlayer][4]++;
				}
				variables.GetComponent<MainVariables>().CHM_Values[7] -= 2;
				if (whichChoice==2){
					variables.GetComponent<MainVariables>().CHM_Values[7] -= int.Parse(inputField.GetComponent<InputField>().text);
				}
				variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];;
				variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
				variables.GetComponent<MainVariables>().saveAction(whichPlayer, whichAction, "Option: "+whichChoice + " Input: "+inputField.GetComponent<InputField>().text+" "+actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction]);
				break;
			case 22:
				if (whichChoice!=0){
					variables.GetComponent<MainVariables>().minigameAttemptsPhase[whichPlayer][4]++;
					variables.GetComponent<MainVariables>().minigameAttemptsSubphase[whichPlayer][4]++;
				}
				variables.GetComponent<MainVariables>().CHM_Values[1] -= 2;
				if (whichChoice==2){
					variables.GetComponent<MainVariables>().CHM_Values[1] -= int.Parse(inputField.GetComponent<InputField>().text);
				}
				variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];;
				variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
				variables.GetComponent<MainVariables>().saveAction(whichPlayer, whichAction, "Option: "+whichChoice + " Input: "+inputField.GetComponent<InputField>().text+" "+actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction]);
				break;
			case 23:
				int ap2 = 3;
				if (variables.GetComponent<MainVariables>().empowered[2]){
					ap2 = Mathf.Max(1, ap2 - variables.GetComponent<MainVariables>().empoweredAmounts[2]);
				}
				int rollResult = 0;
				if (whichChoice==1) rollResult = rollDie(6);
				variables.GetComponent<MainVariables>().CHM_RDE_Phase_Reduction += rollResult;
				variables.GetComponent<MainVariables>().CHM_Values[6] -= 3;
				variables.GetComponent<MainVariables>().player_AP[2] -= ap2;
				variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];;
				variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
				variables.GetComponent<MainVariables>().saveAction(whichPlayer, whichAction, "Option: "+whichChoice + " Roll: "+rollResult+" "+actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction]);
				break;
			case 24:
				variables.GetComponent<MainVariables>().empowered[0] = true;
				variables.GetComponent<MainVariables>().empoweredAmounts[0] = 2;
				variables.GetComponent<MainVariables>().empoweredDurations[0] = 1;
				variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];;
				variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
				variables.GetComponent<MainVariables>().saveAction(whichPlayer, whichAction, "Option: "+whichChoice + " "+actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction]);
				break;
			case 25:
				my_choice = new int[] {1, 2, 6, 7};
				if (whichChoice!=4){
					variables.GetComponent<MainVariables>().CHM_Caps[my_choice[whichChoice]] = true;
					variables.GetComponent<MainVariables>().CHM_Cap_Durations[my_choice[whichChoice]] = 1;
				}
				variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];;
				variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
				variables.GetComponent<MainVariables>().saveAction(whichPlayer, whichAction, "Option: "+whichChoice + " "+actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction]);
				break;
			case 30:
				if (whichChoice==0){
					variables.GetComponent<MainVariables>().CHM_Graph_Reveals[0] = 2;
				}
				else {
					for (int i=0; i<9; i++){
						for (int j=0; j<9; j++){
							if (variables.GetComponent<MainVariables>().Buildings[i][j]==0)
								variables.GetComponent<MainVariables>().Map_Revealed[i][j] = true;
						}
					}
				}
				variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];;
				variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
				variables.GetComponent<MainVariables>().saveAction(whichPlayer, whichAction, "Option: "+whichChoice + " "+actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction]);
				break;
			case 31:
				variables.GetComponent<MainVariables>().CHM_Values[7] -= 2;
				if (whichChoice==1){
					variables.GetComponent<MainVariables>().CHM_Values[7] -= 2;
				}
				variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];;
				variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
				variables.GetComponent<MainVariables>().saveAction(whichPlayer, whichAction, "Option: "+whichChoice + " "+actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction]);
				break;
			case 32:
				variables.GetComponent<MainVariables>().CHM_Values[1] -= 3;
				if (whichChoice%2 == 1){
					variables.GetComponent<MainVariables>().CHM_Values[1] -= 6;
				}
				if (whichChoice>=3){
					variables.GetComponent<MainVariables>().player_AP[3] -= 1;//Med Tech doesn't exist anymore?????
				}
				if (whichChoice!=0){
					variables.GetComponent<MainVariables>().minigameAttemptsSubphase[whichPlayer][2]++;
					variables.GetComponent<MainVariables>().minigameAttemptsPhase[whichPlayer][2]++;
				}
				variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];;
				variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
				variables.GetComponent<MainVariables>().saveAction(whichPlayer, whichAction, "Option: "+whichChoice + " "+actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction]);
				break;
			case 33:
				variables.GetComponent<MainVariables>().empowered[0] = true;
				variables.GetComponent<MainVariables>().empoweredAmounts[0] = 1;
				variables.GetComponent<MainVariables>().empoweredDurations[0] = 1;
				variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];;
				variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
				variables.GetComponent<MainVariables>().saveAction(whichPlayer, whichAction, "Option: "+whichChoice + " "+actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction]);
				break;
			case 34:
				break;
			case 35:
				roll1 = 0;
				if (whichChoice>=3) {
					roll1 = rollDie(6);
				}
				int[] thirtyfive = new int[] {0, 6, 7};
				variables.GetComponent<MainVariables>().CHM_Caps[thirtyfive[whichChoice%3]] = true;
				variables.GetComponent<MainVariables>().CHM_Cap_Durations[thirtyfive[whichChoice%3]] = 1;
				variables.GetComponent<MainVariables>().CHM_Values[0] -= roll1;
				variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];;
				variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
				variables.GetComponent<MainVariables>().saveAction(whichPlayer, whichAction, "Option: "+whichChoice + " Roll: "+roll1 + " "+actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction]);
				break;
			case 40:
				variables.GetComponent<MainVariables>().CHM_Graph_Reveals[5] = Mathf.Max(1, variables.GetComponent<MainVariables>().CHM_Graph_Reveals[5]);
				variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];;
				variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
				variables.GetComponent<MainVariables>().saveAction(whichPlayer, whichAction, "Option: "+whichChoice + " "+actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction]);
				break;
			case 41:
				for (int i=0; i<9; i++){
					for (int j=0; j<9; j++){
						if (variables.GetComponent<MainVariables>().Buildings[i][j]==0)
							variables.GetComponent<MainVariables>().Map_Revealed[i][j] = true;
					}
				}
				variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];;
				variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
				variables.GetComponent<MainVariables>().saveAction(whichPlayer, whichAction, "Option: "+whichChoice + " "+actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction]);
				break;
			case 42:
				if (whichChoice!=0){
					variables.GetComponent<MainVariables>().minigameAttemptsPhase[whichPlayer][4]++;
					variables.GetComponent<MainVariables>().minigameAttemptsSubphase[whichPlayer][4]++;
				}
				variables.GetComponent<MainVariables>().CHM_Values[6] -= 3;
				if (whichChoice==2){
					variables.GetComponent<MainVariables>().CHM_Values[6] -= int.Parse(inputField.GetComponent<InputField>().text);
				}
				variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];;
				variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
				variables.GetComponent<MainVariables>().saveAction(whichPlayer, whichAction, "Option: "+whichChoice + " Input: "+inputField.GetComponent<InputField>().text+" "+actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction]);
				break;
			case 43:
				variables.GetComponent<MainVariables>().empowered[2] = true;
				variables.GetComponent<MainVariables>().empoweredAmounts[2] = 2;
				if (whichChoice%2 == 1){
					variables.GetComponent<MainVariables>().empoweredDurations[2] = 2;
				}
				else{
					variables.GetComponent<MainVariables>().empoweredDurations[2] = 1;
				}
				if (whichChoice>=3){
					variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= 1;
				}
				if (whichChoice!=0){
					variables.GetComponent<MainVariables>().minigameAttemptsSubphase[whichPlayer][2]++;
					variables.GetComponent<MainVariables>().minigameAttemptsPhase[whichPlayer][2]++;
				}
				variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];;
				variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
				variables.GetComponent<MainVariables>().saveAction(whichPlayer, whichAction, "Option: "+whichChoice + " "+actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction]);
				break;
			case 44:
				variables.GetComponent<MainVariables>().empowered[3] = true;
				variables.GetComponent<MainVariables>().empoweredAmounts[3] = 2;
				if (whichChoice%2 == 1){
					variables.GetComponent<MainVariables>().empoweredDurations[3] = 2;
				}
				else{
					variables.GetComponent<MainVariables>().empoweredDurations[3] = 1;
				}
				if (whichChoice>=3){
					variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= 1;
				}
				if (whichChoice!=0){
					variables.GetComponent<MainVariables>().minigameAttemptsSubphase[whichPlayer][2]++;
					variables.GetComponent<MainVariables>().minigameAttemptsPhase[whichPlayer][2]++;
				}
				variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];;
				variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
				variables.GetComponent<MainVariables>().saveAction(whichPlayer, whichAction, "Option: "+whichChoice + " "+actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction]);
				break;
			case 45:
				my_choice = new int[] {0, 6, 7};
				variables.GetComponent<MainVariables>().CHM_Caps[my_choice[whichChoice]] = true;
				variables.GetComponent<MainVariables>().CHM_Cap_Durations[my_choice[whichChoice]] = 1;
				variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];;
				variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
				variables.GetComponent<MainVariables>().saveAction(whichPlayer, whichAction, "Option: "+whichChoice + " "+actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction]);
				break;
			case 50:
				variables.GetComponent<MainVariables>().CHM_Graph_Reveals[4] = 2;
				variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];;
				variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
				variables.GetComponent<MainVariables>().saveAction(whichPlayer, whichAction, "Option: "+whichChoice + " "+actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction]);
				break;
			case 51:
				for (int i=0; i<9; i++){
					for (int j=0; j<9; j++){
						if (variables.GetComponent<MainVariables>().Buildings[i][j]==4)
							variables.GetComponent<MainVariables>().Map_Revealed[i][j] = true;
					}
				}
				variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];;
				variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
				variables.GetComponent<MainVariables>().saveAction(whichPlayer, whichAction, "Option: "+whichChoice + " "+actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction]);
				break;
			case 52:
				variables.GetComponent<MainVariables>().CHM_Values[4] -= 2;
				if (whichChoice==2){
					variables.GetComponent<MainVariables>().CHM_Values[4] -= 4;
				}
				ap2 = 2;
				if (variables.GetComponent<MainVariables>().empowered[6]){
					ap2 = Mathf.Max(1, ap2 - variables.GetComponent<MainVariables>().empoweredAmounts[6]);
				}
				variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];
				if (whichChoice!=0) {
					variables.GetComponent<MainVariables>().player_AP[6] -= ap2;
					variables.GetComponent<MainVariables>().minigameAttemptsPhase[whichPlayer][0]++;
					variables.GetComponent<MainVariables>().minigameAttemptsSubphase[whichPlayer][0]++;
				}
				variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
				variables.GetComponent<MainVariables>().saveAction(whichPlayer, whichAction, "Option: "+whichChoice + " "+actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction]);
				break;
			case 53:
				variables.GetComponent<MainVariables>().CHM_Values[0] -= (1 + whichChoice);
				variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];;
				variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
				variables.GetComponent<MainVariables>().saveAction(whichPlayer, whichAction, "Option: "+whichChoice + " "+actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction]);
				break;
			case 54:
				int fiftyfour = 0;
				if (whichChoice==2){
					fiftyfour = int.Parse(inputField.GetComponent<InputField>().text);
				}
				variables.GetComponent<MainVariables>().CHM_Values[5] -= 1;
				variables.GetComponent<MainVariables>().CHM_Values[8] -= 1;
				variables.GetComponent<MainVariables>().CHM_Values[5] -= fiftyfour;
				variables.GetComponent<MainVariables>().CHM_Values[8] -= fiftyfour;
				if (whichChoice!=0){
					variables.GetComponent<MainVariables>().minigameAttemptsPhase[whichPlayer][1]++;
					variables.GetComponent<MainVariables>().minigameAttemptsSubphase[whichPlayer][1]++;
				}
				variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];;
				variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
				variables.GetComponent<MainVariables>().saveAction(whichPlayer, whichAction, "Option: "+whichChoice + " Input: "+inputField.GetComponent<InputField>().text + " "+actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction]);
				break;
			case 55:
				roll1 = 0;
				if (whichChoice>=3) {
					roll1 = rollDie(6);
				}
				thirtyfive = new int[] {0, 6, 7};//ironically, it is used here as well cuz they're practically duplicate actions lol
				variables.GetComponent<MainVariables>().CHM_Caps[thirtyfive[whichChoice%3]] = true;
				variables.GetComponent<MainVariables>().CHM_Cap_Durations[thirtyfive[whichChoice%3]] = 1;
				variables.GetComponent<MainVariables>().CHM_Values[0] -= roll1;
				variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];;
				variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
				variables.GetComponent<MainVariables>().saveAction(whichPlayer, whichAction, "Option: "+whichChoice + " Roll: "+roll1 + " "+actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction]);
				break;
			case 60:
				revealTiles(inputField.GetComponent<InputField>().text, 5);
				variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];;
				variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
				variables.GetComponent<MainVariables>().saveAction(whichPlayer, whichAction, "Option: "+whichChoice + " Input: "+inputField.GetComponent<InputField>().text+" " + " "+actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction]);
				break;
			case 61:
				for (int i=0; i<9; i++){
					for (int j=0; j<9; j++){
						if (variables.GetComponent<MainVariables>().Buildings[i][j]==10+whichChoice)
							variables.GetComponent<MainVariables>().Map_Revealed[i][j] = true;
					}
				}
				variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];;
				variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
				variables.GetComponent<MainVariables>().saveAction(whichPlayer, whichAction, "Option: "+whichChoice + " "+actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction]);
				break;
			case 62:
				fiftyfour = 0;//ironically using this number again just like thirty five aaaaaaaaa
				if (whichChoice==2){
					fiftyfour = int.Parse(inputField.GetComponent<InputField>().text);
				}
				variables.GetComponent<MainVariables>().CHM_Values[4] -= 1;
				variables.GetComponent<MainVariables>().CHM_Values[5] -= 1;
				variables.GetComponent<MainVariables>().CHM_Values[8] -= 1;
				variables.GetComponent<MainVariables>().CHM_Values[4] -= fiftyfour;
				variables.GetComponent<MainVariables>().CHM_Values[5] -= fiftyfour;
				variables.GetComponent<MainVariables>().CHM_Values[8] -= fiftyfour;
				if (whichChoice!=0){
					variables.GetComponent<MainVariables>().minigameAttemptsPhase[whichPlayer][1]++;
					variables.GetComponent<MainVariables>().minigameAttemptsSubphase[whichPlayer][1]++;
					variables.GetComponent<MainVariables>().player_AP[0] -= 1;
				}
				variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];;
				variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
				variables.GetComponent<MainVariables>().saveAction(whichPlayer, whichAction, "Option: "+whichChoice + " Input: "+inputField.GetComponent<InputField>().text + " "+actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction]);
				break;
			case 63:
				variables.GetComponent<MainVariables>().CHM_Values[6] -= 1;
				variables.GetComponent<MainVariables>().CHM_Values[7] -= 1;
				if (whichChoice==2){
					variables.GetComponent<MainVariables>().CHM_Values[6] -= 1;
					variables.GetComponent<MainVariables>().CHM_Values[7] -= 1;
				}
				if (whichChoice!=0){
					variables.GetComponent<MainVariables>().minigameAttemptsPhase[whichPlayer][0]++;
					variables.GetComponent<MainVariables>().minigameAttemptsSubphase[whichPlayer][0]++;
					variables.GetComponent<MainVariables>().player_AP[2] -= 1;
				}
				variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];;
				variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
				variables.GetComponent<MainVariables>().saveAction(whichPlayer, whichAction, "Option: "+whichChoice + " "+actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction]);
				break;
			case 64:
				variables.GetComponent<MainVariables>().empowered[7] = true;
				variables.GetComponent<MainVariables>().empoweredAmounts[7] = 2;
				if (whichChoice!=0){
					variables.GetComponent<MainVariables>().minigameAttemptsSubphase[whichPlayer][1]++;
					variables.GetComponent<MainVariables>().minigameAttemptsPhase[whichPlayer][1]++;
					variables.GetComponent<MainVariables>().player_AP[7] -= 1;

				}
				if (whichChoice!=2){
					variables.GetComponent<MainVariables>().empoweredDurations[7] = 1;
				}
				else {
					variables.GetComponent<MainVariables>().empoweredDurations[7] = 2;
				}
				variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];;
				variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
				variables.GetComponent<MainVariables>().saveAction(whichPlayer, whichAction, "Option: "+whichChoice + " "+actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction]);
				break;
			case 65:
				rollResult = 0;
				if (variables.GetComponent<MainVariables>().actionTakenPhase[7][5] != 0) rollResult = rollDie(6);
				variables.GetComponent<MainVariables>().CHM_Caps[4 + whichChoice] = true;
				variables.GetComponent<MainVariables>().CHM_Cap_Durations[4 + whichChoice] = 1;//just for the subphase?
				variables.GetComponent<MainVariables>().CHM_Values[4] -= rollResult;
				variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];;
				variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
				variables.GetComponent<MainVariables>().saveAction(whichPlayer, whichAction, "Option: "+whichChoice + " Roll: "+rollResult + " "+actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction]);
				break;
			case 70:
				variables.GetComponent<MainVariables>().CHM_Graph_Reveals[3] = 1+whichChoice;
				variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];;
				variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
				variables.GetComponent<MainVariables>().saveAction(whichPlayer, whichAction, "Option: "+whichChoice + " "+actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction]);
				break;
			case 71:
				for (int i=0; i<9; i++){
					for (int j=0; j<9; j++){
						if (variables.GetComponent<MainVariables>().Buildings[i][j]==3)
							variables.GetComponent<MainVariables>().Map_Revealed[i][j] = true;
					}
				}
				variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];;
				variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
				variables.GetComponent<MainVariables>().saveAction(whichPlayer, whichAction, "Option: "+whichChoice + " "+actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction]);
				break;
			case 72:
				ap2 = 2;
				if (variables.GetComponent<MainVariables>().empowered[0]){
					ap2 = Mathf.Max(1, ap2 - variables.GetComponent<MainVariables>().empoweredAmounts[0]);
				}
				rollResult = 0;
				if (whichChoice==1) rollResult = rollDie(6);
				variables.GetComponent<MainVariables>().CHM_Deaths_Phase_Reduction += rollResult;
				variables.GetComponent<MainVariables>().CHM_Values[3] -= 2;
				variables.GetComponent<MainVariables>().CHM_Values[8] -= 2;
				variables.GetComponent<MainVariables>().player_AP[0] -= ap2;
				variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];;
				variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
				variables.GetComponent<MainVariables>().saveAction(whichPlayer, whichAction, "Option: "+whichChoice + " Roll: "+rollResult+" "+actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction]);
				break;
			case 73:
				variables.GetComponent<MainVariables>().CHM_Values[3] -= 2;
				variables.GetComponent<MainVariables>().CHM_Values[4] -= 2;
				variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];;
				variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
				variables.GetComponent<MainVariables>().saveAction(whichPlayer, whichAction, "Option: "+whichChoice + " "+actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction]);
				break;
			case 74:
				variables.GetComponent<MainVariables>().minigameAttemptsPhase[whichPlayer][3]++;
				variables.GetComponent<MainVariables>().minigameAttemptsSubphase[whichPlayer][3]++;
				variables.GetComponent<MainVariables>().empowered[0] = true;
				variables.GetComponent<MainVariables>().empoweredAmounts[0] = 1;
				variables.GetComponent<MainVariables>().empoweredDurations[0] = whichChoice+1;
				variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];;
				variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
				variables.GetComponent<MainVariables>().saveAction(whichPlayer, whichAction, "Option: "+whichChoice + " "+actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction]);
				break;
			case 75:
				rollResult = 0;
				if (whichChoice>2) rollResult = rollDie(6);
				variables.GetComponent<MainVariables>().CHM_Caps[2 + (whichChoice%3)] = true;
				variables.GetComponent<MainVariables>().CHM_Cap_Durations[2 + (whichChoice%3)] = 1;//just for the subphase?
				variables.GetComponent<MainVariables>().CHM_Values[4] -= rollResult;
				variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];;
				variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
				variables.GetComponent<MainVariables>().saveAction(whichPlayer, whichAction, "Option: "+whichChoice + " Roll: "+rollResult+" "+actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction]);
				break;	
    		default:
				variables.GetComponent<MainVariables>().player_AP[whichPlayer] -= actionVariables.GetComponent<ActionDescriptions>().AP_Costs[whichPlayer][whichAction];;
				variables.GetComponent<MainVariables>().turnTaken[whichPlayer] = true;
				variables.GetComponent<MainVariables>().saveAction(whichPlayer, whichAction, "Option: "+whichChoice + " "+actionVariables.GetComponent<ActionDescriptions>().Descriptions[whichPlayer][whichAction]);
    			break;
    	}

    	variables.GetComponent<MainVariables>().actionTakenGame[whichPlayer][whichAction]++;
    	variables.GetComponent<MainVariables>().actionTakenPhase[whichPlayer][whichAction]++;
    	variables.GetComponent<MainVariables>().actionTakenSubphase[whichPlayer][whichAction]++;
    	variables.GetComponent<MainVariables>().ddev_ap_spent += Mathf.Max(0, (ddev_beginning_ap - variables.GetComponent<MainVariables>().player_AP[2]));
    	recentlyTakenAction = 10;
    	playerChart.GetComponent<ActionTrackerManager>().cancelled();
    }

    //helper function to reveal a number of tiles given by a string
    void revealTiles (string tileString, int number, bool reduce = false){
    	for (int i=0; i<number; i++){
    		int y = -1;
    		switch (tileString[4*i]){
    			case 'A':
    				y=0;
    				break;
    			case 'B':
    				y=1;
    				break;
    			case 'C':
    				y=2;
    				break;
    			case 'D':
    				y=3;
    				break;
    			case 'E':
    				y=4;
    				break;
    			case 'F':
    				y=5;
    				break;
    			case 'G':
    				y=6;
    				break;
    			case 'H':
    				y=7;
    				break;
    			case 'I':
    				y=8;
    				break;
    			default:
    				break;
    			}
			if (y==-1) return;
			mapManager.GetComponent<MapManagement>().toggleTileAtLoc(y, int.Parse(""+tileString[4*i+1])-1, true);
			if (reduce && variables.GetComponent<MainVariables>().Buildings[y][int.Parse(""+tileString[4*i+1])-1] >=0
					 && variables.GetComponent<MainVariables>().Buildings[y][int.Parse(""+tileString[4*i+1])-1] <9){
				variables.GetComponent<MainVariables>().CHM_Values[variables.GetComponent<MainVariables>().Buildings[y][int.Parse(""+tileString[4*i+1])-1]] -= 5;
    		}
    	}
    }

    //dice rolling function, if anything fun wants to be done with it
    int rollDie(int Sides){
    	return randomizer.Next(1, Sides+1);
    }
}
