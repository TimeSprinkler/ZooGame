using UnityEngine;
using System.Collections;

public class PuzzlePage : BasePage {

    protected void RequirementMet()
    {
        //Event for when a requirement is met

        mNextPage.OpenPage(this);
    }

    //Test Story

    //An animal escaped check animals with brown fur and a shout nose
    //Also, check the bears to make sure they are still there
    //Bears -> red panda -> prairie dogs -> otters -> Bush Dogs

    //Puzzle tyeps: GPS Puzzle, Timer Puzzle, Correct Answers puzzle 
}
