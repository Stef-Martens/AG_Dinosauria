using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    [SerializeField] private Image _staminabarSprite;

    public void UpdateStaminaBar(float maxStamina, float currentStamina)
    {
        _staminabarSprite.fillAmount = currentStamina / maxStamina;
    }
}
