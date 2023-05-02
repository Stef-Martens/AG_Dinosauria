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
    int count;

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
        UpdateCount();
    }

    private void UpdateCount()
    {
        text.text = $"{count} / {Nectar.total}";
    }

    private void Update()
    {
        if(count == Nectar.total)
        {
            //SceneManager.LoadScene(0);
            gameObject.SetActive(false);
        }
    }

}
