using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GPSNearbyTest : MonoBehaviour {

    public List<GPSSolution> mLocations;
    public float mHomeLat;
    public float mHomeLon;

   /* void Start() {

        string name;
        float distanceBetween;

        for(int i = 0; i < mLocations.Count; i++) {

            name = mLocations[i].gameObject.name;

            distanceBetween = mLocations[i].DistanceBetweenGPSPoints(mHomeLat, mHomeLon, mLocations[i].mLatitude, mLocations[i].mLongitude);

            Debug.Log(name + " : " + distanceBetween);
        }
    }*/
}
