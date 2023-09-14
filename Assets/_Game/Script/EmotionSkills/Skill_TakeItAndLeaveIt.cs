using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static EmotionSystem;

[CreateAssetMenu(menuName = "Abilities/TakeItAndLeaveIt", fileName = "TakeItAndLeaveIt")]
//Soll Items kreiieren die Sp�ter als disposable oder f�r den Kampf um emotionen zur�ckzubekommen
public class Skill_TakeItAndLeaveIt : BaseAbility
{
    public int emotionToConsume;
    public override void Activate(AbilityHolder holder)
    {
        EmotionSystem emotionSystem = FindObjectOfType<EmotionSystem>();
        //�berpr�fe ob die ben�tigte Emotionen vorhanden sind
        if (HasAnyEmotionWithValue(emotionSystem))
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

    public void ApplySkillEffects(EmotionSystem emotionSystem)
    {
        //creats Item and put it into the Inventory
        emotionSystem.ConsumeEmotionAsResources(emotionToConsume, skillCost);
        emotionSystem.AddEmotionWithValue(EmotionType.�berraschung, 16f);//Placeholder Skill
    }
}
