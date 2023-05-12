using UnityEngine;

public class WinLoseChameleon : MonoBehaviour
{
    public bool HasWon { get; private set; } = false;
    public bool HasLost { get; private set; } = false;

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
            HasWon = true;
            HasLost = false;

            Time.timeScale = 0f;
        }
    }

    private void Lose()
    {
        _currentLives = this.GetComponent<LivesChameleon>().CurrentLives;

        if (_currentLives == 0)
        {
            HasWon = false;
            HasLost = true;

            Time.timeScale = 0f;
        }
    }
}