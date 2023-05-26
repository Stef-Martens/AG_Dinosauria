using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skunk : AnimalIntro
{
    public Sprite GivenImage;

    public override bool HasActionAfterFirstDialogue { get { return false; } }
    public override bool HasActionAfterSecondDialogue { get { return true; } }

    public override bool SecondDialogeDirectlyAfterTalking { get { return false; } }

    public override bool ArrowLeft { get { return true; } }
    public override bool ArrowRight { get { return false; } }

    public Sprite jaguarAwake;
    public GameObject fart;
    public GameObject interactItem;
    public GameObject jaguarInteractItem;

    public override List<string> dialogue
    {
        get
        {
            return new List<string>
            {
                "Hey, I'm a skunk!",
                "I'm a small mamal, and I like to eat small insects and fruit.",
                "I don't usually live in the jungle. I prefer to live in places with less trees.",
                "I'm not scared though. When I feel scared, I just let out a big stinking fart and everyone runs away.",
                "I'm hungry. I would like to eat a delicious piece of fruit."
            };
        }
    }

    public override List<string> secondDialogue
    {
        get
        {
            return new List<string>
            {
                "Oh? A piece of fruit for me? Thankyou!",
                "*The skunk eats the fruit, and then lets out a big fart.*",
                "Oops! Eating fruit always makes me fart...."
            };
        }
    }

    public override Sprite image { get { return GivenImage; } }

    public override string animalName { get { return "Skunk"; } }



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
        // jaguar tweede tekst
        if (FindObjectOfType<Jaguar_02>()) FindObjectOfType<Jaguar_02>().CurrentDialogue = FindObjectOfType<Jaguar_02>().secondDialogue;

        FindObjectOfType<IntroPlayer>().ClearInventory();
        fart.SetActive(true);
        FindObjectOfType<Jaguar_02>().GetComponent<SpriteRenderer>().sprite = jaguarAwake;
        interactItem.SetActive(false);
        jaguarInteractItem.SetActive(true);
    }
}
