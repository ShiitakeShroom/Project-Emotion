using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static EmotionSystem;

//Beruhigung mit der Hilfe der fünf sinne; durch die konstante abnahme von Emotion ist es dem Spieler möglich das Leben des Gegners zu sehen
[CreateAssetMenu(menuName = "Abilities/AchtsamSehen", fileName = "AchtsamSehen")]

public class Skill_AchtsamSehen : BaseAbility
{
    public int resourceEmotions = 1;
    public bool skillActive = false;
    public float consumptionIntervall = 1.5f;
    public override void Activate(AbilityHolder holder)
    {

        EmotionSystem emotionSystem = FindObjectOfType<EmotionSystem>();
        if (skillActive)
        {
            DeaktivateSkill(emotionSystem);
            Debug.Log("End it");
        }
        else if (HasAnyEmotionWithValue(emotionSystem))
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
        emotionSystem.ConsumEmotionOverTime(skillCost, consumptionIntervall);
        skillActive = true;
        //Starte irgendwas in der UI wodurch das Leben des Gegners Sichtabr ist und 
    }

    void DeaktivateSkill(EmotionSystem emotionSystem)
    {
        emotionSystem.StopConsumptionOfEmotions(skillCost, consumptionIntervall);
        skillActive = false;
    }
}
