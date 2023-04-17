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
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Selectable previous = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
            if (previous != null)
                previous.Select();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (next != null)
                next.Select();
        }


        else if (Input.GetKeyDown(KeyCode.Space) && system.currentSelectedGameObject.GetComponent<Button>())
        {
            system.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
        }
    }

    public void Login()
    {

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
    }

    public void BackToLogin()
    {
        RegisterCanvas.SetActive(false);
        LoginCanvas.SetActive(true);
        LoginCanvas.transform.GetChild(1).gameObject.GetComponent<Selectable>().Select();
    }

    public void Register()
    {

    }
}
