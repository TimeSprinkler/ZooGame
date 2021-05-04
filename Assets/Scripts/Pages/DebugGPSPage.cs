using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DebugGPSPage : BasePage {

    public Text mDebugText;

    public void UpdateText() {

        mDebugText.text = "";
        float distanceBetween;
        float mHomeLat = GPSManager.mPlayerGPSLocation.latitude;
        float mHomeLon = GPSManager.mPlayerGPSLocation.longitude;

        for (int i = 0; i < transform.parent.GetComponent<GPSNearbyTest>().mLocations.Count; i++) {

            GPSSolution solution = transform.parent.GetComponent<GPSNearbyTest>().mLocations[i];

            distanceBetween = solution.DistanceBetweenGPSPoints(mHomeLat, mHomeLon, solution.mLatitude, solution.mLongitude);

            mDebugText.text += solution.mLatitude + " , " + solution.mLongitude + " : " + distanceBetween + " \n";

        }
        

        mDebugText.text += GPSManager.mPlayerGPSLocation.horizontalAccuracy + " \n" + "Your location: " + mHomeLat + " , " + mHomeLon;

    }

}
