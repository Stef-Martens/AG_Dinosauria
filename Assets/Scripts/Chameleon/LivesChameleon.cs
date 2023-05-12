using UnityEngine;

public class LivesChameleon : MonoBehaviour
{
    public int CurrentLives { get; set; } = 5;

    public int TotalLives { get; private set;}

    private void Start()
    {
        TotalLives = CurrentLives;
    }
}