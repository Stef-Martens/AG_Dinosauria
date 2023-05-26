using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lemur : AnimalIntro
{
    public Sprite GivenImage;

    public Sprite MilkImage;

    public GameObject interactItem;

    public override bool HasActionAfterFirstDialogue { get { return false; } }
    public override bool HasActionAfterSecondDialogue { get { return true; } }

    public override bool SecondDialogeDirectlyAfterTalking { get { return false; } }

    public override bool ArrowLeft { get { return false; } }
    public override bool ArrowRight { get { return true; } }

    ItemInventory Milk;
    void Start()
    {
        Milk = new ItemInventory(MilkImage, "Milk");
        CurrentDialogue = dialogue;
    }


    public override List<string> dialogue
    {
        get
        {
            return new List<string>
            {
                "Hi, we're ring-tail lemurs! We like to be mischievous and play around a lot!",
                "We're relatives of the monkey, so in a way we're related to you too. Like you we'll also eat anything.",
                "We found this carton of milk from another human, though we can't use it!",
                "Do you want to trade? We'll give you the milk if you give us something sweet. Like honey! Or fruit!"
            };
        }
    }

    public override List<string> secondDialogue
    {
        get
        {
            return new List<string>
            {
                "Mmm, this looks delicious! Thankyou! Here's your milk.",
                "What? You need help finding the chameleon? Sure we'll help!",
                "*The lemurs leave and go to help the chameleon*"
            };
        }
    }

    public override Sprite image { get { return GivenImage; } }

    public override string animalName { get { return "Lemur"; } }



    public override List<string> CurrentDialogue { get; set; }

    public override void DoFirstAction()
    {
    }

    public override void DoSecondAction()
    {
        //aantal frogs optellen
        FindObjectOfType<ChameleonIntro>().TasksDone++;
        Destroy(gameObject);

        FindObjectOfType<IntroPlayer>().inventory = Milk;


        //gameObject.transform.position = Position.transform.position;
    }
}
