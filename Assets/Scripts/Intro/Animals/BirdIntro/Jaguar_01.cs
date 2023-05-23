using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jaguar_01 : AnimalIntro
{
    public Sprite GivenImage;

    public override bool HasActionAfterFirstDialogue { get { return false; } }
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
                "Oh? Who are you?",
                "I'm the mighty Jaguar! I love to eat meat, and I hunt all the creatures in this jungle!.",
                "I usually hunt during the night though! Right now I'm just sleepy. I'm a big cat so I like to sleep a lot.",
                "If you're looking for the hummingbird, he can be found in the flower field past the river.",
            };
        }
    }

    public override List<string> secondDialogue
    {
        get
        {
            return new List<string>
            {
                "What's that? You want me to get rid of the crocodile?",
                "I can do it for you! When I'm hungry I'll even eat a crocodile!",
                "But first.... I'm going to take a little nap....",
                "Zzzzzz",
                "*The Jaguar is sleeping. Maybe you can wake up him?*"
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
    }

}
