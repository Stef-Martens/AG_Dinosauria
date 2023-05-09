using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : AnimalIntro
{
    public Sprite GivenImage;

    ItemInventory fruit;
    void Start()
    {
        fruit = new ItemInventory(GivenImage, "Fruit");
        CurrentDialogue = dialogue;
    }


    public override bool HasActionAfterFirstDialogue { get { return true; } }
    public override bool HasActionAfterSecondDialogue { get { return false; } }

    public override bool SecondDialogeDirectlyAfterTalking { get { return false; } }


    public override List<string> dialogue
    {
        get
        {
            return new List<string>
            {
                "*you pick up the fruit",
                "*move out quick because the bat is angry*"
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

    public override void DoFirstAction()
    {
        FindObjectOfType<IntroPlayer>().inventory = fruit;

        foreach (FruitBat ding in FindObjectsOfType<FruitBat>())
        {
            if (!ding.IsBee)
                ding.GoAfterPlayer = true;
        }

    }

    public override void DoSecondAction()
    {
    }
}
