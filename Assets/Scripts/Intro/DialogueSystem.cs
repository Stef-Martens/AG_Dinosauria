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

    // Start the dialogue with the specified animal
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

    // End the dialogue and hide the dialogue box
    public void EndDialogue()
    {
        if (animal.secondDialogue != null) animal.CurrentDialogue = animal.secondDialogue;

        // change later, for now always action after dialogue
        animal.DoAction();
        FindObjectOfType<IntroPlayer>().canMove = true;
        dialogueBox.SetActive(false);
    }
}
