using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchToNextQuiz : MonoBehaviour
{
    private Text _questionText;
    public List<string> Questions { get; set; } = new List<string>();

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

        _questionText = this.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        _questionText.text = Questions[0];
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
            


            _questionText.text = Questions[_currentQuizIndex];
        }
        else
        {
            _questionText.text = "";
            StartCoroutine(FindObjectOfType<FirebaseManager>().UpdateUSer(AnimalIndex));
            FindObjectOfType<SceneSwitch>().ChangeScene("Selectionscreen");
        }
    }
}