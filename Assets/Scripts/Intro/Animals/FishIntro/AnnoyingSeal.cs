using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnoyingSeal : AnimalIntro
{
    public Sprite GivenImage;

    public GameObject interactItem;

    public override bool HasActionAfterFirstDialogue { get { return false; } }
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
                "Hello, I am a seal! I'm a marine mammal - which means I'm a mammal that lives in the sea.",
                "I'm a carnivore, and I usually eat fish and other small animals that live in the ocean.",
                "When I'm not hunting, I like to lay on the beach. I also love to play, like with this ball!",
                "Oh, do you want the ball? You can have it!",
                "Be careful though, my friend at the top of the hill really likes to play and will slap the ball out of your hands back down to me!"
            };
        }
    }

    public override List<string> secondDialogue
    {
        get
        {
            return new List<string>
            {
                "If you pick up the ball be careful of my friend at the top of the hill.",
                "He will slap the ball out of your hands, back down to me!"
            };
        }
    }

    public override Sprite image { get { return GivenImage; } }

    public override string animalName { get { return "Seal"; } }



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
