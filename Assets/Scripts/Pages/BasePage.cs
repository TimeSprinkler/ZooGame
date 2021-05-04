using UnityEngine;
using System.Collections;

public class BasePage : MonoBehaviour {

    //Requirements

    public BasePage mNextPage;

    public void NextPage()
    {
        mNextPage.OpenPage(this);
    }

    public void OpenPage(BasePage previousPage)
    {
        this.gameObject.SetActive(true);
        previousPage.ClosePage();

    }

    public void ClosePage()
    {
        this.gameObject.SetActive(false); 
    }

    //Fade in and fade out functions
}
