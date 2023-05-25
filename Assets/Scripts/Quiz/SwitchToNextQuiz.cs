using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchToNextQuiz : MonoBehaviour
{
    public int AnimalIndex;
    public List<GameObject> Quizzes = new List<GameObject>();
    
    private int _currentQuizIndex = 0;
    private Text _questionText;
    private List<string> _questions = new List<string>();

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
        }
    }

    public void SwitchQuiz()
    {
        Quizzes[_currentQuizIndex].transform.root.gameObject.SetActive(false);
        _currentQuizIndex += 1;

        if (_currentQuizIndex <= Quizzes.Count - 1)
        {

            ////////////////////////

            Quizzes[_currentQuizIndex].transform.root.gameObject.SetActive(true);
            Quizzes[_currentQuizIndex].transform.root.GetChild(0).gameObject.SetActive(true);
            Quizzes[_currentQuizIndex].transform.root.GetChild(0).GetChild(0).gameObject.SetActive(true);
            Quizzes[_currentQuizIndex].transform.root.GetChild(0).GetChild(1).gameObject.SetActive(false);
            ////////////////////////////////////////
            


            _questionText.text = _questions[_currentQuizIndex];
        }
        else
        {
            _questionText.text = "";
            StartCoroutine(FindObjectOfType<FirebaseManager>().UpdateUSer(AnimalIndex));
            FindObjectOfType<SceneSwitch>().ChangeScene("Selectionscreen");
        }
    }
}