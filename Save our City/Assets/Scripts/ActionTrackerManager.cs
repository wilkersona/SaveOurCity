using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
                variables.GetComponent<MainVariables>().empowered[i] = editors[i].GetComponent<PlayerVarManager>().st1.GetComponent<Toggle>().isOn;
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
}
