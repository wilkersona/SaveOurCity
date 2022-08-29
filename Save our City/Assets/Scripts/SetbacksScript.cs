using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script for determining the effects of setbacks
//yes I realize I can use an array and a for loop to make it prettier but I didn't
public class SetbacksScript : MonoBehaviour
{
    public GameObject variables, actionTracker;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (variables.GetComponent<MainVariables>().popupSignalLive && variables.GetComponent<MainVariables>().popupSignalNum==18){
        	variables.GetComponent<MainVariables>().popupSignalLive = false;
        	int[] temp_PUs = new int[] {
        		variables.GetComponent<MainVariables>().CHM_Values[0],
        		variables.GetComponent<MainVariables>().CHM_Values[1],
        		variables.GetComponent<MainVariables>().CHM_Values[2],
        		variables.GetComponent<MainVariables>().CHM_Values[3],
        		variables.GetComponent<MainVariables>().CHM_Values[4],
        		variables.GetComponent<MainVariables>().CHM_Values[5],
        		variables.GetComponent<MainVariables>().CHM_Values[6],
        		variables.GetComponent<MainVariables>().CHM_Values[7],
        		variables.GetComponent<MainVariables>().CHM_Values[8]
        	};
        	variables.GetComponent<MainVariables>().CHM_Values[0] += setbackAmount(temp_PUs[7], 0);
        	variables.GetComponent<MainVariables>().CHM_Values[7] += setbackAmount(temp_PUs[6], 7);
        	variables.GetComponent<MainVariables>().CHM_Values[6] += setbackAmount(temp_PUs[8], 6);
        	variables.GetComponent<MainVariables>().CHM_Values[8] += setbackAmount(temp_PUs[5], 8);
        	variables.GetComponent<MainVariables>().CHM_Values[5] += setbackAmount(temp_PUs[4], 5);
        	variables.GetComponent<MainVariables>().CHM_Values[4] += setbackAmount(temp_PUs[3], 4);
        	variables.GetComponent<MainVariables>().CHM_Values[3] += setbackAmount(temp_PUs[2], 3);
        	variables.GetComponent<MainVariables>().CHM_Values[2] += setbackAmount(temp_PUs[1], 2);
        	variables.GetComponent<MainVariables>().CHM_Values[1] += setbackAmount(temp_PUs[0], 1);
        	variables.GetComponent<MainVariables>().turnTaken = new bool[] {false, false, false, false, false, false, false, false, false};
        	actionTracker.GetComponent<ActionTrackerManager>().resetTurns();
        	variables.GetComponent<MainVariables>().actionHistory.Add(variables.GetComponent<MainVariables>().CreateActionInfo(8, 0, setbackDetails(temp_PUs)));
        }
    }

    int setbackAmount(int pu, int whichCHM){
    	if (variables.GetComponent<MainVariables>().CHM_Caps[whichCHM]) return 0;
    	if (pu>=35) return 5;
    	if (pu>=20) return 2;
    	return 0;
    }

    string setbackDetails(int[] pus){
    	string result = "";
    	result += "I rose by " + setbackAmount(pus[7], 0) + "; ";
    	result += "MSS rose by " + setbackAmount(pus[6], 7) + "; ";
    	result += "RDE rose by " + setbackAmount(pus[8], 6) + "; ";
    	result += "BHA rose by " + setbackAmount(pus[5], 8) + "; ";
    	result += "HD rose by " + setbackAmount(pus[4], 5) + "; ";
    	result += "MM rose by " + setbackAmount(pus[3], 4) + "; ";
    	result += "AD rose by " + setbackAmount(pus[2], 3) + "; ";
    	result += "D rose by " + setbackAmount(pus[1], 2) + "; ";
    	result += "H rose by " + setbackAmount(pus[0], 1);
    	return result;
    }
}
