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


    public void Start()
    {
        healthSliderPlayer = FindObjectOfType<StatusHUDSliderPlayer>();
        LifeReg();
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


        if(playerStatus.health < 0)
        {
            playerStatus.health = 0;
        }

        if (playerStatus.health <= 0)
        {
            Die();
        }

        float targetValue = playerStatus.health - amount;
        Debug.Log("current amount" + targetValue);
        healthSliderPlayer.SetHealt(GetPlayerHelath(), playerStatus.maxHealth);
    }


    public void Die()
    {
        Debug.Log("its dead");
    }

    public bool IsDead()
    {
        return playerStatus.health <= 0;
    }
    public void IncreasHealth(float amount)
    {
        float targetValue = playerStatus.health + amount;

        healthSliderPlayer.SetHealt(GetPlayerHelath(), playerStatus.maxHealth);
    }
}
