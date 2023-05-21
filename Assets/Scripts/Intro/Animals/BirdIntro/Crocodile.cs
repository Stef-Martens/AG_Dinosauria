using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crocodile : AnimalIntro
{
    public Sprite GivenImage;

    public override bool HasActionAfterFirstDialogue { get { return true; } }
    public override bool HasActionAfterSecondDialogue { get { return true; } }

    public override bool SecondDialogeDirectlyAfterTalking { get { return true; } }

    public override bool ArrowLeft { get { return true; } }
    public override bool ArrowRight { get { return false; } }


    public override List<string> dialogue
    {
        get
        {
            return new List<string>
            {
                "Hmm? What do you want? Oh, you want to know more about me?",
                "I am a crocodile - a large reptilian. I lay eggs, have scales, and have sharp teeth.",
                "Like most reptiles my blood is cold. This means I have to spend several hours per day lying in the sun to warm up.",
                "I'm basking in the sun right now & nothing will scare me away from this spot! Well, except maybe the Jaguar...",
            };
        }
    }

    public override List<string> secondDialogue
    {
        get
        {
            return new List<string>
            {
                "*The crocodile is basking in the sun and doesn't move*",
                "*Maybe the Jaguar can scare her away?*",
            };
        }
    }

    public override Sprite image { get { return GivenImage; } }

    public override string animalName { get { return "Crocodile"; } }



    public override List<string> CurrentDialogue { get; set; }
    protected virtual void Start()
    {
        CurrentDialogue = dialogue;
    }

    public override void DoFirstAction()
    {
        if (FindObjectOfType<Jaguar_01>()) FindObjectOfType<Jaguar_01>().CurrentDialogue = FindObjectOfType<Jaguar_01>().secondDialogue;
    }

    public override void DoSecondAction()
    {

    }
}
