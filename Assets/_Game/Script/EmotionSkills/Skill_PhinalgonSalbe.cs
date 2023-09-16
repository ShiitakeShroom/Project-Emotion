using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using static EmotionSystem;

[CreateAssetMenu(menuName = "Abilities/FireShower", fileName = "FireShower")]

//Setz dich selbst starker Hitze aus um schnell hohe emotioen zu verbauchen; +statusboost; leider mit emotionalCharge +
public class Skill_PhinalgonSalbe : BaseAbility
{
    public float newAbsorbtionRate = 1.25f;
    public float duration = 30f;
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
        Player_Base playerbase = FindObjectOfType<Player_Base>();
        emotionSystem.NewAbsorbtionRate(newAbsorbtionRate, duration);


        playerbase.FireAttacks(duration);
    }
}
