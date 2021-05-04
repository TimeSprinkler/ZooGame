using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimalData {

    //public static List<string> mAnimalTypeList;
    public static Dictionary<string, Dictionary<string, string>> mAnimalDictionary = new Dictionary<string, Dictionary<string, string>>();

    public static string Query(string attribute, string name) {

        Debug.Assert(AnimalData.mAnimalDictionary.Count > 0);

        Dictionary<string, string> tempDictionary = mAnimalDictionary[attribute];

        Debug.Assert(mAnimalDictionary[attribute] != null);

        string tempString = "";
        if ( tempDictionary.TryGetValue(name, out tempString)){
            return tempString;
        }else{
            return null;
        }
    }

    public static List<string> QueryAllValues(string attribute) {

        List<string> tempList = new List<string>();

        Dictionary<string, string> tempDictionary = mAnimalDictionary[attribute];
        List<string> keyList = new List<string>(tempDictionary.Keys);
        string tempString;

        foreach (string key in keyList) {      
            if(tempDictionary.TryGetValue(key, out tempString)){
                tempList.Add(tempString);
            }
        }

        return tempList;
    }
}
