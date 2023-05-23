using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : AnimalIntro
{
    public Sprite GivenImage;

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
                "Hello, I am a black howler monkey!",
                "I'm a mammal and an omnivore - like most mammals I have fur, and I like to eat meat as well as plants.",
                "I'm called a howler monkey because I howl very loudly at times. It often scares away other animals.",
                "I have a long tail, and I tend to always howl when people pull on my tail...",
            };
        }
    }

    public override Sprite image { get { return GivenImage; } }

    public override string animalName { get { return "Monkey"; } }



    public override List<string> CurrentDialogue { get; set; }
    protected virtual void Start()
    {
        CurrentDialogue = dialogue;
    }

    public override void DoFirstAction()
    {
        FindObjectOfType<MonkeyTail>().CurrentDialogue = FindObjectOfType<MonkeyTail>().secondDialogue;
    }

    public override void DoSecondAction()
    {
    }
}
