using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabClaw : AnimalIntro
{
    public Sprite GivenImage;

    public override bool HasActionAfterFirstDialogue { get { return true; } }
    public override bool HasActionAfterSecondDialogue { get { return false; } }

    public override bool SecondDialogeDirectlyAfterTalking { get { return false; } }


    public override List<string> dialogue
    {
        get
        {
            return new List<string>
            {
                "You remove the crab claw."
            };
        }
    }

    public override List<string> secondDialogue
    {
        get
        {
            return new List<string>
            {

            };
        }
    }

    public override Sprite image { get { return GivenImage; } }

    public override string animalName { get { return "Crab Claw"; } }



    public override List<string> CurrentDialogue { get; set; }
    protected virtual void Start()
    {
        CurrentDialogue = dialogue;
    }

    public override void DoFirstAction()
    {
        Destroy(gameObject);
    }

    public override void DoSecondAction()
    {
    }
}