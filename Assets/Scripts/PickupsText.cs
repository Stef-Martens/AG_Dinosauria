using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupsText : MonoBehaviour
{
    public void ShowText(string pickupName)
    {
        Color temp = transform.GetChild(0).gameObject.GetComponent<Text>().color;
        temp.a = 1f;
        transform.GetChild(0).gameObject.GetComponent<Text>().color = temp;

        transform.GetChild(0).gameObject.GetComponent<Animator>().Play("FadeText");
        transform.GetChild(0).gameObject.GetComponent<Text>().text = pickupName;
    }
}
