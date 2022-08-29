using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script to manage the GM's popup windows
//it just intakes a signal number, pauses, and forwards it to whoever wants it
public class PopupManager : MonoBehaviour
{
	public GameObject confirmButton, cancelButton;
	public GameObject confirmText, cancelText;
	public GameObject textArea, noteInput;
	public GameObject tabManager, actionTaker;
	public GameObject variables;
	public GameObject actionTrackerMngr;
	private int prevTab;
	private int curSignal;
	int signalTimer = 0;
	public bool doubleConfirm = false;

	public string[] textBubbles;/* = {
		"This is a popup window",
		"Do you want to select portions of the map to reveal?",
		"Do you want to select portions of the graph to reveal/change?",
		"Do you want to change the AP of this role?",
		"Do you want to toggle if this role has taken their turn?",
		"Do you want to toggle the Empowered status on this role?",
		"Do you want to toggle the [Status 2] status on this role?",
		"Do you want to toggle the [Status 3] status on this role?",
		"Move on to the next phase?",
		"Move on to the next subphase?"
	};*/

    // Start is called before the first frame update
    void Start()
    {
        textBubbles = new string[] {
			"This is a popup window",
			"Do you want to select portions of the map to reveal?",
			"Do you want to select portions of the graph to reveal/change?",
			"Do you want to change the AP of this role?",
			"Do you want to toggle if this role has taken their turn?",
			"Do you want to toggle the Empowered status on this role?",
			"Do you want to toggle the [Status 2] status on this role?",
			"Do you want to toggle the [Status 3] status on this role?",
			"Move on to the next phase?",
			"Move on to the next subphase?", 
			"Change this role's quiz score?",
			"Change this role's quiz score?",
			"Change this role's quiz score?",
			"Change this role's quiz score?",
			"Change this role's quiz score?",
			"Change this role's quiz score?",
			"Change this role's quiz score?",
			"Change this role's quiz score?",
			"Do you want to trigger Jepali's setbacks and start a new round of actions?",
			"Do you want to end the game, delete all progress, and reset all variables? THIS CANNOT BE UNDONE.",
			"Create a note",
			"Save the game? Input a unique name to save this game under (duplicate names will overwrite).",
			"Load a previous game? Input the unique name that game was saved under."
		};
    }

    // Update is called once per frame
    void Update()
    {
        Display();
        if (signalTimer >0) signalTimer--;
        else if (signalTimer==0) {
        	variables.GetComponent<MainVariables>().popupSignalNum = 0;
        	signalTimer--;
        }
    }

    //whether to display the popup window
    void Display(){
    	if (doubleConfirm) {
    		textArea.GetComponent<Text>().text = "Are you REALLY sure you want to do this? Clicking confirm will permanently delete all your progress. The buttons have swapped positions to make sure you have read this warning.";
    		cancelText.GetComponent<Text>().text = "Confirm";
    		confirmText.GetComponent<Text>().text = "Cancel";
        	cancelButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(-200, 0, 0);
    	}
    	else {
    		confirmText.GetComponent<Text>().text = "Confirm";
    		cancelText.GetComponent<Text>().text = "Cancel";
        	cancelButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(-200, -180, 0);
    	}
    	if (tabManager.GetComponent<TabManagement>().whichTab == 8){
    		confirmButton.SetActive(true);
    		cancelButton.SetActive(true);
    		textArea.SetActive(true);
    		noteInput.SetActive(curSignal>=20 && curSignal<= 22);
    	}
    	else {
    		confirmButton.SetActive(false);
    		cancelButton.SetActive(false);
    		textArea.SetActive(false);
    		noteInput.GetComponent<InputField>().text="";
    		noteInput.SetActive(false);
    	}
    }

    //receive signal
    public void startPopup(int signal){
    	if (actionTaker.GetComponent<ActionSpecificsScript>().recentlyTakenAction > 0 || signalTimer>0) return;
    	curSignal = signal;
    	if (tabManager.GetComponent<TabManagement>().whichTab!=8){
    		prevTab = tabManager.GetComponent<TabManagement>().whichTab;
    	}
    	tabManager.GetComponent<TabManagement>().whichTab = 8;
    	tabManager.GetComponent<TabManagement>().hideTabs = true;
    	if (signal<textBubbles.Length)
    		textArea.GetComponent<Text>().text = textBubbles[signal];
    	else textArea.GetComponent<Text>().text = "Popup Text TBD";
    }

    //forward signal
    public void endPopup(bool confirmed/*, int signal*/){
		if (confirmed && curSignal==19 && !doubleConfirm){//end game?
			doubleConfirm = true;
			return;
		}
		if (curSignal==19 && doubleConfirm) {
			confirmed = !confirmed;
			doubleConfirm = false;
		}
    	tabManager.GetComponent<TabManagement>().whichTab = prevTab;
    	tabManager.GetComponent<TabManagement>().hideTabs = false;
    	if (confirmed){
    		if (curSignal>=20 && curSignal<= 22){
    			variables.GetComponent<MainVariables>().saveAction(10, curSignal, noteInput.GetComponent<InputField>().text);
    		}
    		else {
    			variables.GetComponent<MainVariables>().saveAction(10, curSignal, textBubbles[curSignal]);
    		}
	    	if (curSignal==21) {
	    		variables.GetComponent<MainVariables>().SaveToFile(noteInput.GetComponent<InputField>().text);
	    	}
	    	else if (curSignal==22){
	    		signalTimer = 10;
	    		variables.GetComponent<MainVariables>().loadGame(noteInput.GetComponent<InputField>().text);
    			actionTrackerMngr.GetComponent<ActionTrackerManager>().cancelled();
	    	}
	    	else {
	    		variables.GetComponent<MainVariables>().popupSignalLive = true;
	    		variables.GetComponent<MainVariables>().popupSignalNum = curSignal;
	    		signalTimer = 10;
	    	}
    	}
    	else if (curSignal<=7 && curSignal>=3) {
    		actionTrackerMngr.GetComponent<ActionTrackerManager>().cancelled();
    	}
    }
}
