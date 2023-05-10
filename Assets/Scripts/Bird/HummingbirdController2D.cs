using UnityEngine;

public class HummingbirdController2D : BaseController2D
{
    private bool _hasReleasedJump = false;

    protected override void CeilingCheck()
    {
        base.CeilingCheck();
    }

    protected override void Jump()
    {
        if (Inputs.Action && JumpTimeoutDelta <= 0.0f && !_hasReleasedJump)
        {
            // Set _hasReleasedJump to true so that we only jump once per release of the jump input
            _hasReleasedJump = true;

            // the square root of H * -2 * G = how much velocity needed to reach desired height
            VerticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
        }

        // Reset _hasReleasedJump when the jump input is released
        if (!Inputs.Action)
        {
            _hasReleasedJump = false;
        }

        // Jump timeout
        if (JumpTimeoutDelta >= 0.0f)
        {
            JumpTimeoutDelta -= Time.deltaTime;
        }
    }
}