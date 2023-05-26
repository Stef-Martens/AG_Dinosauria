using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jaguar_03 : AnimalIntro
{
    public Sprite GivenImage;
    public Sprite SleepingJaguar;

    public override bool HasActionAfterFirstDialogue { get { return true; } }
    public override bool HasActionAfterSecondDialogue { get { return false; } }

    public override bool SecondDialogeDirectlyAfterTalking { get { return true; } }

    public override bool ArrowLeft { get { return false; } }
    public override bool ArrowRight { get { return false; } }

    public GameObject interactItem;

    public override List<string> dialogue
    {
        get
        {
            return new List<string>
            {
                "I didn't manage to catch the crocodile! Guess I'll just sleep instead.",
                "Oh, by the way did you know that I'm also considered a panther?",
                "My brother was born with black fur - He is what we call a black panther!",
                "Anyways, I'm going to sleep now...",
                "Zzzzzz"
            };
        }
    }

    public override List<string> secondDialogue
    {
        get
        {
            return new List<string>
            {
                "*The Jaguar is sleeping. You decide to leave it alone*"
            };
        }
    }

    public override Sprite image { get { return GivenImage; } }

    public override string animalName { get { return "Jaguar"; } }



    public override List<string> CurrentDialogue { get; set; }
    protected virtual void Start()
    {
        CurrentDialogue = dialogue;
    }

    public override void DoFirstAction()
    {
        interactItem.SetActive(false);
        GetComponent<SpriteRenderer>().sprite = SleepingJaguar;
    }

    public override void DoSecondAction()
    {

    }
}
