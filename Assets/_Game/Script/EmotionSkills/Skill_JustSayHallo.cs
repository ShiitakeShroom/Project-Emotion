using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using static EmotionSystem;

//beschwört einen Companien der an einem Festen Punkt in der Map steht und dir hilft dich unter kontroll zu haben plus heal + defence
[CreateAssetMenu(menuName = "Abilities/JustSayHello", fileName = "JustSayHello")]

public class Skill_JustSayHallo : BaseAbility
{
    public float newMaxEmotionValue = 74f;
    public float duration = 10f;
    public Transform SpawnPosition;
    public GameObject friend;
    public int resourceEmotions = 1;

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
        BuffManager buffManager = FindObjectOfType<BuffManager>();
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        
        BuffData companionBuff = new BuffData(
            "CompanionBuff", //name
            duration, //duration
            1f, //attack
            1f, //attackSpeed
            1f, //defenceModifier
            1f, //speed
            20f, //healthregen
            1f, //mindregen
            0f
        ); //skillattack
        SpawnCompanion();
        buffManager.Addbuff(companionBuff);
        emotionSystem.SetMaxEmoitionValue(newMaxEmotionValue, duration);
        emotionSystem.ConsumeEmotionAsResources(resourceEmotions, skillCost);
    }
    
    private void SpawnCompanion()
    {
        Instantiate(friend, SpawnPosition.position, Quaternion.identity);
    }
}
