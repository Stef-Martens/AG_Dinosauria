using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beetle2 : AnimalIntro
{
    public Sprite GivenImage;

    public override bool HasActionAfterFirstDialogue { get { return false; } }
    public override bool HasActionAfterSecondDialogue { get { return false; } }

    public override bool SecondDialogeDirectlyAfterTalking { get { return false; } }

    public override bool ArrowLeft { get { return false; } }
    public override bool ArrowRight { get { return true; } }

    public GameObject interactItem;

    public override List<string> dialogue
    {
        get
        {
            return new List<string>
            {
                "Thankyou again for finding my sister!",
                "Did you know that according to the cultures of the old natives of Mexico, we beetles are proud and strong warriors?",
                "Haha! With my six legs, there is nothing I can't overcome.",
            };
        }
    }

    public override Sprite image { get { return GivenImage; } }

    public override string animalName { get { return "Beetle"; } }



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
