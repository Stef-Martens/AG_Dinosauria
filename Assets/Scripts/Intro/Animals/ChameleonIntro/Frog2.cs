using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog2 : AnimalIntro
{
    public Sprite GivenImage;


    public GameObject Position;

    public override bool HasActionAfterFirstDialogue { get { return true; } }

    public override bool SecondDialogeDirectlyAfterTalking { get { return false; } }

    public override bool ArrowLeft { get { return false; } }
    public override bool ArrowRight { get { return false; } }

    public GameObject interactItem;

    public override List<string> dialogue
    {
        get
        {
            return new List<string>
            {
                "*ribbit* Hello, I'm one of the frogs! Congratulations on finding me.",
                "What? Mr. Frog needs us back? Sure, I'll go home! *ribbit*",
                "*The frog leaves and returns to the other frogs*"
            };
        }
    }


    public override Sprite image { get { return GivenImage; } }

    public override string animalName { get { return "Frog"; } }



    public override List<string> CurrentDialogue { get; set; }
    protected virtual void Start()
    {
        CurrentDialogue = dialogue;
    }

    public override void DoFirstAction()
    {
        //aantal frogs optellen
        FindObjectOfType<FrogMain>().AmountOfFrogsFound++;
        Destroy(gameObject);


        //gameObject.transform.position = Position.transform.position;


    }

}
