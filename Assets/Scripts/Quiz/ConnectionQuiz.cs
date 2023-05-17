using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionQuiz : MonoBehaviour
{
    public Inputs Inputs;
    public Selectable FirstInput;
    public bool IsWithSound = false;
    
    // Right Answer

    // Question text
    [Space(10)]
    [TextArea]
    public string QuestionText;

    // Animal Image
    [Space(10)]
    public List<Sprite> AnimalLeftColImgs = new List<Sprite>();
    public List<Sprite> AnimalRightColImgs = new List<Sprite>();
    
    // Correct Answer

    // Question text
    private bool _canSetQuestionTxt = true;

    // Extra Variables
    private void OnEnable()
    {
        FirstInput.Select();
    }
    private void Awake()
    {
        SetAnimalImages();
    }

    void Update()
    {
        SetQuestionText();
    }

    private void SetQuestionText()
    {
        if (_canSetQuestionTxt)
        {
            FindObjectOfType<SwitchToNextQuiz>().QuestionText.text = QuestionText;
            _canSetQuestionTxt = false;
        }
    }

    private void SetAnimalImages()
    {
        for (int buttonIndex = 0; buttonIndex < AnimalLeftColImgs.Count; buttonIndex++)
        {
            this.transform.transform.GetChild(0).GetChild(0).GetChild(buttonIndex).GetChild(1).GetComponent<Image>().sprite = AnimalLeftColImgs[buttonIndex];
        }

        for (int buttonIndex = 0; buttonIndex < AnimalRightColImgs.Count; buttonIndex++)
        {
            this.transform.transform.GetChild(0).GetChild(1).GetChild(buttonIndex).GetChild(1).GetComponent<Image>().sprite = AnimalRightColImgs[buttonIndex];
        }
    }
}