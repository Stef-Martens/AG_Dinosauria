using UnityEngine;

public class ChameleonController2D : BaseController2D
{
    [SerializeField] private Animator _baseAnimator;
    [SerializeField] private Animator _brownAnimator;
    [SerializeField] private Animator _greenAnimator;

    protected override void Update()
    {
        _baseAnimator.SetFloat("ChameleonBaseSpeed", Mathf.Abs(Movement.x));
        _brownAnimator.SetFloat("ChameleonBrownSpeed", Mathf.Abs(Movement.x));
        _greenAnimator.SetFloat("ChameleonGreenSpeed", Mathf.Abs(Movement.x));

        base.Update();
    }

    protected override void CeilingCheck()
    {

    }

    protected override void Jump()
    {

    }
}