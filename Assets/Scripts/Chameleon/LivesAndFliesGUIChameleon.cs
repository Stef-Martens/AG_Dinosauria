using UnityEngine;
using UnityEngine.UI;

public class LivesAndFliesGUIChameleon : MonoBehaviour
{
    public GameObject LivesAndFlies;
    public GameObject Chameleon;

    private Text _currentLivesTxt;
    private Text _totalLivesTxt;

    private Text _currentFliesTxt;
    private Text _totalFliesTxt;

    private bool _setTotalLivesAndFlies = true;

    void Start()
    {
        _currentLivesTxt = LivesAndFlies.transform.GetChild(1).GetChild(1).GetComponent<Text>();
        _currentFliesTxt = LivesAndFlies.transform.GetChild(2).GetChild(1).GetComponent<Text>();

        _totalLivesTxt = LivesAndFlies.transform.GetChild(1).GetChild(3).GetComponent<Text>();
        _totalFliesTxt = LivesAndFlies.transform.GetChild(2).GetChild(3).GetComponent<Text>();
    }

    void Update()
    {
        SetCurrentLivesAndFlies();
        SetTotalLivesAndFlies();
    }

    private void SetCurrentLivesAndFlies()
    {
        _currentLivesTxt.text = Chameleon.GetComponent<LivesChameleon>().CurrentLives.ToString();
        _currentFliesTxt.text = Chameleon.GetComponent<FlyCount>().CurrentFlies.ToString();
    }

    private void SetTotalLivesAndFlies()
    {
        if (_setTotalLivesAndFlies)
        {
            _totalLivesTxt.text = Chameleon.GetComponent<LivesChameleon>().TotalLives.ToString();
            _totalFliesTxt.text = Chameleon.GetComponent<FlyCount>().TotalFlies.ToString();

            _setTotalLivesAndFlies = false;
        }
    }
}