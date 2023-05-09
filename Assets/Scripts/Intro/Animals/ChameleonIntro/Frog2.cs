using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog2 : AnimalIntro
{
    public Sprite GivenImage;


    public GameObject Position;

    public override bool HasActionAfterFirstDialogue { get { return true; } }

    public override bool SecondDialogeDirectlyAfterTalking { get { return false; } }


    public override List<string> dialogue
    {
        get
        {
            return new List<string>
            {
                "You found one of the frogs",
                "*Frog moves to start*"
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
