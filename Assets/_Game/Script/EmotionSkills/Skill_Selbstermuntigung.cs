using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static EmotionSystem;

[CreateAssetMenu(menuName = "Abilities/Selbstermuntigung", fileName = "Selbstermuntigung")]

//Selbstermuntigung; passiver Skill der nur aktivarbar ist wenn der Spieler im Kampf verliert
//Anstatt das der Spieler zurück in die Overworld teleportiert wird und frustriert ist bekommt er eine zweite Chance indem er wiederaufersteht mit einem MotivationsSatz und abgezogenen emotionen
public class Skill_Selbstermuntigung : BaseAbility
{
    public bool diedOnce = false;
    public int emotionToConsume = 6;
    private PlayerHealth playerHealth;
    public override void Activate(AbilityHolder holder)
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        EmotionSystem emotionSystem = FindObjectOfType<EmotionSystem>();
        if(!diedOnce)
        {
            if (HasAnyEmotionWithValue(emotionSystem))
            {
                ApplySkillEffects(emotionSystem, playerHealth);
            }
        }
        else
        {
            Debug.Log("Your time has come");
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

    private void ApplySkillEffects(EmotionSystem emotionSystem, PlayerHealth playerHealth)
    {
        //Activate Secondwind Window; Der Spieler wird an den Punkt gesetzt wo er im Kampf gefallen 
        //helas for amount of health
        //reduce emotions+
        diedOnce = true;
        emotionSystem.ConsumeEmotionAsResources(emotionToConsume, skillCost);
        playerHealth.Heal(50f);
        Debug.Log("Player died and wurde wiederbelebt");
    }
}
