using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : AnimalIntro
{
    public Sprite GivenImage;

    public override List<string> dialogue
    {
        get
        {
            return new List<string>
            {
                "*Hm there is a bush in the way?",
                "*Maybe something to the left will help me.*",
            };
        }
    }

    public override Sprite image { get { return GivenImage; } }

    public override string animalName { get { return null; } }

    public override void DoAction()
    {

    }

    protected virtual void Start()
    {
        CurrentDialogue = dialogue;
    }
}
