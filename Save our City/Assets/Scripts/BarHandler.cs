using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//script to manage an individual bar on the graph
public class BarHandler : MonoBehaviour
{
	public GameObject bar, cloud;
	public GameObject my_name, my_value, q_mark;
    public GameObject handler;
	public int which;
	public GameObject variables;
	public GameObject cap;
    public GameObject editObj;
    public GameObject dropdown, inputField, toggle;
	public Sprite[] barSprites;
	public bool GM_View = false;
	public bool alwaysDisplayed = false;
	int rect_scale = 30;
	int offset = 110, height = 190; 
	//int revealLevel = 0; 

    public bool editing = false;

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

    //displaying it
    void Display()
    {
        //get temporary values if applicable
        int reveals;
        int values;
        bool capped;
        if (!editing){
            reveals = variables.GetComponent<MainVariables>().CHM_Graph_Reveals[which];
            values = variables.GetComponent<MainVariables>().CHM_Values[which];
            capped = variables.GetComponent<MainVariables>().CHM_Caps[which];
        }
        else {
            reveals = handler.GetComponent<BarOverallManager>().CHM_Graph_Reveals[which];
            values = handler.GetComponent<BarOverallManager>().CHM_Values[which];
            capped = handler.GetComponent<BarOverallManager>().CHM_Caps[which];
        }
        int actual_values = values;
        values = Mathf.Min(50, Mathf.Max(0, values));
    	//show and hide
    	if (variables.GetComponent<MainVariables>().whichTab == 4 || alwaysDisplayed){
    		bar.SetActive(true);
    		cloud.SetActive(true);
    		my_name.SetActive(true);
    		my_value.SetActive(true);
            q_mark.SetActive(true);
            cap.SetActive(true);
    	}
    	else{
    		bar.SetActive(false);
    		cloud.SetActive(false);
    		my_name.SetActive(false);
    		my_value.SetActive(false);
    		q_mark.SetActive(false);
            cap.SetActive(false);
            editObj.SetActive(false);
    	}
        //set some variables
    	height = 190-40*which;
    	int revealLevel = reveals;
    	float PU = (float)values;
        bar.GetComponent<RectTransform>().anchoredPosition = new Vector3(120+offset+(100*PU/(rect_scale*2)), height, 0);
        bar.GetComponent<RectTransform>().localScale = new Vector3(Mathf.Max(PU, 5)/rect_scale, 0.2f, 0);
        bar.GetComponent<Image>().sprite = barSprites[ProblemLevel(PU)];
        //what gets displayed
        if (!editing && (variables.GetComponent<MainVariables>().whichTab == 4 || alwaysDisplayed)){
            editObj.SetActive(false);
            my_name.SetActive(true);
            my_name.GetComponent<RectTransform>().anchoredPosition = new Vector3(offset, height, 0);
            my_name.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 40);
            my_name.GetComponent<Text>().text = variables.GetComponent<MainVariables>().CHM_Names[which];
        }
        else if (variables.GetComponent<MainVariables>().whichTab == 4 || alwaysDisplayed){
            //Vector3 temp = my_name.GetComponent<RectTransform>().anchoredPosition;
            my_name.SetActive(false);
            if (!editObj.activeInHierarchy) editObj.SetActive(true);
            editObj.GetComponent<RectTransform>().anchoredPosition = new Vector3(offset-10, height/*+8*which*/, 0);
        }

        //caps
        if (capped){
	        cap.GetComponent<RectTransform>().anchoredPosition = new Vector3(120+offset, height, 0);
	        cap.GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 0);
        }
        else {
	        cap.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
        }
        //displaying bar part based on how much is revealed
        if (revealLevel<=1 && !GM_View){
            float min, max;
            if (revealLevel==0){
                min = ProblemLevelToCutoff(0);
                max = ProblemLevelToCutoff(4);
            }
            else {
                min = ProblemLevelToCutoff(ProblemLevel(PU));
                max = ProblemLevelToCutoff(ProblemLevel(PU)+1);
            }
	        cloud.GetComponent<RectTransform>().anchoredPosition = new Vector3((min+max)/2, height, 0);
	        cloud.GetComponent<RectTransform>().localScale = new Vector3((max-min)/100, 0.3f, 0);
	        cloud.GetComponent<Image>().sprite = barSprites[4];
            cloud.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        	q_mark.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
	        q_mark.GetComponent<RectTransform>().anchoredPosition = new Vector3((min+max)/2, height, 0);;
	        q_mark.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 40);
	        q_mark.GetComponent<Text>().text = "?";
        	my_value.GetComponent<RectTransform>().localScale = Vector3.zero;
        }
        else {
        	my_value.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
	        my_value.GetComponent<RectTransform>().anchoredPosition = new Vector3(10+offset+(100*PU/(rect_scale)), height, 0);
	        my_value.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 40);
	        my_value.GetComponent<Text>().text = "" + actual_values;
        	//cloud.GetComponent<RectTransform>().localScale = Vector3.zero;
            if (editing){
                float min, max;
                if (revealLevel==0){
                    min = ProblemLevelToCutoff(0);
                    max = ProblemLevelToCutoff(4);
                }
                else if (revealLevel==1){
                    min = ProblemLevelToCutoff(ProblemLevel(PU));
                    max = ProblemLevelToCutoff(ProblemLevel(PU)+1);
                }
                else {
                    min=0; max=0;
                }
                cloud.GetComponent<Image>().color = new Color(1, 1, 1, 0.6f);
                cloud.GetComponent<RectTransform>().anchoredPosition = new Vector3((min+max)/2, height, 0);
                cloud.GetComponent<RectTransform>().localScale = new Vector3((max-min)/100, 0.3f, 0);
                cloud.GetComponent<Image>().sprite = barSprites[4];
            }
            else {
                cloud.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            }
        	q_mark.GetComponent<RectTransform>().localScale = Vector3.zero;
    	}
    }

    //helper functions for mapping PU
    int ProblemLevel(float PU){
    	if (PU>=30) return 3;
    	if (PU>=20) return 2;
    	if (PU>=10) return 1;
    	return 0;
    }

    float ProblemLevelToCutoff(int PL){
    	if (PL==4) return (120 + offset + (5000/rect_scale));
    	return (120 + offset + (1000*PL/rect_scale));
    }

    //seting a chm value, cap, or reveal
    public void setValue(string val_){
        string val = inputField.GetComponent<InputField>().text;
        //Debug.Log(val);
        handler.GetComponent<BarOverallManager>().CHM_Values[which] = int.Parse(val);
    }
    public void setCap(bool cap_){
        if (toggle.GetComponent<Toggle>().isOn){
            handler.GetComponent<BarOverallManager>().CHM_Caps[which] = true;
            handler.GetComponent<BarOverallManager>().CHM_Cap_Durations[which] = 1;
        }
        else {
            handler.GetComponent<BarOverallManager>().CHM_Caps[which] = false;
            handler.GetComponent<BarOverallManager>().CHM_Cap_Durations[which] = 0;
        }
        
    }
    public void setReveal(int val){
        handler.GetComponent<BarOverallManager>().CHM_Graph_Reveals[which] = dropdown.GetComponent<Dropdown>().value;
    }
}
