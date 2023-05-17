using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Android;
using UnityEngine.UI;

public class BtnNavigationConnectionQuiz : MonoBehaviour
{
    public bool isVerticalLayout = true;

    public List<Button> LeftColBtns;
    public List<Button> RightColBtns;

    private BtnLeftColNavigation _deactivateLeftColBtns;
    private BtnRightColNavigation _deactivateRightColBtns;


    private void Start()
    {
        _deactivateLeftColBtns = gameObject.AddComponent<BtnLeftColNavigation>();
        _deactivateLeftColBtns.IsVerticalLayout = isVerticalLayout;
        _deactivateLeftColBtns.Buttons = LeftColBtns;


        _deactivateRightColBtns = gameObject.AddComponent<BtnRightColNavigation>();
        _deactivateRightColBtns.IsVerticalLayout = isVerticalLayout;
        _deactivateRightColBtns.Buttons = RightColBtns;
    }
    
    
    //////////////////////////////////////////////////////////
    // REDO with events
    ////////////////////////////////////////////////////////////

    private void Update()
    {
        if (LeftColBtns.Count > 0)
            _deactivateLeftColBtns.FirstInput = LeftColBtns[0];


        if (RightColBtns.Count > 0)
            _deactivateRightColBtns.FirstInput = RightColBtns[0];

        if (_deactivateLeftColBtns.HasPressedBtn)
        {

            _deactivateRightColBtns.FirstInput.Select();
            _deactivateLeftColBtns.ResetHasPressedButton();
        }

        if (_deactivateRightColBtns.HasPressedBtn)
        {
            _deactivateLeftColBtns.FirstInput.Select();
            _deactivateRightColBtns.ResetHasPressedButton();
        }

   /*     Debug.Log("Selected Button (Right Column): " + EventSystem.current.currentSelectedGameObject?.name);

        // Log the name of the currently selected button in the left column
        Debug.Log("Selected Button (Left Column): " + EventSystem.current.currentSelectedGameObject?.name);*/

    }
}

public class BtnLeftColNavigation: BtnNavigationBase
{

}

public class BtnRightColNavigation: BtnNavigationBase
{

}