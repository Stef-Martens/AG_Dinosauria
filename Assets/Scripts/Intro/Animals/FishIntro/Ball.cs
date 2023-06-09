using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : AnimalIntro
{
    ItemInventory BallInventory;
    void Start()
    {
        BallInventory = new ItemInventory(GivenImage, "Ball");
        CurrentDialogue = dialogue;
    }

    public Sprite GivenImage;

    public override bool HasActionAfterFirstDialogue { get { return true; } }
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
                "You've found the dolphin's ball! You pick up the ball.",
            };
        }
    }

    public override Sprite image { get { return GivenImage; } }

    public override string animalName { get { return "Ball"; } }



    public override List<string> CurrentDialogue { get; set; }


    public override void DoFirstAction()
    {
        FindObjectOfType<IntroPlayer>().inventory = BallInventory;
        Destroy(gameObject);
    }

    public override void DoSecondAction()
    {
    }
}
