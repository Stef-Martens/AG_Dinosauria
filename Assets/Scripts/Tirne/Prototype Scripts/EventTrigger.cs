using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : InteractableObject
{
    [SerializeField]
    private UnityEvent _onEventTrigger;

    protected override void OnInteract()
    {
        _onEventTrigger.Invoke();
    }


}
