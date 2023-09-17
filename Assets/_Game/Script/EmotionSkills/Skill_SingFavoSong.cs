using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static EmotionSystem;

//BattleSkill und Overworldskill; Summe iene melodie die dich beruhigt; Buffed; und Gegner beinflusst
[CreateAssetMenu(menuName = "Abilities/LieblingsSong", fileName = "LieblingsSong")]

public class Skill_SingFavoSong : BaseAbility
{
    public float buffDurattion = 15f;
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

        BuffData SongBuff = new BuffData(
            "CompanionBuff", //name
            buffDurattion, //duration
            1.2f, //attack
            1f, //attackSpeed
            1.2f, //defenceModifier
            1f, //speed
            20f, //healthregen
            1f, //mindregen
            0f
        ); //skillattack

        buffManager.Addbuff(SongBuff);

        //EnemyBuffManager; Debuff; NPC beruhigen maybe; 

    }
}
