using UnityEngine;
using UnityEngine.UI;

public class SelectionQuiz : MonoBehaviour
{
    public Inputs Inputs;
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
    private bool _canSetQuestionTxt = true;

    // Animal text
    private Text _firstAnimalTxt;
    private Text _secondAnimalTxt;
    private Text _thirdAnimalTxt;

    // Animal Image
    private Image _firstAnimalImg;
    private Image _secondAnimalImg;
    private Image _thirdAnimalImg;

    // Extra Variables
    private bool _isEndQuizKeyReleased = false;

    private void Awake()
    {
        SetAnimalTexts();
        SetAnimalImages();
        
        FirstInput.Select();
        _deactiveButtons = this.GetComponent<DeactiveButtons>();
        this.transform.GetChild(0).gameObject.SetActive(true);
        this.transform.GetChild(1).gameObject.SetActive(false);
    }

    void Update()
    {
        SetQuestionText();
        CheckAnswer();
        EndQuiz();
    }

    private void SetQuestionText()
    { 
        if(_canSetQuestionTxt)
        {
            FindObjectOfType<SwitchToNextQuiz>().QuestionText.text = QuestionText;
            _canSetQuestionTxt = false;
        }
    }

    private void SetAnimalTexts()
    {
        _firstAnimalTxt = this.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();
        _secondAnimalTxt = this.transform.GetChild(0).transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>();
        _thirdAnimalTxt = this.transform.GetChild(0).transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>();
        
        _firstAnimalTxt.text = FirstAnimalTxt;
        _secondAnimalTxt.text = SecondAnimalTxt;
        _thirdAnimalTxt.text = ThirdAnimalTxt;
    }

    private void SetAnimalImages()
    {
        _firstAnimalImg = this.transform.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        _secondAnimalImg = this.transform.transform.GetChild(0).GetChild(1).GetComponent<Image>();
        _thirdAnimalImg = this.transform.transform.GetChild(0).GetChild(2).GetComponent<Image>();
        
        _firstAnimalImg.sprite = FirstAnimalImg;
        _secondAnimalImg.sprite = SecondAnimalImg;
        _thirdAnimalImg.sprite = ThirdAnimalImg;
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
                    FindObjectOfType<SwitchToNextQuiz>().SwitchQuiz();
                }
            }
            else
                _isEndQuizKeyReleased = true;
        }
    }
}