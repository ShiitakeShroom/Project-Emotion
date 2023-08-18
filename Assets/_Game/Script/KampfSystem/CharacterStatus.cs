using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static EmotionObject;

[CreateAssetMenu(fileName = "Health/EmotionStatusData", menuName = "StatusObject/Health", order = 0)]
public class CharacterStatus : ScriptableObject
{
    public string charName = "name"; //GIbt dem File einen Namen 
    public GameObject characterGameObject;
    public EmotionSystem emotionSystem;

    public int level = 1;
    public float maxHealth;
    public float health;
    public float stamina;
    public float maxStamina;
    public bool isHealedMax = false;

    public void UpdateEmotions(EmotionSystem.FloatValue[] newEmotionValues)
    {
        emotionSystem.UpdateEmotions(newEmotionValues);
    }


}
