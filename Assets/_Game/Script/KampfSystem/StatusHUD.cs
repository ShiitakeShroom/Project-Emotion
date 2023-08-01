using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusHUD : MonoBehaviour
{
    public Image statusHPBar;
    public TextMeshProUGUI statusHPValue;
    public Image statusStaminaBar;
    public TextMeshProUGUI statusStatimaValue;


    public void SetStatusHUD(CharacterStatus status)
    {
        float currentHealth = status.health * (100 / status.maxHealth);
        float currentStamina = status.stamina * (100 / status.maxStamina);

        statusHPBar.fillAmount = currentHealth / 100;
        statusHPValue.SetText(status.health + "/" + status.maxHealth);

        statusStaminaBar.fillAmount += currentStamina / 100;
        statusStatimaValue.SetText(status.stamina + ("/") + status.maxStamina);
    }

    public void SetHP(CharacterStatus status, float hp)
    {
        StartCoroutine(GraduallySetStatusBar(status, hp, false, 10, 0.05f));
    }

    IEnumerator GraduallySetStatusBar(CharacterStatus status, float amount, bool Increase, int fillTimes, float fillDelay)
    {
        float precentage = 1 / (float) fillTimes;
        float currentHealth = status.health;
        if(Increase)
        {
            for (int fillStep = 0; fillStep < fillTimes; fillStep++)
            {
                float _fAmount = amount * precentage;//die Amount of HP die zurück kommt
                float _dAmount = _fAmount / status.maxHealth;//
                currentHealth += _fAmount;
                statusHPBar.fillAmount += _dAmount;
                if (status.health <= status.maxHealth)
                    statusHPValue.SetText(status.health + ("/") + status.maxHealth);
                yield return new WaitForSeconds(fillDelay);
            }
        }
        else
        {
            for (int fillStep = 0; fillStep < fillTimes; fillStep++)
            {
                float _fAmount = amount * precentage;
                float _dAmount = _fAmount / status.maxHealth;
                currentHealth -= _fAmount;
                statusHPBar.fillAmount -= _dAmount;
                if (status.health >= 0)
                    statusHPValue.SetText(status.health + "/" + status.maxHealth);

                yield return new WaitForSeconds(fillDelay);
            }
        }
    }
}
