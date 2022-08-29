using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//a whole ton of text that is relevant to the specific (1-6) actions
public class ActionDescriptions : MonoBehaviour
{
    public GameObject variables;
    public GameObject playerDropdown;
    private int everyTen = 0;

    
    public int[][] Beginning_AP_Costs = {
    	new int[] {3, 2, 3, 2, 3, 5},
    	new int[] {2, 5, 2, 4, 4, 4},
    	new int[] {2, 2, 2, 3, 4, 4},//
    	new int[] {5, 2, 3, 3, 0, 4},//
    	new int[] {2, 3, 3, 2, 2, 5},
    	new int[] {3, 5, 1, 1, 2, 4},
    	new int[] {2, 3, 3, 2, 3, 4},
    	new int[] {2, 4, 3, 3, 3, 5}
    };
    public int[][] AP_Costs = {
    	new int[] {3, 2, 3, 2, 3, 5},
    	new int[] {1, 5, 2, 4, 4, 8},
    	new int[] {2, 2, 2, 3, 0, 4},//
    	new int[] {5, 2, 3, 3, 0, 4},//
    	new int[] {2, 3, 3, 2, 2, 5},
    	new int[] {3, 5, 1, 1, 2, 4},
    	new int[] {2, 3, 3, 2, 3, 4},
    	new int[] {2, 4, 3, 3, 3, 5}
    };
    //0 = reveal(g); 2 = reduce; 4 = cap; 6 = empower; 8 = reveal(m)
    //even = level 1; odd = level 2
    //if I didn't know what to do I just put multiple?
    public int[][] Action_Categories = {
    	new int[] {0, 2, 2, 2, 2, 4},
    	new int[] {0, 1, 8, 8, 9, 4/*0*/},
    	new int[] {0, 2, 2, 2, 6, 4},
    	new int[] {1/*9*/, 2, 2, 6, 0/**/, 4},//
    	new int[] {0, 9, 2, 6, 6, 4},
    	new int[] {1, 9, 2, 2, 2, 4},
    	new int[] {8, 8, 2, 2, 6, 4},
    	new int[] {0, 9, 2, 2, 6, 4}
    };

    public string[][] Names = {
    	new string[] {
    		"Review hospital-wide data",
    		"Prescribe established therapeutics",
    		"Prescribe experimental therapeutics",
    		"Attend continuing education (CE) in social determinants of health",
    		"Refer patients to counseling",
    		"Institute MORBIDITY AND MORTALITY ROUNDS"
    	},
    	new string[] {
    		"Collect health outcomes data",
    		"Analyze health outcomes data",
    		"Conduct neighborhood survey",
    		"Create geographic heat maps",
    		"Conduct cluster analysis",
    		"Present City wide data at Strategic Planning meeting"
    	},
    	new string[] {
    		"Review requests for proposals (RFPs) from National Institutes of Health (NIH)",
    		"Optimize existing drug therapy",
    		"Conduct clinical trials for new therapy",
    		"Submit an NIH research grant application",
    		"Post findings for new D-22 therapeutics on open-access pre-print servers ",
    		"Add budgetary line for x (Where X is related to the CHM you are capping, be creative)"
    	},
    	new string[] {
    		"Publish clinical outcomes research",
    		"Modernize lab equipment (\"The Panther\" can test multiple slies at once)",
    		"Implement early screening procedure (protocol)",
    		"Make testing and triage recommendations to clinicians",
    		"???",
    		"Call Strategic Planning meeting to improve sanitation and water"
    	},
    	new string[] {
    		"Identify biological risk factors",
    		"Identify routes of transmission",
    		"Submit an NIH research grant application",
    		"Identify viral antigens",
    		"Develop a diagnostic test",
    		"Initiate journal Club in Department"
    	},
    	new string[] {
    		"Conduct city-wide public opinion polling",
    		"Conduct neighborhood-specific focus groups",
    		"Produce myth vs. fact infographic for posters and billboards",
    		"Create hand hygiene TikTok challenge",
    		"Advertise testing clinics at community-based organizations",
    		"Collaborate with Local Community Leaders to improve sanitation and water"
    	},
    	new string[] {
    		"Hold public hearing in under-served communities",
    		"Consult city planning maps",
    		"Introduce an emergency health spending bill to the legislature",
    		"Negotiate public-private partnership for clinical lab renovations",
    		"Announce expansion of behavioral health campaign workforce on the podcast \"Jepali News Today\"",
    		"Coordinate a Panel Discussion on Local Media Station"
    	},
    	new string[] {
    		"Review mental health service utilization data",
    		"Cross tabulate mental health utilization by zip code",
    		"Establish telemedicine counseling program",
    		"Offer cognitive behavioral therapy in neighborhood clinics",
    		"Provide counseling for frontline healthcare workers",
    		"Attend the Panel Discussion on Local Media Station"
    	}
    };

    public string[][] Descriptions = {
    	new string[] {
    		"Reveal the approximate PU value of CHM Hospitalizations",
    		"Reduce CHM Hospitalizations by 3 PU",
    		"Roll a D6 and reduce the PU of CHM Deaths by the resulting roll",
    		"Reduce CHM Health Disparities by 1 PU",
    		"Reduce CHM Anxiety & Depression by 2 PU",
    		"Cap the approximate PU value of any of the following CHMs: Hospitalizations, Death, Anxiety & Depression"
    	},
    	new string[] {
    		"Reveal the approximate PU value of any of the following CHMs: Infections, Hospitalizations, Death, Anxiety & Depression, Health Disparities, Barriers to Healthcare Access",
    		"Reveal the exact PU value of any of the following CHMs: Infections, Hospitalizations, Death, Anxiety & Depression, Health Disparities, Barriers to Healthcare Access",
    		"Reveal a single district of the City Map. *If this reveals a CHM Cluster, reduce the CHM by 5 PU as a bonus effect (exclusive to The Epidemiologist).",
    		"Reveal 5 continuous regions of the City Map. 'Continuous' means that each of the 5 revealed regions must share at least one border (side) with another one of the 5. Corners don't count as a border.",
    		"Reveal the region(s) of the City Map that include the cluster for one of the following CHMs (player chooses which one):  Infections, Hospitalizations, Deaths, Anxiety & Depression, Health Disparities, Barriers to Healthcare Access",
    		"Reveal and Cap the approximate PU value of one of the following CHMs for current phase: Infections, Hospitalizations, Death, Anxiety & Depression, Health Disparities, Barriers to Healthcare Access"
    	},
    	new string[] {
    		"Reveal the approximate PU value of CHM Research and Development Expenses",
    		"Reduce CHM Medical Supply Shortages by 2 PU",
    		"Reduce CHM Hospitalizations by 2 PU",
    		"Reduce CHM Research & Development Expenses by 3 PU",
    		"The AP cost of all base actions (not enhancements) taken by The Clinician in this game sub-phase are reduced by 2 AP. The minimum cost of all actions by The Clinician is 1 AP.",
    		"Cap the approximate PU value of any of the following CHMs for this Phase: Hospitalizations, Death, Research & Development Expenses, Medical Supply Shortages."
    	},
    	new string[] {
    		"Reveal the precise PU value of CHM Infections OR the City Map district that contains the cluster for CHM Infections",
    		"Reduce CHM Medical Supply Shortages by 2 PU",
    		"Reduce CHM Hospitalizations by 3 PU",
    		"The AP cost of all base actions (not enhancements) taken by The Clinician in this game sub-phase are reduced by 1 AP. The minimum cost of all actions by The Clinician is 1 AP.",
    		"",
    		"Cap the approximate PU value of any of the following CHMs for current phase: Infections, Research & Development Expenses, Medical Supply Shortages."
    	},
    	new string[] {
    		"Reveal the approximate PU value of CHM Health Disparities",
    		"Reveal the region(s) of the City Map that include the cluster for CHM Infections",
    		"Reduce CHM Research & Development Expenses by 3 PU",
    		"The AP cost of all base actions (not enhancements) taken by The Drug Developer in this game phase are reduced by 2 AP. The minimum cost of all actions by The Drug Developer is 1 AP.",
    		"The AP cost of all base actions (not enhancements) taken by The Laboratory Diagnostician in this game phase are reduced by 2 AP. The minimum cost of all actions by The Laboratory Diagnostician is 1 AP.",
    		"Cap the approximate PU value of any of the following CHMs for this Phase: Infections, Research & Development Expenses, Medical Supply Shortages."
    	},
    	new string[] {
    		"Reveal the exact PU value of CHM Misinformation & Mistrust",
    		"Reveal the district of the City Map that contains the cluster for CHM Misinformation & Mistrust",
    		"Reduce CHM Misinformation & Mistrust by 2 AP",
    		"Reduce CHM Infections by 1 PU. This action can be enhanced by consulting the social & behavioral expertise of The Health Behaviorist.",
    		"Reduce CHM Health Disparities by 1 PU and reduce CHM Barriers to Healthcare Access by 1 PU",
    		"Cap the approximate PU value of any of the following CHMs: Infections, Research & Development Expenses, Medical Supply Shortages."
    	},
    	new string[] {
    		"Reveal any 5 districts in the Urban and/or Rural Zones of the City Map",
    		"Choose a single sector type (e.g. Industrial) and reveal all 3 districts that contain that sector type.",
    		"Reduce CHM Barriers to Healthcare Access, CHM Health Disparities, and CHM Misinformation & Mistrust by 1 PU each.",
    		"Reduce CHM Medical Supply Shortages and CHM Research & Development Expenses by 1 PU each.",
    		"The AP cost of all base actions (not enhancements) taken by The Health Behaviorist in this game sub-phase is reduced by 2 AP. The minimum cost of all actions taken by the enhanced role is 1 AP.",
    		"Cap the approximate PU value of any of the following CHMs for this game phase: Misinformation & Mistrust, Health Disparities, Research & Development Expenses, Medical Supply Shortages, Barriers to Healthcare Access."
    	},
    	new string[] {
    		"Reveal the approximate PU value of CHM Anxiety & Depression",
    		"Reveal the region(s) of the City Map that include the cluster for CHM Anxiety & Depression",
    		"Reduce CHM Anxiety & Depression by 2 PU and Reduce CHM Barriers to Healthcare Access by 2 PU",
    		"Reduce CHM Anxiety & Depression by 2 PU and Reduce CHM Misinformation & Mistrust by 2 PU",
    		"The AP cost of all base actions (not enhancements) taken by The Clinician in this sub-phase are reduced by 1 AP. The minimum cost of all actions by The Clinician is 1 AP.",
    		"Cap the approximate PU value of any of the following CHMs for this Phase: Deaths, Anxiety & Depression, misinformation & Mistrust."
    	}
    };

    public string[][] Enhancements = {
    	new string[] {
    		"",
    		"",
    		"Role-AP Spent: If The Drug Developer has already spent at least 5 AP during this game phase, roll 2 D6 and apply that total.",
    		"",
    		"Role-Action Taken: If The Health Behaviorist has already taken the action \"Establish telemedicine counseling program\" at any point in the game, triple the PU reduction",
    		""
    	},
    	new string[] {
    		"",
    		"Role-Action Taken: If The Clinician has already taken the action \"Review hospital-wide data\", at any point in the game, roll 2 D6 and lower the PU by that amount.",
    		"",
    		"Role-Add AP: Add 1 AP from The Politician to allow any already revealed regions of the City Map to be used to extend the 4 continuous regions. Already revealed regions do not count toward the 4 total.",
    		"",
    		"Role-Action Taken: If The Health Behaviorist has already taken the action \"Establish telemedicine counseling program\" at any point in the game, the cap is permanent for the game (Duration Level 2)"
    	},
    	new string[] {
    		"Role-Add AP: Add 1AP to roll a D6.  A roll of 4 or more reveals the precise PU value of CHM Research and Development Expenses (Graph - Insight Level 2). If the D6 is 3 or below, no additional insight occurs.",
    		"Mini Game-Scientific Breakthrough: Play the Scientific Breakthrough Mini-Game. If successful, reduce the PU of CHM Medical Supply Shortages accordingly.",
    		"Mini Game-Scientific Breakthrough: Play the Scientific Breakthrough Mini-Game. If successful, reduce the PU of CHM Hospitalizations accordingly.",
    		"Game Phase-Phase 1: Only in Phase 1, add 3 AP from The Drug Developer to roll a D6. Reduce CHM Research & Development Expenses by the number shown at the start of Phases 2 and 3.",
    		"",
    		""
    	},
    	new string[] {
    		"",
    		"Role-Action Taken: If The Politician has taken the action \"Negotiate public-private partnership for clinical lab renovations\" in this game phase, double the PU reduction.",
    		"Mini Game-Visual Acuity: Play the Visual Acuity Mini-Game. If The Laboratory Diagnostician passes the test, triple the PU reduction. If The Laboratory Diagnostician adds 1 AP, the team can help with the Visual Acuity Mini-Game.",
    		"",
    		"",
    		"Role-Action Taken: If The Media Campaigner has taken the action \"Collaborate with local community leaders to improve sanitation and water\" in this game phase, reduce infections PU by 1 D6."
    	},
    	new string[] {
    		"",
    		"",
    		"Mini Game-Scientific Breakthrough: Play the Scientific Breakthrough Mini-Game. If successful, reduce the PU of CHM Research & Development Expenses accordingly.",
    		"Mini Game-Visual Acuity: Play the Visual Acuity Mini-Game. If The Virologist succeeds, upgrade to Duration Level 2. If The Virologist adds 1 AP, the team can help with the Visual Acuity Mini-Game.",
    		"Mini Game-Visual Acuity: Play the Visual Acuity Mini-Game. If The Virologist succeeds, upgrade to Duration Level 2. If The Virologist adds 1 AP, the team can help with the Visual Acuity Mini-Game.",
    		""
    	},
    	new string[] {
    		"",
    		"",
    		"Role-Add AP & Mini Game-Logistics: Add 2 more AP from The Politician to play The Logistics Mini-Game. If the targeted region contains the cluster for CHM Misinformation & Mistrust, triple the PU reduction.",
    		"Role-Quiz: If The Health Behaviorist scored at or above 50% on their role quiz in this phase, double the PU reduction.",
    		"Mini Game-Speech!: Play The Speech! Mini Game. If successful, increase PU reduction by up to 4X.",
    		"Role-Action Taken: If The Laboratory Diagnostician has taken the action \"Call strategic planning meeting to improve sanitation and water\", in this game phase, reduce CHM Infections PU by 1 D6."
    	},
    	new string[] {
    		"",
    		"",
    		"Mini Game - Speech!: Add 1 AP from The Clinician -- a subject matter expert -- to play the Speech! Mini Game. If successful, increase PU reduction by up to 4X.",
    		"Mini Game - Logistics: Add 1 AP from The Drug Developer to play the Logistics Mini Game. Successfully targeting either CHM will double the reduction for both.",
    		"Mini Game - Speech!: Add 1 AP from The Health Behaviorist to play the Speech! Mini Game. The Health Behaviorist must assist in the speech, delivering 2 out of the required 5 key words/phrases. If successful, extend the Duration level of the Empower effect.",
    		"Role-Action Taken:  If The Health Behaviorist has taken the action \"Attend the Panel Discussion on Local Media Station\" in this game phase, reduce CMH Misinformation & Mistrust PU by 1 D6."
    	},
    	new string[] {
    		"Role-Add AP: Add 1 more AP from The Epidemiologist to upgrade to Insight Level 2",
    		"",
    		"Game Phase-Phase 1 & Role-Add AP: Only in Phase 1, add 2 AP from The Clinician to roll a d6. Reduce CHM Deaths by the number shown at the start of Phases 2 and 3.",
    		"",
    		"Mini-Game-Reflect: Play the Reflective Listening Mini-Game. If successful, upgrade the effect to Duration Level 2",
    		"Role-Action Taken: If The Politician has taken the action \"Cordinate a Panel Discussion on Local Media Station\" in this game phase, reduce CHM Misinformation & Mistrust PU by 1 D6."
    	}
    };

    public string[][] Limitations = {
    	new string[] {
    		"",
    		"",
    		"CHM-Below Threshold: CHM Research & Development Expenses must be below 35 PU",
    		"",
    		"",
    		""
    	},
    	new string[] {
    		"",
    		"",
    		"CHM-Below Threshold: CHM Misinformation & Mistrust must be below 35 PU",
    		"",
    		"CHM-Below Threshold: CHM Research & Development Expenses must be below 35 PU",
    		""
    	},
    	new string[] {
    		"",
    		"Game Phase-During: Scientific Breakthrough can only be attempted by The Drug Developer once per game sub-phase.",
    		"Game Phase-During: Scientific Breakthrough can only be attempted by The Drug Developer once per game sub-phase.",
    		"Role-Quiz: The Drug Developer must have scored at least 50% on the Role Quiz in this game phase.",
    		"",
    		"Gamemaster's perogative: If you do not make the budget line appear causative  to effect the CHM you are capping, it will not pass and you will lose your AP. "
    	},
    	new string[] {
    		"",
    		"",
    		"Game Phase-During: The Visual Acuity Mini-Game can only be attempted by The Laboratory Diagnostician once per game sub-phase.",
    		"Once a role has been empowered, it cannot be empowered again until the duration of the first empowerment is complete.",
    		"",
    		""
    	},
    	new string[] {
    		"",
    		"",
    		"Role-Quiz: The Virologist must have scored at least 50% on the Role Quiz in this game phase.",
    		"Game Phase-During: The Visual Acuity Mini-Game can only be attempted by The Virologist once per game phase. Once a role has been empowered, it cannot be empowered again until the duration of the first empowerment is complete.",
    		"Game Phase-During: The Visual Acuity Mini-Game can only be attempted by The Virologist once per game phase. Once a role has been empowered, it cannot be empowered again until the duration of the first empowerment is complete.",
    		""
    	},
    	new string[] {
    		"",
    		"Role-Quiz: The Media Campaigner must have scored at least 50% on the Role Quiz in this game phase.",
    		"",
    		"",
    		"CHM-Below Threshold: CHM Misinformation & Mistrust must be below 35 PU AND Game Phase-During: The Speech! Mini-Game can only be attempted by The Media Campaigner once per game phase.",
    		""
    	},
    	new string[] {
    		"",
    		"",
    		"Game Phase-During: The Speech! Mini-Game can only be attempted by The Politician twice per game phase.",
    		"",
    		"Game Phase-During: The Speech! Mini-Game can only be attempted by The Politician twice per game phase. Once a role has been empowered, it cannot be empowered again until the duration of the first empowerment is complete.",
    		""
    	},
    	new string[] {
    		"",
    		"",
    		"",
    		"",
    		"Once a role has been empowered, it cannot be empowered again until the duration of the first empowerment is complete.",
    		""
    	}
    };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    // Make sure AP costs are accurate
    void Update()
    {
    	everyTen++;
    	if (everyTen%10==0){
    		for (int i=0; i<8; i++){
    			if (variables.GetComponent<MainVariables>().empowered[i]){
    				for (int j=0; j<6; j++){
    					AP_Costs[i][j] = Mathf.Max(1, Beginning_AP_Costs[i][j] - variables.GetComponent<MainVariables>().empoweredAmounts[i]);
    				}
    			}
    			else {
    				for (int j=0; j<6; j++){
    					AP_Costs[i][j] = Beginning_AP_Costs[i][j];
    				}
    			}
    		}

    	}
    }
}
