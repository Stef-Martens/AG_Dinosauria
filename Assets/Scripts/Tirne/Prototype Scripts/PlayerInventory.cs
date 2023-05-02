using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{ 
    //protected List<GameObject> _inventoryItems = new List<GameObject>(1);

    [SerializeField]
    protected LayerMask _playerMask;
    [SerializeField]
    protected LayerMask _carryingMask;

    protected GameObject _player = null;

    [SerializeField]
    protected Sprite _sprite = null;
    [SerializeField]
    protected string _name = null;

    private void Start()
    {
        _player = this.gameObject;
    }

    private void Update()
    {
        if(_sprite != null)
        {
            _player.layer = LayerMask.NameToLayer("Carrying");
                
        }
        else
        {
            _player.layer = LayerMask.NameToLayer("Player");
        }
    }

    public void AddItem(Sprite sprite, string name)
    {
        _sprite = sprite;
        _name = name;
        Debug.Log("Added Item Successfully");
    }

    public void RemoveItem()
    {
        _sprite = null;
        _name = null;
        _player.layer = _playerMask;
    }


}
