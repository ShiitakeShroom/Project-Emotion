using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/SkillsPlayer", fileName = "SelfHarm")]
public class Skill_SelfHarm : BaseAbility
{
    public PlayerHealth playerHealth;
    public float damage = 25f;
    public float damageAmount = 5f;//Seconds of Damage
    public float dmagaeIntervall = 3f;
    public float damageDuration = 10f;//Gesamtanzahl von Schaden
    

    public override void Activate(AbilityHolder holder)
    {
        EmotionSystem emotionSystem = FindObjectOfType<EmotionSystem>();

        //�berpr�fe ob die ben�tigte Emotionen vorhanden sind
        if (HasRequiredEmotions(emotionSystem))
        {
            //F�hren sie die akitone fpr den Skill aus
            ApplySkillEffects(emotionSystem);

            // F�gen Sie hier ggf. Logik hinzu, um die Kosten des Skills zu berechnen und abzuziehen
        }
        else
        {
            //meldung kann aktiviert werden oder andere Akiton ausgef�hrt werden 
            Debug.Log("Der Skill kann nicht aktiviert werden");
        }
    }


    public bool HasRequiredEmotions(EmotionSystem emotionSystem)
    {
        foreach (EmotionSystem.EmotionType emotionType in requiredEmotions)
        {
            //�berpr�fe ob die ben�tigten Emotionen vorhanden sind
            float emotionValue = emotionSystem.GetEmotionValue(emotionType);

            //�ndere die Bedinung nach anfroderung
            if (emotionValue < skillCost)
            {
                return false;
            }
        }
        return true;
    }

    private void ApplySkillEffects(EmotionSystem emotionSystem)
    {
        //F�hren sie hier die Akitio f�r den Skill aus
        //Zum Beispiel Schaden anrichten oder effekte ausl�sen
        playerHealth = FindObjectOfType<PlayerHealth>();

        playerHealth.DecreaseHealth(damage);

        playerHealth.DecreaseHealthOverTime(dmagaeIntervall, damageAmount, damageDuration);

        emotionSystem.ConsumeEmotionAsResources(requiredEmotions, skillCost);
    }
}
