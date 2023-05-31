using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchToNextQuiz : MonoBehaviour
{
    public int AnimalIndex;
    public List<GameObject> Quizzes = new List<GameObject>();

    [Space(10)]
    public GameObject Animal;
    public GameObject EndScreen;

    private int _currentQuizIndex = 0;
    private Text _questionText;
    private List<string> _questions = new List<string>();

    private Inputs _inputs;
    private bool _canEnd = false;
    private bool _isEndScreenKeyPressed = false;

    private void OnEnable()
    {
        _inputs = FindObjectOfType<Inputs>();

        _inputs.ActionInputEvent += OnActionConfirmInput;
       _inputs.ConfirmInputEvent += OnActionConfirmInput;
    }

    private void OnDisable()
    {
        _inputs.ActionInputEvent -= OnActionConfirmInput;
        _inputs.ConfirmInputEvent -= OnActionConfirmInput;
    }

    private void OnActionConfirmInput(bool newActionConfirmState)
    {
        if (newActionConfirmState && EndScreen.gameObject.activeSelf)
        {
            if (!_canEnd)
                _canEnd = true;
            else
            {
                StartCoroutine(FindObjectOfType<FirebaseManager>().UpdateUSer(AnimalIndex));
                FindObjectOfType<SceneSwitch>().ChangeScene("Selectionscreen");
            }
        }
    }

    void Start()
    {
        _questionText = this.transform.GetChild(0).GetChild(0).GetComponent<Text>();

        if (Quizzes.Count > 0)
        {
            for (int quizIndex = 0; quizIndex < Quizzes.Count; quizIndex++)
            {
                Quizzes[quizIndex].transform.gameObject.SetActive(false);
                _questions.Add(Quizzes[quizIndex].transform.GetChild(0).GetComponent<QuizBase>().QuestionText);
            }

            Quizzes[0].transform.gameObject.SetActive(true);
            _questionText.text = _questions[0];
            this.transform.GetChild(0).gameObject.SetActive(true);
            Animal.SetActive(true);
            EndScreen.gameObject.SetActive(false); ;
        }
    }

    public void SwitchQuiz()
    {
        Quizzes[_currentQuizIndex].transform.root.gameObject.SetActive(false);
        _currentQuizIndex += 1;

        if (_currentQuizIndex <= Quizzes.Count - 1)
        {
            Transform quizRoot = Quizzes[_currentQuizIndex].transform.root;
            Transform quizPanel = quizRoot.transform.GetChild(0);

            quizRoot.gameObject.SetActive(true);
            
            quizPanel.gameObject.SetActive(true);
            quizPanel.GetChild(0).gameObject.SetActive(true);
            quizPanel.GetChild(1).gameObject.SetActive(false);

            _questionText.text = _questions[_currentQuizIndex];
        }
        else
        {
            _questionText.text = "";
            _isEndScreenKeyPressed = true;
            ShowEndScreen(); 
        }
    }

    private void ShowEndScreen()
    {
        if (!EndScreen.gameObject.activeSelf && _isEndScreenKeyPressed)
        {
            this.transform.GetChild(0).gameObject.SetActive(false);
            Animal.SetActive(false);
            EndScreen.gameObject.SetActive(true);
        }
    }
}