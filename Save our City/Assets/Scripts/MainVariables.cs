using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainVariables : MonoBehaviour
{
    public int whichTab = 0;
    public bool popupSignalLive = false;
    public int popupSignalNum = 0;
    public string[] CHM_Names = {"Infections", "Hospitalizations", "Deaths", "Anxiety and Depression", "Misinformation and Mistrust", 
    	"Health Disparities", "R & D Expenses", "Medical Supply Shortages", "Barriers to Healthcare Access"};
    public int[] CHM_Values = {25, 37, 18, 35, 19, 25, 40, 23, 17};
    public int[] CHM_Graph_Reveals = {0, 0, 0, 0, 0, 0, 0, 0, 0};
    public bool[] CHM_Caps = {false, false, false, false, false, false, false, false, false};
    public bool[][] Map_Revealed = {
    	new bool[] {false, false, false, false, false, false, false, false, false},
    	new bool[] {false, false, false, false, false, false, false, false, false},
    	new bool[] {false, false, false, false, false, false, false, false, false},
    	new bool[] {false, false, false, false, false, false, false, false, false},
    	new bool[] {false, false, false, false, true, false, false, false, false},
    	new bool[] {false, false, false, false, false, false, false, false, false},
    	new bool[] {false, false, false, false, false, false, false, false, false},
    	new bool[] {false, false, false, false, false, false, false, false, false},
    	new bool[] {false, false, false, false, false, false, false, false, false},
    };
    // 10:Community, 11:Government, 12:Industrial, 13:Medical
    // 14: Rail 1, 20: Rail 2, 26: Rail 3
    // +0: North-East, +1: North-South, +2: North-West, +3: East-South, +4: East-West, +5: South-West
    public int[][] Buildings = {
    	new int[] {-1, -1, 11, 14, 24, 28, -1, -1, 2},
    	new int[] {-1, -1, 13, 7, -1, -1, -1, 10, -1},
    	new int[] {-1, -1, 1, -1, 13, -1, -1, -1, 13},
    	new int[] {31, 4, -1, 12, -1, 10, -1, -1, 17},
    	new int[] {21, -1, 12, 6, 9, 3, -1, -1, 21},
    	new int[] {16, -1, -1, 11, 13, -1, 11, -1, 26},
    	new int[] {10, 11, 5, -1, -1, 10, -1, -1, -1},
    	new int[] {-1, -1, -1, -1, 12, -1, 0, -1, -1},
    	new int[] {8, -1, -1, 29, 24, 19, -1, -1, 12},
    };

    public string[] Role_Names = {"Clinician", "Epidemiologist", "Drug Developer", "Lab Diagnostician", "Virologist", "Media Campaigner", "Politician", "Health Behaviorist"};
    public bool[] turnTaken = {false, false, false, false, false, false, false, false};
    public bool[] empowered = {false, false, false, false, false, false, false, false};
    public bool[] stat2 = {false, false, false, false, false, false, false, false};
    public bool[] stat3 = {false, false, false, false, false, false, false, false};
    public int[] player_AP = {12, 7, 21, 25, 18, 15, 0, 6};

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    	string s = "";
    	for (int i=0; i<9; i++){
    		s += CHM_Names[i] + ": " + CHM_Values[i] + "\n";
    	}
        //tempTextDisplay.GetComponent<Text>().text = s;
    }
}
