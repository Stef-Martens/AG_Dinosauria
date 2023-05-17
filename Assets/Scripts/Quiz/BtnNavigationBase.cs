using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class BtnNavigationBase : MonoBehaviour
{
    public Selectable FirstInput { get; set; }
    public bool HasPressedBtn { get; private set; } = false;
    public Button PressedBtn { get; private set; }

    public bool IsVerticalLayout = false;
    public List<Button> Buttons;

    private HashSet<Button> _permanentlyDisabledBtns;
    private Color _pressedColor;
    private Color _normalColor;

    private void Start()
    {
        _pressedColor = Buttons[0].colors.pressedColor;
        _normalColor = Buttons[0].colors.normalColor;

        _permanentlyDisabledBtns = new HashSet<Button>();

        foreach (Button btn in Buttons)
        {
            btn.onClick.AddListener(() => OnButtonPressed(btn));
        }
    }

    private void OnButtonPressed(Button pressedBtn)
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

                Buttons.Remove(pressedBtn); // Remove the permanently disabled button

                ResetButtonNavigation();
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

    public void ResetButtons()
    {
        _permanentlyDisabledBtns.Clear();
        Buttons.AddRange(_permanentlyDisabledBtns); // Restore the permanently disabled buttons

        foreach (Button btn in Buttons)
        {
            btn.interactable = true;
            ColorBlock colors = btn.colors;
            colors.disabledColor = _normalColor;
            btn.colors = colors;
        }

        HasPressedBtn = false;
        PressedBtn = null;
    }

    public void ResetHasPressedButton()
    {
        HasPressedBtn = false;
    }
}