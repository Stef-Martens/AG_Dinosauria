using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.Windows;

public class ChameleonMoveV2 : MonoBehaviour
{
    public InputV1 InputV1;
    public Rigidbody Rigidbody;

    public float MoveSpeed = 3f;
    public bool IsFacingRight = true;

    public GameObject HidingSpot;

    public GameObject BodyGreen;
    public GameObject BodyBrown;

    public bool IsHiding;


    private void Update()
    {
        Rigidbody.velocity = new Vector2(InputV1.Move * MoveSpeed, Rigidbody.velocity.y);
        if (!IsFacingRight && InputV1.Move > 0f)
            Flip();
        else if (IsFacingRight && InputV1.Move < 0f)
            Flip();

        ChangeColor();
    }

    private void Flip()
    {
        IsFacingRight = !IsFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    private void ChangeColor()
    {
        var lowerBound = HidingSpot.transform.position.x - 1.5f;
        var upperBound = HidingSpot.transform.position.x + 1.5f;



        if (this.transform.position.x >= lowerBound && this.transform.position.x <= upperBound )
        {
            BodyGreen.SetActive(false);
            BodyBrown.SetActive(true);

            IsHiding = true;
        }
        else
        {
            BodyGreen.SetActive(true);
            BodyBrown.SetActive(false);

            IsHiding = false;
        }

    }
}