using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : AnimalIntro
{
    public string TextToSay;
    public Sprite GivenImage;


    public override bool HasActionAfterFirstDialogue { get { return true; } }
    public override bool SecondDialogeDirectlyAfterTalking { get { return false; } }

    public override List<string> dialogue
    {
        get
        {
            return new List<string>
            {
                TextToSay
            };
        }
    }


    public override Sprite image { get { return GivenImage; } }

    public override string animalName { get { return "Teleport"; } }



    public override List<string> CurrentDialogue { get; set; }
    protected virtual void Start()
    {
        CurrentDialogue = dialogue;
    }

    public override void DoFirstAction()
    {
        FindObjectOfType<IntroPlayer>().gameObject.transform.position = transform.GetChild(0).transform.position;
    }

}