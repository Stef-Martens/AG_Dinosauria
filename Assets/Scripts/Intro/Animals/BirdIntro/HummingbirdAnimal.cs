using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HummingbirdAnimal : AnimalIntro
{
    public Sprite GivenImage;

    public override bool HasActionAfterFirstDialogue { get { return true; } }
    public override bool HasActionAfterSecondDialogue { get { return false; } }

    public override bool SecondDialogeDirectlyAfterTalking { get { return true; } }

    public override bool ArrowLeft { get { return false; } }
    public override bool ArrowRight { get { return false; } }

    public GameObject interactItem;

    public override List<string> dialogue
    {
        get
        {
            return new List<string>
            {
                "Hello! You've found me!",
                "I'm the hummingbird - so called because I make a humming sound with my wings when I fly.",
                "I fly very quickly, and like to eat insects, and nectar from flowers.",
                "I'm a distant relative of the dinosaurs, and I was once revered by the people of Mexico as the spirit of a warrior.",
                "But enough about me! Why don't I show YOU what a day in my life is like?"
            };
        }
    }

    public override Sprite image { get { return GivenImage; } }

    public override string animalName { get { return "Hummingbird"; } }



    public override List<string> CurrentDialogue { get; set; }
    protected virtual void Start()
    {
        CurrentDialogue = dialogue;
    }

    public override void DoFirstAction()
    {
        interactItem.SetActive(false);
        FindObjectOfType<SceneSwitch>().ChangeScene("HummingBird_Rico");
    }

    public override void DoSecondAction()
    {

    }

}