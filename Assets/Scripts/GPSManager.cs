using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GPSManager : MonoBehaviour {

    public static LocationInfo mPlayerGPSLocation = new LocationInfo(); //use struct Location Info
    public static int mNearbyDistance = 50; //meters
    public static bool mGPSIsPaused = false;
    //public static string mDebugString = "";

    public int mTimeOutTime = 20;
    public int mQualityTimeMinutes = 2;
    public GameObject mLoadingObject;
    public Text mLoadingDebugString;

    private bool mCheckingLocation = false;
    private float mGPSUpdateTime = 1.0f;  

	void Start () {

        InitializeGPS();
    }
	
	// Update is called once per frame
	void Update () {

        if (!mGPSIsPaused) {
            if (Input.location.status == LocationServiceStatus.Running && !mCheckingLocation) {
                StartCoroutine(CheckGPSLocation());
            }
        }       
	}

    //This is placed above Focus in hopes it is called and handled before Focus
    void OnApplicationPause(bool state) {
        if (state) {
            Input.location.Stop();

            mGPSIsPaused = state;
        }
    }

    void OnApplicationFocus(bool state) {

        if (state) {
           StartCoroutine(InitializeGPS());
        }
        else {
           Input.location.Stop();
        }
    }

    /*Function Check the GPS location every so often*/
    IEnumerator CheckGPSLocation() {

        mCheckingLocation = true;
        if(IsBetterLocation(mPlayerGPSLocation, Input.location.lastData)) {
            mPlayerGPSLocation = Input.location.lastData;
        }

        yield return new WaitForSeconds(mGPSUpdateTime);

        mCheckingLocation = false;
    }

    public void UpdateGPSLocationManually() {
        StopCoroutine(CheckGPSLocation());
        StartCoroutine(CheckGPSLocation());
    }

    private bool IsBetterLocation(LocationInfo currentBestLocation, LocationInfo location) {

        if(currentBestLocation.latitude == 0) {
            return true;
        }

        float waitUpdateTimeMS = 60 * mQualityTimeMinutes;//Timestamp is in seconds, so convert minutes to seconds

        double timeDelta = currentBestLocation.timestamp - location.timestamp;
        bool isSignificantlyNewer = timeDelta > waitUpdateTimeMS;
        bool isSignificantlyOlder = timeDelta < -waitUpdateTimeMS;
        bool isNewer = timeDelta > 0;
        bool isAlotNewer = (timeDelta + 15) > 0;

        if (isSignificantlyNewer) {
            return true;
        }
        else if(isSignificantlyOlder){
            return false;
        }

        float deltaAccuracy = location.horizontalAccuracy - currentBestLocation.horizontalAccuracy;

        bool isLessAccurate = deltaAccuracy > 0;
        bool isMoreAccurate = deltaAccuracy < 0;
        bool isSignificantlyLessAccurate = deltaAccuracy > 100;

        if (isMoreAccurate) {
            return true;
        }else if(isNewer && isLessAccurate) {
            return true;
        }else if(isAlotNewer && !isSignificantlyLessAccurate) {
            return true;
        }

        return false;
    }

    IEnumerator InitializeGPS() {

        if (!Input.location.isEnabledByUser) {
            Debug.Log("GPS not enabled by User");

            yield break;
        }

        Input.location.Start();
        mLoadingObject.SetActive(true);

        while (Input.location.status == LocationServiceStatus.Initializing && mTimeOutTime > 0) {
            yield return new WaitForSeconds(1);
            mTimeOutTime--;
        }

        if (mTimeOutTime < 1) {
            Debug.LogWarning("Timed Out");
            mLoadingDebugString.text = "Timed Out";
            yield break;
        }

        if (LocationServiceStatus.Failed == Input.location.status) {
            Debug.Log("Unable To Determine Location");
            mLoadingDebugString.text = "Unable To Determine Location";

            yield break;
        }
        else {
            mLoadingDebugString.text = "success";
            mPlayerGPSLocation = Input.location.lastData;

        }

        mLoadingObject.SetActive(false);
        mGPSIsPaused = false;
    } 
}
