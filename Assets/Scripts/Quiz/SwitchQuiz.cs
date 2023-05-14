using UnityEngine;

public class SwitchQuiz : MonoBehaviour
{
    public bool IsFirstQuiz = false;
    public bool IsLastQuiz = false;
    public GameObject NextQuiz = null;
    public int UserIndex;

    public delegate void OnSwitchQuizDelegate();
    public static event OnSwitchQuizDelegate SwitchQuizEvent;

    void Start()
    {
        if (IsFirstQuiz)
            this.transform.GetChild(0).gameObject.SetActive(true);
        else
            this.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void SwitchToNextQuiz()
    {
        if (NextQuiz != null && !IsLastQuiz)
        {
            gameObject.SetActive(false);
            NextQuiz.transform.GetChild(0).gameObject.SetActive(true);
            SwitchQuizEvent?.Invoke();
        }
        else
        {
            StartCoroutine(FindObjectOfType<FirebaseManager>().UpdateUSer(UserIndex));
            FindObjectOfType<SceneSwitch>().ChangeScene("Selectionscreen");
        }
    }
}