using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimmingState : IFishState
{
    public void Update(Fish fish)
    {
        if (Input.GetKey(KeyCode.Space))
        {
            fish.rb.velocity = -fish.transform.right * fish.speed;
        }
    }

    public void FixedUpdate(Fish fish)
    {
        // Rotate the object on the Z axis with the horizontal arrows
        float horizontalInput = Input.GetAxis("Horizontal");
        fish.transform.Rotate(0, 0, -horizontalInput * fish.rotationSpeed * Time.fixedDeltaTime);
    }

    public void OnEnterState(Fish fish)
    {
        // Enable swimming animation and movement controls
        //fish.animator.SetBool("Swimming", true);
        //fish.canMove = true;
    }

    public void OnExitState(Fish fish)
    {
        // Disable swimming animation and movement controls
        //fish.animator.SetBool("Swimming", false);
        //fish.canMove = false;
    }
}
