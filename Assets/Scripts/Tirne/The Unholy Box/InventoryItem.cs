using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : InteractableObject
{
    [SerializeField]
    protected Sprite _sprite;
    [SerializeField]
    protected string _name;

    protected GameObject _gameObject;

    protected override void OnCollided(GameObject collidedObject)
    {
        base.OnCollided(collidedObject);
        _gameObject = collidedObject;
    }

    protected override void OnInteract()
    {
        PlayerInventory inventory = _gameObject.GetComponent<PlayerInventory>();
        inventory.AddItem(_sprite, _name);
    }

}
