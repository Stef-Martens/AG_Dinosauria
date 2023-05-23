using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AnimalIntro : MonoBehaviour
{
    public abstract List<string> dialogue { get; }

    public abstract void DoFirstAction();
    public virtual void DoSecondAction() { }

    public virtual void Update() { }

    public abstract Sprite image { get; }
    public abstract string animalName { get; }
    public virtual List<string> secondDialogue { get; }
    public virtual List<string> CurrentDialogue { get; set; }
    public abstract bool HasActionAfterFirstDialogue { get; }
    public virtual bool HasActionAfterSecondDialogue { get; }
    public abstract bool ArrowLeft { get; }
    public abstract bool ArrowRight { get; }

    public abstract bool SecondDialogeDirectlyAfterTalking { get; }

}

