using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeInventoryItem : MonoBehaviour
{
    [SerializeField]
    private Image _targetImage;
    [SerializeField]
    private Text _targetTextBox;
    [SerializeField]
    private string _itemName;


    public void ChangeItem()
    {
        _targetImage.enabled = true;
        SpriteRenderer baseImage = GetComponent<SpriteRenderer>();
        _targetImage.sprite = baseImage.sprite;

        _targetTextBox.text = _itemName;

    }

    public void RemoveItem()
    {
        _targetImage.enabled = false;

        _targetTextBox.text = "Empty";
    }

}
