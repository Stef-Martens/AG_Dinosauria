using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : AnimalIntro
{
    public Sprite GivenImage;

    public GameObject interactItem;

    public override bool HasActionAfterFirstDialogue { get { return true; } }
    public override bool HasActionAfterSecondDialogue { get { return false; } }

    public override bool SecondDialogeDirectlyAfterTalking { get { return false; } }

    public override bool ArrowLeft { get { return true; } }
    public override bool ArrowRight { get { return false; } }


    public override List<string> dialogue
    {
        get
        {
            return new List<string>
            {
                "Yo! I'm a big crab! Look at me!",
                "I'm not like you, because I am an invertebrate. That means I don't have bones in my body.",
                "In my case my strong armour is my skeleton on the outside of my body - that's why they call it an exoskeleton.",
                "I'm an omnivore. I'll eat anything! I can use my claws to pinch creatures that scare me or grab my food.",
                "We can also use our claws to dig. There's a lot of my friends hidden under the ground here. We hide under rocks and sand.",
                "Don't worry about me. I'm a happy crab. I'm not hungry. I'll leave everyone alone."
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

    public override string animalName { get { return "Crab"; } }



    public override List<string> CurrentDialogue { get; set; }
    protected virtual void Start()
    {
        CurrentDialogue = dialogue;
    }

    public override void DoFirstAction()
    {
        interactItem.SetActive(false);
        GetComponent<SpriteRenderer>().enabled = false;
    }

    public override void DoSecondAction()
    {
    }
}
