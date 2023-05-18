public class BtnNavigationSelectionQuiz : BtnNavigationBase
{
    private void Awake()
    {
        if (Buttons.Count > 0)
        {
            FirstSelectable = Buttons[0];
            FirstSelectable.Select();
        }
    }
}