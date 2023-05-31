using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iguana : AnimalIntro
{
    public Sprite GivenImage;

    public override bool HasActionAfterFirstDialogue { get { return true; } }
    public override bool HasActionAfterSecondDialogue { get { return true; } }

    public override bool SecondDialogeDirectlyAfterTalking { get { return true; } }

    public override bool ArrowLeft { get { return false; } }
    public override bool ArrowRight { get { return true; } }


    bool GoRight = false;

    Vector3 StartPosition;

    public GameObject Prefab;
    public GameObject interactItem;
    public GameObject questionItem;

    public override List<string> dialogue
    {
        get
        {
            return new List<string>
            {
                "Hello! I am an Iguana.",
                "Like many reptiles I might look a bit scary, but I only eat plants.",
                "When I'm scared by someone, I use my tail as a whip like this!",
                "*The Iguana slaps its tail against the tree, scaring away the woodpecker!*",
            };
        }
    }

    public override List<string> secondDialogue
    {
        get
        {
            return new List<string>
            {
                "I'm feeling quite hungry. I heard there's a patch of delicious plants on the other side of the forest.",
                "I'm going to try and find these plants now.",
                "I hope I don't encounter the Jaguar! He's very scary. I'll probably run away if I see him."
            };
        }
    }

    public override Sprite image { get { return GivenImage; } }

    public override string animalName { get { return "Iguana"; } }



    public override List<string> CurrentDialogue { get; set; }

    protected virtual void Start()
    {
        CurrentDialogue = dialogue;
        StartPosition = transform.position;
    }

    public override void DoFirstAction()
    {
        Destroy(transform.GetChild(0).gameObject);
    }

    public override void Update()
    {
        if (GoRight)
        {
            transform.Translate(Vector2.right * 7f * Time.deltaTime);
            Physics2D.IgnoreLayerCollision(8, 7, true);
        }
    }

    public override void DoSecondAction()
    {
        GoRight = true;
    }

    void OnTriggerEnter2D(Collider2D Collider)
    {
        if (Collider.gameObject.layer == 9)
        {
            GoRight = false;
            gameObject.transform.position = StartPosition;
        }
        if (Collider.gameObject.name == "Plants4Iguana")
        {
            Destroy(Collider.gameObject);
            GoRight = false;
            interactItem.SetActive(false);
            questionItem.SetActive(false);
        }
    }
}
