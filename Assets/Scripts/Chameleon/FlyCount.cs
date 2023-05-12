using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FlyCount : MonoBehaviour
{
    public int CurrentFlies { get; private set; }

    public int TotalFlies { get; private set; }

    private List<GameObject> _fliesList;

    private void Start()
    {
        _fliesList = new List<GameObject>(FindObjectsOfType<Fly>().Select(x => x.gameObject));
        TotalFlies = _fliesList.Count;
    }

    void Update()
    {
        _fliesList.RemoveAll(x => x == null || !x.activeSelf);
        CurrentFlies = TotalFlies - _fliesList.Count;
    }
}