using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabClaw : AnimalIntro
{
    public Sprite GivenImage;

    public override bool HasActionAfterFirstDialogue { get { return true; } }
    public override bool HasActionAfterSecondDialogue { get { return false; } }

    public override bool SecondDialogeDirectlyAfterTalking { get { return false; } }

    public override bool ArrowLeft { get { return false; } }
    public override bool ArrowRight { get { return false; } }


    public override List<string> dialogue
    {
        get
        {
            return new List<string>
            {
                "There is a crab claw poking out of the ground here.",
                "You scare away the crab hiding under the sand before it can hurt anyone."
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
