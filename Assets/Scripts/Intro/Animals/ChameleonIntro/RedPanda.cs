using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPanda : AnimalIntro
{
    public Sprite GivenImage;

    bool finished = false;

    public override bool HasActionAfterFirstDialogue { get { return false; } }
    public override bool HasActionAfterSecondDialogue { get { return true; } }

    public override bool SecondDialogeDirectlyAfterTalking { get { return false; } }


    public override List<string> dialogue
    {
        get
        {
            return new List<string>
            {
                "Hi",
                "I need milk"
            };
        }
    }

    public override List<string> secondDialogue
    {
        get
        {
            return new List<string>
            {
                "thank you"
            };
        }
    }

    public override Sprite image { get { return GivenImage; } }

    public override string animalName { get { return "Red Panda"; } }



    public override List<string> CurrentDialogue { get; set; }
    protected virtual void Start()
    {
        CurrentDialogue = dialogue;
    }

    public override void DoFirstAction()
    {
    }

    public override void DoSecondAction()
    {
        if (!finished)
            FindObjectOfType<ChameleonIntro>().TasksDone++;

        finished = true;

        FindObjectOfType<IntroPlayer>().ClearInventory();
    }
}
