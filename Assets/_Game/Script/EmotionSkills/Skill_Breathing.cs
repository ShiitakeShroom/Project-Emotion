using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static EmotionSystem;

[CreateAssetMenu(menuName = "Abilities/Breathing", fileName = "Breathing Exercise")]

public class Skill_Breathing : BaseAbility
{
    public float duration;
    public float monsterDuration;
    public int resourceEmotions = 4;

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

    /*public bool HasAnyEmotionWithValue(EmotionSystem emotionSystem)
    {
        foreach (EmotionSystem.EmotionType emotionType in requiredEmotions)
        {
            float emotionValue = emotionSystem.GetEmotionValue(emotionType);

            // Überprüfe, ob der Wert des Emotionstyps den Zielwert erreicht oder überschreitet
            if (emotionValue >= skillCost)
            {
                return true;
            }
        }
        return false;S
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

        emotionSystem.SetMonsterTransformationThreshhold(monsterDuration, duration);

        BuffData emotionBalanceBuff = new BuffData(
            "EmotionBuff", //name
            duration, //duration
            1f, //attack
            1f, //attackSpeed
            1f, //defenceModifier
            1f, //speed
            10f, //healthregen
            1f, //mindregen
            0f); //skillattack

        buffManager.Addbuff(emotionBalanceBuff);
        emotionSystem.ConsumeEmotionAsResources(resourceEmotions, skillCost);

    }

}
