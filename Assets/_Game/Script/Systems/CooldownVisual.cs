using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CooldownVisual : MonoBehaviour
{

    public Image abilityImage;
    public BaseAbility ability; //cooldown des Skills wird benutzt 
    public Character owner;


    public bool isCoolDown = false;
    public float cooldownTimer = 0.0f;
    public float cooldownTime;

    public void Start()
    {
        abilityImage.fillAmount = 0.0f;
        cooldownTime = ability.CooldDown;
        if (owner == null)
        {
            owner = FindObjectOfType<Character>();
        }
    }


    public void Update()
    {
        //Nichts an der UpdateFunktion ändern; sie Funktioniert so wie sie ist
        if (owner == null)
        {
            owner = FindObjectOfType<Character>();
        }
        EmotionSystem emotionSystem = FindObjectOfType<EmotionSystem>();
        
        ApplyCooldown();

        if (!HasRequiredEmotions(emotionSystem) && !isCoolDown)
        {
            AbilityNotUseble();
        }

        if (!CharacterIsOnAllowedState())
        {
            AreaNotUseable();
        }

        else if (HasRequiredEmotions(emotionSystem) && !isCoolDown && CharacterIsOnAllowedState())
        {
            AbilityUseable();
        }
    }

    public void ApplyCooldown()
    {
        if (isCoolDown)
        {
            cooldownTimer -= Time.deltaTime;
            abilityImage.fillAmount = cooldownTimer / cooldownTime;

            if (cooldownTimer <= 0)
            {
                isCoolDown = false;
                abilityImage.fillAmount = 0.0f;
            }
        }
    }

    public bool HasRequiredEmotions(EmotionSystem emotionSystem)
    {
        foreach (EmotionSystem.EmotionType emotionType in ability.requiredEmotions)
        {
            //Überprüfe ob die benötigten Emotionen vorhanden sind
            float emotionValue = emotionSystem.GetEmotionValue(emotionType);

            //Ändere die Bedinung nach anfroderung
            if (emotionValue < ability.skillCost)
            {
                return false;
            }
        }
        return true;
    }

    public void OnIconclick()
    {
        EmotionSystem emotionSystem = FindObjectOfType<EmotionSystem>();

        if (!CharacterIsOnAllowedState())
            return;

        if (!isCoolDown && HasRequiredEmotions(emotionSystem))
        {
            Debug.Log("Icon wurde gecklickt");
            StartCooldown();
        }
    }

    public bool CharacterIsOnAllowedState()
    {
        return ability.AllowedCharacterStates.Contains(owner.CurrentCharacterStates);

    }

    void StartCooldown()
    {
        isCoolDown = true;
        cooldownTimer = cooldownTime;
    }

    void AbilityNotUseble()
    {
        abilityImage.fillAmount = 1.00f;
        abilityImage.color = Color.blue;
    }

    void AbilityUseable()
    {
        abilityImage.fillAmount = 0.0f;
    }

    void AreaNotUseable()
    {
        abilityImage.fillAmount = 1.0f;
        abilityImage.color = Color.gray;
    }
}