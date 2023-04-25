using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mouse : AnimalIntro
{
    public float speed = 5f;
    private bool MoveRight = false;
    public Sprite GivenImage;


    public override List<string> dialogue
    {
        get
        {
            return new List<string>
            {
                "Hello, I am a mouse!",
                "I'm tiny, but a lot of big creatures are scared of me!",
                "I like to travel places and I'm going to see what's to the right.",
                "Talk to me again and I will start my journey!",
            };
        }
    }

    public override List<string> secondDialogue
    {
        get
        {
            return new List<string>
            {
                "Hello, this is the second conversation",
                "Bye"
            };
        }
    }

    public override Sprite image { get { return GivenImage; } }

    public override string animalName { get { return "Mouse"; } }

    protected override void Update()
    {
        base.Update();

        if (MoveRight)
            transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    public override List<string> CurrentDialogue { get; set; }
    protected virtual void Start()
    {
        CurrentDialogue = dialogue;
    }

    public override void DoAction()
    {
        MoveRight = true;
    }
}
