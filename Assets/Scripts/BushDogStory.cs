using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BushDogStory : MonoBehaviour {

    public GPSPuzzlePage mLookingForNewLocationPage; //First Page is always the opening Page
    public DebugGPSPage mWrongLocationPage;
    public Text mLocationUpdateText;

    public List<string> mDiscoveredAttributeList;
    public Text mAttributeText;
    public List<BasePage> mAnimalsPages;

    void Awake(){       
        LoadInAnimalData.CreateDictionary();
    }

    private void UpdateAttributeText(){

        string text ="";

        for (int i = 0; i < mDiscoveredAttributeList.Count; i++) {

            text += mDiscoveredAttributeList[i];
            text += " \n";
        }
        mAttributeText.text = text;
        
    }

    public void AcquiredNewAttribute(string attribute, string gpsTextUpdate){

        if(attribute.Length > 0) {
            mDiscoveredAttributeList.Add(attribute);
            UpdateAttributeText();
            mLookingForNewLocationPage.mGPSDisplayText.text = gpsTextUpdate;
        }        
    }

    public void GPSCheck(){

        bool hasFoundAPage = false;
        float closestDistance = 1000f;
        
        for(int i = 0; i < mAnimalsPages.Count; i++) {

            GPSSolution currentPageGPSSolution = mAnimalsPages[i].GetComponent<GPSSolution>();

            if (currentPageGPSSolution.AreGPSPointsNear(closestDistance)) {

                closestDistance = currentPageGPSSolution.mDistanceFromPlayer;

                if (!currentPageGPSSolution.mHasBeenVisited) {
                    mLocationUpdateText.text = currentPageGPSSolution.mDistanceFromPlayer.ToString();
                    mAnimalsPages[i].GetComponent<BasePage>().OpenPage(mLookingForNewLocationPage);

                    hasFoundAPage = true;

                    break;
                }
                else {
                    closestDistance = currentPageGPSSolution.mDistanceFromPlayer;
                }                
            }            
        }

        mLocationUpdateText.text = GPSManager.mPlayerGPSLocation.latitude.ToString() + ", " + GPSManager.mPlayerGPSLocation.longitude.ToString();

       if (!hasFoundAPage) {
            mWrongLocationPage.UpdateText();
            mWrongLocationPage.OpenPage(mLookingForNewLocationPage);
       }

        
    }

 //Animals listed should have the same position in dictionarys as their page in the PageList for this story
}
