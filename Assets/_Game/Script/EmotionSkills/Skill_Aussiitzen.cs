using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using static EmotionSystem;

//Problem die vor dem Spieler auftauchen, die jetzt noch zu schwer zu bewältigen sind können ausgesitzt werden und später angegangen werden
[CreateAssetMenu(menuName = "Abilities/BeiSeiteschieben", fileName = "BeiSeiteschieben")]

public class Skill_Aussiitzen : BaseAbility
{
    public Transform restartPosition;
    public GameObject player;
    public int consumedEmotions;

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
        //verhindert dass der Enemy stirbt
        EnemyLoader enemyLoader = FindObjectOfType<EnemyLoader>();
        PlayerPosition.SavePosition(restartPosition.position);
        enemyLoader.isTouched = false;
        LevelLoader.instance.ReturnToOverWorld("SampleScene");

        PlayerPosition.GetPosition();


        emotionSystem.ConsumeEmotionAsResources(consumedEmotions, skillCost);
    }
}
