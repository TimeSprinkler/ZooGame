using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class StoryPage : BasePage {

    public BasePage mYesPage;
    public BasePage mNoPage;

    public void OpenYesPage() { OpenAnotherPage(mYesPage); }//Add null checks?
    public void OpenNoPage() { OpenAnotherPage(mNoPage); }//Add null checks?
    public void OpenNextPage() { OpenAnotherPage(mNextPage); }//Add null checks?

    private void OpenAnotherPage(BasePage pageToOpen){
        pageToOpen.OpenPage(this);
    }

    void Awake()
    {
        //Put asserts here as null checks for the type of page
    }

    

}
