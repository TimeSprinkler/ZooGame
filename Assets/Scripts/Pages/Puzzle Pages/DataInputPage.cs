using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DataInputPage : PuzzlePage {

    public List<InputField> mInputText;
    public string mUpdateText;
    public string mAdditionalStoryText;
    public BushDogStory mBushDogStoryScript;

    [SerializeField] private Text mHeaderText;
    [SerializeField]private Button mSubmitButton;

    private bool[] mInputFieldIsCorrect;

    void Awake() {   

        mInputFieldIsCorrect =new bool[mInputText.Count];//Everything should be false at this point

        for(int i = 0; i < mInputFieldIsCorrect.Length; i++) {
            Debug.Assert(!mInputFieldIsCorrect[i]);
        }
    }
    public void CheckRequirement() {

        foreach(InputField i in mInputText) {
            CheckForAcceptableAnswer(i);
        }

        CheckDataFields();
    }

    private void CheckDataFields(){

        bool allTrue = true;

        for(int i = 0; i < mInputFieldIsCorrect.Length; i++) {
            if (!mInputFieldIsCorrect[i]) {
                allTrue = false;
                mInputText[i].textComponent.text = "";
                Debug.Log(i + " is false");
            }
        }

        if (allTrue) {
            mBushDogStoryScript.AcquiredNewAttribute(mUpdateText, mAdditionalStoryText);

            GPSSolution tempGPSSolution = this.GetComponent<GPSSolution>();

            if(tempGPSSolution != null){
                Debug.Log("set!");
                tempGPSSolution.mHasBeenVisited = true;
            }

            RequirementMet();
        }
        else {
            //this is where i tell the player something is wrong
            string tempText = "Some of your data doesn't make sense. Please look again.";
            mHeaderText.text = tempText;
        }
    }

    private void CheckForAcceptableAnswer(InputField inField) {

        string animalType = inField.transform.parent.parent.name;//I know this is dumb and clunky but its so that it works for the prototype
        string attribute = inField.transform.parent.name;

        string correctAnswer = AnimalData.Query(attribute, animalType);
        string playerAnswer = inField.text;

        int indexToChange = GetindexToChange(attribute);
        Debug.Assert(indexToChange >= 0);

        switch (attribute) {

            default:
                break;

            case "Animal_Name":
            case "Habitat_Continent":
            case "BonusString":
                mInputFieldIsCorrect[indexToChange] = StringAnswerCheck(correctAnswer, playerAnswer, indexToChange);

                break;

            case "BonusInt":
            case "Quantity":

                mInputFieldIsCorrect[indexToChange] = SizeAnswerCheck(correctAnswer, playerAnswer, indexToChange);

                break;

        }
    }

    private bool SizeAnswerCheck(string correctAnswer, string playerAnswer, int indexToChange) {

        int playerAnswerInt = int.Parse(playerAnswer);
        //bool result = int.TryParse(playerAnswer, out playerAnswerInt);

        int answer = int.Parse(correctAnswer.ToString());
        float answerTolerance = 0.2f;

        if (playerAnswerInt < (answer * (1f +  answerTolerance)) && playerAnswerInt > (answer * ( 1f - answerTolerance))) {
            return true;
        }
        else {
            //mInputText[indexToChange].text = "";
            mInputText[indexToChange].placeholder.color = Color.red;
            return false;
        }
    }

    private bool StringAnswerCheck(string correctAnswer, string playerAnswer, int indexToChange) {

        if(correctAnswer.ToLower() == playerAnswer.ToLower()) {
            return true;
        }
        
        mInputText[indexToChange].placeholder.color = Color.red;
        return false;
    }

    private int GetindexToChange(string name) {

        for (int i = 0; i < mInputText.Count; i++) {
            if (mInputText[i].transform.parent.name == name) {
                return i;
            }
        }

        return -1;
    }

    //Animal stores all the correct answers and you only link the animal to the text box group
    //The text box names need to correspond to the attributes of the animal
}
