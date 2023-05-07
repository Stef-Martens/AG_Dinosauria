using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public GameObject dialogueBox;
    protected string dialogueText;

    public AnimalIntro animal;

    private int currentIndex = 0;
    public void NextLine()
    {
        if (currentIndex < animal.CurrentDialogue.Count - 1)
        {
            currentIndex++;
            dialogueText = animal.CurrentDialogue[currentIndex];
            dialogueBox.transform.GetChild(2).GetComponent<Text>().text = dialogueText;
        }
        else
        {
            EndDialogue();
        }
    }

    public void StartDialogue()
    {
        // Set the flag and initialize the dialogue text
        currentIndex = 0;
        dialogueText = animal.CurrentDialogue[currentIndex];

        if (animal.animalName != null) dialogueBox.transform.GetChild(0).GetComponent<Text>().text = animal.animalName;
        if (animal.image != null) dialogueBox.transform.GetChild(1).GetComponent<Image>().sprite = animal.image;
        dialogueBox.transform.GetChild(2).GetComponent<Text>().text = dialogueText;

        // Show the dialogue box
        dialogueBox.SetActive(true);
    }

    public void EndDialogue()
    {
        if (animal.HasActionAfterFirstDialogue && animal.CurrentDialogue[0] == animal.dialogue[0])
        {
            animal.DoFirstAction();
        }
        if (animal.HasActionAfterSecondDialogue && animal.CurrentDialogue[0] == animal.secondDialogue[0])
        {
            animal.DoSecondAction();
        }


        if (animal.SecondDialogeDirectlyAfterTalking) animal.CurrentDialogue = animal.secondDialogue;

        FindObjectOfType<IntroPlayer>().canMove = true;
        dialogueBox.SetActive(false);
    }
}
