using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static EmotionSystem;

//Overworld only Skill; Betrachte die Umgeebung um dich herum und konzentrier dich auf das was dich beruhigt 
[CreateAssetMenu(menuName = "Abilities/TheBeuatyOfNatur", fileName = "TheBeuatyOfNatur")]

public class Skill_TheBeautyOfNatur : BaseAbility
{
    public override void Activate(AbilityHolder holder)
    {

        EmotionSystem emotionSystem = FindObjectOfType<EmotionSystem>();

        if (HasAnyEmotionWithValue(emotionSystem))
        {
            ApplySkillEffects(emotionSystem);
        }
        else
        {
            Debug.Log("Its not the time and Space");
        }
    }

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

        //Starte irgendwas in der UI wodurch Items die nicht sichtbar sind sichterbar zu amchen in einem Radius des Spielers 
    }
}
