using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurgeonAnimal : AnimalIntro
{
    public Sprite GivenImage;

    public override bool HasActionAfterFirstDialogue { get { return true; } }
    public override bool HasActionAfterSecondDialogue { get { return false; } }

    public override bool SecondDialogeDirectlyAfterTalking { get { return false; } }

    public override bool ArrowLeft { get { return false; } }
    public override bool ArrowRight { get { return false; } }


    public override List<string> dialogue
    {
        get
        {
            return new List<string>
            {
                "Ah hello! Were you looking for me? Sorry, I live underwater!",
                "I'm a fish, and instead of breathing in the air I breathe underwater using my gills",
                "Like all fish I was born from an egg - and I have hundreds of brothers and sisters! My parents laid a lot of eggs, you see",
                "I eat lots of things. In fact, I'm quite hungry. Why don't you help me find some food?"
            };
        }
    }


    public override Sprite image { get { return GivenImage; } }

    public override string animalName { get { return "Surgeonfish"; } }



    public override List<string> CurrentDialogue { get; set; }
    protected virtual void Start()
    {
        CurrentDialogue = dialogue;
    }

    public override void DoFirstAction()
    {
        FindObjectOfType<SceneSwitch>().ChangeScene("Fish");
    }
}