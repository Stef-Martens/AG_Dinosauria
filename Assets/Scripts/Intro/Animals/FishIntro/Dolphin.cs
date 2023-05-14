using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dolphin : AnimalIntro
{
    public Sprite GivenImage;

    public override bool HasActionAfterFirstDialogue { get { return false; } }
    public override bool HasActionAfterSecondDialogue { get { return true; } }

    public override bool SecondDialogeDirectlyAfterTalking { get { return false; } }


    public override List<string> dialogue
    {
        get
        {
            return new List<string>
            {
                "Hello, who are you? I'm the dolphin. Everyone loves me!",
                "I'm a marine mammal, and I'm a carnivore - I love to eat meat, like little fishes. What do you like to eat?",
                "I can help you find the Surgeonfish - she lives out in the reef. I can take you there!",
                "But before I help you, you need to help me! I need you to do two things:",
                "First, I need you to help the baby turtle get to the sea.",
                "Second, I need my ball back - I think the seals took my ball. Can you go look?",
                "If you've done everything you can come back to me"
            };
        }
    }

    public override List<string> secondDialogue
    {
        get
        {
            return new List<string>
            {
                "Thankyou for helping me! You did a great job!",
                "Come, I'll bring you to the Surgeonfish now."
            };
        }
    }

    public override Sprite image { get { return GivenImage; } }

    public override string animalName { get { return "Dolphin"; } }



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
        // vis actief zetten
        // zelf destroyen

        foreach (var item in FindObjectOfType<SurgeonAnimal>().GetComponents<BoxCollider2D>())
        {
            item.enabled = true;
        }
        FindObjectOfType<SurgeonAnimal>().GetComponent<SpriteRenderer>().enabled = true;

        Destroy(gameObject);
    }
}
