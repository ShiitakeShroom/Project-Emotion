using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BaseAbility : ScriptableObject
{
    [Header("Cooldown and Casting Time")]
    public bool HasCooldown = true;
    public float CooldDown = 1f;
    public float CastingTime = 0f;
    public float skillCost;
    

    public EmotionSystem.EmotionType[] requiredEmotions;//emotionTypen die für den Skill benötigt werden 

    [Header("Allowed Stats")] 
    public List<Character.CharacterStates> AllowedCharacterStates = new List<Character.CharacterStates>() { Character.CharacterStates.Idle };

    public virtual void OnAbilityUpdate(AbilityHolder holder) { }
    public abstract void Activate(AbilityHolder holder);
}
