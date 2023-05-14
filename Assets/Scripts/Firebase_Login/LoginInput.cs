using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LoginInput : MonoBehaviour
{
    EventSystem system;

    public Selectable FirstInput;

    public GameObject LoginCanvas;
    public GameObject RegisterCanvas;
    public GameObject ResetCanvas;

    void Start()
    {
        system = EventSystem.current;
        FirstInput.Select();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && system.currentSelectedGameObject.GetComponent<InputField>())
        {
            Selectable previous = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
            if (previous != null)
                previous.Select();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && system.currentSelectedGameObject.GetComponent<InputField>())
        {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (next != null)
                next.Select();
        }
        if (Input.GetKeyDown(KeyCode.Tab) && system.currentSelectedGameObject.GetComponent<InputField>())
        {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnRight();
            if (next != null)
                next.Select();
        }


        if (Input.GetKeyDown(KeyCode.Space) && system.currentSelectedGameObject.GetComponent<Button>())
        {
            system.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
        }
    }


    public void RegisterScreen()
    {
        LoginCanvas.SetActive(false);
        RegisterCanvas.SetActive(true);
        RegisterCanvas.transform.GetChild(1).gameObject.GetComponent<Selectable>().Select();
    }

    public void ResetScreen()
    {
        LoginCanvas.SetActive(false);
        ResetCanvas.SetActive(true);
        ResetCanvas.transform.GetChild(1).gameObject.GetComponent<Selectable>().Select();
    }

    public void BackToLogin()
    {
        if (RegisterCanvas.activeSelf) RegisterCanvas.SetActive(false);
        if (ResetCanvas.activeSelf) ResetCanvas.SetActive(false);
        LoginCanvas.SetActive(true);
        LoginCanvas.transform.GetChild(1).gameObject.GetComponent<Selectable>().Select();
    }
}
