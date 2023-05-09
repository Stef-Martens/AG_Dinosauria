using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMain : AnimalIntro
{
    public Sprite GivenImage;

    public bool finished = false;

    public int AmountOfFrogsFound = 0;

    public override bool HasActionAfterFirstDialogue { get { return false; } }
    public override bool HasActionAfterSecondDialogue { get { return true; } }

    public override bool SecondDialogeDirectlyAfterTalking { get { return false; } }

    public override void Update()
    {
        if (AmountOfFrogsFound >= 3)
        {
            CurrentDialogue = secondDialogue;
        }
    }


    public override List<string> dialogue
    {
        get
        {
            return new List<string>
            {
                "Hi",
                "Find all my friends"
            };
        }
    }

    public override List<string> secondDialogue
    {
        get
        {
            return new List<string>
            {
                "Thank you for finding all my friends"
            };
        }
    }

    public override Sprite image { get { return GivenImage; } }

    public override string animalName { get { return "Frog"; } }



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
    }
}
