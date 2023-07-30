using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StatusManager : MonoBehaviour
{
    public CharacterStatus playerStatus;//bezug auf das GAmeobejct vom Spieler 
    public Player_Base player;
    public bool isAttacked = false; // schaut ob der Charakter schon im Kampf ist

    //Refernze für Health und EmotionSystem 
    public HealthSystem healthSystem { get; private set;}

    public EmotionSystem emotionSystem;
    //Emotionswert hinzufügen 
    public float[] emotionValues = new float[System.Enum.GetValues(typeof(EmotionSystem.EmotionType)).Length];

    public EmotionBar emotionSlider;
    public HealthBar slider;

    void OnTriggerEnter(Collider other)
    {
        if(this.healthSystem.GetHealth() > 0)//schaut ob der Charakter überhaupt am Leben ist
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
        healthSystem = FindObjectOfType<HealthSystem>();
        healthSystem = new HealthSystem(playerStatus.maxHealth);
        Debug.Log("health " +  healthSystem.GetHealth());
        slider.SetMaxHealth(playerStatus.maxHealth);
    }


    public void TakeDamage(int amount)
    {
        healthSystem.DealDamage(amount);

        if (healthSystem.GetHealth() <= 0)
        {
            player.IsAlive = false;
            healthSystem.Die();
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



    /*private void setBattleData(Collider other)
    {
        //Spielerdaten die Gespeichert werden
        /*playerStatus.position[0] = this.transform.position.x;
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
    }*/
}
