using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    public CharacterStatus enemyStatus;
    public StatusHUDSilderEnemy healthHUD;

    public bool isAlive = true;

    public void Awake()
    {
        healthHUD = FindObjectOfType<StatusHUDSilderEnemy>();
    }

    public void Start()
    {   
        //gibt dem Gegner Maxhealth at the Start of the Level 
        //enemyStatus.health = enemyStatus.maxHealth;
        Debug.Log("health"+ enemyStatus.name + GetEnemyHealth());
    }
    public float GetEnemyHealth()
    {
        return enemyStatus.health;
    }

    //Funktion die Aufgreufen wird wenn man dem Gegner Damage zufügen will
    public void DecreaseHealth(float amount)
    {
        enemyStatus.health -= amount;


        if(enemyStatus.health < 0)
        {
            enemyStatus.health = 0;
        }

        if (enemyStatus.health <= 0)
        {
            LoadOnDeath();
            Debug.Log("Oh no there is Death");
        }
        float targetValue = enemyStatus.health - amount;
        Debug.Log("current amount" + targetValue);
        healthHUD.SetHealt(GetEnemyHealth(), enemyStatus.maxHealth);
    }

    public void LoadOnDeath()
    {
        if(enemyStatus.health <= 0 && SceneManager.GetActiveScene().name == "SampleScene") 
        {
            Destroy(gameObject);
        }
    }
}
