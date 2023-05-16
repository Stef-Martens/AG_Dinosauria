using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchToNextQuiz : MonoBehaviour
{
    public Text QuestionText { get; set; }

    public int AnimalIndex;
    public List<GameObject> Quizzes = new List<GameObject>();
    private int _currentQuizIndex = 0;

    void Start()
    {
        if (Quizzes.Count > 0)
        {
            foreach (GameObject quiz in Quizzes)
                quiz.transform.gameObject.SetActive(false);

            Quizzes[0].transform.gameObject.SetActive(true);
        }

        QuestionText = this.transform.GetChild(0).GetChild(0).GetComponent<Text>();
    }

    public void SwitchQuiz()
    {
        Quizzes[_currentQuizIndex].transform.root.gameObject.SetActive(false);
        _currentQuizIndex += 1;

        if (_currentQuizIndex <= Quizzes.Count - 1)
        {
            Quizzes[_currentQuizIndex].transform.root.gameObject.SetActive(true);
        }
        else
        {
            QuestionText.text = "";
            StartCoroutine(FindObjectOfType<FirebaseManager>().UpdateUSer(AnimalIndex));
            FindObjectOfType<SceneSwitch>().ChangeScene("Selectionscreen");
        }
    }
}