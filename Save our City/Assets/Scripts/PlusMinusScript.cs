using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//a script for maintaining the plus-minus buttons on the share ap screen
public class PlusMinusScript : MonoBehaviour
{
	public GameObject parent, myself;
	public GameObject giveText, getText;
	public int whichGiver, whichGetter;
	public int[][] giveNums = new int[][] {
		new int[] {3, 3, 3, 3, 3, 3, 3, 3},
		new int[] {3, 3, 3, 3, 3, 3, 3, 3},
		new int[] {3, 3, 3, 3, 3, 3, 3, 3},
		new int[] {3, 3, 3, 3, 3, 3, 3, 3},
		new int[] {1, 3, 1, 1, 3, 3, 3, 3},
		new int[] {3, 3, 3, 3, 3, 3, 3, 3},
		new int[] {1, 1, 1, 1, 1, 1, 1, 1},
		new int[] {3, 3, 3, 3, 3, 3, 3, 3}
	};
	public int[][] getNums = new int[][] {
		new int[] {3, 2, 2, 2, 2, 2, 2, 3},
		new int[] {2, 3, 2, 2, 2, 2, 2, 2},
		new int[] {2, 2, 3, 2, 2, 2, 2, 2},
		new int[] {3, 2, 2, 3, 2, 2, 2, 2},
		new int[] {1, 2, 1, 1, 3, 2, 2, 2},
		new int[] {2, 2, 2, 2, 2, 3, 3, 2},
		new int[] {1, 1, 1, 1, 1, 1, 1, 1},
		new int[] {2, 2, 2, 2, 2, 3, 3, 3}
	};
	public int curVal = 0;
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

    void Display(){
    	myself.GetComponent<RectTransform>().anchoredPosition = new Vector3(-100, 140 - 40*whichGetter, 0);
    	giveText.GetComponent<Text>().text = "" + (curVal*giveNums[whichGiver][whichGetter]);
    	getText.GetComponent<Text>().text = "AP to "+parent.GetComponent<ActionGeneralScript>().variables.GetComponent<MainVariables>().Role_Names[whichGetter]+", who gets "+(curVal*getNums[whichGiver][whichGetter])+" AP";
    }

    public void plussed(){
    	curVal++;
    	return;
    }

    public void minussed(){
    	curVal = Mathf.Max(0, curVal-1);
    	return;
    }
}
