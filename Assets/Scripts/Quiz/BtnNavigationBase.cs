using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class BtnNavigationBase : MonoBehaviour
{
    public Selectable FirstSelectable { get; set; }
    public bool HasPressedBtn { get; private set; } = false;
    public Button PressedBtn { get; private set; }
    public List<Button> ButtonsOrigList { get; private set; }

    public bool IsVerticalLayout = false;
    public List<Button> Buttons;

    private HashSet<Button> _permanentlyDisabledBtns;
    private Color _pressedColor;

    public event Action<Button> ButtonPressedEvent;

    private void OnEnable()
    {
        FirstSelectable = Buttons?.FirstOrDefault();
        FirstSelectable?.Select();
    }

    protected virtual void Start()
    {
        _permanentlyDisabledBtns = new HashSet<Button>();
        _pressedColor = Buttons[0].colors.pressedColor;

        foreach (Button btn in Buttons)
        {
            btn.onClick.AddListener(() => HandleButtonPressed(btn));
        }

        ButtonsOrigList = Buttons.ToList();
    }

    private void HandleButtonPressed(Button pressedBtn)
    {
        if (_permanentlyDisabledBtns.Contains(pressedBtn))
            return;

        foreach (Button btn in Buttons)
        {
            if (btn == pressedBtn)
            {
                ColorBlock colors = pressedBtn.colors;
                colors.disabledColor = _pressedColor;
                pressedBtn.colors = colors;

                _permanentlyDisabledBtns.Add(pressedBtn);

                HasPressedBtn = true;
                PressedBtn = pressedBtn;

                btn.interactable = false;
                EventSystem.current.SetSelectedGameObject(null);

                // Remove the permanently disabled button
                Buttons.Remove(pressedBtn); 

                ResetButtonNavigation();

                // Raise the event when a button is pressed and permanently disabled
                ButtonPressedEvent?.Invoke(pressedBtn);

                break;
            }
        }
    }

    private void ResetButtonNavigation()
    {
        int buttonCount = Buttons.Count;

        for (int i = 0; i < buttonCount; i++)
        {
            Button btn = Buttons[i];

            Navigation navigation = btn.navigation;
            navigation.mode = Navigation.Mode.Explicit;

            // Clear existing navigation
            navigation.selectOnUp = null;
            navigation.selectOnDown = null;
            navigation.selectOnLeft = null;
            navigation.selectOnRight = null;

            // Calculate new navigation indices
            int previousIndex;
            int nextIndex;

            if (IsVerticalLayout)
            {
                previousIndex = (i - 1 + buttonCount) % buttonCount;
                nextIndex = (i + 1) % buttonCount;
            }
            else
            {
                previousIndex = (i - 1 + buttonCount) % buttonCount;
                nextIndex = (i + 1) % buttonCount;
            }

            // Set new navigation targets
            if (IsVerticalLayout)
            {
                navigation.selectOnUp = Buttons[previousIndex];
                navigation.selectOnDown = Buttons[nextIndex];
            }
            else
            {
                navigation.selectOnLeft = Buttons[previousIndex];
                navigation.selectOnRight = Buttons[nextIndex];
            }

            btn.navigation = navigation;
        }
    }

    public void ResetHasPressedButton()
    {
        HasPressedBtn = false;
    }
}