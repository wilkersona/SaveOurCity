using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using UnityEngine.Windows;

#if UNITY_EDITOR
using UnityEditor;
#endif


//Main container of all relevant variables; also manages saving and loading data
public class MainVariables : MonoBehaviour
{
	//some stuff for folder management
	private static Guid FolderDownloads = new Guid("374DE290-123F-4565-9164-39C4925E467B");
	[DllImport("shell32.dll", CharSet = CharSet.Auto)]
	private static extern int SHGetKnownFolderPath(ref Guid id, int flags, IntPtr token, out IntPtr path);

	//all variables get declared, but not set, because sometimes the game gets reset
	private int escapeHeld = 0;
    private int majorVersion, minorVersion;
    public bool loadFile = true;
    public int phase, subphase;// = 0;
    public int whichTab;// = 0;
    public bool popupSignalLive;// = false;
    public int popupSignalNum;// = 0;
    public string[] CHM_Names;// = {"Infections", "Hospitalizations", "Deaths", "Anxiety and Depression", "Misinformation and Mistrust", 
    	//"Health Disparities", "R & D Expenses", "Medical Supply Shortages", "Barriers to Healthcare Access"};
    public int[] CHM_Values;// = {25, 37, 18, 35, 19, 25, 40, 23, 17};
    public int[] CHM_Graph_Reveals;// = {0, 0, 0, 0, 0, 0, 0, 0, 0};
    public bool[] CHM_Caps;// = {false, false, false, false, false, false, false, false, false};
    public int[] CHM_Cap_Durations;
    public int CHM_Deaths_Phase_Reduction;
    public int CHM_RDE_Phase_Reduction;
    public int ddev_ap_spent;
    public bool[][] Map_Revealed;/* = {
    	new bool[] {false, false, false, false, false, false, false, false, false},
    	new bool[] {false, false, false, false, false, false, false, false, false},
    	new bool[] {false, false, false, false, false, false, false, false, false},
    	new bool[] {false, false, false, false, false, false, false, false, false},
    	new bool[] {false, false, false, false, true, false, false, false, false},
    	new bool[] {false, false, false, false, false, false, false, false, false},
    	new bool[] {false, false, false, false, false, false, false, false, false},
    	new bool[] {false, false, false, false, false, false, false, false, false},
    	new bool[] {false, false, false, false, false, false, false, false, false},
    };*/
    // 10:Community, 11:Government, 12:Industrial, 13:Medical
    // 14: Rail 1, 20: Rail 2, 26: Rail 3
    // +0: North-East, +1: North-South, +2: North-West, +3: East-South, +4: East-West, +5: South-West
    public int[][] Buildings;/* = {
    	new int[] {-1, -1, 11, 14, 24, 28, -1, -1, 2},
    	new int[] {-1, -1, 13, 7, -1, -1, -1, 10, -1},
    	new int[] {-1, -1, 1, -1, 13, -1, -1, -1, 13},
    	new int[] {31, 4, -1, 12, -1, 10, -1, -1, 17},
    	new int[] {21, -1, 12, 6, 9, 3, -1, -1, 21},
    	new int[] {16, -1, -1, 11, 13, -1, 11, -1, 26},
    	new int[] {10, 11, 5, -1, -1, 10, -1, -1, -1},
    	new int[] {-1, -1, -1, -1, 12, -1, 0, -1, -1},
    	new int[] {8, -1, -1, 29, 24, 19, -1, -1, 12},
    };*/

    public string[] Role_Names;// = {"Clinician", "Epidemiologist", "Drug Developer", "Lab Diagnostician", "Virologist", "Media Campaigner", "Politician", "Health Behaviorist", "Setbacks"};
    public bool[] turnTaken;// = {false, false, false, false, false, false, false, false};
    public bool[] empowered;// = {false, false, false, false, false, false, false, false};
	public int[] empoweredAmounts;// = new int[] {0, 0, 0, 0, 0, 0, 0, 0};
	public int[] empoweredDurations;// = new int[] {0, 0, 0, 0, 0, 0, 0, 0};
    public bool[] stat2;// = {false, false, false, false, false, false, false, false};
    public bool[] stat3;// = {false, false, false, false, false, false, false, false};
    public int[] player_AP;// = {12, 7, 21, 25, 18, 15, 0, 6};
    public int[] quizScores;// = {0, 0, 0, 0, 0, 0, 0, 0};
    public int[][] minigameAttemptsPhase;
    public int[][] minigameAttemptsSubphase;
    public int[][] actionTakenGame;
    public int[][] actionTakenPhase;
    public int[][] actionTakenSubphase;
    public bool prereqsWaived;

    //chunk of stuff that "defines" each action
    public class ActionInfo {
    	public int player;
    	public int actionNum;
    	public string moreInfo;

    	public ActionInfo(int player_, int actionNum_, string moreInfo_){
    		player = player_;
    		actionNum = actionNum_;
    		moreInfo = moreInfo_;
    	}
    }
    public List<ActionInfo> actionHistory;// = new List<ActionInfo>();

    // Start is called before the first frame update
    void Start()
    {

#if !UNITY_EDITOR
    	if (Display.displays.Length > 1)
    		Display.displays[1].Activate();
#endif
    	SetInitialVariables();
    	//CHM_Values[0] = Display.displays.Length;

    	/*
    	if (!loadFile){
    		SetInitialVariables();
    	}
    	else {
    		//string[] content = LoadFromFile("Input");
    		string[] content = PlayerPrefs.GetString("csvContent").Split('\n');
    		readCSV(content);
    	}
    	*/
    	//tempoary code to test action history
    	/*
    	actionHistory.Add(new ActionInfo(3, 2, "qwertyuiop"));
    	actionHistory.Add(new ActionInfo(5, 0, "qwertyuiopaaaaaaa aaaaaaaaaa aaaaaaaaaaaaaaaaaaaaaaa aaaaaaaaaaaa aaaaaaaaaaaaaaaaaaaaaa aaaaaaaaaaaaaaaa aaaaaaaaaaaaaaaaaaaaa aaaaaa aaaaaaaaaaaaa aaa"));
    	actionHistory.Add(new ActionInfo(7, 4, "qwertyuiop"));
    	actionHistory.Add(new ActionInfo(0, 5, "qwertyuiop"));
    	actionHistory.Add(new ActionInfo(4, 1, "qwertyuiop"));
    	actionHistory.Add(new ActionInfo(4, 1, "qwertyuiop"));
    	actionHistory.Add(new ActionInfo(4, 1, "qwertyuiop"));
    	actionHistory.Add(new ActionInfo(4, 1, "qwertyuiop"));
    	actionHistory.Add(new ActionInfo(4, 1, "qwertyuiop"));
    	*/
    }

    // Update is called once per frame
    void Update()
    {
    	/*
    	string s = "";
    	for (int i=0; i<9; i++){
    		s += CHM_Names[i] + ": " + CHM_Values[i] + "\n";
    	}
        //tempTextDisplay.GetComponent<Text>().text = s;
        */
        if (popupSignalLive && popupSignalNum==19){
        	//SaveToFile();
        	SetInitialVariables();
        	//Temporary to test this method
        	SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKey(KeyCode.Escape)){
        	escapeHeld++;
        }
        else {
        	escapeHeld=0;
        }
        if (escapeHeld>=300){
        	Application.Quit();
        }
    }

    public void loadGame(string fileTag) {
    	if (!PlayerPrefs.HasKey(fileTag)){
    		saveAction(10, 22, "Failed to load file with name: "+fileTag);
    		return;
    	}
    	string[] content = PlayerPrefs.GetString(fileTag).Split('\n');
    	readCSV(content);
    }

    //stuff so I dont have to export actioninfo
    public ActionInfo CreateActionInfo(int player_, int action_, string description_){
    	return new ActionInfo(player_, action_, description_);
    }

    public void saveAction(int player_, int action_, string description_){
    	actionHistory.Add(new ActionInfo(player_, action_, description_));
    }

    //called to set the default variables
    void SetInitialVariables() {
    	//version control; do this so that the file reader code can stay sane
    	majorVersion = 1;
    	minorVersion = 1;
    	//which phase and subphase we're currently in
    	phase = 0;
    	subphase = 0;
    	//which tab the GM is looking at
    	whichTab = 0;
    	//if a popup window was recently confirmed, and the corresponding signal
    	popupSignalLive = false;
    	popupSignalNum = 0;
    	//names of chms
    	CHM_Names = new string[] {"Infections", "Hospitalizations", "Deaths", "Anxiety and Depression", "Misinformation and Mistrust", 
    		"Health Disparities", "R & D Expenses", "Medical Supply Shortages", "Barriers to Healthcare Access"};
    	//the values of each chm currently, and the extent to which the graph is revealed
    	CHM_Values = new int[] {25, 37, 18, 35, 19, 25, 40, 23, 17};
    	CHM_Graph_Reveals = new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0};
    	//whether a chm is capped, and how long it lasts
    	CHM_Caps = new bool[] {false, false, false, false, false, false, false, false, false};
    	CHM_Cap_Durations = new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0};
    	//things that reduce pu at the start of later phases
    	CHM_Deaths_Phase_Reduction = 0;
    	CHM_RDE_Phase_Reduction = 0;
    	//theres a weird action that needs to track how much ap the drug developer spends
    	ddev_ap_spent = 0;
    	//which tiles of the map are revealed
    	Map_Revealed = new bool[][] {
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
	    //which building is where, as determined by the following code:
	    // -1: no building
	    // 0-8: chm cluster of corresponding chm
	    // 9: center
	    // 10:Community, 11:Government, 12:Industrial, 13:Medical
	    // 14: Rail 1, 20: Rail 2, 26: Rail 3
	    // 		+0: North-East, +1: North-South, +2: North-West, +3: East-South, +4: East-West, +5: South-West
	    Buildings = new int[][] {
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
	    //each role's name, and if they've taken their turn
	    Role_Names = new string[] {"Clinician", "Epidemiologist", "Drug Developer", "Lab Diagnostician", "Virologist", "Media Campaigner", "Politician", "Health Behaviorist", "Setbacks", "Multiple", "GM"};
	    turnTaken = new bool[] {false, false, false, false, false, false, false, false};
	    //if a role is empowered, and the amount/duration
	    empowered = new bool[] {false, false, false, false, false, false, false, false};
	    empoweredAmounts = new int[] {0, 0, 0, 0, 0, 0, 0, 0};
	    empoweredDurations = new int[] {0, 0, 0, 0, 0, 0, 0, 0};
	    //two buffer stats
	    stat2 = new bool[] {false, false, false, false, false, false, false, false};
	    stat3 = new bool[] {false, false, false, false, false, false, false, false};
	    //player's AP and quiz scores
	    player_AP = new int[] {10, 10, 10, 10, 10, 10, 10, 10};
	    quizScores = new int[] {0, 0, 0, 0, 0, 0, 0, 0};
	    //minigame attempts per player, minigames ordered as follows:
	    //0: CMMG; 1: Speech; 2: Visual Acuity; 3: Reflective Listening; 4: Scientific Breakthrough
    	minigameAttemptsPhase = new int [][] {
    		new int[] {0, 0, 0, 0, 0},
    		new int[] {0, 0, 0, 0, 0},
    		new int[] {0, 0, 0, 0, 0},
    		new int[] {0, 0, 0, 0, 0},
    		new int[] {0, 0, 0, 0, 0},
    		new int[] {0, 0, 0, 0, 0},
    		new int[] {0, 0, 0, 0, 0},
    		new int[] {0, 0, 0, 0, 0}
    	};
    	minigameAttemptsSubphase = new int [][] {
    		new int[] {0, 0, 0, 0, 0},
    		new int[] {0, 0, 0, 0, 0},
    		new int[] {0, 0, 0, 0, 0},
    		new int[] {0, 0, 0, 0, 0},
    		new int[] {0, 0, 0, 0, 0},
    		new int[] {0, 0, 0, 0, 0},
    		new int[] {0, 0, 0, 0, 0},
    		new int[] {0, 0, 0, 0, 0}
    	};
    	//each specific action taken this game amounts
    	actionTakenGame = new int [][] {
    		new int[] {0, 0, 0, 0, 0, 0},
    		new int[] {0, 0, 0, 0, 0, 0},
    		new int[] {0, 0, 0, 0, 0, 0},
    		new int[] {0, 0, 0, 0, 0, 0},
    		new int[] {0, 0, 0, 0, 0, 0},
    		new int[] {0, 0, 0, 0, 0, 0},
    		new int[] {0, 0, 0, 0, 0, 0},
    		new int[] {0, 0, 0, 0, 0, 0}
    	};
    	actionTakenPhase = new int [][] {
    		new int[] {0, 0, 0, 0, 0, 0},
    		new int[] {0, 0, 0, 0, 0, 0},
    		new int[] {0, 0, 0, 0, 0, 0},
    		new int[] {0, 0, 0, 0, 0, 0},
    		new int[] {0, 0, 0, 0, 0, 0},
    		new int[] {0, 0, 0, 0, 0, 0},
    		new int[] {0, 0, 0, 0, 0, 0},
    		new int[] {0, 0, 0, 0, 0, 0}
    	};
    	actionTakenSubphase = new int [][] {
    		new int[] {0, 0, 0, 0, 0, 0},
    		new int[] {0, 0, 0, 0, 0, 0},
    		new int[] {0, 0, 0, 0, 0, 0},
    		new int[] {0, 0, 0, 0, 0, 0},
    		new int[] {0, 0, 0, 0, 0, 0},
    		new int[] {0, 0, 0, 0, 0, 0},
    		new int[] {0, 0, 0, 0, 0, 0},
    		new int[] {0, 0, 0, 0, 0, 0}
    	};
    	//whether prerequisites are temporarily waived
    	prereqsWaived = false;
    	//history of all actions
	    actionHistory = new List<ActionInfo>();
    }

    //called to create a csv string from the current variables
    string formCSV() {
    	string result = "Game Version:,";
    	result += ""+majorVersion+"."+minorVersion+",\n";
    	result += "Game State:,\n";
    	result += "Phase,"+phase+",Subphase,"+subphase+",\n";
    	result += "CHM Values:,\n";
    	for (int i=0; i<9; i++){
    		result+="" + CHM_Values[i] + ",";
    	}
    	result+="\n";
    	result += "CHM Graph Reveal Indexes:,\n";
    	for (int i=0; i<9; i++){
    		result+="" + CHM_Graph_Reveals[i] + ",";
    	}
    	result+="\n";
    	result += "CHM Caps:,\n";
    	for (int i=0; i<9; i++){
    		result+="" + CHM_Caps[i] + ",";
    	}
    	result+="\n";
    	result += "CHM Cap Durations:,\n";
    	for (int i=0; i<9; i++){
    		result+="" + CHM_Cap_Durations[i] + ",";
    	}
    	result+="\n";
    	result+="Certain Variables for Specific Actions:,\n";
    	result+="" + CHM_Deaths_Phase_Reduction+ ",";
    	result+="" + CHM_RDE_Phase_Reduction+ ",";
    	result+="" + ddev_ap_spent+ ",\n";
    	result += "CHM Map Reveals:,\n";
    	for (int i=0; i<9; i++){
    		for (int j=0; j<9; j++){
    			result+="" + Map_Revealed[i][j] + ",";
    		}
    		result+="\n";
    	}
    	result += "Buildings:,\n";
    	for (int i=0; i<9; i++){
    		for (int j=0; j<9; j++){
    			result+="" + Buildings[i][j] + ",";
    		}
    		result+="\n";
    	}
    	result += "Players Taken Turns:,\n";
    	for (int i=0; i<8; i++){
    		result+="" + turnTaken[i] + ",";
    	}
    	result+="\n";
    	result += "Players Empowered:,\n";
    	for (int i=0; i<8; i++){
    		result+="" + empowered[i] + ",";
    	}
    	result+="\n";
    	result += "Players Empowered Amounts:,\n";
    	for (int i=0; i<8; i++){
    		result+="" + empoweredAmounts[i] + ",";
    	}
    	result+="\n";
    	result += "Players Empowered Durations:,\n";
    	for (int i=0; i<8; i++){
    		result+="" + empoweredDurations[i] + ",";
    	}
    	result+="\n";
    	result += "Players with Status 2:,\n";
    	for (int i=0; i<8; i++){
    		result+="" + stat2[i] + ",";
    	}
    	result+="\n";
    	result += "Players with Status 3:,\n";
    	for (int i=0; i<8; i++){
    		result+="" + stat3[i] + ",";
    	}
    	result+="\n";
    	result += "Player AP:,\n";
    	for (int i=0; i<8; i++){
    		result+="" + player_AP[i] + ",";
    	}
    	result+="\n";
    	result += "Player Quiz Scores:,\n";
    	for (int i=0; i<8; i++){
    		result+="" + quizScores[i] + ",";
    	}
    	result+="\n";
    	result += "Minigame Attempts this Phase:,\n";
    	for (int i=0; i<8; i++){
    		for (int j=0; j<5; j++){
    			result+="" + minigameAttemptsPhase[i][j] + ",";
    		}
    		result+="\n";
    	}
    	result += "Minigame Attempts this Subphase:,\n";
    	for (int i=0; i<8; i++){
    		for (int j=0; j<5; j++){
    			result+="" + minigameAttemptsSubphase[i][j] + ",";
    		}
    		result+="\n";
    	}
    	result += "Actions Taken this Game:,\n";
    	for (int i=0; i<8; i++){
    		for (int j=0; j<6; j++){
    			result+="" + actionTakenGame[i][j] + ",";
    		}
    		result+="\n";
    	}
    	result += "Actions Taken this Phase:,\n";
    	for (int i=0; i<8; i++){
    		for (int j=0; j<6; j++){
    			result+="" + actionTakenPhase[i][j] + ",";
    		}
    		result+="\n";
    	}
    	result += "Actions Taken this Subphase:,\n";
    	for (int i=0; i<8; i++){
    		for (int j=0; j<6; j++){
    			result+="" + actionTakenSubphase[i][j] + ",";
    		}
    		result+="\n";
    	}
    	result+="Action History (Oldest to Most Recent):,\n";
    	for (int i=0; i<actionHistory.Count; i++){
    		result+="" + actionHistory[i].player + "," + actionHistory[i].actionNum + "," + actionHistory[i].moreInfo + ",\n";
    	}
    	return result;
    }

    //called to set the current variables to a csv string
    void readCSV(string[] content){
    	SetInitialVariables();
    	string[] temp = content[0].Split(',');
    	//Debug.Log(temp[1]);
    	majorVersion = int.Parse(temp[1].Split('.')[0]);
    	minorVersion = int.Parse(temp[1].Split('.')[1]);
    	temp = content[2].Split(',');
    	phase = int.Parse(temp[1]);
    	subphase = int.Parse(temp[3]);
    	temp = content[4].Split(',');
    	for (int i=0; i<9; i++){
    		CHM_Values[i] = int.Parse(temp[i]);
    	}
    	temp = content[6].Split(',');
    	for (int i=0; i<9; i++){
    		CHM_Graph_Reveals[i] = int.Parse(temp[i]);
    	}
    	temp = content[8].Split(',');
    	for (int i=0; i<9; i++){
    		CHM_Caps[i] = bool.Parse(temp[i]);
    	}
    	temp = content[10].Split(',');
    	for (int i=0; i<9; i++){
    		CHM_Cap_Durations[i] = int.Parse(temp[i]);
    	}
    	temp = content[12].Split(',');
    	CHM_Deaths_Phase_Reduction = int.Parse(temp[0]);
    	CHM_RDE_Phase_Reduction = int.Parse(temp[1]);
    	ddev_ap_spent = int.Parse(temp[2]);
    	for (int i=14; i<23; i++){
    		temp = content[i].Split(',');
    		for (int j=0; j<9; j++){
    			Map_Revealed[i-14][j] = bool.Parse(temp[j]);
    		}
    	}
    	for (int i=24; i<33; i++){
    		temp = content[i].Split(',');
    		for (int j=0; j<9; j++){
    			Buildings[i-24][j] = int.Parse(temp[j]);
    		}
    	}
    	temp = content[34].Split(',');
    	for (int i=0; i<8; i++){
    		turnTaken[i] = bool.Parse(temp[i]);
    	}
    	temp = content[36].Split(',');
    	for (int i=0; i<8; i++){
    		empowered[i] = bool.Parse(temp[i]);
    	}
    	temp = content[38].Split(',');
    	for (int i=0; i<8; i++){
    		empoweredAmounts[i] = int.Parse(temp[i]);
    	}
    	temp = content[40].Split(',');
    	for (int i=0; i<8; i++){
    		empoweredDurations[i] = int.Parse(temp[i]);
    	}
    	temp = content[42].Split(',');
    	for (int i=0; i<8; i++){
    		stat2[i] = bool.Parse(temp[i]);
    	}
    	temp = content[44].Split(',');
    	for (int i=0; i<8; i++){
    		stat3[i] = bool.Parse(temp[i]);
    	}
    	temp = content[46].Split(',');
    	for (int i=0; i<8; i++){
    		player_AP[i] = int.Parse(temp[i]);
    	}
    	temp = content[48].Split(',');
    	for (int i=0; i<8; i++){
    		quizScores[i] = int.Parse(temp[i]);
    	}
    	for (int i=50; i<58; i++){
    		temp = content[i].Split(',');
    		for (int j=0; j<5; j++){
    			minigameAttemptsPhase[i-50][j] = int.Parse(temp[j]);
    		}
    	}
    	for (int i=59; i<67; i++){
    		temp = content[i].Split(',');
    		for (int j=0; j<5; j++){
    			minigameAttemptsSubphase[i-59][j] = int.Parse(temp[j]);
    		}
    	}
    	for (int i=68; i<76; i++){
    		temp = content[i].Split(',');
    		for (int j=0; j<6; j++){
    			actionTakenGame[i-68][j] = int.Parse(temp[j]);
    		}
    	}
    	for (int i=77; i<85; i++){
    		temp = content[i].Split(',');
    		for (int j=0; j<6; j++){
    			actionTakenPhase[i-77][j] = int.Parse(temp[j]);
    		}
    	}
    	for (int i=86; i<94; i++){
    		temp = content[i].Split(',');
    		for (int j=0; j<6; j++){
    			actionTakenSubphase[i-86][j] = int.Parse(temp[j]);
    		}
    	}
    	int a = 95;
	    actionHistory = new List<ActionInfo>();
    	while (a<content.Length-1){
    		temp = content[a].Split(',');
    		//Debug.Log(content[a]);
    		saveAction(int.Parse(temp[0]), int.Parse(temp[1]), temp[2]);
    		a++;
    	}
    }

    //get csv string from file
    //I actually don't think I'll use this function
    public string[] LoadFromFile (string name_)
	{
	    string[] content = new string[] {};

	    // The target file path e.g.
#if UNITY_EDITOR
	    var folder = Application.streamingAssetsPath;

	    if(! Directory.Exists(folder)) Directory.CreateDirectory(folder);
#else
	    var folder = Application.persistentDataPath;
#endif

	    string fileName = name_ + ".csv";
	    var filePath = Path.Combine(folder, fileName);

	    content = System.IO.File.ReadAllLines(filePath);

#if UNITY_EDITOR
	    AssetDatabase.Refresh();
#endif
	    return content;
	}

	//put csv string in file
    public void SaveToFile (string fileTag)
	{
	    string content = formCSV();

	    // The target file path e.g.
#if UNITY_EDITOR
	    var folder = Application.streamingAssetsPath;

	    if(! Directory.Exists(folder)) Directory.CreateDirectory(folder);
#else
	    var folder = GetDownloadsPath();//getDownloads();//Application.persistentDataPath;
#endif

	    string fileName = "SoC_data_" + System.DateTime.Now.ToString("MM_dd_yyyy_HH_mm") + "_" + fileTag + ".csv";
	    var filePath = Path.Combine(folder, fileName);

	    using(var writer = new StreamWriter(filePath, false))
	    {
	        writer.Write(content);
	    }
	    PlayerPrefs.SetString(fileTag, content);

#if UNITY_EDITOR
	    AssetDatabase.Refresh();
#endif
	}

	//get the downloads folder
	public static string GetDownloadsPath()
	{
	    if (Environment.OSVersion.Version.Major < 6) throw new NotSupportedException();

	    IntPtr pathPtr = IntPtr.Zero;

	    try
	    {
	        SHGetKnownFolderPath(ref FolderDownloads, 0, IntPtr.Zero, out pathPtr);
	        return Marshal.PtrToStringUni(pathPtr);
	    }
	    finally
	    {
	        Marshal.FreeCoTaskMem(pathPtr);
	    }
	}
}
