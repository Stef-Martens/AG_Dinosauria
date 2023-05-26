using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jaguar_02 : AnimalIntro
{
    public Sprite GivenImage;

    public override bool HasActionAfterFirstDialogue { get { return false; } }
    public override bool HasActionAfterSecondDialogue { get { return true; } }

    public override bool SecondDialogeDirectlyAfterTalking { get { return false; } }

    public override bool ArrowLeft { get { return true; } }
    public override bool ArrowRight { get { return true; } }

    public GameObject interactItem;
    public GameObject skunk;

    public override List<string> dialogue
    {
        get
        {
            return new List<string>
            {
                "That monkey was way too loud. I'll sleep over here.",
                "Hmmrmmm.... Zzzz.....",
                "*The Jaguar has fallen asleep again!*",
                "*Maybe there is some way to wake up the Jaguar again?*"
            };
        }
    }

    public override List<string> secondDialogue
    {
        get
        {
            return new List<string>
            {
                "Ew, what's that smell?!",
                "I can't sleep over here... Fine, I'll go to the river then!",
                "*The Jaguar has fallen asleep again!*",
                "*Maybe there is some way to wake up the Jaguar again?*"
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
    }

    public override void DoSecondAction()
    {
        FindObjectOfType<Jaguar_03>().GetComponent<BoxCollider2D>().enabled = true;
        FindObjectOfType<Jaguar_03>().GetComponent<SpriteRenderer>().enabled = true;
        Destroy(FindObjectOfType<Crocodile>().gameObject);
        interactItem.SetActive(false);
        Destroy(gameObject);
        Destroy(skunk);
    }
}
