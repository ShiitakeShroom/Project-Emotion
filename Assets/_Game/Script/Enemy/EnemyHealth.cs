using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Das Script ist das BattleHealthScript
public class EnemyHealth : MonoBehaviour
{
    public CharacterStatus enemyStatus;
    public StatusHUDSilderEnemy healthHUD;

    public void Awake()
    {
        healthHUD = FindObjectOfType<StatusHUDSilderEnemy>();
        enemyStatus.health = enemyStatus.maxHealth;
    }

    public void Start()
    {
        EnemyLifReg();
    }

    public void EnemyLifReg()
    {
        enemyStatus.health = enemyStatus.maxHealth;
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
            Debug.Log("Oh no there is Death");
        }

        float targetValue = enemyStatus.health - amount;
        Debug.Log("current amount" + targetValue);
        healthHUD.SetHealt(GetEnemyHealth(), enemyStatus.maxHealth);
    }
}
