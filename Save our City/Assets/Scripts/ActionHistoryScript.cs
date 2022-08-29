using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//script for a single entry in action history
public class ActionHistoryScript : MonoBehaviour
{
    public GameObject variables, actionDescriptions;
    public GameObject myself, playerText, actionText, descriptionText;
    public int which;

    // Start is called before the first frame update
    void Start()
    {
        myself.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 140 -55 * which, 0);
        //Display();
    }

    // Update is called once per frame
    void Update()
    {
        Display();
    }

    void Display(){
    	int count = variables.GetComponent<MainVariables>().actionHistory.Count;
    	if (count - 1 - which >= 0) {
    		int playerNum = variables.GetComponent<MainVariables>().actionHistory[count - 1 - which].player;
    		int actionNum = variables.GetComponent<MainVariables>().actionHistory[count - 1 - which].actionNum;
    		string description = variables.GetComponent<MainVariables>().actionHistory[count - 1 - which].moreInfo;
    		playerText.GetComponent<Text>().text = variables.GetComponent<MainVariables>().Role_Names[playerNum];
    		if (playerNum==8){
    			actionText.GetComponent<Text>().text = "Triggered Setbacks";
    		}
    		else if (playerNum==9){
    			actionText.GetComponent<Text>().text = "Synergy: " + actionNum;
    		}
    		else if (playerNum==10){
    			actionText.GetComponent<Text>().text = "Popup Window #: " + actionNum;
    		}
    		else if (actionNum<6){
    			actionText.GetComponent<Text>().text = actionDescriptions.GetComponent<ActionDescriptions>().Names[playerNum][actionNum];
    		}
    		else if (actionNum==6){
    			actionText.GetComponent<Text>().text = "Shared AP";
    		}
    		else if (actionNum==7){
    			actionText.GetComponent<Text>().text = "Lobby Action";
    		}
    		else if (actionNum==8){
    			actionText.GetComponent<Text>().text = "Skipped Turn";
    		}
    		descriptionText.GetComponent<Text>().text = description;
    	}
    	else {
    		playerText.GetComponent<Text>().text = "";
    		actionText.GetComponent<Text>().text = "";
    		descriptionText.GetComponent<Text>().text = "";
    	}
    }
}
