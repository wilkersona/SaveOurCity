using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//a container for all the text relevant to synergies
public class SynergyTextScript : MonoBehaviour
{
    
    public string[] names = {
    	"Coordinate a 1/2 day health and wellness fair for the community",
    	"Launch mobile medical clinic",
    	"Hold a Townhall Meeting",
    	"Government funded Vaccine Develpment Program",
    	"Chair Jepali Medical Association Task Force on Insurance coverage and minority providers"
    };
    public string[] descriptions = {
    	"Pool AP from The Epidemiologist, The Health Behavioralist, and The Clinician (3 AP from each) to reduce CHM Anxiety & Depression by 12 PU",
    	"Pool AP from The Epidemiologist, The Laboratory Diagnostician, and The Clinician (4 AP from each) to reduce CHM Infection by 6 PU, reduce CHM Death by 6 PU. CHM Hospitalizations is capped for the current phase.",
    	"Pool AP from The Epidemiologist, The Politician, and The Media Campaigner (3 AP from each) to reduce CHM Misinformation & Mistrust by 12 PU",
    	"Pool AP from The Politician, The Virologist, and The Drug Developer (4 AP from each) to Reduce CHM Research & Development Expenses by 6 PU, and reduce CHM Medical Supply Shortages by 6 PU. Cap CHM Health Disparities for current phase.",
    	"Pool AP from The Politician, The Health Behaviorist, and The Media Campaigner (4 AP from each) to Reduce CHM Barriers to Healthcare Access by 6 PU, and reduce CHM Health Disparities by 6 PU. Cap CHM Misinformation & Mistrust for current phase."
    };
    public string[] enhancements = {
    	"",
    	"Role-Action Taken:  If The Drug Developer has taken the action \"Add Budgetary line for x\" in this game phase, the Cap is permanent for the game (Duration level 2)",
    	"",
    	"Role-Action Taken:  If The Media Campaigner has taken the action \"Advertise testing clinics at community based organizations\" in this game phase, the Cap is permanent for the game (Duration level 2)",
    	"Role-Action Taken:  If The Health Behaviorist has taken the action \"Offer cognitive behavioral therapy in neighborhood clinics\" in this game phase, the Cap is permanent for the game (Duration level 2)"
    };
    public string[] limitations = {
    	"CHM-Below Threshold: CHM Anxiety & Depression must be below 35 PU",
    	"Role-Action Taken: The Clinician must have already taken the action Review hospital-wide data",
    	"Role-Action Taken: The Media Campaigner must have already taken the action \"Produce myth vs. fact infographic for posters and billboards\"",
    	"",
    	"Role-Action Taken: The Media Campaigner must have already taken the action \"Advertise testing clinics at community based organizations\""
    };
    public int[][] participants = {
    	new int[] {0, 1, 7},
    	new int[] {0, 1, 3},
    	new int[] {1, 5, 6},
    	new int[] {2, 4, 6},
    	new int[] {5, 6, 7}
    };
    public int[] costs = {3, 4, 3, 4, 4};
    public int which;
    public GameObject variables;
    public GameObject nameText, overviewText, descriptionText, enhancementText, limitationText;

    // Start is called before the first frame update
    void Start()
    {
        nameText.GetComponent<Text>().text = names[which];
        descriptionText.GetComponent<Text>().text = descriptions[which];
        enhancementText.GetComponent<Text>().text = enhancements[which];
        limitationText.GetComponent<Text>().text = limitations[which];
    }

    // Update is called once per frame
    // I added colors
    void Update()
    {
        limitationText.GetComponent<Text>().color = new Color(255, 255, 255, 1);
        enhancementText.GetComponent<Text>().color = new Color(255, 255, 255, 1);
        overviewText.GetComponent<Text>().color = new Color(255, 255, 255, 1);
        overviewText.GetComponent<Text>().text = formOverview();
        Color red_ = new Color(255, 0, 0, 1);
        Color green_ = new Color(0, 255, 0, 1);
        for (int i=0; i<3; i++) {
        	if (variables.GetComponent<MainVariables>().empowered[participants[which][i]]){
        		overviewText.GetComponent<Text>().color = green_;
        	}
        }
        for(int i=0; i<3; i++){
        	if (variables.GetComponent<MainVariables>().turnTaken[participants[which][i]] || 
        			(costs[which] - variables.GetComponent<MainVariables>().empoweredAmounts[participants[which][i]]) > variables.GetComponent<MainVariables>().player_AP[participants[which][i]]) {
        		overviewText.GetComponent<Text>().color = red_;
        	}
        }
        switch (which){
        	case 0:
        		if (variables.GetComponent<MainVariables>().CHM_Values[3]>=35) {
        			limitationText.GetComponent<Text>().color = red_;
        		}
        		break;
        	case 1:
        		if (variables.GetComponent<MainVariables>().actionTakenGame[0][0]==0) {
        			limitationText.GetComponent<Text>().color = red_;
        		}
        		if (variables.GetComponent<MainVariables>().actionTakenGame[2][5]!=0) {
        			enhancementText.GetComponent<Text>().color = green_;
        		}
        		break;
        	case 2:
        		if (variables.GetComponent<MainVariables>().actionTakenGame[5][2]==0) {
        			limitationText.GetComponent<Text>().color = red_;
        		}
        		break;
        	case 3:
        		if (variables.GetComponent<MainVariables>().actionTakenGame[5][4]!=0) {
        			enhancementText.GetComponent<Text>().color = green_;
        		}
        		break;
        	case 4:
        		if (variables.GetComponent<MainVariables>().actionTakenGame[5][4]==0) {
        			limitationText.GetComponent<Text>().color = red_;
        		}
        		if (variables.GetComponent<MainVariables>().actionTakenGame[7][3]!=0) {
        			enhancementText.GetComponent<Text>().color = green_;
        		}
        		break;
        }
    }

    //prints who and how much per synergy
    string formOverview(){
    	string result = "";
    	for (int i=0; i<3; i++){
    		result+=variables.GetComponent<MainVariables>().Role_Names[participants[which][i]] + ": "+ (costs[which] - variables.GetComponent<MainVariables>().empoweredAmounts[participants[which][i]]) + " AP\n";
    	}
    	return result;
    }
}
