using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static EmotionSystem;

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

        if (!HasAnyEmotionWithValue(emotionSystem) && !isCoolDown)
        {
            AbilityNotUseble();
        }

        if (!CharacterIsOnAllowedState())
        {
            AreaNotUseable();
        }

        else if (HasAnyEmotionWithValue(emotionSystem) && !isCoolDown && CharacterIsOnAllowedState())
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

    public bool HasAnyEmotionWithValue(EmotionSystem emotionSystem)
    {
        // Erstelle eine Kopie der Emotionen, um die Sortierung vorzunehmen.
        FloatValue[] sortedEmotions = emotionSystem.emotionValues.ToArray();

        // Sortiere die Emotionen nach ihrem Wert in absteigender Reihenfolge.
        sortedEmotions = sortedEmotions.OrderByDescending(e => e.value).ToArray();

        // Überprüfe den höchsten Wert der Emotionen und vergleiche ihn mit skillCost.
        if (sortedEmotions.Length > 0 && sortedEmotions[0].value >= ability.skillCost)
        {
            return true;
        }

        return false;
    }

    public void OnIconclick()
    {
        EmotionSystem emotionSystem = FindObjectOfType<EmotionSystem>();

        if (!CharacterIsOnAllowedState())
            return;

        if (!isCoolDown && HasAnyEmotionWithValue(emotionSystem))
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