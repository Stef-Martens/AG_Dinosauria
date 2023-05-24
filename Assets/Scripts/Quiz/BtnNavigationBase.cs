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

    private bool _hasQuizBeenEnabled = false;
    private bool _hasQuizStarted = false;
    private bool _isEndQuizKeyReleased = false;
    private bool _isEndRecapKeyReleased = false;
    private bool _hasQuizEnded = false;

    public event Action<Button> ButtonPressedEvent;

    private void OnEnable()
    {
        _inputs = FindObjectOfType<Inputs>();

        _inputs.ActionInputEvent += OnBtnActionConfirmInput;
        _inputs.ConfirmInputEvent += OnBtnActionConfirmInput;

        _hasQuizBeenEnabled = true;
        _inputs.ActionInputEvent += OnStartQuizActionConfirmInput;
        _inputs.ConfirmInputEvent += OnStartQuizActionConfirmInput;

        if (_hasQuizEnded)
        {
            _permanentlyDisabledBtns?.Clear();
            _permanentlyDisabledBtns = new HashSet<Button>();

            _inputs.ActionInputEvent += OnEndQuizActionConfirmInput;
            _inputs.ConfirmInputEvent += OnEndQuizActionConfirmInput;

            if (_isEndQuizKeyReleased && !_isEndRecapKeyReleased)
                if (ButtonsOrigList != null)
                    foreach (Button btn in ButtonsOrigList)
                        btn.onClick.RemoveListener(ButtonPressed);

            ResetButtonsList();
        }

        FirstSelectable = Buttons?.FirstOrDefault();
        FirstSelectable?.Select();
    }








    //private void OnEnable()
    //{///////////////////////
    //    _hasStarted = true;

    //    if (_hasEndedQuiz)
    //    {
    //        _permanentlyDisabledBtns?.Clear();
    //        _permanentlyDisabledBtns = new HashSet<Button>();
    //    }

    //    _inputs = FindObjectOfType<Inputs>();

    //    if (_hasEndedQuiz)
    //    {
    //        _inputs.ActionInputEvent += OnEndQuizActionInput;
    //        _inputs.ConfirmInputEvent += OnEndQuizConfirmInput;
    //    }

    //    _inputs.ActionInputEvent += OnBtnActionInput;
    //    _inputs.ConfirmInputEvent += OnBtnConfirmInput;

    //    _inputs.ActionInputEvent += OnStartQuizActionInput;

    //    if (_hasEndedQuiz)
    //    {
    //        if (_isEndQuizKeyReleased && !_isEndRecapKeyReleased)
    //            if (ButtonsOrigList != null)
    //                foreach (Button btn in ButtonsOrigList)
    //                    btn.onClick.RemoveListener(ButtonPressed);

    //        ResetButtonsList();
    //    }

    //    FirstSelectable = Buttons?.FirstOrDefault();
    //    FirstSelectable?.Select();
    //}

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
        if (_isEndQuizKeyReleased)
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

            _hasQuizStarted = true;
        }
    }

    private void OnBtnActionConfirmInput(bool isPressed)
    {
        ResetButtonsStates();
    }

    private void OnStartQuizActionConfirmInput(bool newActionState)
    {
        if (ButtonsOrigList != null)
        {
            if (_hasQuizBeenEnabled && _hasQuizStarted)
            {
                foreach (Button btn in ButtonsOrigList)
                    btn.onClick.RemoveListener(ButtonPressed);

                if (!newActionState)
                {
                    foreach (Button btn in ButtonsOrigList)
                        btn.onClick.AddListener(ButtonPressed);

                    _hasQuizBeenEnabled = false;
                }
            }
        }
    }

    private void OnEndQuizActionConfirmInput(bool newActionState)
    {
        if (!newActionState)
        {
            if (ButtonsOrigList != null)
            {
                foreach (Button btn in ButtonsOrigList)
                {
                    btn.onClick.RemoveListener(ButtonPressed);
                    btn.onClick.AddListener(ButtonPressed);
                }
            }
        }
    }

    public void SetEndQuizKeyReleased(bool isEndQuizKeyReleased)
    {
        _isEndQuizKeyReleased = isEndQuizKeyReleased;
    }

    public void SetHasQuizEnded(bool hasQuizEnded)
    {
        _hasQuizEnded = hasQuizEnded;
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