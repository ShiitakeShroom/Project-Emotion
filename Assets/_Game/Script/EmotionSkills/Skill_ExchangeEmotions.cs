using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//nur ewinsetzbar wenn ein Skill über einen wert von 70% hat wird der in die anderen Emotionen aufgeteilt
[CreateAssetMenu(menuName = "Abilities/ExchangeEmotions", fileName = "ExchangeEmotions")]
public class Skill_ExchangeEmotions : BaseAbility
{
    public override void Activate(AbilityHolder holder)
    {
        EmotionSystem emotionSystem = FindObjectOfType<EmotionSystem>();

        if(HasAnyEmotionWithValue(emotionSystem))
        {
            ApplySkillEffects();
        }
        else
        {
            Debug.Log("Its not the time and Space");
        }
    }

    public bool HasAnyEmotionWithValue(EmotionSystem emotionSystem)
    {
        foreach (EmotionSystem.EmotionType emotionType in requiredEmotions)
        {
            float emotionValue = emotionSystem.GetEmotionValue(emotionType);

            // Überprüfe, ob der Wert des Emotionstyps den Zielwert erreicht oder überschreitet
            if (emotionValue >= 75f)
            {
                return true;
            }
        }
        return false;
    }

    private void ApplySkillEffects()
    {
        EmotionSystem emotion = FindObjectOfType<EmotionSystem>();
        emotion.DistributeEmotions();
    }
}
