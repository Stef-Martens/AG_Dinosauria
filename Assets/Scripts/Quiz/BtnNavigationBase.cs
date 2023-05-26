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
    public List<Tuple<Button, Navigation>> NavigationsOrigList { get; private set; }

    public bool IsVerticalLayout = false;
    public List<Button> Buttons;

    private Inputs _inputs;

    private HashSet<Button> _permanentlyDisabledBtns;
    private Color _pressedColor;

    private bool _hasBtnsBeenReset = false;
    private bool _hasQuizBeenEnabled = false;
    private bool _hasQuestionEnded = false;
    private bool _isEndQuestionKeyReleased = false;
    private bool _isEndRecapKeyReleased = false;

    public event Action<Button> ButtonPressedEvent;

    private void OnEnable()
    {
        _inputs = FindObjectOfType<Inputs>();

        _inputs.ActionInputEvent += OnActionConfirmInput;
        _inputs.ConfirmInputEvent += OnActionConfirmInput;

        _hasQuizBeenEnabled = true;

        if (_hasQuestionEnded)
        {
            _permanentlyDisabledBtns?.Clear();
            _permanentlyDisabledBtns = new HashSet<Button>();

            ResetButtonsList();

            _hasQuestionEnded = false;
        }

        FirstSelectable = Buttons?.FirstOrDefault();
        FirstSelectable?.Select();
    }

    protected virtual void Start()
    {
        _permanentlyDisabledBtns = new HashSet<Button>();
        _pressedColor = Buttons[0].colors.pressedColor;

        if (Buttons != null)
            foreach (Button btn in Buttons)
                btn.onClick.AddListener(ButtonPressed);

        InitOriginalLists();
    }

    private void ButtonPressed()
    {
        Button btn = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        HandleButtonPressed(btn);
    }

    private void InitOriginalLists()
    {
        ButtonsOrigList = Buttons.ToList();

        NavigationsOrigList = new List<Tuple<Button, Navigation>>();
        for (int btnIndex = 0; btnIndex < Buttons.Count; btnIndex++)
        {
            NavigationsOrigList.Add(new Tuple<Button, Navigation>(Buttons[btnIndex], Buttons[btnIndex].navigation));
        }
    }

    public void SetOriginalButtons(List<Button> originalButtons)
    {
        ButtonsOrigList = new List<Button>(originalButtons);
    }

    public void SetOriginalNavigations(List<Tuple<Button, Navigation>> originalNavigations)
    {
        NavigationsOrigList = new List<Tuple<Button, Navigation>>();
        foreach (var tuple in originalNavigations)
        {
            NavigationsOrigList.Add(new Tuple<Button, Navigation>(tuple.Item1, tuple.Item2));
        }
    }

    private void ResetButtonsList()
    {
        if (ButtonsOrigList != null && NavigationsOrigList != null)
        {
            Buttons = ButtonsOrigList.ToList();

            foreach (Button btn in Buttons) btn.interactable = true;

            for (int btnIndex = 0; btnIndex < Buttons.Count; btnIndex++)
            {
                for (int navIndex = 0; navIndex < NavigationsOrigList.Count; navIndex++)
                {
                    if (NavigationsOrigList[navIndex].Item1 == Buttons[btnIndex])
                    {
                        Buttons[btnIndex].navigation = NavigationsOrigList[navIndex].Item2;
                    }
                }
            }
        }
    }

    private void ResetButtonsStates()
    {
        if (_isEndQuestionKeyReleased)
        {
            if (_inputs.Action || _inputs.Confirm)
            {
                if (_isEndRecapKeyReleased)
                {
                    _isEndRecapKeyReleased = false;
                }
            }
            else
            {
                _isEndRecapKeyReleased = true;
            }

            _hasBtnsBeenReset = true;
        }
    }

    private void OnActionConfirmInput(bool isPressed)
    {
        ResetButtonsStates();
    }

    public void SetEndQuestionKeyReleased(bool isEndQuestionKeyReleased)
    {
        _isEndQuestionKeyReleased = isEndQuestionKeyReleased;
    }

    public void SetHasQuestionEnded(bool hasQuestionEnded)
    {
        _hasQuestionEnded = hasQuestionEnded;
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
        => HasPressedBtn = false;

    public bool GetHasBtnsBeenReset()
        => _hasBtnsBeenReset;

    public bool GetHasQuizBeenEnabled()
        => _hasQuizBeenEnabled;

    public bool GetHasQuestionEnded()
        => _hasQuestionEnded;

    public bool GetIsEndRecapKeyReleased()
        => _isEndRecapKeyReleased;
}

public abstract class SetOriginalButtonValues : MonoBehaviour
{
    private BtnNavigationBase _btnNavigationBase;
    private List<Button> _buttonsOrigList;
    private List<Tuple<Button, Navigation>> _navigationsOrigList;

    private void Start()
    {
        _btnNavigationBase.SetOriginalButtons(_buttonsOrigList);
        _btnNavigationBase.SetOriginalNavigations(_navigationsOrigList);
    }
}