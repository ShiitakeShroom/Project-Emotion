using System;
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
        float currentStamina = status.mind * (100 / status.maxMind);

        statusHPBar.fillAmount = currentHealth / 100;
        statusHPValue.SetText(status.health + "/" + status.maxHealth);

        statusStaminaBar.fillAmount = currentStamina / 100;
        statusStatimaValue.SetText(status.mind + ("/") + status.maxMind);
    }

    public void SetHP(CharacterStatus status, float hp)
    {   
        if(gameObject != null) 
        { 
            StartCoroutine(GraduallySetStatusBar(status, hp, false, 1, 0.05f));
        }
    }

    IEnumerator GraduallySetStatusBar(CharacterStatus status, float amount, bool Increase, int fillTimes, float fillDelay)
    {
        float precentage = 1 / (float)fillTimes; // 1/5 = 0,1
        if (Increase)
        {
            for (int fillStep = 0; fillStep < fillTimes; fillStep++)
            {
                float _fAmount = amount * precentage;//die Amount of HP die zurück kommt 
                float _dAmount = _fAmount / status.maxHealth;//
                status.health += _fAmount;
                statusHPBar.fillAmount += _dAmount;
                if (status.health <= status.maxHealth) 
                { 
                    statusHPValue.SetText(status.health + ("/") + status.maxHealth);
                }

                yield return new WaitForSeconds(fillDelay);
            }
        }
        else
        {
            for (int fillStep = 0; fillStep < fillTimes; fillStep++)
            {
                float _fAmount = amount * precentage; // 10 * 0,1 = 1
                float _dAmount = _fAmount / status.maxHealth; // 1/200 = 0,005
                status.health -= _fAmount; // 200 - 1 = 199
                statusHPBar.fillAmount -= _dAmount; // 0,995
                if (status.health >= 0)
                {
                    statusHPValue.SetText(status.health + "/" + status.maxHealth);
                }
                if(status.health < 0)
                {
                    statusHPValue.SetText(0 + "/" + status.maxHealth);
                }
                yield return new WaitForSeconds(fillDelay);
            }
        }

        /*float startAmount = status.health;
        float tragetAmount = status.health - amount;

        for(int fillStep = 0; fillStep < fillTimes; fillStep++)
        {
            float currentAmount = Mathf.Lerp(startAmount, tragetAmount, (float)fillStep / fillTimes);
            status.health = currentAmount;
            statusHPBar.fillAmount = currentAmount / status.maxHealth;
            statusHPValue.SetText(Mathf.Max(0, currentAmount) + "/" + status.maxHealth);
            yield return new WaitForSeconds(fillDelay);
        }*/

    }
}

