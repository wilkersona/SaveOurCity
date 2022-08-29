using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//script to manage a single quiz score when the GM edits it
public class QuizScoreMngr : MonoBehaviour
{
    public GameObject variables;
    public GameObject inputField, text, myself;
    public int which;

    // Start is called before the first frame update
    void Start()
    {
        Display();
    }

    // Update is called once per frame
    void Update()
    {
        if (variables.GetComponent<MainVariables>().popupSignalLive && variables.GetComponent<MainVariables>().popupSignalNum==(10 + which)){
        	variables.GetComponent<MainVariables>().popupSignalLive = false;
        	saveScore();
        }
    }

    void Display() {
        myself.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -25 - 40 * which, 0);
        text.GetComponent<Text>().text = variables.GetComponent<MainVariables>().Role_Names[which];
    	inputField.GetComponent<InputField>().text = "" + variables.GetComponent<MainVariables>().quizScores[which];
    }

    void saveScore() {
    	variables.GetComponent<MainVariables>().quizScores[which] = int.Parse(inputField.GetComponent<InputField>().text);
        Display();
    }
}
