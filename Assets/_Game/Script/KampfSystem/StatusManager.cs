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
    public EnemyScriptSpawn scripty;

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
                Debug.Log("Name" + other.name);
                Debug.Log("i´m in");
                if (!isAttacked) 
                {
                    PlayerPosition.SavePosition(other.transform.position);
                    isAttacked = true;
                    Debug.Log("Gather Dtata ...");
                    //Debug.Log(PlayerPosition.GetPosition());
                    Destroy(other.gameObject);
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
        scripty = FindObjectOfType<EnemyScriptSpawn>();

        if (LevelLoader.instance.playerWins)
        {
            Debug.Log("its a win");
            transform.position = PlayerPosition.GetPosition();
            Debug.Log("Get Psoition");
            scripty.OnCombatSceneExit();
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
