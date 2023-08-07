using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    public CharacterStatus enemyStatus;
    public StatusHUDSilderEnemy healthHUD;

    public event EventHandler OnDeath;
    public event EventHandler<HealthChangeEnemyEventArgs> ValueHealthEnemyChanged;
    //Even das benutzt wird damit man nicht die UPDATE funktion immer wieder aufrufen muss
    public class HealthChangeEnemyEventArgs : EventArgs
    {
        public float amount;
    }

    public bool isAlive = true;

    public void Awake()
    {
        enemyStatus.health = enemyStatus.maxHealth;
        healthHUD = FindObjectOfType<StatusHUDSilderEnemy>();
            //LoadOnDeath();
    }

    public float GetEnemyHealth()
    {
        return enemyStatus.health;
    }

    //Funktion die Aufgreufen wird wenn man dem Gegner Damage zufügen will
    public void DecreaseHealth(float amount)
    {
        enemyStatus.health -= amount;

        float targetValue = enemyStatus.health - amount;
        Debug.Log("current amount" + targetValue);
        healthHUD.SetHealt(GetEnemyHealth(), enemyStatus.maxHealth);
    }

    public void Die()
    {
        OnDeath?.Invoke(this, EventArgs.Empty);
    }


    public void LoadOnDeath()
    {
        if(enemyStatus.health <= 0 && SceneManager.GetActiveScene().name == "SampleScene") 
        {
            Destroy(gameObject);
        }
    }
}
