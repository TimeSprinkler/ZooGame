using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;


public class LoadInAnimalData {

    //I want a system that will load in all animals that are ot be used in each story at the loading up of the story.
    //Use Mini JSON to serialize the animal data into the lists.  The script will need to accept a list 

    //MiniJSOn will be replaced and the entire system should eventually move to a database as it will be faster

    private static string mJSONFileName = "AnimalsSampleJSON";
    private static Dictionary<string, object> mJSONData;

    //private static int mAnimalTypeKeyInt = 0;

    public static void CreateDictionary()
    {
        TextAsset mFile = Resources.Load(mJSONFileName, typeof(TextAsset)) as TextAsset;
        mJSONData = MiniJSON.Json.Deserialize(mFile.text) as Dictionary<string, object>;

        foreach(KeyValuePair<string, object> pair in mJSONData) {

            List<object> tempObjectList = (List<object>)(pair.Value);
            string key = pair.Key;

            Dictionary<string, string> value = ConvertObjectToDictionary(tempObjectList);
            AnimalData.mAnimalDictionary.Add(key, value);

        }
    }

    private static Dictionary<string,string> ConvertObjectToDictionary(List<object> incomingObjectList) {

        Dictionary<string, string> tempDictionary = new Dictionary<string, string>();

        string tempKey;
        string tempValue;
       
        foreach(object incomingObject in incomingObjectList) {

            List<string> keyList = new List<string>((incomingObject as Dictionary<string, object>).Keys);

            foreach(string key in keyList) {
                tempKey = key;
                tempValue = (incomingObject as Dictionary<string, object>)[tempKey].ToString();

                tempDictionary.Add(tempKey, tempValue);
            }         
        }

        Debug.Assert(tempDictionary.Count > 0);

        return tempDictionary;
    }

   /* private static List<string> UnpackHabitatKeywords(object animalObject){

        List<string> tempList = new List<string>();

        string stringToParse = (animalObject as Dictionary<string, object>)["Habitat_Keywords"].ToString();
        int stringIndex = 0;

        while(stringIndex < stringToParse.Length){

            string singleString = stringToParse.Substring(stringIndex, stringToParse.IndexOf("/"));
            tempList.Add(singleString);

            stringIndex = FindNextDividerSymbol(stringIndex, stringToParse);
        }

        return tempList;
    }

    private static int FindNextDividerSymbol(int index, string stringToParse){
        int tempInt = index + 1;

        for(int i = tempInt; i < stringToParse.Length + 1; i++){
            if (stringToParse[i] == '/') return i;
        }

        return stringToParse.Length;
    }*/
}
