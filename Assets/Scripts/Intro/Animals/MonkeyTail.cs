using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyTail : AnimalIntro
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
                "A monkey's tail dangles from the tree in front of you.",
            };
        }
    }

    public override List<string> secondDialogue
    {
        get
        {
            return new List<string>
            {
                "*You pull on the monkey's taill.*",
                "A loud howl resounds from the tree, waking up the Jaguar, who walks away.",
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
    }

    public override void DoSecondAction()
    {
        if (FindObjectOfType<Jaguar_02>())
        {
            FindObjectOfType<Jaguar_02>().GetComponent<BoxCollider2D>().enabled = true;
            FindObjectOfType<Jaguar_02>().GetComponent<SpriteRenderer>().enabled = true;
        }


        if (FindObjectOfType<Jaguar_01>()) Destroy(FindObjectOfType<Jaguar_01>().gameObject);
    }
}
