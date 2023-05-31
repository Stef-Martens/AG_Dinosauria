using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonInputController : MonoBehaviour
{
    private Button[] _quizButtons;
    private EventSystem _eventSystem;
    private bool _isInputEnabled = true;
    private Selectable _lastSelectedQuizButton;
    private bool _canGetLastSelectedBtn = true;

    private void Start()
    {
        _quizButtons = FindObjectsOfType<Button>();
        _eventSystem = FindObjectOfType<EventSystem>();
    }

    private void Update()
    {
        bool timePaused = Mathf.Approximately(Time.timeScale, 0f);

        if (timePaused && _eventSystem.currentSelectedGameObject != null)
        {
            if (_canGetLastSelectedBtn)
            {
                _lastSelectedQuizButton = _eventSystem.currentSelectedGameObject.GetComponent<Selectable>();
                _canGetLastSelectedBtn = false;
            }
            _eventSystem.SetSelectedGameObject(null);

        }

        if (timePaused)
        {
            DisableButtonInput();
        }
        else
        {
            EnableButtonInput();
        }
    }

    private void DisableButtonInput()
    {
        if (!_isInputEnabled)
            return;

        _isInputEnabled = false;

        foreach (Button button in _quizButtons)
        {
            button.interactable = false;
        }
    }

    private void EnableButtonInput()
    {
        if (_isInputEnabled)
            return;

        _isInputEnabled = true;
        _canGetLastSelectedBtn = true;

        foreach (Button button in _quizButtons)
        {
            button.interactable = true;
        }

        if (_lastSelectedQuizButton != null)
        {
            _eventSystem.SetSelectedGameObject(_lastSelectedQuizButton.gameObject);
        }
    }
}