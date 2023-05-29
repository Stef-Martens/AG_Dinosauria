using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject _panel;

    private void Awake()
    {
        _panel.SetActive(true);
        

    }

    private void Update()
    {
        OnPressE();
    }

    void OnPressE()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {

            FindObjectOfType<IntroPlayer>().IntroOpen = false;
            _panel.SetActive(false);
            Destroy(this);
        }
    }


}
