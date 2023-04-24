using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFishState
{
    void Update(Fish fish);
    void FixedUpdate(Fish fish);
    void OnEnterState(Fish fish);
    void OnExitState(Fish fish);
}
