using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionQuiz : QuizBase
{
    [Space(10)]
    public List<Sprite> AnimalLeftColImgs = new List<Sprite>();
    public List<Sprite> AnimalRightColImgs = new List<Sprite>();

    [Space(10)]
    public Color BaseLineColor;
    public Color CorrectAnswerLineColor;
    public Color WrongAnswerLineColor;

    [Space(10)]
    public Button FirstCorrectAnswerLftBtn;
    public Button FirstCorrectAnswerRghtBtn;
    public Button SecondCorrectAnswerLftBtn;
    public Button SecondCorrectAnswerRghtBtn;
    public Button ThirdCorrectAnswerLftBtn;
    public Button ThirdCorrectAnswerRghtBtn;

    private BtnNavigationConnectionQuiz _btnNavigation;
    private LineDrawerConnectionQuiz _lineDrawer;
    public List<Tuple<Button, Button>> _answers = new List<Tuple<Button, Button>>();
    private List<Tuple<Button, Button>> _correctAnswers = new List<Tuple<Button, Button>>();

    protected override void Awake()
    {
        base.Awake();

        _btnNavigation = this.GetComponent<BtnNavigationConnectionQuiz>();
        _lineDrawer = this.GetComponent<LineDrawerConnectionQuiz>();

        SetAnswersList();
    }

    protected override void OnEnable()
    {
        BtnNavigation = this.GetComponent<BtnRightColNavigation>();
        _btnNavigation.ButtonPressedEvent += OnButtonsPressedList;

        base.OnEnable();
    }

    protected override void OnDisable()
    {
        _btnNavigation.ButtonPressedEvent -= OnButtonsPressedList;

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

    private void SetAnswersList()
    {
        _answers = _btnNavigation.PressedBtnsList;

        _correctAnswers = new List<Tuple<Button, Button>>
        {
            new Tuple<Button, Button>(FirstCorrectAnswerLftBtn, FirstCorrectAnswerRghtBtn),
            new Tuple<Button, Button>(SecondCorrectAnswerLftBtn, SecondCorrectAnswerRghtBtn),
            new Tuple<Button, Button>(ThirdCorrectAnswerLftBtn, ThirdCorrectAnswerRghtBtn)
        };
    }

    private void SetCorrectAnswerLines()
    {
        Transform quizPanel = transform.GetChild(0);

        for (int i = 0; i < _correctAnswers.Count; i++)
        {
            Transform animalPanel1 = _correctAnswers[i].Item1.transform.parent;
            Transform animalPanel2 = _correctAnswers[i].Item2.transform.parent;

            Image line = quizPanel.GetChild(i + 2).GetComponent<Image>();
            RectTransform root1 = animalPanel1.GetChild(2).GetComponent<RectTransform>();
            RectTransform root2 = animalPanel2.GetChild(2).GetComponent<RectTransform>();

            if (_answers.Contains(_correctAnswers[i]))
                line.color = this.GetComponent<ConnectionQuiz>().CorrectAnswerLineColor;
            else
                line.color = this.GetComponent<ConnectionQuiz>().WrongAnswerLineColor;

            _lineDrawer.DrawLine(root1, root2, line);
        }
    }

    protected override void CheckAnswer()
    {
        HashSet<Tuple<Button, Button>> answersSet = new HashSet<Tuple<Button, Button>>(_answers);
        HashSet<Tuple<Button, Button>> correctAnswersSet = new HashSet<Tuple<Button, Button>>(_correctAnswers);

        bool areAnswersCorrect = false;

        if (_answers.Count == _correctAnswers.Count)
        {
            if (_answers[_correctAnswers.Count- 1].Item2 != null && !HasSelectedAnswer)
            {
                areAnswersCorrect = answersSet.SetEquals(correctAnswersSet);

                HasSelectedAnswer= true;

                ShowRecapAnswer(areAnswersCorrect);
            }
        }
    }

    protected override void ShowRecapAnswer(bool isRightAnswer)
    {
        if (!HasSetRecapAnswer)
        {
            HasSetRecapAnswer = true;
            this.transform.GetChild(0).GetChild(5).gameObject.SetActive(false);

            GameObject recapAnswer = this.transform.GetChild(1).gameObject;
            recapAnswer.SetActive(true);

            SetCorrectAnswerLines();

            if (isRightAnswer)
            {
                recapAnswer.transform.GetChild(0).gameObject.SetActive(true);
                recapAnswer.transform.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                recapAnswer.transform.GetChild(0).gameObject.SetActive(false);
                recapAnswer.transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }
}