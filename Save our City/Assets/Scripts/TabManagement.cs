using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//manages what tab the GM is on, which tells things if they should display
public class TabManagement : MonoBehaviour
{
	public GameObject variables;
	public GameObject[] tabs;
	public GameObject myCamera;
	private Color[] tabColors = {
		new Color (0.375f, 0, 0, 1),
		new Color (0.375f, 0.1875f, 0, 1),
		new Color (0.375f, 0.375f, 0, 1),
		new Color (0, 0.375f, 0, 1),
		new Color (0, 0.375f, 0.375f, 1),
		new Color (0, 0, 0.375f, 1),
		new Color (0.1875f, 0, 0.375f, 1),
		new Color (0.375f, 0, 0.375f, 1), 
		new Color (0, 0, 0, 1)//popup window
	};
	public int whichTab = 0;
	public bool hideTabs = false;

    // Start is called before the first frame update
    void Start()
    {
        Display();
    }

    // Update is called once per frame
    void Update()
    {
        Display();
    	variables.GetComponent<MainVariables>().whichTab = whichTab;
    }

    void Display(){
    	if (hideTabs){
    		for (int i=0; i<8; i++){
    			tabs[i].SetActive(false);
    		}
    	}
    	else {
    		for (int i=0; i<8; i++){
    			tabs[i].SetActive(true);
    		}
    	}

    	for (int i=0; i<8; i++){
    		tabs[i].GetComponent<Image>().color = tabColors[i];
    	}
    	myCamera.GetComponent<Camera>().backgroundColor = tabColors[whichTab];
    }

    public void TabClicked(int which){
    	whichTab = which;
    	variables.GetComponent<MainVariables>().whichTab = which;
    }
}
