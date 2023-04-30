using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TextScript : InteractableObject
{
    [SerializeField]
    protected GameObject _dialoguePanel;
    [SerializeField]
    protected Text _dialogueText;
    [SerializeField]
    protected Image _textBoxImage;
    [SerializeField]
    protected Text _npcNameTextbox;
    [SerializeField]
    protected string _npcName;
    
    [SerializeField]
    protected string[] _dialogue;
    

    protected int _index;

    [SerializeField]
    protected float _wordSpeed;

    [SerializeField]
    protected UnityEvent _dialogueClosedEvent;

    protected override void Start()
    {
        base.Start();
        _dialogueText.text = "";
        _npcNameTextbox.text = "";

    }

    protected override void OnInteract()
    {
        if (!_dialoguePanel.activeInHierarchy)
            OpenDialogue();
        else if (_dialogueText.text == _dialogue[_index])
        {
            NextLine();
        }
    }
    


    protected void OpenDialogue()
    {
        _dialoguePanel.SetActive(true);
        SpriteRenderer spriteRend = GetComponent<SpriteRenderer>();
        _textBoxImage.sprite = spriteRend.sprite;
        _npcNameTextbox.text = _npcName;
        
        StartCoroutine(Typing());
        foreach(var player in _collidedObjects)
        {
            Movement playerScript = player.GetComponent<Movement>();
            playerScript._movementLocked = true;
        }
    }

    protected void CloseDialogue()
    {
        ZeroText();
        foreach (var player in _collidedObjects)
        {
            Movement playerScript = player.GetComponent<Movement>();
            playerScript._movementLocked = false;
        }
    }
    protected void ZeroText()
    {
        _dialogueText.text = "";
        _index = 0;
        _dialoguePanel.SetActive(false);
        _dialogueClosedEvent?.Invoke();
    }

    protected void NextLine()
    {
        if (_index < _dialogue.Length - 1)
        {
            _index++;
            _dialogueText.text = "";
            StartCoroutine(Typing());

        }
        else
        {
            CloseDialogue();
        }
    }

    IEnumerator Typing()
    {
        foreach(char letter in _dialogue[_index].ToCharArray())
        {
            _dialogueText.text += letter;
            yield return new WaitForSeconds(_wordSpeed);
        }
    }
    

}
