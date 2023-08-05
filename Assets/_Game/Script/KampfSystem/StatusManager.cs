using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StatusManager : MonoBehaviour
{

    public CharacterStatus playerStatus;//bezug auf das GAmeobejct vom Spieler 
    CharacterStatus currentEnemyStatus;
    public bool isAttacked = false; // schaut ob der Charakter schon im Kampf ist

    //Refernze für Health und EmotionSystem 

    public EmotionSystem emotionSystem;
    //Emotionswert hinzufügen 
    public float[] emotionValues = new float[System.Enum.GetValues(typeof(EmotionSystem.EmotionType)).Length];
    public EmotionBar emotionSlider;

    void OnTriggerEnter(Collider other)
    {   
        
        if(this.playerStatus.health > 0)//schaut ob der Charakter überhaupt am Leben ist
        {
            if(other.CompareTag("Enemy"))
            {
                CharacterStatus currentEnemyStatus = other.GetComponent<EnemyStatus>().enemyStatus;
                CharacterStatusManager.Instance.enemyCharacterStatus = currentEnemyStatus;
                if (!isAttacked) 
                { 
                    isAttacked = true;
                    Debug.Log("Gather Dtata ...");
                    //SetBattleData(other);
                    Debug.Log("loadLevel");
                    LevelLoader.instance.LoadLevel("BattleArena");
                }
                
            }
        }
    }

    public void Awake()
    {
        emotionSystem = FindObjectOfType<EmotionSystem>();
    }

    public void UpdateEmotion()
    {
        if (emotionSystem != null)
        {
            foreach (EmotionSystem.EmotionType emotionType in System.Enum.GetValues(typeof(EmotionSystem.EmotionType)))
            {
                float emotionValue = emotionSystem.GetEmotionValue(emotionType);
                //EmotionValue abhängig vom EmotionType speichern oder anzeigen.
            }
        }
    }

    private void SetBattleData(Collider other)
    {
        //Spielerdaten die Gespeichert werden
        playerStatus.position[0] = this.transform.position.x;
        playerStatus.position[1] = this.transform.position.y;
        playerStatus.position[2] = this.transform.position.z;

        //EnemyData
        CharacterStatus status = other.gameObject.GetComponent<EnemyStatus>().enemyStatus;
        currentEnemyStatus.charName = status.charName;
        currentEnemyStatus.characterGameObject = status.characterGameObject;
        currentEnemyStatus.health = status.health;
        currentEnemyStatus.maxHealth = status.maxHealth;
        currentEnemyStatus.stamina = status.stamina;
        currentEnemyStatus.maxStamina = status.maxStamina;
    }
}
