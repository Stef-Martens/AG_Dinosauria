using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class Timer : MonoBehaviour
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Pause = !Pause;
    }

    [SerializeField]
    private Image _uiFill;
    [SerializeField]
    private TMP_Text _uiText;

    public int remainingDuration;
    private bool Pause;

    public int Duration;
    public GameObject Bird;

    private void Start()
    {
        Begin(Duration);
    }

    private void Begin(int Seconds)
    {
        remainingDuration = Seconds;
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (remainingDuration >= 0)
        {
            if (!Pause)
            {
                _uiText.text = $"{remainingDuration / 60:00}:{remainingDuration % 60:00}";
                _uiFill.fillAmount = Mathf.InverseLerp(0, Duration, remainingDuration);
                remainingDuration--;
                yield return new WaitForSeconds(1f);
            }
            yield return null;
        }
        OnEnd();
    }

    private void OnEnd()
    {
        Destroy(Bird);
        Destroy(gameObject);
    }
}
