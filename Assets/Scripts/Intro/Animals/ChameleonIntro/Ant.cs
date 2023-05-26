using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : AnimalIntro
{
    public Sprite GivenImage;

    public GameObject RemoveObject;

    public GameObject interactItem;

    public override bool HasActionAfterFirstDialogue { get { return false; } }
    public override bool HasActionAfterSecondDialogue { get { return true; } }

    public override bool SecondDialogeDirectlyAfterTalking { get { return false; } }

    public override bool ArrowLeft { get { return true; } }
    public override bool ArrowRight { get { return true; } }

    public override void Update()
    {

    }


    public override List<string> dialogue
    {
        get
        {
            return new List<string>
            {
                "Behold, human! I am the mighty ant! Greatest of all the insects!",
                "I may look small and weak, but together with my friends there's nothing we cannot do.",
                "Like other insects I'm an invertebrate and have my skeleton on the outside of my body. That's what makes us so strong.",
                "We'll eat anything, but we really like sweet things.",
                "Can you bring us something sweet, like honey or a piece of fruit? If you do, we'll let you go past our anthill."
            };
        }
    }

    public override List<string> secondDialogue
    {
        get
        {
            return new List<string>
            {
                "Mmm, delicious! Thankyou, human!",
                "Behold, the power of the ants as we get rid of this rock in your way!"
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
