using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyTurtle : AnimalIntro
{
    public Sprite GivenImage;

    public override bool HasActionAfterFirstDialogue { get { return false; } }
    public override bool HasActionAfterSecondDialogue { get { return true; } }

    public override bool SecondDialogeDirectlyAfterTalking { get { return true; } }

    public override bool ArrowLeft { get { return false; } }
    public override bool ArrowRight { get { return true; } }

    bool GoRight = false;

    Vector3 StartPosition;

    public bool finished = false;


    public override List<string> dialogue
    {
        get
        {
            return new List<string>
            {
                "Hey, I'm a baby sea turtle. Nice to meet you!",
                "I'm a reptile that lives in the water. You might think I'm an amphibian, but I have scales! Amphibians do not.",
                "My mother came to this beach to lay eggs. When we're born, we make a run for the sea to be safe from the predators on the land.",
                "My sisters and brothers have already escaped, but I'm too scared! I'm afraid I'll get pinched. Did you know crabs like to eat baby turtles?",
                "Do you want to help me? Can you get rid of the crab claws on the beach? If you do, I'll be able to escape.",
                "Talk to me again and I'll try to make a run for the ocean!"
            };
        }
    }

    public override List<string> secondDialogue
    {
        get
        {
            return new List<string>
            {
                "Alright, I'm going to try and make a run for the ocean!. I hope it's safe now.",
                "If I run into any crab claws, I'll probably run back to my nest here..."
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
