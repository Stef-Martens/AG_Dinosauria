using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class AlternateTextScript : TextScript
{
    [SerializeField]
    private string[] _altDialogue;

    public bool _isDialogueConditionMet = false;

    [SerializeField]
    private UnityEvent _altDialogueClosedEvent;

    public void ConditionMet()
    {
        _isDialogueConditionMet = true;
    }

    public void ConditionLost()
    {
        _isDialogueConditionMet = false;
    }

    protected override void OnInteract()
    {
        if(!_isDialogueConditionMet)
        base.OnInteract();
        else
        {
            if (!_dialoguePanel.activeInHierarchy)
                AltOpenDialogue();
            else if (_dialogueText.text == _altDialogue[_index])
            {
                AltNextLine();
            }
        }

    }

    protected void AltOpenDialogue()
    {
        _dialoguePanel.SetActive(true);
        SpriteRenderer spriteRend = GetComponent<SpriteRenderer>();
        _textBoxImage.sprite = spriteRend.sprite;
        _npcNameTextbox.text = _npcName;

        _dialoguePanel.SetActive(true);
        StartCoroutine(AltTyping());
        foreach (var player in _collidedObjects)
        {
            Movement playerScript = player.GetComponent<Movement>();
            playerScript._movementLocked = true;
        }
    }

    protected void AltNextLine()
    {
        if (_index < _altDialogue.Length - 1)
        {
            _index++;
            _dialogueText.text = "";
            StartCoroutine(AltTyping());

        }
        else
        {
            CloseDialogue();
            _altDialogueClosedEvent?.Invoke();
        }
    }

    IEnumerator AltTyping()
    {
        foreach (char letter in _altDialogue[_index].ToCharArray())
        {
            _dialogueText.text += letter;
            yield return new WaitForSeconds(_wordSpeed);
        }
    }



}
