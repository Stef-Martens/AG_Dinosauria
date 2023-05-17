public class BtnNavigationSelectionQuiz : BtnNavigationBase
{
    private void Awake()
    {
        if (Buttons.Count > 0)
        {
            FirstInput = Buttons[0];
            FirstInput.Select();
        }
    }
}