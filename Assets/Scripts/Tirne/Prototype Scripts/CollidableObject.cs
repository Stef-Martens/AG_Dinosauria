using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidableObject : MonoBehaviour
{
    private Collider2D _collider;
    [SerializeField]
    private ContactFilter2D _filter;
    protected List<Collider2D> _collidedObjects = new List<Collider2D>(1);

    //protected bool _isColliding;

    protected virtual void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    protected virtual void Update()
    {
        _collider.OverlapCollider(_filter, _collidedObjects);
        foreach(var o in _collidedObjects)
        {
            OnCollided(o.gameObject);
        }
    }

    protected virtual void OnCollided(GameObject collidedObject)
    {
        Debug.Log("Collided With " + collidedObject.name);
    }

}
