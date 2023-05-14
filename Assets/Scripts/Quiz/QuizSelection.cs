using UnityEngine;
using UnityEngine.UI;

public class QuizSelection : MonoBehaviour
{
    public Inputs Inputs;
    public Canvas QuizBase;
    public Selectable FirstInput;
    public bool IsWithSound = false;

    // Right Answer
    [Header("Correct Answer")]
    public Button CorrectAnswerBtn;
    public string CorrectAnswerTxt;
    public Sprite CorrectAnswerImg;

    // Question text
    [Header("Quiz Question")]
    public string QuestionText;

    // Animal text
    [Header("Animals Text")]
    public string FirstAnimalTxt;
    public string SecondAnimalTxt;
    public string ThirdAnimalTxt;

    // Animal Image
    [Header("Animals Images")]
    public Sprite FirstAnimalImg;
    public Sprite SecondAnimalImg;
    public Sprite ThirdAnimalImg;

    // Correct Answer
    private bool _setRecapAnswer = false;

    private DeactiveButtons _deactiveButtons;
    private bool _hasSelectedAnswer = false;

    // Question text
    private bool _setQuestionTxt = true;

    // Animal text
    private bool _setAnimalTxt = true;

    private Text _firstAnimalTxt;
    private Text _secondAnimalTxt;
    private Text _thirdAnimalTxt;

    // Animal Image
    private bool _setAnimalImg = true;

    private Image _firstAnimalImg;
    private Image _secondAnimalImg;
    private Image _thirdAnimalImg;

    // Extra Variables
    private bool _isEndQuizKeyReleased = false;
    private SwitchQuiz _switchQuiz;

    private void Start()
    {
        if (transform.root.gameObject.activeSelf)
        {
            FirstInput.Select();
            _deactiveButtons = this.GetComponent<DeactiveButtons>();
            this.transform.GetChild(0).gameObject.SetActive(true);
            this.transform.GetChild(1).gameObject.SetActive(false);

            _switchQuiz= this.gameObject.transform.root.GetComponent<SwitchQuiz>();
        }
    }

    void Update()
    {
        if (transform.root.gameObject.activeSelf)
        {
            SetQuestionText();
            SetAnimalText();
            SetAnimalImage();
            CheckAnswer();
            EndQuiz();
        }
    }

    private void SetQuestionText()
    {
        if (_setQuestionTxt)
        {
            QuizBase.GetComponent<QuizBase>().QuizQuestionText.text = QuestionText;

            _setQuestionTxt = false;
        }
    }

    private void SetAnimalText()
    {
        if (_setAnimalTxt)
        {
            _firstAnimalTxt = this.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();
            _secondAnimalTxt = this.transform.GetChild(0).transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>();
            _thirdAnimalTxt = this.transform.GetChild(0).transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>();

            _firstAnimalTxt.text = FirstAnimalTxt;
            _secondAnimalTxt.text = SecondAnimalTxt;
            _thirdAnimalTxt.text = ThirdAnimalTxt;

            _setAnimalTxt = false;
        }
    }

    private void SetAnimalImage()
    {
        if (_setAnimalImg)
        {
            _firstAnimalImg = this.transform.transform.GetChild(0).GetChild(0).GetComponent<Image>();
            _secondAnimalImg = this.transform.transform.GetChild(0).GetChild(1).GetComponent<Image>();
            _thirdAnimalImg = this.transform.transform.GetChild(0).GetChild(2).GetComponent<Image>();

            _firstAnimalImg.sprite = FirstAnimalImg;
            _secondAnimalImg.sprite = SecondAnimalImg;
            _thirdAnimalImg.sprite = ThirdAnimalImg;

            _setAnimalImg = false;
        }
    }

    private void CheckAnswer()
    {
        if (_deactiveButtons.HasPressedBtn && !_hasSelectedAnswer)
        {
            _hasSelectedAnswer = true;

            if (_deactiveButtons.PressedBtn == CorrectAnswerBtn)
                ShowRecapAnswer(true);
            else
                ShowRecapAnswer(false);
        }
    }

    private void ShowRecapAnswer(bool isRightAswer)
    {
        if (!_setRecapAnswer)
        {
            _setRecapAnswer = true;

            this.transform.GetChild(0).gameObject.SetActive(false);

            GameObject recapAnswer = this.transform.GetChild(1).gameObject;
            recapAnswer.SetActive(true);
            recapAnswer.transform.GetChild(0).GetComponent<Image>().sprite = CorrectAnswerImg;
            recapAnswer.transform.GetChild(4).GetComponent<Text>().text = CorrectAnswerTxt;

            if (isRightAswer)
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

    private void EndQuiz()
    {
        if (_setRecapAnswer)
        {
            if (Inputs.Action || Inputs.Confirm)
            {
                if (_isEndQuizKeyReleased)
                {
                    _isEndQuizKeyReleased = false;
                    _switchQuiz.SwitchToNextQuiz();
                }
            }
            else
                _isEndQuizKeyReleased = true;
        }
    }
}