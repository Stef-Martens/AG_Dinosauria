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
                "I will get the fish if you help my friends.",
                "friends are turtles.",
                "kwil ook nen bal"
            };
        }
    }

    public override List<string> secondDialogue
    {
        get
        {
            return new List<string>
            {
                "Danke",
                "kga vis halen"
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
