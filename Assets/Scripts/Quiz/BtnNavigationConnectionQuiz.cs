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

    private void Start()
    {
        _btnLeftColNavigation = gameObject.AddComponent<BtnLeftColNavigation>();
        _btnLeftColNavigation.IsVerticalLayout = IsVerticalLayout;
        _btnLeftColNavigation.Buttons = LeftColBtns;
        LeftColBtnsOrigList = LeftColBtns.ToList();

        _btnRightColNavigation = gameObject.AddComponent<BtnRightColNavigation>();
        _btnRightColNavigation.IsVerticalLayout = IsVerticalLayout;
        _btnRightColNavigation.Buttons = RightColBtns;
        RightColBtnsOrigList = RightColBtns.ToList();

        _btnLeftColNavigation.DerivedButtonPressedEvent += OnLeftColButtonPressed;
        _btnRightColNavigation.DerivedButtonPressedEvent += OnRightColButtonPressed;
    }

    private void OnLeftColButtonPressed(Button pressedButton)
    {
        _btnLeftColNavigation.FirstSelectable = _btnLeftColNavigation.Buttons?.FirstOrDefault();
        _btnLeftColNavigation.ResetHasPressedButton();
        _btnRightColNavigation.FirstSelectable?.Select();
    }

    private void OnRightColButtonPressed(Button pressedButton)
    {
        _btnRightColNavigation.FirstSelectable = _btnRightColNavigation.Buttons?.FirstOrDefault();
        _btnRightColNavigation.ResetHasPressedButton();
        _btnLeftColNavigation.FirstSelectable?.Select();
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