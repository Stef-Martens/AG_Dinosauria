using UnityEngine;

public class WinLoseChameleon : MonoBehaviour
{
    public int UserIndex;
    public GameObject GamoverObject;

    private int _currentLives;
    private int _currentFlies;
    private int _totalFlies;
    private bool _hasTotalFlyCount = false;
    
    void Update()
    {
        if(!_hasTotalFlyCount)
        {
            _totalFlies = this.GetComponent<FlyCount>().TotalFlies;
            _hasTotalFlyCount = true;
        }

        Win();
        Lose();
    }

    private void Win()
    {
        _currentFlies = this.GetComponent<FlyCount>().CurrentFlies;

        if (_currentFlies == _totalFlies)
        {
            Time.timeScale = 0f;

            StartCoroutine(FindObjectOfType<FirebaseManager>().UpdateUSer(UserIndex));
            FindObjectOfType<SceneSwitch>().ChangeScene("QuizChameleon");
        }
    }

    private void Lose()
    {
        _currentLives = this.GetComponent<LivesChameleon>().CurrentLives;

        if (_currentLives == 0)
        {
            Time.timeScale = 0f;

            GamoverObject.SetActive(true);
            FindObjectOfType<GameOver>().StartGameOver("The Raptorbird attacked you too many times.");
        }
    }
}