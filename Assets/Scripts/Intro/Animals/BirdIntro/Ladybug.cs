using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladybug : AnimalIntro
{
    ItemInventory ladybug;
    void Start()
    {
        ladybug = new ItemInventory(GivenImage, "Ladybug");
        CurrentDialogue = dialogue;
    }

    public Sprite GivenImage;

    public override bool HasActionAfterFirstDialogue { get { return true; } }
    public override bool HasActionAfterSecondDialogue { get { return false; } }

    public override bool SecondDialogeDirectlyAfterTalking { get { return false; } }

    public override bool ArrowLeft { get { return false; } }
    public override bool ArrowRight { get { return true; } }


    public override List<string> dialogue
    {
        get
        {
            return new List<string>
            {
                "Hey, you found me! I'm a ladybug - I'm family of the beetle.",
                "I'm an omnivore - like to eat plants and the little insects that live on them.",
                "I bet my brother Beetle is worried about me. Can you bring me to him?"
            };
        }
    }

    public override Sprite image { get { return GivenImage; } }

    public override string animalName { get { return "Ladybug"; } }



    public override List<string> CurrentDialogue { get; set; }


    public override void DoFirstAction()
    {
        FindObjectOfType<IntroPlayer>().inventory = ladybug;
    }

    public override void DoSecondAction()
    {
    }
}
