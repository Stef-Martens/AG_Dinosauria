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

    public override bool ArrowLeft { get { return false; } }
    public override bool ArrowRight { get { return true; } }


    public override List<string> dialogue
    {
        get
        {
            return new List<string>
            {
                "Well hello. I'm a kangaroo. I'm a big mammal - a marsupial, to be exact.",
                "I love to eat grass and brushes. I'm one of the bigger herbivores.",
                "My babies grow in a little pouch on my belly after being born. It's nice and warm in there!",
                "I don't usually live near the beach, but I wanted to go on vacation.",
                "I want to do something fun though - like kicking a ball. I have very strong legs, you know. Maybe I should become a football star?"
            };
        }
    }

    public override List<string> secondDialogue
    {
        get
        {
            return new List<string>
            {
                "Oh a ball, thanks! Watch, I'll kick it very far!",
                "*The kangaroo kicks the ball over the head of the seal on the hill, as it lands near the ocean."

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
