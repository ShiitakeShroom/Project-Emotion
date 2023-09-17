using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static EmotionSystem;

//passiver Skill; Lass die Situation in deinen Gedanken revoir passieren;
//jenachdem welche Situation kann der Skill deine Stats boosten oder debuffen 
//levelSystem
//kann und soll nicht durch den Spieler aktiviert werden
//wird passiv aktiviert durch bestimmte Aktionen (!Events)
/// <summary>
/// Der Spieler wird durch eine kleine Erinnerung drauf aufmerksam gemacht wenn 
/// die Emotionen einen bestimmten Threshhold haben um den Skill auszulösen
/// der Spieler wird auch gefragt ob er den Skill aktivieren will oder bestimmt aktionen
/// </summary>
[CreateAssetMenu(menuName = "Abilities/DurchSinngebung", fileName = "DurchSinngebung")]

public class Skill_durchSinngebung : BaseAbility
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
        //Checked Level des Spielers; 
        //Fügt den Stats punkt bis zu einem bestimmt Punkt hinzu(!nicht indefinitly!)
        //Stats werden errechnet anhand der Aktion die ausgeführt werden 
        //bestimmte Aktion in Kämpfen oder OverWorld; Questfortschritte; 
        //KampfAktionen; Schaden erlitten; ausweichen; kontern, Schaden in Sekunden ausgeteilt; angriff in sekunden usw. 
        //jedes Level hat ein max Punktsystem an stats 
        //BSP: level 10 hat +20 punkte die ich hinzufügen darf
        //der Spieler ist level 10 und hat nur 2 punkte bis jetzt dadurch bekommt er einen kleinen Boost im hinzufügen der Stats
    }
}
