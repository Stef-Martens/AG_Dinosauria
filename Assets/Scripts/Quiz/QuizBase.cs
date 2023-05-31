using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class QuizBase : MonoBehaviour
{
    public BtnNavigationBase BtnNavigation { get; set; } = null;
    public bool HasSelectedAnswer { get; set; } = false;
    public bool HasSetRecapAnswer { get; set; } = false;
    public bool CanSwitchQuiz { get; set; } = false;

    public bool IsWithSound = false;

    [Space(10)]
    [TextArea]
    public string QuestionText;

    private Inputs _inputs;

    private bool _isEndQuestionKeyReleased = false;
    private bool _hasQuestionEnded = false;

    protected virtual void Awake()
    {
        SetAnimalTexts();
        SetAnimalImages();

        this.transform.GetChild(0).gameObject.SetActive(true);
        this.transform.GetChild(1).gameObject.SetActive(false);
    }

    protected virtual void OnEnable()
    {
        _inputs = FindObjectOfType<Inputs>();

        _inputs.ActionInputEvent += OnActionConfirmInput;
        _inputs.ConfirmInputEvent += OnActionConfirmInput;
    }

    protected virtual void OnDisable()
    {
        if (_hasQuestionEnded)
        {
            HasSelectedAnswer = false;
            HasSetRecapAnswer = false;

            _hasQuestionEnded = false;
        }

        _inputs.ActionInputEvent -= OnActionConfirmInput;
        _inputs.ConfirmInputEvent -= OnActionConfirmInput;
    }

    protected Inputs GetInputs()
        => _inputs;

    protected bool GetIsEndQuestionKeyReleased()
        => _isEndQuestionKeyReleased;

    protected bool GetHasQuestionEnded()
        => _hasQuestionEnded;

    protected virtual void SetAnimalTexts()
    {

    }

    protected virtual void SetAnimalImages()
    {
      
    }

    protected void OnButtonPressed(Button pressedBtn)
    {
        CheckAnswer();
    }

    protected void OnButtonsPressedList(List<Tuple<Button, Button>> pressedBtnsList)
    {
        CheckAnswer();
    }

    private void OnActionConfirmInput(bool newActionConfirmState)
    {
        EndQuestion();
    }

    protected virtual void Update()
    {
        EndQuiz();
    }

    protected virtual void CheckAnswer()
    {

    }

    protected virtual void ShowRecapAnswer(bool isRightAnswer)
    {

    }

    private void EndQuestion()
    {
        if (HasSetRecapAnswer)
        {
            _hasQuestionEnded = true;

            if (_inputs.Action || _inputs.Confirm)
            {
                if (_isEndQuestionKeyReleased)
                    _isEndQuestionKeyReleased = false;
            }
            else
                _isEndQuestionKeyReleased = true;

            if (BtnNavigation != null)
            {
                BtnNavigation.SetEndQuestionKeyReleased(_isEndQuestionKeyReleased);
                BtnNavigation.SetHasQuestionEnded(_hasQuestionEnded);
            }
        }
    }

    protected virtual void EndQuiz()
    {
        if (BtnNavigation != null)
        {
            if (_inputs.Action || _inputs.Confirm)
            {
                if (!_isEndQuestionKeyReleased && _hasQuestionEnded && !BtnNavigation.GetIsEndRecapKeyReleased())
                {
                    CanSwitchQuiz = true;
                    FindObjectOfType<SwitchToNextQuiz>().SwitchQuiz();
                }
                else
                    CanSwitchQuiz = false;
            }
        }
    }
}