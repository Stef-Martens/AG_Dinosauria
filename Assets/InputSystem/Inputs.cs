using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inputs : MonoBehaviour
{
    [Header("Character Input Values")]
    public Vector2 HorizontalMove;
    public Vector2 VerticalMove;
    public bool Action;
    public bool Interact;
    public bool OpenMenu;
    public bool NavigateMenu;
    public bool Confirm;
    public bool Return;
    public char TypedLetter { get; private set; }
    
    [Header("Movement Settings")]
    public bool AnalogMovement;

    private InputAction _anyKeyInputAction;
    private bool _isAnyKeyPressed = false;

    public event System.Action<bool> ActionInputEvent;
    public event System.Action<bool> ConfirmInputEvent;

    public void OnEnable()
    {
        _anyKeyInputAction = new InputAction("anyKeyAction", InputActionType.Button, "<Keyboard>/anyKey");
        _anyKeyInputAction.Enable();
        _anyKeyInputAction.performed += OnAlphabetKeyPress;
    }

    public void OnDisable()
    {
        _anyKeyInputAction.performed -= OnAlphabetKeyPress;
        _anyKeyInputAction.Disable();
    }

    public void OnHorizontalMove(InputAction.CallbackContext context)
    {
        HorizontalMoveInput(context.ReadValue<Vector2>());
    }

    public void OnVerticalMove(InputAction.CallbackContext context)
    {
        VerticalMoveInput(context.ReadValue<Vector2>());
    }

    public void OnAction(InputAction.CallbackContext context)
    {
        if (context.started) ActionInput(true);
        if (context.performed) ActionInput(true);
        if (context.canceled) ActionInput(false);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started) InteractInput(true);
        if (context.performed) InteractInput(true);
        if (context.canceled) InteractInput(false);
    }

    public void OnOpenMenu(InputAction.CallbackContext context)
    {
        if (context.started) OpenMenuInput(true);
        if (context.performed) OpenMenuInput(true);
        if (context.canceled) OpenMenuInput(false);
    }

    public void OnNavigateMenu(InputAction.CallbackContext context)
    {
        if (context.started) NavigateMenuInput(true);
        if (context.performed) NavigateMenuInput(true);
        if (context.canceled) NavigateMenuInput(false);
    }

    public void OnConfirm(InputAction.CallbackContext context)
    {
        if (context.started) ConfirmInput(true);
        if (context.performed) ConfirmInput(true);
        if (context.canceled) ConfirmInput(false);
    }

    public void OnReturn(InputAction.CallbackContext context)
    {
        if (context.started) ReturnInput(true);
        if (context.performed) ReturnInput(true);
        if (context.canceled) ReturnInput(false);
    }

    public void OnAlphabetKeyPress(InputAction.CallbackContext context)
    {
        var keyboard = InputSystem.GetDevice<Keyboard>();

        if (keyboard.anyKey.ReadValue() > 0)
        {
            if (!_isAnyKeyPressed)
            {
                _isAnyKeyPressed = true;

                _anyKeyInputAction.Disable(); // disable the action to prevent further key presses

                foreach (var key in keyboard.allKeys)
                {
                    if (key.wasPressedThisFrame)
                    {
                        // check if the displayName of the key contains only letters of the alphabet
                        bool containsOnlyLetters = Regex.IsMatch(key.displayName, @"^[A-Z]+$");

                        if (containsOnlyLetters)
                        {
                            TypedLetter = key.displayName[0];
                            break;
                        }
                        else
                        {
                            TypedLetter = '\0';
                            break;
                        }
                    }
                }
            }
        }
        else if (keyboard.anyKey.ReadValue() == 0 && _isAnyKeyPressed)
        {
            _isAnyKeyPressed = false;
            TypedLetter = '\0';
            _anyKeyInputAction.Enable(); // re-enable the action to detect the next key press
        }
    }

    public void HorizontalMoveInput(Vector2 newHorizontalMoveDirection)
    {
        HorizontalMove = newHorizontalMoveDirection;
    }

    public void VerticalMoveInput(Vector2 newVerticalMoveDirection)
    {
        VerticalMove = newVerticalMoveDirection;
    }

    public void ActionInput(bool newActionState)
    {
        Action = newActionState;
        ActionInputEvent?.Invoke(newActionState);
    }

    public void InteractInput(bool newInteractState)
    {
        Interact = newInteractState;
    }

    public void OpenMenuInput(bool newOpenMenuState)
    {
        OpenMenu = newOpenMenuState;
    }

    public void NavigateMenuInput(bool newNavigateMenuState)
    {
        NavigateMenu = newNavigateMenuState;
    }

    public void ConfirmInput(bool newConfirmState)
    {
        Confirm = newConfirmState;
        ConfirmInputEvent?.Invoke(newConfirmState);
    }

    public void ReturnInput(bool newReturnState)
    {
        Return = newReturnState;
    }
}