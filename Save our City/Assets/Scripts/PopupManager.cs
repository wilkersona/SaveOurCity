using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
	public GameObject confirmButton, cancelButton;
	public GameObject textArea;
	public GameObject tabManager;
	public GameObject variables;
	public GameObject actionTrackerMngr;
	private int prevTab;
	private int curSignal;

	public string[] textBubbles = {
		"This is a popup window",
		"Do you want to select portions of the map to reveal?",
		"Do you want to select portions of the graph to reveal/change?",
		"Do you want to change the AP of this role?",
		"Do you want to toggle if this role has taken their turn?",
		"Do you want to toggle the Empowered status on this role?",
		"Do you want to toggle the [Status 2] status on this role?",
		"Do you want to toggle the [Status 3] status on this role?"
	};

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
    	if (tabManager.GetComponent<TabManagement>().whichTab == 8){
    		confirmButton.SetActive(true);
    		cancelButton.SetActive(true);
    		textArea.SetActive(true);
    	}
    	else {
    		confirmButton.SetActive(false);
    		cancelButton.SetActive(false);
    		textArea.SetActive(false);
    	}
    }

    public void startPopup(int signal){
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

    public void endPopup(bool confirmed/*, int signal*/){
    	tabManager.GetComponent<TabManagement>().whichTab = prevTab;
    	tabManager.GetComponent<TabManagement>().hideTabs = false;
    	if (confirmed){
    		variables.GetComponent<MainVariables>().popupSignalLive = true;
    		variables.GetComponent<MainVariables>().popupSignalNum = curSignal;
    	}
    	else if (curSignal<=7 && curSignal>=3) {
    		actionTrackerMngr.GetComponent<ActionTrackerManager>().cancelled();
    	}
    }
}
