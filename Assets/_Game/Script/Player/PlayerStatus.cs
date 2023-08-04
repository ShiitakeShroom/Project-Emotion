using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StatusManager;

public class PlayerStatus : MonoBehaviour
{
    public CharacterStatus playerStatus;

    public event EventHandler OnDeath;
    public event EventHandler<HealthChangeEventArgs> ValueHealthChanged;

    public class HealthChangeEventArgs : EventArgs
    {
        public float amount;
    }

    public void Die()
    {
        OnDeath?.Invoke(this, EventArgs.Empty);
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
            Die();
        }
    }
}
