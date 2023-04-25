using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownState : IFishState
{

    private float timer = 0f;
    public void Update(Fish fish)
    {
        timer += Time.deltaTime;
        if (timer >= 3f)
        {
            fish.ChangeState(new SwimmingState());
        }

        if (Input.GetKey(KeyCode.Space))
        {
            fish.rb.velocity = -fish.transform.right * fish.speed;
            fish.FishSound.Play();
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
        //fish.fishHookCollider.enabled = false;
        //fish.canMove = true;
    }

    public void OnExitState(Fish fish)
    {
        // kan terug gepakt worden
    }
}
