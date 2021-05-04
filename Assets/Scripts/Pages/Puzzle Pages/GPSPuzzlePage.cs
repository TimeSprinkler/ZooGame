using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GPSPuzzlePage : PuzzlePage {

    public Text mGPSDisplayText;
    public Text mSucceedText;
    public GPSSolution mSolutionGPS;

    public string mFailureText = "Please move closer to Rackham Fountain.";

   public void PressedHereButton() {

        if (!Input.location.isEnabledByUser) {
            mSucceedText.text = GPSManager.mPlayerGPSLocation.latitude.ToString();
            RequirementMet();
        }

        mGPSDisplayText.text = GPSManager.mPlayerGPSLocation.latitude.ToString() + " , " + GPSManager.mPlayerGPSLocation.longitude.ToString();

        if (mSolutionGPS.AreGPSPointsNear()) {

            RequirementMet();
        }
    }
}
