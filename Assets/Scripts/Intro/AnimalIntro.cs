using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AnimalIntro : MonoBehaviour
{
    public abstract List<string> dialogue { get; }

    public abstract void DoAction();

    protected virtual void Update() { }

    public abstract Sprite image { get; }
    public abstract string animalName { get; }
    public virtual List<string> secondDialogue { get; }
    public virtual List<string> CurrentDialogue { get; set; }
}

