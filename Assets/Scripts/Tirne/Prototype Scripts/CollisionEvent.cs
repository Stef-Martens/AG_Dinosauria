using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionEvent : CollidableObject
{
    public UnityEvent _onCollisionEvent;

    protected override void OnCollided(GameObject collidedObject)
    {
        _onCollisionEvent.Invoke();
    }
}
