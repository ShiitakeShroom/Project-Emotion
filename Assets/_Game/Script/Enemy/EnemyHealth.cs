using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public CharacterStatus enemyStatus;

    public event EventHandler OnDeath;
    public event EventHandler<HealthChangeEnemyEventArgs> ValueHealthEnemyChanged;
    //Even das benutzt wird damit man nicht die UPDATE funktion immer wieder aufrufen muss
    public class HealthChangeEnemyEventArgs : EventArgs
    {
        public float amount;
    }

    public bool isAlive = true;

    //Funktion die Aufgreufen wird wenn man dem Gegner Damage zufügen will
    public void TakeDmgEnemy(float amount)
    {
        ValueHealthEnemyChanged?.Invoke(this, new HealthChangeEnemyEventArgs
            {
            amount = amount
        });
        if(enemyStatus.health <= 0 )
        {
            enemyStatus.health = 0;
            Die();
        }
    }

    public void Die()
    {
        OnDeath?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }
}
