using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LetterQuiz : QuizBase
{
    public string CorrectAnswerTxt;
    public Color LetterPressedColor;

    [Space(10)]
    public Color BaseAnwerBoxColor;
    public Color CorrectAnswerBoxColor;
    public Color WrongAnswerBoxColor;

    [Space(10)]
    [Header("Fill the list with 12 single Letters including the correct answer Letters.")]
    public List<char> ScrambledLetters = new List<char>();

    [Space(10)]
    public List<Text> LetterSelectionTxtFields = new List<Text>();
    public List<Image> LetterSelectionImgFields = new List<Image>();

    [Space(10)]
    public List<Text> AnswerTxtFields = new List<Text>();
    public List<Image> AnswerImgFields = new List<Image>();
    private List<char> _answerList = new List<char>();

    private KeyCode _currentKey;
    private bool _isLetterKeyPressed = false;
    private bool _isReturnKeyPressed = false;

    private bool _canSetLetterSelectOrigImgColor = true;
    private Color _letterSelectOrigImgColor;

    private List<char> _correctAnswerList = new List<char>();

    private UnityEvent OnKeyDown;
    private UnityEvent OnKeyUp;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnEnable()
    {
        FillLetterSelectionTextFields();
        
        foreach(Text answerText in AnswerTxtFields)
            answerText.text = "";

        foreach (Image answerBox in AnswerImgFields)
            answerBox.color = BaseAnwerBoxColor;

        _correctAnswerList = CorrectAnswerTxt.ToUpper().ToCharArray().ToList();

        this.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        this.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);

        base.OnEnable();

        GetInputs().AlphabetKeyInputEvent += HandleTypedLetter;
        GetInputs().ReturnInputEvent += DeleteLetters;
    }

    protected override void OnDisable()
    {
        _answerList = new List<char>();

        _isLetterKeyPressed = false;
        _isReturnKeyPressed = false;

        _canSetLetterSelectOrigImgColor = true;

        HasSetRecapAnswer = false;

        GetInputs().AlphabetKeyInputEvent -= HandleTypedLetter;
        GetInputs().ReturnInputEvent -= DeleteLetters;

        base.OnDisable();
    }

    private void FillLetterSelectionTextFields()
    {
        for (int letterIndex = 0; letterIndex < LetterSelectionTxtFields.Count; letterIndex++)
        {
            LetterSelectionTxtFields[letterIndex].text = ScrambledLetters[letterIndex].ToString();
        }
    }

    private void SetIsLetterKeyPressed()
    {
        if (Input.GetKeyDown(_currentKey) && !_isLetterKeyPressed)
        {
            _isLetterKeyPressed = true;
            OnKeyDown?.Invoke();
        }

        if (Input.GetKeyUp(_currentKey) && _isLetterKeyPressed)
        {
            _isLetterKeyPressed = false;
            OnKeyUp?.Invoke();
        }
    }

    private void HandleTypedLetter(char typedLetter)
    {
        if (ScrambledLetters.Contains(typedLetter))
        {
            SetCurrentKey(typedLetter);

            foreach (Text textField in AnswerTxtFields)
            {
                if (textField.text.Length == 0)
                {
                    textField.text = typedLetter.ToString();
                    _answerList.Add(typedLetter);
                    break;
                }
            }
        }

        CheckAnswer();
    }

    private void DeleteLetters(bool isPressed)
    {
        if (isPressed && !_isReturnKeyPressed)
        {
            int lastIndex = _answerList.Count - 1;
            if (lastIndex >= 0)
            {
                _answerList.RemoveAt(lastIndex);
                AnswerTxtFields[lastIndex].text = "";

                // Update the count of _answerList
                _answerList.TrimExcess();
            }

            _isReturnKeyPressed = true;
        }
        else if (!isPressed)
        {
            _isReturnKeyPressed = false;
        }
    }

    private void SetCurrentKey(char typedLetter)
    {
        KeyCode[] keyCodes = (KeyCode[])Enum.GetValues(typeof(KeyCode));
        string letter = typedLetter.ToString().ToUpper();

        foreach (KeyCode keyCode in keyCodes)
        {
            if (keyCode.ToString().ToUpper() == letter)
            {
                _currentKey = keyCode;
                break;
            }
        }
    }

    private void ChangeLetterSelectionImageColor()
    {
        for (int letterIndex = 0; letterIndex < LetterSelectionTxtFields.Count; letterIndex++)
        {
            if (LetterSelectionTxtFields[letterIndex].text == _currentKey.ToString())
            {
                Image currentImage = LetterSelectionImgFields[letterIndex];

                if (_canSetLetterSelectOrigImgColor)
                {
                    _letterSelectOrigImgColor = currentImage.color;
                    _canSetLetterSelectOrigImgColor = false;
                }

                if (_isLetterKeyPressed)
                {
                    currentImage.color = LetterPressedColor;
                }
                else
                {
                    currentImage.color = _letterSelectOrigImgColor;
                    _canSetLetterSelectOrigImgColor = true;
                }
            }
        }
    }

    protected override void SetAnimalTexts()
    {

    }

    protected override void SetAnimalImages()
    {

    }

    protected override void Update()
    {
        SetIsLetterKeyPressed();
        ChangeLetterSelectionImageColor();

        base.Update();
    }

    protected override void CheckAnswer()
    {
        if (_answerList.Count == _correctAnswerList.Count && !HasSelectedAnswer)
        {
            HasSelectedAnswer = true;

            if (_answerList.SequenceEqual(_correctAnswerList))
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

            this.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
            this.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);

            GameObject recapAnswer = this.transform.GetChild(1).gameObject;
            recapAnswer.SetActive(true);
            recapAnswer.transform.GetChild(3).GetComponent<Text>().text = CorrectAnswerTxt;

            for (int answerIndex = 0; answerIndex < _answerList.Count; answerIndex++)
            {
                if (_answerList[answerIndex] == _correctAnswerList[answerIndex])
                    AnswerImgFields[answerIndex].color = CorrectAnswerBoxColor;
                else
                    AnswerImgFields[answerIndex].color = WrongAnswerBoxColor;
            }

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

    protected override void EndQuiz()
    {
        if (GetInputs().Action || GetInputs().Confirm)
        {
            if (HasSetRecapAnswer)
            {
                CanSwitchQuiz = true;
                FindObjectOfType<SwitchToNextQuiz>().SwitchQuiz();
            }
        }
    }
}