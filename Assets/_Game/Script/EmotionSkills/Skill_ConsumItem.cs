using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static EmotionSystem;

//BaseSkill; Konsumiert bestimmt Items die den Spielercharckter beruhigen aoder aufregen;
[CreateAssetMenu(menuName = "Abilities/ConsumFavoriteFood", fileName = "ConsumFavoriteFood")]

public class Skill_ConsumItem : BaseAbility
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
        //Iventory; Eat something out of it; applyBuff or debuff; depends on Item positiv effect or negative
        //favorite food 
    }
}
