using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beehive : AnimalIntro
{
    public Sprite GivenImage;

    public GameObject interactItem;

    ItemInventory beehive;
    void Start()
    {
        beehive = new ItemInventory(GivenImage, "Honey");
        CurrentDialogue = dialogue;
    }

public override bool ArrowLeft { get { return true; } }
    public override bool ArrowRight { get { return false; } }

    public override bool HasActionAfterFirstDialogue { get { return true; } }
    public override bool HasActionAfterSecondDialogue { get { return false; } }

    public override bool SecondDialogeDirectlyAfterTalking { get { return false; } }


    public override List<string> dialogue
    {
        get
        {
            return new List<string>
            {
                "*There's a beehive full of delicious honey here...*",
                "*You take some of the honey, but oh no! The bee is angry! Quick, run away!"
            };
        }
    }


    public override Sprite image { get { return GivenImage; } }

    public override string animalName { get { return ""; } }



    public override List<string> CurrentDialogue { get; set; }

    public override void DoFirstAction()
    {
        FindObjectOfType<IntroPlayer>().inventory = beehive;

        foreach (FruitBat ding in FindObjectsOfType<FruitBat>())
        {
            if (ding.IsBee)
                ding.GoAfterPlayer = true;
        }

    }

    public override void DoSecondAction()
    {
    }
}
