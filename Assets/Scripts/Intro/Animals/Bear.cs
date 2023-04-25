using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bear : AnimalIntro
{
    public override Sprite image => throw new System.NotImplementedException();

    public override string animalName => throw new System.NotImplementedException();


    public override List<string> dialogue
    {
        get
        {
            return new List<string>
            {
                "test",
            };
        }
    }

    public override void DoAction()
    {
        throw new System.NotImplementedException();
    }
}
