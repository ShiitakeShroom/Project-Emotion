using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Health/EmotionStatusData", menuName = "StatusObject/Health", order = 0)]
public class CharacterStatus : ScriptableObject
{
    public string charName = "name"; //GIbt dem File einen Namen 
    public float[] position = new float[3];
    public GameObject characterGameObject;
    public int level = 1;
    public float maxHealth;
    public float health;
    public float stamina;
    public float maxStamina;

    public bool isHealedMax = false;
    private const string FirstTimeKey = "FirstTime";

    private void Awake()
    {
        if(PlayerPrefs.GetInt(FirstTimeKey, 0) == 0)
        {
            PlayerPrefs.SetInt(FirstTimeKey, 1);
            isHealedMax = false;
        }
    }
}
