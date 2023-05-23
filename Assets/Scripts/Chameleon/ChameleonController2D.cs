using System.Diagnostics;

public class ChameleonController2D : BaseController2D
{
    protected override void Update()
    {

        UnityEngine.Debug.Log(Movement.x);

        base.Update();
    }

    protected override void CeilingCheck()
    {

    }

    protected override void Jump()
    {

    }
}