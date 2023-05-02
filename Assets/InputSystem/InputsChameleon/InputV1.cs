using UnityEngine;
using UnityEngine.InputSystem;

public class InputV1 : MonoBehaviour
{
    [Header("Character Input Values")]
    public float Move;
    public bool ShootTongue;
    public bool MoveIndicatorDown;
    public bool MoveIndicatorUp;

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveInput(context.ReadValue<Vector2>().x);
    }

    public void OnShootTongue(InputAction.CallbackContext context)
    {
        if (context.started) ShootTongueInput(true);
        if (context.performed) ShootTongueInput(true);
        if (context.canceled) ShootTongueInput(false);
    }

    public void OnMoveIndicatorDown(InputAction.CallbackContext context)
    {
        if (context.started) MoveIndicatorDownInput(true);
        if (context.performed) MoveIndicatorDownInput(true);
        if (context.canceled) MoveIndicatorDownInput(false);
    }

    public void OnMoveIndicatorUp(InputAction.CallbackContext context)
    {
        if (context.started) MoveIndicatorUpInput(true);
        if (context.performed) MoveIndicatorUpInput(true);
        if (context.canceled) MoveIndicatorUpInput(false);
    }

    public void MoveInput(float newMoveDirection)
    {
        Move = newMoveDirection;
    }

    public void ShootTongueInput(bool newShootTongueState)
    {
        ShootTongue = newShootTongueState;
    }

    public void MoveIndicatorDownInput(bool newSMoveIndicatorDownState)
    {
        MoveIndicatorDown = newSMoveIndicatorDownState;
    }

    public void MoveIndicatorUpInput(bool newMoveIndicatorUpState)
    {
        MoveIndicatorUp = newMoveIndicatorUpState;
    }
}