using UnityEngine;

public class ChameleonController2D : BaseController2D
{
    [SerializeField] private Animator _animator;

    protected override void Update()
    {
        _animator.SetFloat("ChameleonBaseSpeed", Mathf.Abs(Movement.x));

        base.Update();
    }

    protected override void CeilingCheck()
    {

    }

    protected override void Jump()
    {

    }
}