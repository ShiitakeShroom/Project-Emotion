using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusHUDSliderPlayer : MonoBehaviour
{
    public TextMeshProUGUI statusHPValue;
    public Image statusStaminaBar;
    public TextMeshProUGUI statusStatimaValue;
    public Slider statusHPBar;

    public void SetStatusHUD(CharacterStatus status)
    {
        //float currentHealth = status.health * (100 / status.maxHealth);
        float currentStamina = status.stamina * (100 / status.maxStamina);

        statusHPBar.maxValue = status.maxHealth;
        statusHPBar.value = status.health;
        statusHPValue.SetText(status.health + "");

        statusStaminaBar.fillAmount = currentStamina / 100;
    }

    public void SetHealt(float currentValue, float maxValue)
    {

        statusHPBar.value = currentValue;
        statusHPValue.SetText(currentValue + "");
        /*if (gameObject != null)
        {
            StartCoroutine(GraduallySetStatusBar(status, amount, 10, 0.05f));
        }*/
    }

    /*IEnumerator GraduallySetStatusBar(CharacterStatus status, float targetValue, int fillTimes, float fillDelay)
    {
        float startValue = statusHPBar.value; //400
        float step = (targetValue - startValue) / fillTimes;

        for (int fillStep = 0; fillStep < fillTimes; fillStep++)
        {
            statusHPBar.value += step;  // -35; () *10)

            statusHPValue.SetText(status.health + "/" + status.maxHealth);

            yield return new WaitForSeconds(fillDelay);
        }
    }*/
}

