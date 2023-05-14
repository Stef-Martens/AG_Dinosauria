using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeactiveButtons : MonoBehaviour
{
    public Button[] Buttons;

    public bool HasPressedBtn { get; private set; } = false;
    public Button PressedBtn { get; private set; }

    private Color _pressedColor;
    private Color _normalColor;

    private void Start()
    {
        _pressedColor = Buttons[0].colors.pressedColor;
        _normalColor= Buttons[0].colors.normalColor;

        foreach (Button btn in Buttons)
        {
            btn.onClick.AddListener(() => OnButtonPressed(btn));
        }
    }

    private void OnButtonPressed(Button pressedBtn)
    {
        foreach (Button btn in Buttons)
        {
            if (btn == pressedBtn)
            {
                ColorBlock colors = pressedBtn.colors;
                colors.disabledColor = _pressedColor;
                pressedBtn.colors = colors;

                HasPressedBtn = true;
                PressedBtn = pressedBtn;
            }
            else
            {
                ColorBlock colors = btn.colors;
                colors.disabledColor = _normalColor;
                btn.colors = colors;
            }
        }

        foreach (Button btn in Buttons)
        {
            btn.interactable = !HasPressedBtn;
        }
    }
}