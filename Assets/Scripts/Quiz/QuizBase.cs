using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public abstract class QuizBase : MonoBehaviour
{
    public bool HasSelectedAnswer { get; set; } = false;
    public bool HasSetRecapAnswer { get; set; } = false;

    public Inputs Inputs;
    public bool IsWithSound = false;

    [Space(10)]
    [TextArea]
    public string QuestionText;

    private bool _isEndQuizKeyReleased = false;
    private bool _hasQuizEnded = false;

    protected virtual void Awake()
    {
        SetAnimalTexts();
        SetAnimalImages();

        this.transform.GetChild(0).gameObject.SetActive(true);
        this.transform.GetChild(1).gameObject.SetActive(false);
    }

    protected virtual void OnEnable()
    {
        SetQuestionText();

        Inputs.ActionInputEvent += OnAction;
        Inputs.ConfirmInputEvent += OnConfirm;
    }

    protected virtual void OnDisable()
    {
        if (_hasQuizEnded)
        {
            HasSelectedAnswer = false;
            HasSetRecapAnswer = false;

            _hasQuizEnded = false;
        }

        Inputs.ActionInputEvent -= OnAction;
        Inputs.ConfirmInputEvent -= OnConfirm;
    }

    private void SetQuestionText()
    {
        List<GameObject> quizzes = FindObjectOfType<SwitchToNextQuiz>()?.Quizzes;
        
        if(quizzes != null && quizzes.Count > 0)
        {
            FindObjectOfType<SwitchToNextQuiz>().Questions.AddRange(Enumerable.Repeat("", quizzes.Count - 1));
            int index = quizzes.IndexOf(this.transform.root.gameObject);

            if (index >= 0)
            {
                FindObjectOfType<SwitchToNextQuiz>()?.Questions.Insert(index, QuestionText);
            }
        }
    }

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

    private void OnAction(bool isPressed)
    {
        EndQuiz();
    }

    private void OnConfirm(bool isPressed)
    {
        EndQuiz();
    }

    protected virtual void CheckAnswer()
    {

    }

    protected virtual void ShowRecapAnswer(bool isRightAnswer)
    {

    }

    private void EndQuiz()
    {
        if (HasSetRecapAnswer)
        {
            _hasQuizEnded = true;

            if (Inputs.Action || Inputs.Confirm)
            {
                if (_isEndQuizKeyReleased)
                {
                    _isEndQuizKeyReleased = false;
                    FindObjectOfType<SwitchToNextQuiz>().SwitchQuiz();
                }
            }
            else
                _isEndQuizKeyReleased = true;

            FindObjectOfType<BtnNavigationBase>().SetEndQuizKeyReleased(_isEndQuizKeyReleased);
            FindObjectOfType<BtnNavigationBase>().SetHasQuizEnded(_hasQuizEnded);
        }
    }

 /*   private void Update()
    {
        Debug.Log(HasSelectedAnswer);
    }*/
}