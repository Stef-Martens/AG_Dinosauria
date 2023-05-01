using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnApple : MonoBehaviour
{
    [SerializeField]
    private GameObject _applePrefab;
    [SerializeField]
    private Transform _parentLocation;

    private Vector2 _spawnLocation;

    private void Start()
    {
        _spawnLocation = _parentLocation.position;
    }

    public void InstantiateApple()
    {
        Instantiate(_applePrefab, _spawnLocation, Quaternion.identity);
    }
}
