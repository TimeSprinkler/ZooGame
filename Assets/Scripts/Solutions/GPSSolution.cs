using UnityEngine;
using System.Collections;

public class GPSSolution : Solution {

    public float mLatitude;
    public float mLongitude;

    [HideInInspector] public bool mHasBeenVisited = false;
    [HideInInspector] public float mDistanceFromPlayer;

    public bool AreGPSPointsNear(float lastShortestDistance = 1000f) {

        mDistanceFromPlayer = DistanceBetweenGPSPoints(GPSManager.mPlayerGPSLocation.latitude, GPSManager.mPlayerGPSLocation.longitude, mLatitude, mLongitude);

        if (mDistanceFromPlayer <= GPSManager.mNearbyDistance) {
            if (mDistanceFromPlayer < lastShortestDistance) {
                return true;
            }
            else {
                return false;
            }
        }
        else {
            return false;
        }
    }

    //Using the Haversine formula
    public float DistanceBetweenGPSPoints(float lat1, float lon1, float lat2, float lon2) {

        float conversionToMeters = 6371000;

        //convertToRads
        float thetaInRads = deg2rad(lon2 - lon1);
        float phiInRads = deg2rad(lat2 - lat1);
        lat1 = deg2rad(lat1);
        lat2 = deg2rad(lat2);

        float a = Mathf.Sin(phiInRads / 2f) * Mathf.Sin(phiInRads / 2f) + Mathf.Cos(lat1) * Mathf.Cos(lat2) * Mathf.Sin(thetaInRads / 2f) * Mathf.Sin(thetaInRads / 2f);
        float c = 2f * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));

        float dist = c * conversionToMeters;

        return (dist);
    }

    private float deg2rad(float deg) {
        return (deg * Mathf.PI / 180.0f);
    }

    private float rad2deg(float rad) {
        return (rad / Mathf.PI * 180.0f);
    }

}
