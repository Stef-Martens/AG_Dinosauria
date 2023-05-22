using UnityEngine;
public class ChameleonController2D : BaseController2D
{
    //[SerializeField] private Animator _baseAnimator;
    [SerializeField] private Animator _brownCamoAnimator;
    //[SerializeField] private Animator _greenCamoAnimator;

    protected override void CeilingCheck()
    {

    }

    protected override void Jump()
    {

    }

    protected override void Update()
    {
        //_baseAnimator.SetFloat("ChameleonBaseSpeed", 1);
        _brownCamoAnimator.SetFloat("ChameleonBrownSpeed", Mathf.Abs(Inputs.HorizontalMove.x));
        //_greenCamoAnimator.SetFloat("ChameleonGreenSpeed", 1);
        Debug.Log(Mathf.Abs(Inputs.HorizontalMove.x));
    }
}