using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//beschwört einen Companien der an einem Festen Punkt in der Map steht und dir hilft dich unter kontroll zu haben plus heal + defence
[CreateAssetMenu(menuName = "Abilities/JustSayHello", fileName = "JustSayHello")]

public class Skill_JustSayHallo : BaseAbility
{

    public override void Activate(AbilityHolder holder)
    {

        string currentSceneName = SceneManager.GetActiveScene().name;
        Debug.Log("Current Scene: " + currentSceneName);

        if (currentSceneName == "BattleArena")
        {
            EmotionSystem emotionSystem = FindObjectOfType<EmotionSystem>();

            if (HasRequiredEmotions(emotionSystem))
            {
                ApplySkillEffects(emotionSystem);
            }
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

        playerHealth.DecreaseHealth(100f);
        BuffData companionBuff = new BuffData(
            "CompanionBuff", //name
            5f, //duration
            1f, //attack
            1f, //attackSpeed
            1f, //defenceModifier
            1f, //speed
            10f, //healthregen
            1f, //mindregen
            0f
        ); //skillattack

        buffManager.Addbuff(companionBuff);

        emotionSystem.ConsumeEmotionAsResources(requiredEmotions, skillCost);
    }
}
