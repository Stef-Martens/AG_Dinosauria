using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kangeroo : AnimalIntro
{
    public Sprite GivenImage;

    public GameObject TargetSpawnBall;
    public GameObject BallPrefab;

    public override bool HasActionAfterFirstDialogue { get { return false; } }
    public override bool HasActionAfterSecondDialogue { get { return true; } }

    public override bool SecondDialogeDirectlyAfterTalking { get { return false; } }


    public override List<string> dialogue
    {
        get
        {
            return new List<string>
            {
                "Ik kan hard schoppe",
                "fix bal"
            };
        }
    }

    public override List<string> secondDialogue
    {
        get
        {
            return new List<string>
            {
                "mercikes",
                "ik trap bal weg"
            };
        }
    }

    public override Sprite image { get { return GivenImage; } }

    public override string animalName { get { return "Kangaroo"; } }



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
        // bal naar den andere kant schoppe
        FindObjectOfType<IntroPlayer>().ClearInventory();
        Instantiate(BallPrefab, TargetSpawnBall.transform.position, Quaternion.identity);
    }
}
