using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FlyCount : MonoBehaviour
{
    public int FliesTaken;
    public Text FliesTakenText;

    public int FliesTakenCount;

    private List<GameObject> FliesList;

    private void Start()
    {
        FliesList = new List<GameObject>(FindObjectsOfType<FlyV1>().Select(x => x.gameObject));
    }

    void Update()
    {
        FliesList.RemoveAll(x => x == null || !x.activeSelf);


        FliesTakenCount = 4 - FliesList.Count;

        FliesTakenText.text = FliesTakenCount.ToString();

    }

}