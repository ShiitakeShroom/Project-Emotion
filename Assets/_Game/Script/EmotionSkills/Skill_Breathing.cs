using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Breathing", fileName = "Breathing Exercise")]

public class Skill_Breathing : BaseAbility
{
    public float duration;
    public float monsterDuration;
    public override void Activate(AbilityHolder holder)
    {
        EmotionSystem emotionSystem = FindObjectOfType<EmotionSystem>();   
        //überprüfe ob die benötigte Emotionen vorhanden sind
        if (HasRequiredEmotions(emotionSystem))
        {
            //Führen sie die akitone fpr den Skill aus
            ApplySkillEffects(emotionSystem);

            // Fügen Sie hier ggf. Logik hinzu, um die Kosten des Skills zu berechnen und abzuziehen
        }
        else
        {
            //meldung kann aktiviert werden oder andere Akiton ausgeführt werden 
            Debug.Log("Der Skill kann nicht aktiviert werden");
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

        emotionSystem.SetMonsterTransformationThreshhold(monsterDuration, duration);

        BuffData emotionBalanceBuff = new BuffData(
            "EmotionBuff", //name
            duration, //duration
            1f, //attack
            1f, //attackSpeed
            1f, //defenceModifier
            1f, //speed
            10f, //healthregen
            1f, //mindregen
            0f); //skillattack

        buffManager.Addbuff(emotionBalanceBuff);
        emotionSystem.ConsumeEmotionAsResources(requiredEmotions, skillCost);

    }

}
