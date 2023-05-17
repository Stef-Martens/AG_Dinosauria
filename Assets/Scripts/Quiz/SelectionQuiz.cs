using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionQuiz : MonoBehaviour
{
    public Inputs Inputs;
    public bool IsWithSound = false;

    // Right Answer
    [Space(10)]
    public Button CorrectAnswerBtn;
    public string CorrectAnswerTxt;
    public Sprite CorrectAnswerImg;

    // Question text
    [Space(10)]
    [TextArea]
    public string QuestionText;

    // Animal text
    [Space(10)]
    public List<string> AnimalTxts = new List<string>();

    // Animal Image
    [Space(10)]
    public List<Sprite> AnimalImgs = new List<Sprite>();

    // Correct Answer
    private bool _setRecapAnswer = false;
    private BtnNavigationSelectionQuiz _deactiveButtons;
    private bool _hasSelectedAnswer = false;

    // Question text
    private bool _canSetQuestionTxt = true;

    // Extra Variables
    private bool _isEndQuizKeyReleased = false;

    private void Awake()
    {
        SetAnimalTexts();
        SetAnimalImages();
        
        _deactiveButtons = this.GetComponent<BtnNavigationSelectionQuiz>();
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
        for (int buttonIndex = 0; buttonIndex < AnimalTxts.Count; buttonIndex ++)
        {
            this.transform.GetChild(0).GetChild(buttonIndex).GetChild(0).GetChild(0).GetComponent<Text>().text = AnimalTxts[buttonIndex];
        }
    }

    private void SetAnimalImages()
    {
        for (int buttonIndex = 0; buttonIndex < AnimalImgs.Count; buttonIndex++)
        {
            this.transform.transform.GetChild(0).GetChild(buttonIndex).GetComponent<Image>().sprite = AnimalImgs[buttonIndex];
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
                    FindObjectOfType<SwitchToNextQuiz>().SwitchQuiz();
                }
            }
            else
                _isEndQuizKeyReleased = true;
        }
    }
}