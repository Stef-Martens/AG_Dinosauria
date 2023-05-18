using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionQuiz : QuizBase
{
    public List<string> AnimalTxts = new List<string>();

    [Space(10)]
    public List<Sprite> AnimalImgs = new List<Sprite>();

    [Space(10)]
    public Button CorrectAnswerBtn;
    public string CorrectAnswerTxt;
    public Sprite CorrectAnswerImg;

    private BtnNavigationSelectionQuiz _btnNavigation;

    protected override void Awake()
    {
        base.Awake();
        
        _btnNavigation = this.GetComponent<BtnNavigationSelectionQuiz>();
    }

    protected override void OnEnable()
    {
        _btnNavigation.ButtonPressedEvent += HandleButtonPressed;

        base.OnEnable();
    }

    protected override void OnDisable()
    {
        _btnNavigation.ButtonPressedEvent -= HandleButtonPressed;

        base.OnDisable();
    }

    protected override void SetAnimalTexts()
    {
        for (int buttonIndex = 0; buttonIndex < AnimalTxts.Count; buttonIndex ++)
        {
            this.transform.GetChild(0).GetChild(buttonIndex).GetChild(0).GetChild(0).GetComponent<Text>().text = AnimalTxts[buttonIndex];
        }
    }

    protected override void SetAnimalImages()
    {
        for (int buttonIndex = 0; buttonIndex < AnimalImgs.Count; buttonIndex++)
        {
            this.transform.transform.GetChild(0).GetChild(buttonIndex).GetComponent<Image>().sprite = AnimalImgs[buttonIndex];
        }
    }

    protected override void CheckAnswer()
    {
        if (_btnNavigation.HasPressedBtn && !HasSelectedAnswer)
        {
            HasSelectedAnswer = true;

            if (_btnNavigation.PressedBtn == CorrectAnswerBtn)
                ShowRecapAnswer(true);
            else
                ShowRecapAnswer(false);
        }
    }

    protected override void ShowRecapAnswer(bool isRightAnswer)
    {
        if (!HasSetRecapAnswer)
        {
            HasSetRecapAnswer = true;

            this.transform.GetChild(0).gameObject.SetActive(false);

            GameObject recapAnswer = this.transform.GetChild(1).gameObject;
            recapAnswer.SetActive(true);
            recapAnswer.transform.GetChild(0).GetComponent<Image>().sprite = CorrectAnswerImg;
            recapAnswer.transform.GetChild(4).GetComponent<Text>().text = CorrectAnswerTxt;

            if (isRightAnswer)
            {
                recapAnswer.transform.GetChild(1).gameObject.SetActive(true);
                recapAnswer.transform.GetChild(2).gameObject.SetActive(false);
            }
            else
            {
                recapAnswer.transform.GetChild(1).gameObject.SetActive(false);
                recapAnswer.transform.GetChild(2).gameObject.SetActive(true);
            }
        }
    }
}