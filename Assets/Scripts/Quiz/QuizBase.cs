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

    protected virtual void Awake()
    {
        SetQuestionText();
        SetAnimalTexts();
        SetAnimalImages();

        this.transform.GetChild(0).gameObject.SetActive(true);
        this.transform.GetChild(1).gameObject.SetActive(false);
    }

    protected virtual void OnEnable()
    {
        Inputs.ActionInputEvent += OnAction;
        Inputs.ConfirmInputEvent += OnConfirm;
    }

    protected virtual void OnDisable()
    {
        Inputs.ActionInputEvent -= OnAction;
        Inputs.ConfirmInputEvent -= OnConfirm;
    }

    private void SetQuestionText()
    {
        FindObjectOfType<SwitchToNextQuiz>()?.Questions?.Add(QuestionText);
    }

    protected virtual void SetAnimalTexts()
    {

    }

    protected virtual void SetAnimalImages()
    {
      
    }

    protected void HandleButtonPressed(Button pressedBtn)
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
        }
    }
}