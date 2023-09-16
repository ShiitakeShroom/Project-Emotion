using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static EmotionSystem;

//Fügt sich selbst kleine Mengen an Schaden zu um die Emotioen die überhand nehmen schnell runterzu fahren 
[CreateAssetMenu(menuName = "Abilities/Akkupunktur", fileName = "Akkupunktur")]

public class Skill_Akupunktur : BaseAbility
{
    public int emotionToConsum;
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
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        BuffManager buffmanger = FindObjectOfType<BuffManager>();
        playerHealth.DecreaseHealth(playerHealth.GetPlayerHelath() * 0.2f);
        emotionSystem.ConsumeEmotionAsResources(emotionToConsum, skillCost);


    }
}
