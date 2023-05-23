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

    public override bool ArrowLeft { get { return false; } }
    public override bool ArrowRight { get { return false; } }

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
                "Wow! Look at all these friends who came here to see me!",
                "Sorry, I was quite shy! I can change colours, so I hid myself so you wouldn't find me.",
                "I'm a little reptile that loves to climb in trees with my sticky feet, and I can shoot my sticky tongue out to catch bugs far away from me.",
                "I do also eat little leaves and fruits though - not just meat.",
                "Anyways, I've talked long enough! Come, let me show you how I live."
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
        FindObjectOfType<SceneSwitch>().ChangeScene("Chameleon");
    }

    public override void DoSecondAction()
    {
    }
}
