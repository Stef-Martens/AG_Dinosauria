using UnityEngine;
using UnityEngine.UI;

public class QuizBase : MonoBehaviour
{
    public Text QuizQuestionText { get;  set; }
    public int AnimalIndex;

    void Start()
    {
        QuizQuestionText = this.transform.GetChild(0).GetChild(0).GetComponent<Text>();
    }
}