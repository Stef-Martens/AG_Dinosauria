using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishHookState : IFishState
{
    public void Update(Fish fish)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            fish.CurrentTaps++;
            fish.UpdateCircleImage();
        }
    }

    public void FixedUpdate(Fish fish)
    {
        fish.transform.position = fish.AttachedHook.position;
    }

    public void OnEnterState(Fish fish)
    {
        //fish.animator.SetBool("FishHook", true);
        //fish.canMove = false;
    }

    public void OnExitState(Fish fish)
    {
        //fish.animator.SetBool("FishHook", false);
        //fish.canMove = true;
    }
}
