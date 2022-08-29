using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//script for when a phase or subphase moves on
//it resets a lot of things to 0
public class SessionManagement : MonoBehaviour
{
    public GameObject variables, playerChart, actionSpecifics;
    public GameObject phaseText, subphaseText;
    public GameObject playersToggle, playersCanvas;
    string[] subphaseLetters = {"A", "B"};

    // Start is called before the first frame update
    void Start()
    {
        Display();
    }

    // Update is called once per frame
    void Update()
    {
        if (variables.GetComponent<MainVariables>().popupSignalLive && variables.GetComponent<MainVariables>().popupSignalNum==8){
        	//next phase
        	variables.GetComponent<MainVariables>().popupSignalLive = false;
        	variables.GetComponent<MainVariables>().phase++;
        	if (variables.GetComponent<MainVariables>().phase>=3){//loop around
        		variables.GetComponent<MainVariables>().phase=0;
        	}
        	variables.GetComponent<MainVariables>().subphase = 0;
        	variables.GetComponent<MainVariables>().empoweredDurations = new int[] {0, 0, 0, 0, 0, 0, 0, 0};
        	variables.GetComponent<MainVariables>().empoweredAmounts = new int[] {0, 0, 0, 0, 0, 0, 0, 0};
        	variables.GetComponent<MainVariables>().empowered = new bool[] {false, false, false, false, false, false, false, false};
	    	variables.GetComponent<MainVariables>().minigameAttemptsPhase = new int [][] {
	    		new int[] {0, 0, 0, 0, 0},
	    		new int[] {0, 0, 0, 0, 0},
	    		new int[] {0, 0, 0, 0, 0},
	    		new int[] {0, 0, 0, 0, 0},
	    		new int[] {0, 0, 0, 0, 0},
	    		new int[] {0, 0, 0, 0, 0},
	    		new int[] {0, 0, 0, 0, 0},
	    		new int[] {0, 0, 0, 0, 0}
	    	};
	    	variables.GetComponent<MainVariables>().minigameAttemptsSubphase = new int [][] {
	    		new int[] {0, 0, 0, 0, 0},
	    		new int[] {0, 0, 0, 0, 0},
	    		new int[] {0, 0, 0, 0, 0},
	    		new int[] {0, 0, 0, 0, 0},
	    		new int[] {0, 0, 0, 0, 0},
	    		new int[] {0, 0, 0, 0, 0},
	    		new int[] {0, 0, 0, 0, 0},
	    		new int[] {0, 0, 0, 0, 0}
	    	};
	    	variables.GetComponent<MainVariables>().actionTakenPhase = new int [][] {
	    		new int[] {0, 0, 0, 0, 0, 0},
	    		new int[] {0, 0, 0, 0, 0, 0},
	    		new int[] {0, 0, 0, 0, 0, 0},
	    		new int[] {0, 0, 0, 0, 0, 0},
	    		new int[] {0, 0, 0, 0, 0, 0},
	    		new int[] {0, 0, 0, 0, 0, 0},
	    		new int[] {0, 0, 0, 0, 0, 0},
	    		new int[] {0, 0, 0, 0, 0, 0}
	    	};
	    	variables.GetComponent<MainVariables>().actionTakenSubphase = new int [][] {
	    		new int[] {0, 0, 0, 0, 0, 0},
	    		new int[] {0, 0, 0, 0, 0, 0},
	    		new int[] {0, 0, 0, 0, 0, 0},
	    		new int[] {0, 0, 0, 0, 0, 0},
	    		new int[] {0, 0, 0, 0, 0, 0},
	    		new int[] {0, 0, 0, 0, 0, 0},
	    		new int[] {0, 0, 0, 0, 0, 0},
	    		new int[] {0, 0, 0, 0, 0, 0}
	    	};
	    	for (int i=0; i<9; i++){
	    		if (variables.GetComponent<MainVariables>().CHM_Cap_Durations[i]<=2){
	    			variables.GetComponent<MainVariables>().CHM_Cap_Durations[i]=0;
	    			variables.GetComponent<MainVariables>().CHM_Caps[i] = false;
	    		}
	    	}
	    	variables.GetComponent<MainVariables>().CHM_Values[2] -= variables.GetComponent<MainVariables>().CHM_Deaths_Phase_Reduction;
	    	variables.GetComponent<MainVariables>().CHM_Values[6] -= variables.GetComponent<MainVariables>().CHM_RDE_Phase_Reduction;
	    	variables.GetComponent<MainVariables>().ddev_ap_spent = 0;
        	actionSpecifics.GetComponent<ActionSpecificsScript>().recentlyTakenAction = 10;
        	playerChart.GetComponent<ActionTrackerManager>().cancelled();
        }
        if (variables.GetComponent<MainVariables>().popupSignalLive && variables.GetComponent<MainVariables>().popupSignalNum==9){
        	//next subphase
        	variables.GetComponent<MainVariables>().popupSignalLive = false;
        	variables.GetComponent<MainVariables>().subphase++;
        	for (int i=0; i<8; i++){
        		if (variables.GetComponent<MainVariables>().empoweredDurations[i] != 2){
        			variables.GetComponent<MainVariables>().empoweredDurations[i] = 0;
        			variables.GetComponent<MainVariables>().empoweredAmounts[i] = 0;
        			variables.GetComponent<MainVariables>().empowered[i] = false;
        		}
        	}
	    	variables.GetComponent<MainVariables>().minigameAttemptsSubphase = new int [][] {
	    		new int[] {0, 0, 0, 0, 0},
	    		new int[] {0, 0, 0, 0, 0},
	    		new int[] {0, 0, 0, 0, 0},
	    		new int[] {0, 0, 0, 0, 0},
	    		new int[] {0, 0, 0, 0, 0},
	    		new int[] {0, 0, 0, 0, 0},
	    		new int[] {0, 0, 0, 0, 0},
	    		new int[] {0, 0, 0, 0, 0}
	    	};
	    	variables.GetComponent<MainVariables>().actionTakenSubphase = new int [][] {
	    		new int[] {0, 0, 0, 0, 0, 0},
	    		new int[] {0, 0, 0, 0, 0, 0},
	    		new int[] {0, 0, 0, 0, 0, 0},
	    		new int[] {0, 0, 0, 0, 0, 0},
	    		new int[] {0, 0, 0, 0, 0, 0},
	    		new int[] {0, 0, 0, 0, 0, 0},
	    		new int[] {0, 0, 0, 0, 0, 0},
	    		new int[] {0, 0, 0, 0, 0, 0}
	    	};
	    	for (int i=0; i<9; i++){
	    		if (variables.GetComponent<MainVariables>().CHM_Cap_Durations[i]==1){
	    			variables.GetComponent<MainVariables>().CHM_Cap_Durations[i]=0;
	    			variables.GetComponent<MainVariables>().CHM_Caps[i] = false;
	    		}
	    	}
        	actionSpecifics.GetComponent<ActionSpecificsScript>().recentlyTakenAction = 10;
        	playerChart.GetComponent<ActionTrackerManager>().cancelled();
        }
        Display();
    }

    void Display(){
    	phaseText.GetComponent<Text>().text = "Phase " + (variables.GetComponent<MainVariables>().phase+1);
    	subphaseText.GetComponent<Text>().text = "Subphase " + subphaseLetters[variables.GetComponent<MainVariables>().subphase%2];
    	playersCanvas.GetComponent<PlayerCanvasManager>().trackActions = playersToggle.GetComponent<Toggle>().isOn;
    }
}
