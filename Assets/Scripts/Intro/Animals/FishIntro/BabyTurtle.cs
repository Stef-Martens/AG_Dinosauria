using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyTurtle : AnimalIntro
{
    public Sprite GivenImage;

    public override bool HasActionAfterFirstDialogue { get { return false; } }
    public override bool HasActionAfterSecondDialogue { get { return true; } }

    public override bool SecondDialogeDirectlyAfterTalking { get { return true; } }

    bool GoRight = false;

    Vector3 StartPosition;

    public bool finished = false;


    public override List<string> dialogue
    {
        get
        {
            return new List<string>
            {
                "Hi",
                "jow"
            };
        }
    }

    public override List<string> secondDialogue
    {
        get
        {
            return new List<string>
            {
                "Im going to the right."
            };
        }
    }

    public override Sprite image { get { return GivenImage; } }

    public override string animalName { get { return "Baby Turtle"; } }



    public override List<string> CurrentDialogue { get; set; }
    protected virtual void Start()
    {
        CurrentDialogue = dialogue;
        StartPosition = transform.position;

    }

    public override void Update()
    {
        if (GoRight)
        {
            transform.Translate(Vector2.right * 5f * Time.deltaTime);
        }
    }

    public override void DoFirstAction()
    {
    }

    public override void DoSecondAction()
    {
        GoRight = true;
    }

    void OnTriggerEnter2D(Collider2D Collider)
    {
        if (Collider.gameObject.name == "CrabClaw")
        {
            GoRight = false;
            gameObject.transform.position = StartPosition;
        }
        if (Collider.gameObject.tag == "Ocean")
        {
            GoRight = false;
            finished = true;
        }
    }
}
