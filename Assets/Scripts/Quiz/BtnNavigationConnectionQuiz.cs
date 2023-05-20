using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BtnNavigationConnectionQuiz : MonoBehaviour
{
    public List<Button> LeftColBtnsOrigList { get; private set; }
    public List<Button> RightColBtnsOrigList { get; private set; }

    public bool IsVerticalLayout = true;

    public List<Button> LeftColBtns;
    public List<Button> RightColBtns;

    private BtnLeftColNavigation _btnLeftColNavigation;
    private BtnRightColNavigation _btnRightColNavigation;

    public List<Tuple<Button, Button>> PressedBtnsList { get; private set; } = new List<Tuple<Button, Button>>();

    private Tuple<Button, Button> _currentBtnTuple;

    public event Action<List<Tuple<Button, Button>>> ButtonPressedEvent;

    private void Start()
    {
        SetLeftColumnValues();
        SetRightColumnValues();
    }

    private void SetLeftColumnValues()
    {
        _btnLeftColNavigation = gameObject.AddComponent<BtnLeftColNavigation>();
        _btnLeftColNavigation.IsVerticalLayout = IsVerticalLayout;
        _btnLeftColNavigation.Buttons = LeftColBtns;
        LeftColBtnsOrigList = LeftColBtns.ToList();

        _btnLeftColNavigation.DerivedButtonPressedEvent += OnLeftColButtonPressed;
    }

    private void SetRightColumnValues()
    {
        _btnRightColNavigation = gameObject.AddComponent<BtnRightColNavigation>();
        _btnRightColNavigation.IsVerticalLayout = IsVerticalLayout;
        _btnRightColNavigation.Buttons = RightColBtns;
        RightColBtnsOrigList = RightColBtns.ToList();

        _btnRightColNavigation.DerivedButtonPressedEvent += OnRightColButtonPressed;
    }

    private void OnLeftColButtonPressed(Button pressedButton)
    {
        _btnLeftColNavigation.FirstSelectable = _btnLeftColNavigation.Buttons?.FirstOrDefault();
        _btnLeftColNavigation.ResetHasPressedButton();
        _btnRightColNavigation.FirstSelectable?.Select();
        
        _currentBtnTuple = new Tuple<Button, Button>(pressedButton, null);
        PressedBtnsList.Add(_currentBtnTuple);
        ButtonPressedEvent?.Invoke(PressedBtnsList);
    }

    private void OnRightColButtonPressed(Button pressedButton)
    {
        _btnRightColNavigation.FirstSelectable = _btnRightColNavigation.Buttons?.FirstOrDefault();
        _btnRightColNavigation.ResetHasPressedButton();
        _btnLeftColNavigation.FirstSelectable?.Select();
        
        _currentBtnTuple = new Tuple<Button, Button>(_currentBtnTuple.Item1, pressedButton);
        PressedBtnsList[PressedBtnsList.Count - 1] = _currentBtnTuple;
        ButtonPressedEvent?.Invoke(PressedBtnsList);
    }
}

public class BtnLeftColNavigation : BtnNavigationBase
{
    public event System.Action<Button> DerivedButtonPressedEvent;

    protected override void Start()
    {
        base.Start();

        foreach (Button btn in Buttons)
        {
            btn.onClick.AddListener(() => DerivedButtonPressedEvent?.Invoke(btn));
        }

        // Set the initial input focus to the first button in the left column
        FirstSelectable = Buttons?.FirstOrDefault();
        FirstSelectable?.Select();
    }
}

public class BtnRightColNavigation : BtnNavigationBase
{
    public event System.Action<Button> DerivedButtonPressedEvent;

    protected override void Start()
    {
        base.Start();

        foreach (Button btn in Buttons)
        {
            btn.onClick.AddListener(() => DerivedButtonPressedEvent?.Invoke(btn));
        }
    }
}