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
    //Refernze für Health und EmotionSystem 
    public bool hasEntererdTrigger = false;
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
                    Debug.Log("i´m in");
                    CharacterStatus currentEnemyStatus = other.GetComponent<EnemyStatus>().enemyStatus;
                    CharacterStatusManager.Instance.enemyCharacterStatus = currentEnemyStatus;
                    if (!isAttacked) 
                    { 
                        isAttacked = true;
                        //Debug.Log("Gather Dtata ...");
                        PlayerPosition.SavePosition(other.transform.position);
                        //DestroyObjectTracker.MarkObjectAsDestroyed(other.gameObject);
                        LevelLoader.instance.charaStatus = currentEnemyStatus;

                        //Debug.Log(PlayerPosition.GetPosition());
                        //Debug.Log("loadLevel");
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
        if (LevelLoader.instance.playerWins)
        {
            Debug.Log("its a win");
            transform.position = PlayerPosition.GetPosition();
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
