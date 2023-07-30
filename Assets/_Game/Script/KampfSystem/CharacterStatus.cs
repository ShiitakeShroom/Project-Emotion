using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Health/EmotionStatusData", menuName = "StatusObject/Health", order = 0)]
public class CharacterStatus : ScriptableObject
{
    public string charName = "name"; //GIbt dem File einen Namen 
    public float[] position = new float[3];
    public GameObject characterGameObject;
    public int level = 1;
    public int maxHealth = 100;
    public int health = 100;
    public float stamina = 100;
    public float maxStamina = 100;
}
