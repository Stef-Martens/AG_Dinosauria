using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChameleonIntro : AnimalIntro
{
    public Sprite GivenImage;

    public int TasksDone = 0;

    public override bool HasActionAfterFirstDialogue { get { return true; } }
    public override bool HasActionAfterSecondDialogue { get { return false; } }

    public override bool SecondDialogeDirectlyAfterTalking { get { return false; } }

    public override void Update()
    {
        if (TasksDone >= 3)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }


    public override List<string> dialogue
    {
        get
        {
            return new List<string>
            {
                "Congrats",
            };
        }
    }

    public override List<string> secondDialogue
    {
        get
        {
            return new List<string>
            {
                ""
            };
        }
    }

    public override Sprite image { get { return GivenImage; } }

    public override string animalName { get { return "Chameleon"; } }



    public override List<string> CurrentDialogue { get; set; }
    protected virtual void Start()
    {
        CurrentDialogue = dialogue;
    }

    public override void DoFirstAction()
    {
        FindObjectOfType<SceneSwitch>().ChangeScene("QuizChameleon");
    }

    public override void DoSecondAction()
    {
    }
}
