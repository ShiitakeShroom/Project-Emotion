using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StatusManager : MonoBehaviour
{

    public event EventHandler<HealthChangeEventArgs> ValueHealthChanged;

    public class HealthChangeEventArgs: EventArgs
    {
        public float amount;
    }

    public event EventHandler OnDeath;

    public CharacterStatus playerStatus;//bezug auf das GAmeobejct vom Spieler 
    CharacterStatus enemyStatus;
    public Player_Base player;
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
                if(!isAttacked) 
                { 
                    isAttacked = true;
                    //setBattleData(other);
                    LevelLoader.instance.LoadLevel("BattleArena");
                }
            }
        }
    }

    public void Awake()
    {
        emotionSystem = FindObjectOfType<EmotionSystem>();
        playerStatus.health = playerStatus.maxHealth;
    }


    public void TakeDamage(float amount)
    {

        ValueHealthChanged?.Invoke(this, new HealthChangeEventArgs
        {
           amount = amount,
        });
        
        if (playerStatus.health <= 0)
        {
            playerStatus.health = 0;
            player.IsAlive = false;
            Die();
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

    private void setBattleData(Collider other)
    {
        //Spielerdaten die Gespeichert werden
        playerStatus.position[0] = this.transform.position.x;
        playerStatus.position[1] = this.transform.position.y;
        playerStatus.position[2] = this.transform.position.z;

        //EnemyData
        CharacterStatus status = other.gameObject.GetComponent<EnemyStatus>().enemyStatus;
        enemyStatus.charName = status.charName;
        enemyStatus.characterGameObject = status.characterGameObject.transform.GetChild(0).gameObject;
        enemyStatus.health = status.health;
        enemyStatus.maxHealth = status.maxHealth;
        enemyStatus.stamina = status.stamina;
        enemyStatus.maxStamina = status.maxStamina;
    }

    public void Die()
    {
        OnDeath?.Invoke(this, EventArgs.Empty);
    }

}
