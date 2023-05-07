using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beetle : AnimalIntro
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
                "Aha! You have the pleasure of meeting me! I am the great Beetle - mightiest of all insects!",
                "Though I am small, my armour protects me from many things. My strong jaws help me protect myself from danger!",
                "I'll eat anything! I love to eat smaller bugs, plants, and even pieces of wood.",
                "If you're looking for the hummingbird, he is hiding in the flowers. With the help of my friend, the ladybug, I could find him.",
                "I don't know where the ladybug is though... Maybe you can find her?"
            };
        }
    }

    public override List<string> secondDialogue
    {
        get
        {
            return new List<string>
            {
                "Ah! You've found my sister the Ladybug!",
                "Excellent. Now we will help you find the hummingbird!",
                "You see: We too can fly! Finding the Hummingbird like this will be easy.",
                "*The beetle & hummingbird fly off and bring back the hummingbird*"
            };
        }
    }

    public override Sprite image { get { return GivenImage; } }

    public override string animalName { get { return "Beetle"; } }



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
        FindObjectOfType<HummingbirdAnimal>().GetComponent<BoxCollider2D>().enabled = true;
        FindObjectOfType<HummingbirdAnimal>().GetComponent<SpriteRenderer>().enabled = true;

        FindObjectOfType<Beetle2>().GetComponent<BoxCollider2D>().enabled = true;
        FindObjectOfType<Beetle2>().GetComponent<SpriteRenderer>().enabled = true;

        Destroy(gameObject);

    }
}
