using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apples : AnimalIntro
{
    ItemInventory apple;
    void Start()
    {
        apple = new ItemInventory(GivenImage, "Apple");
        CurrentDialogue = dialogue;
    }

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
                "Delicious fresh fruit hangs from the tree here!",
                "You pick up a delicious piece of fruit. Maybe someone would like to eat it?"
            };
        }
    }

    public override Sprite image { get { return GivenImage; } }

    public override string animalName { get { return "Apple"; } }



    public override List<string> CurrentDialogue { get; set; }


    public override void DoFirstAction()
    {
        FindObjectOfType<IntroPlayer>().inventory = apple;
    }

    public override void DoSecondAction()
    {
    }
}
