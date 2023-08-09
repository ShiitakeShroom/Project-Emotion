using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StatusManager : MonoBehaviour
{
    private CharacterStatusManager characterStatusManager;
    private CharacterStatus enemyStatus;
    public CharacterStatus playerStatus;//bezug auf das GAmeobejct vom Spieler 
    public bool isAttacked = false; // schaut ob der Charakter schon im Kampf ist
    public LevelLoader levelLoader;
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
                    PlayerPosition.SavePosition(other.transform.position);
                    Debug.Log(PlayerPosition.GetPosition());
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

    public void Start()
    {
        characterStatusManager = CharacterStatusManager.Instance;
        enemyStatus = characterStatusManager.enemyCharacterStatus;
        levelLoader = FindObjectOfType<LevelLoader>();

        if (levelLoader.playerWins)
        {
            transform.position = PlayerPosition.GetPosition();
            GameObject enemy = enemyStatus.characterGameObject;

            if(enemy != null)
            {
                enemy.SetActive(false);
            }
        }
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
}
