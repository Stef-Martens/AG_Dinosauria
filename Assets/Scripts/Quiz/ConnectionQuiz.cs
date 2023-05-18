using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionQuiz : QuizBase
{
    [Space(10)]
    public List<Sprite> AnimalLeftColImgs = new List<Sprite>();
    public List<Sprite> AnimalRightColImgs = new List<Sprite>();

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void SetAnimalTexts()
    {
        base.SetAnimalTexts();
    }

    protected override void SetAnimalImages()
    {
        for (int buttonIndex = 0; buttonIndex < AnimalLeftColImgs.Count; buttonIndex++)
        {
            this.transform.transform.GetChild(0).GetChild(0).GetChild(buttonIndex).GetChild(1).GetComponent<Image>().sprite = AnimalLeftColImgs[buttonIndex];
        }

        for (int buttonIndex = 0; buttonIndex < AnimalRightColImgs.Count; buttonIndex++)
        {
            this.transform.transform.GetChild(0).GetChild(1).GetChild(buttonIndex).GetChild(1).GetComponent<Image>().sprite = AnimalRightColImgs[buttonIndex];
        }
    }

    protected override void CheckAnswer()
    {
        base.CheckAnswer();
    }

    protected override void ShowRecapAnswer(bool isRightAnswer)
    {
        base.ShowRecapAnswer(isRightAnswer);
    }
}