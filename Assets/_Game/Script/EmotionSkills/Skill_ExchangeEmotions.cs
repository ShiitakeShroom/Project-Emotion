using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static EmotionSystem;

//nur ewinsetzbar wenn ein Skill �ber einen wert von 70% hat wird der in die anderen Emotionen aufgeteilt
[CreateAssetMenu(menuName = "Abilities/ExchangeEmotions", fileName = "ExchangeEmotions")]
public class Skill_ExchangeEmotions : BaseAbility
{
    public int numberOfEmotions;
    public override void Activate(AbilityHolder holder)
    {
        EmotionSystem emotionSystem = FindObjectOfType<EmotionSystem>();

        if(HasAnyEmotionWithValue(emotionSystem))
        {
            ApplySkillEffects(emotionSystem);
            Debug.Log("yes");
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

        // �berpr�fe den h�chsten Wert der Emotionen und vergleiche ihn mit skillCost.
        if (sortedEmotions.Length > 0 && sortedEmotions[0].value >= skillCost)
        {
            return true;
        }
        return false;
    }


    private void ApplySkillEffects(EmotionSystem emotionSystem)
    {
        Debug.LogErrorFormat("Fuck you");
        emotionSystem.DistributeEmotions();
        emotionSystem.ConsumeEmotionAsResources(numberOfEmotions, skillCost);
    }
}
