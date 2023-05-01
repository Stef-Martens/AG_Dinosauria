using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckJaguar : MonoBehaviour
{
    [SerializeField]
    private GameObject _jaguar;

    public void IfJaguarEnabledDoFart()
    {
        if(_jaguar != null)
        {
            AlternateTextScript script = _jaguar.GetComponent<AlternateTextScript>();
            script.ConditionMet();
        }

    }
}
