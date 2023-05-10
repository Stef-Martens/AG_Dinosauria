using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurgeonAnimal : AnimalIntro
{
    public Sprite GivenImage;

    public override bool HasActionAfterFirstDialogue { get { return true; } }
    public override bool HasActionAfterSecondDialogue { get { return false; } }

    public override bool SecondDialogeDirectlyAfterTalking { get { return false; } }


    public override List<string> dialogue
    {
        get
        {
            return new List<string>
            {
                "ge hebt me gevonden"
            };
        }
    }


    public override Sprite image { get { return GivenImage; } }

    public override string animalName { get { return "Surgeonfish"; } }



    public override List<string> CurrentDialogue { get; set; }
    protected virtual void Start()
    {
        CurrentDialogue = dialogue;
    }

    public override void DoFirstAction()
    {
        FindObjectOfType<SceneSwitch>().ChangeScene("QuizFish");
    }

}
