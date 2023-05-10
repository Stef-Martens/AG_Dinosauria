using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : AnimalIntro
{
    public Sprite GivenImage;

    public GameObject RemoveObject;



    public override bool HasActionAfterFirstDialogue { get { return false; } }
    public override bool HasActionAfterSecondDialogue { get { return true; } }

    public override bool SecondDialogeDirectlyAfterTalking { get { return false; } }

    public override void Update()
    {

    }


    public override List<string> dialogue
    {
        get
        {
            return new List<string>
            {
                "kmoet zoet hebben"
            };
        }
    }

    public override List<string> secondDialogue
    {
        get
        {
            return new List<string>
            {
                "danke"
            };
        }
    }

    public override Sprite image { get { return GivenImage; } }

    public override string animalName { get { return "Ant"; } }



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
        Destroy(RemoveObject);
        FindObjectOfType<ChameleonIntro>().TasksDone++;
        Destroy(gameObject);
    }
}
