using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static StatusManager;

public class PlayerHealth : MonoBehaviour
{
    public CharacterStatus playerStatus;
    public StatusHUDSliderPlayer healthSliderPlayer;


    public event EventHandler OnDeath;
    public event EventHandler<HealthChangeEventArgs> ValueHealthChanged;

    public void Start()
    {
        healthSliderPlayer = FindObjectOfType<StatusHUDSliderPlayer>();
        LifeReg();
    }
    public class HealthChangeEventArgs : EventArgs
    {
        public float amount;
    }

    public void Die()
    {
        OnDeath?.Invoke(this, EventArgs.Empty);
    }

    public void LifeReg()
    {
        if(SceneManager.GetActiveScene().name == "SampleScene") {
            playerStatus.health = playerStatus.maxHealth;
        }
    }

    public float GetPlayerHelath()
    {
        return playerStatus.health;
    }
        
    public void DecreaseHealth(float amount)
    {
        playerStatus.health -= amount;

        float targetValue = playerStatus.health - amount;
        Debug.Log("current amount" + targetValue);

        healthSliderPlayer.SetHealt(GetPlayerHelath(), playerStatus.maxHealth);
    }

    public void IncreasHealth(float amount)
    {
        float targetValue = playerStatus.health + amount;

        healthSliderPlayer.SetHealt(GetPlayerHelath(), playerStatus.maxHealth);
    }
}
