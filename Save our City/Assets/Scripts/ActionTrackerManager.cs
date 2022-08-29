using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//overhead class for managing the player chart
public class ActionTrackerManager : MonoBehaviour
{
    public GameObject actionTracker, variables;
    int displayTab = 2;
    public GameObject[] editors;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        actionTracker.SetActive(variables.GetComponent<MainVariables>().whichTab == displayTab);
        if (variables.GetComponent<MainVariables>().popupSignalLive && variables.GetComponent<MainVariables>().popupSignalNum>=3 
                                                                    && variables.GetComponent<MainVariables>().popupSignalNum<=7){
            variables.GetComponent<MainVariables>().popupSignalLive=false;
            for (int i=0; i<8; i++){
                variables.GetComponent<MainVariables>().player_AP[i] = int.Parse(editors[i].GetComponent<PlayerVarManager>().ap.GetComponent<InputField>().text);
                variables.GetComponent<MainVariables>().turnTaken[i] = editors[i].GetComponent<PlayerVarManager>().turn.GetComponent<Toggle>().isOn;
                if (editors[i].GetComponent<PlayerVarManager>().st1.GetComponent<Toggle>().isOn){
                    variables.GetComponent<MainVariables>().empowered[i] = true;
                    variables.GetComponent<MainVariables>().empoweredAmounts[i] = 1;
                    variables.GetComponent<MainVariables>().empoweredDurations[i] = 1;
                }
                else {
                    variables.GetComponent<MainVariables>().empowered[i] = false;
                    variables.GetComponent<MainVariables>().empoweredAmounts[i] = 0;
                    variables.GetComponent<MainVariables>().empoweredDurations[i] = 0;
                }
                variables.GetComponent<MainVariables>().stat2[i] = editors[i].GetComponent<PlayerVarManager>().st2.GetComponent<Toggle>().isOn;
                variables.GetComponent<MainVariables>().stat3[i] = editors[i].GetComponent<PlayerVarManager>().st3.GetComponent<Toggle>().isOn;
                editors[i].GetComponent<PlayerVarManager>().Display();
            }
        }
    }

    public void cancelled(){
        for (int i=0; i<8; i++){
            editors[i].GetComponent<PlayerVarManager>().Display();
        }
    }

    public void resetTurns(){
        for (int i=0; i<8; i++){
            editors[i].GetComponent<PlayerVarManager>().Display();
            editors[i].GetComponent<PlayerVarManager>().turn.GetComponent<Toggle>().isOn = false;
        }
    }
}
