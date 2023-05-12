using UnityEngine;

public class GameOverGUIChameleon : MonoBehaviour
{
    public GameObject Chameleon;

    void Start()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
    }

    void Update()
    {
        if (Chameleon.GetComponent<WinLoseChameleon>().HasLost)
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}