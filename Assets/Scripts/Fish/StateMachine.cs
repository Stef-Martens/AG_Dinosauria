using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<Fish>
{
    public Fish CurrentState { get; private set; }

    public StateMachine(Fish initialState)
    {
        CurrentState = initialState;
    }

    public void ChangeState(Fish newState)
    {
        CurrentState = newState;
        (CurrentState as IFishState)?.OnEnterState(null);
    }
}
