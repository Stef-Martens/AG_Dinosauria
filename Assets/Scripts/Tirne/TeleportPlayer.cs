using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private Transform _targetPosition;

    public void Teleport()
    {
        _player.transform.position = _targetPosition.position;
    }
}
