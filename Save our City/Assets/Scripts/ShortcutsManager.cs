using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//three big dumb buttons that really do nothing? they're shortcuts I guess but I never used them
public class ShortcutsManager : MonoBehaviour
{
    public GameObject variables, tabManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void editMap(){
    	tabManager.GetComponent<TabManagement>().whichTab = 3;
    	variables.GetComponent<MainVariables>().popupSignalLive = true;
    	variables.GetComponent<MainVariables>().popupSignalNum = 1;
    }
    public void editGraph(){
    	tabManager.GetComponent<TabManagement>().whichTab = 4;
    	variables.GetComponent<MainVariables>().popupSignalLive = true;
    	variables.GetComponent<MainVariables>().popupSignalNum = 2;
    }
    public void editChart(){
    	tabManager.GetComponent<TabManagement>().whichTab = 2;
    }
    /*
    public void editPhase(){
        variables.GetComponent<MainVariables>().phase =;
    }
    */
}
