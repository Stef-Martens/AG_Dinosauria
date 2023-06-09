using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerChameleon : AnimalIntro
{
    public Sprite GivenImage;

    public override bool HasActionAfterFirstDialogue { get { return false; } }
    public override bool HasActionAfterSecondDialogue { get { return false; } }

    public override bool SecondDialogeDirectlyAfterTalking { get { return false; } }

    public override bool ArrowLeft { get { return false; } }
    public override bool ArrowRight { get { return false; } }

    public GameObject interactItem;


    public override List<string> dialogue
    {
        get
        {
            return new List<string>
            {
                "",
            };
        }
    }

    public override List<string> secondDialogue
    {
        get
        {
            return new List<string>
            {
                ""
            };
        }
    }

    public override Sprite image { get { return GivenImage; } }

    public override string animalName { get { return ""; } }



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
