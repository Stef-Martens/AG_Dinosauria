using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordfish : AnimalIntro
{
    public Sprite GivenImage;

    public override bool HasActionAfterFirstDialogue { get { return false; } }
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
                "test",
                "test",
            };
        }
    }

    public override List<string> secondDialogue
    {
        get
        {
            return new List<string>
            {
                "test",
                "test"
            };
        }
    }

    public override Sprite image { get { return GivenImage; } }

    public override string animalName { get { return "Swordfish"; } }



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
    }
}
