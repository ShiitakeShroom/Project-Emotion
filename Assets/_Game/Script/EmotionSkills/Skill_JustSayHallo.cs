using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//beschwört einen Companien der an einem Festen Punkt in der Map steht und dir hilft dich unter kontroll zu haben plus heal + defence
[CreateAssetMenu(menuName = "Abilities/JustSayHello", fileName = "JustSayHello")]

public class Skill_JustSayHallo : BaseAbility
{
    public float newMaxEmotionValue = 74f;
    public float duration = 10f;
    public Transform SpawnPosition;
    public GameObject friend;

    public override void Activate(AbilityHolder holder)
    {

        EmotionSystem emotionSystem = FindObjectOfType<EmotionSystem>();

        if (HasRequiredEmotions(emotionSystem))
        {
            ApplySkillEffects(emotionSystem);
        }
        else
        {
            Debug.Log("Its not the time and Space");
        }
    }

    public bool HasRequiredEmotions(EmotionSystem emotionSystem)
    {
        foreach (EmotionSystem.EmotionType emotionType in requiredEmotions)
        {
            //Überprüfe ob die benötigten Emotionen vorhanden sind
            float emotionValue = emotionSystem.GetEmotionValue(emotionType);

            //Ändere die Bedinung nach anfroderung
            if (emotionValue < skillCost)
            {
                return false;
            }
        }
        return true;
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
        emotionSystem.ConsumeEmotionAsResources(requiredEmotions, skillCost);
    }
    
    private void SpawnCompanion()
    {
        Instantiate(friend, SpawnPosition.position, Quaternion.identity);
    }
}
