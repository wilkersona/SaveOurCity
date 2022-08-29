using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//manage the things/statuses on a single player on the player chart
//moreso the game variables?
//also see PlayerVarManager
public class RoleTracker : MonoBehaviour
{
    public GameObject variables;
    public GameObject my_name, myAP_text, myAP_bar;
    public GameObject[] statuses;
    //public GameObject[] editors;
    public GameObject turnTracker;
    public int which;

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

    void Display() {
    	my_name.GetComponent<Text>().text = variables.GetComponent<MainVariables>().Role_Names[which];
    	int my_ap = variables.GetComponent<MainVariables>().player_AP[which];
    	myAP_text.GetComponent<Text>().text = "AP: "+my_ap;

    	myAP_bar.GetComponent<RectTransform>().anchoredPosition = new Vector3(-70 + my_ap, -15, 0);
    	myAP_bar.GetComponent<RectTransform>().localScale = new Vector3(my_ap/50.0f, 0.1f, 1);

    	statuses[0].SetActive(variables.GetComponent<MainVariables>().empowered[which]);
    	statuses[1].SetActive(variables.GetComponent<MainVariables>().stat2[which]);
    	statuses[2].SetActive(variables.GetComponent<MainVariables>().stat3[which]);

    	turnTracker.GetComponent<Toggle>().isOn = variables.GetComponent<MainVariables>().turnTaken[which];
    }
}
