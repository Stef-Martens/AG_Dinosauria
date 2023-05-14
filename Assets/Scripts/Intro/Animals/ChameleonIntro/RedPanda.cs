using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPanda : AnimalIntro
{
    public Sprite GivenImage;

    bool finished = false;

    public override bool HasActionAfterFirstDialogue { get { return false; } }
    public override bool HasActionAfterSecondDialogue { get { return true; } }

    public override bool SecondDialogeDirectlyAfterTalking { get { return false; } }


    public override List<string> dialogue
    {
        get
        {
            return new List<string>
            {
                "Hello there! I'm Mrs. Red Panda, and these are my children!",
                "We're on vacation right now. We don't live in Madagascar. Instead, we come from the Himalayas!",
                "Like most mammals we also have fur, but so much fur doesn't help when it's so warm! Phew, I'm exhausted...",
                "My children need to drink. Mammals are unique because they're the only animals that drink milk. Do you have some milk for us?"
            };
        }
    }

    public override List<string> secondDialogue
    {
        get
        {
            return new List<string>
            {
                "Thankyou for the delicious milk! With this my children can drink.",
                "Oh? Do you need help finding the Chameleon? Sure we can help. Come on kids, let's go help!"
            };
        }
    }

    public override Sprite image { get { return GivenImage; } }

    public override string animalName { get { return "Red Panda"; } }



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

        FindObjectOfType<IntroPlayer>().ClearInventory();
    }
}
