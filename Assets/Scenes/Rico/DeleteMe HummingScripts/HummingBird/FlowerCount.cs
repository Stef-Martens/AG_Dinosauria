using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FlowerCount : MonoBehaviour
{
    public Text text;
    public int count;

    void Awake()
    {
        text = GetComponent<Text>();
        count = 0;
    }

    private void Start() => UpdateCount();

    private void OnEnable() => Nectar.OnCollected += OnNectarCollected;
    private void OnDisable() => Nectar.OnCollected -= OnNectarCollected;

    void OnNectarCollected()
    {
        count++;
        FindObjectOfType<PickupsText>().ShowText("Nectar");
        UpdateCount();
    }

    private void UpdateCount()
    {
        //  text.text = $"{count} / {Nectar.total}";
        text.text = $"{count} / {30}";
    }

    private void Update()
    {
        //tijdelijk aangepast om snel naar volgende scene te gaan
        if (count >= 30 /*Nectar.total*/)
        {
            FindObjectOfType<SceneSwitch>().ChangeScene("QuizBird");
            gameObject.SetActive(false);
        }
    }

}
