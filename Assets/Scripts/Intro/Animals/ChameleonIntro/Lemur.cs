using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lemur : AnimalIntro
{
    public Sprite GivenImage;

    public Sprite MilkImage;

    public override bool HasActionAfterFirstDialogue { get { return false; } }
    public override bool HasActionAfterSecondDialogue { get { return true; } }

    public override bool SecondDialogeDirectlyAfterTalking { get { return false; } }

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
                "Hi",
                "wij moeten iets zoet"
            };
        }
    }

    public override List<string> secondDialogue
    {
        get
        {
            return new List<string>
            {
                "danke",
                "jow"
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
