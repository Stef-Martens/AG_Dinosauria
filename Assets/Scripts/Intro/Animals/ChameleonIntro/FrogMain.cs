using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMain : AnimalIntro
{
    public Sprite GivenImage;

    public bool finished = false;

    public int AmountOfFrogsFound = 0;

    public override bool HasActionAfterFirstDialogue { get { return false; } }
    public override bool HasActionAfterSecondDialogue { get { return true; } }

    public override bool SecondDialogeDirectlyAfterTalking { get { return false; } }

    public override bool ArrowLeft { get { return true; } }
    public override bool ArrowRight { get { return true; } }

    public override void Update()
    {
        if (AmountOfFrogsFound >= 3)
        {
            CurrentDialogue = secondDialogue;
        }
    }


    public override List<string> dialogue
    {
        get
        {
            return new List<string>
            {
                "Hello human. *croak* I'm a frog. When people think about amphibians they usually think about me! *croak*",
                "I love being around the water. When I was a baby, fresh out of the egg, I didn't even have any legs! All I could do was swim.",
                "When I was young I loved to eat plants, but nowadays I usually eat flies with my sticky tongue.",
                "My best friend - Mister Chameleon - also loves to eat flies with his sticky tongue.",
                "Oh? You're looking for the chameleon? Well he's pretty shy! He's right there, but he camouflaged himself!",
                "But he loves friends. If we had a lot of friends here I bet he'd come right out.",
                "I myself have a lot of frog friends, but they're all over the place. Can you find the other frogs?"

            };
        }
    }

    public override List<string> secondDialogue
    {
        get
        {
            return new List<string>
            {
                "*croak* Thanks for finding all my friends! You're great!",
                "We might need a few more friends though. I think if you can get the Red Panda, Lemurs, and Ant to come over, Mr. Chameleon should come out!"
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
    }

    public override void DoSecondAction()
    {
        if (!finished)
            FindObjectOfType<ChameleonIntro>().TasksDone++;

        finished = true;
    }
}
