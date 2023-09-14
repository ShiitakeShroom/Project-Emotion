using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static EmotionSystem;

[CreateAssetMenu(menuName = "Abilities/SkillsPlayer", fileName = "SelfHarm")]
public class Skill_SelfHarm : BaseAbility
{
    
    public PlayerHealth playerHealth;
    public float damage = 25f;
    public float damageAmount = 5f;//Seconds of Damage
    public float dmagaeIntervall = 3f;
    public float damageDuration = 10f;//Gesamtanzahl von Schaden
    public int resourceEmotions = 2;

    public override void Activate(AbilityHolder holder)
    {
        EmotionSystem emotionSystem = FindObjectOfType<EmotionSystem>();

        //überprüfe ob die benötigte Emotionen vorhanden sind
        if (HasAnyEmotionWithValue(emotionSystem))
        {
            //Führen sie die akitone fpr den Skill aus
            ApplySkillEffects(emotionSystem);

            // Fügen Sie hier ggf. Logik hinzu, um die Kosten des Skills zu berechnen und abzuziehen
        }
        else
        {
            //meldung kann aktiviert werden oder andere Akiton ausgeführt werden 
            Debug.Log("Der Skill kann nicht aktiviert werden");
        }
    }


    /*public bool HasRequiredEmotions(EmotionSystem emotionSystem)
    {
        foreach (EmotionSystem.EmotionType emotionType in requiredEmotions)
        {
            //Überprüfe ob die benötigten Emotionen vorhanden sind
            float emotionValue = emotionSystem.GetEmotionValue(emotionType);

            //Ändere die Bedinung nach anfroderung
            if (emotionValue < skillCost)
            {
                return false;
            }
        }
        return true;
    }*/

    public bool HasAnyEmotionWithValue(EmotionSystem emotionSystem)
    {
        // Erstelle eine Kopie der Emotionen, um die Sortierung vorzunehmen.
        FloatValue[] sortedEmotions = emotionSystem.emotionValues.ToArray();

        // Sortiere die Emotionen nach ihrem Wert in absteigender Reihenfolge.
        sortedEmotions = sortedEmotions.OrderByDescending(e => e.value).ToArray();

        // Überprüfe den höchsten Wert der Emotionen und vergleiche ihn mit skillCost.
        if (sortedEmotions.Length > 0 && sortedEmotions[0].value >= skillCost)
        {
            return true;
        }

        return false;
    }

    private void ApplySkillEffects(EmotionSystem emotionSystem)
    {
        BuffManager buffManager = FindObjectOfType<BuffManager>();
        //Führen sie hier die Akitio für den Skill aus
        //Zum Beispiel Schaden anrichten oder effekte auslösen
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerHealth.DecreaseHealth(damage);
        playerHealth.DecreaseHealthOverTime(dmagaeIntervall, damageAmount, damageDuration);

        BuffData newBuff = new BuffData("attackBoost", 6.75f, 5f, 1f, 1f, 1f, 1f, 1f, 0f);
        buffManager.Addbuff(newBuff);
        emotionSystem.ConsumeEmotionAsResources(resourceEmotions, skillCost);
    }
}
