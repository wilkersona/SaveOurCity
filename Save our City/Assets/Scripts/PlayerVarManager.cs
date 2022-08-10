using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVarManager : MonoBehaviour
{
    public GameObject popup, variables;
    public GameObject tabManager, actionsDropdown;
    public int playerNum;
    public GameObject ap, turn, st1, st2, st3, button;

    // Start is called before the first frame update
    void Start()
    {
        Display();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Display(){
    	ap.GetComponent<InputField>().text = "" + variables.GetComponent<MainVariables>().player_AP[playerNum];
    	turn.GetComponent<Toggle>().isOn = variables.GetComponent<MainVariables>().turnTaken[playerNum];
    	st1.GetComponent<Toggle>().isOn = variables.GetComponent<MainVariables>().empowered[playerNum];
    	st2.GetComponent<Toggle>().isOn = variables.GetComponent<MainVariables>().stat2[playerNum];
    	st3.GetComponent<Toggle>().isOn = variables.GetComponent<MainVariables>().stat3[playerNum];

    }

    public void relayMessage(int signal) {
    	popup.GetComponent<PopupManager>().startPopup(signal);
    }

    public void shortcut() {
    	tabManager.GetComponent<TabManagement>().whichTab = 1;
    	actionsDropdown.GetComponent<Dropdown>().value = playerNum;
    }
}
