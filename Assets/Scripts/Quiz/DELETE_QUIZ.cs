using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DELETE_QUIZ : MonoBehaviour
{
    private string inputString = ""; // keep track of the current input string
    private int letterCount = 0; // keep track of the number of letters entered

    public GameObject letterI;
    public GameObject letterN;
    public GameObject[] letterS;
    public GameObject letterE;
    public GameObject letterC;
    public GameObject letterT;

    public GameObject letterIui;
    public GameObject letterNui;
    public GameObject[] letterSui;
    public GameObject letterEui;
    public GameObject letterCui;
    public GameObject letterTui;

    private List<GameObject> _uiElements = new List<GameObject>();








    private void Start()
    {
        _uiElements.Add(letterIui);
        _uiElements.Add(letterNui);
        _uiElements.AddRange(letterSui);
        _uiElements.Add(letterEui);
        _uiElements.Add(letterCui);
        _uiElements.Add(letterTui);
    }


    void Update()
    {
        bool allInactive = true;

        // Check if all game objects in the list are inactive
        foreach (GameObject obj in _uiElements)
        {
            if (obj.activeSelf)
            {
                allInactive = false;
                break;
            }
        }

        if (allInactive)
        {
            // Do something when all game objects are inactive
            FindObjectOfType<SwitchToNextQuiz>().SwitchQuiz();

            // UpdateAndLoadScene();
        }


        // Check for player input
        string keyPressed = Input.inputString;
        if (keyPressed != "\b" && keyPressed != "\n" && keyPressed != "\r")
        {
            // Add the new key to the current input string
            inputString += keyPressed;
            letterCount++;

            // Check if the player has entered "INSECTS"
            if (inputString == "insect")
            {
                // Reset the input string and letter count
                inputString = "";
                letterCount = 0;
            }

            // Check for player input
            switch (Input.inputString)
            {
                case "i":
                    letterI.SetActive(true);
                    letterIui.SetActive(false);
                    break;
                case "n":
                    letterN.SetActive(true);
                    letterNui.SetActive(false);
                    break;
                case "s":
                    for (int i = 0; i < letterS.Length; i++)
                    {
                        letterS[i].SetActive(true);
                        for (int j = 0; j < letterSui.Length; j++)
                        {
                            letterSui[j].SetActive(false);
                        }
                    }
                    break;
                case "e":
                    letterE.SetActive(true);
                    letterEui.SetActive(false);
                    break;
                case "c":
                    letterC.SetActive(true);
                    letterCui.SetActive(false);
                    break;
                case "t":
                    letterT.SetActive(true);
                    letterTui.SetActive(false);
                    break;
                default:
                    break;
            }
        }
        else if (keyPressed == "\b" && letterCount > 0)
        {
            // Remove the last character from the input string if backspace is pressed
            inputString = inputString.Substring(0, inputString.Length - 1);
            letterCount--;
        }
    }

   /* void UpdateAndLoadScene()
    {
        StartCoroutine(FindObjectOfType<FirebaseManager>().UpdateUSer(Index));
        //new WaitForSeconds(2f);
        FindObjectOfType<SceneSwitch>().ChangeScene("Selectionscreen");
    }*/
}
