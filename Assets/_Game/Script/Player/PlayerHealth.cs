using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using static StatusManager;

public class PlayerHealth : MonoBehaviour
{

    public event EventHandler HealChangeEvent;
    public event EventHandler HealthChangeEvent;
    
    public CharacterStatus playerStatus;
    public StatusHUDSliderPlayer healthSliderPlayer;

    [Header("Damage Over Timer")]
    public bool isDamageOverTime = false;

    [Header("Regeneration")]
    bool isRegenHealth;
    public float lifeRegvalue;
    public float regTime;


    public void Start()
    {
        LifeReg();
        healthSliderPlayer = FindObjectOfType<StatusHUDSliderPlayer>();
        HealthChangeEvent += HealthChange;
        HealChangeEvent += HealChange;
    }

    public void Update()
    {
        if(playerStatus.health != playerStatus.maxHealth && !isRegenHealth)
        {
            HealthRegenaration();
        }
    }

    public void LifeReg()
    {
        if (playerStatus.isHealedMax == false)
        {
            SetHEalthMax();
            playerStatus.isHealedMax = true;
            Debug.Log("we dont heal here");
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
        HealthChangeEvent?.Invoke(this, EventArgs.Empty);
    }

    public void Heal(float amount)
    {
        playerStatus.health += amount;
        if (playerStatus.health >= playerStatus.maxHealth)
        {
            playerStatus.health = playerStatus.maxHealth;
        }
        float targetvalue = playerStatus.health + amount;
        HealthChangeEvent?.Invoke(this, EventArgs.Empty);
        HealChangeEvent?.Invoke(this, EventArgs.Empty);
    }

    public void Die()
    {
        Debug.Log("its dead");
    }

    public bool IsDead()
    {
        return playerStatus.health <= 0;
    }

    public void HealthChange(object sender, EventArgs e)
    {
        healthSliderPlayer.SetHealt(GetPlayerHelath(), playerStatus.maxHealth);
    }

    public void SetHEalthMax()
    {
        playerStatus.health = playerStatus.maxHealth;
        HealthChangeEvent?.Invoke(this, EventArgs.Empty);
    }

    public void HealthRegenaration()
    {
        regTime = 1f;
        lifeRegvalue = playerStatus.maxHealth * 0.0005f;

        if(playerStatus.health != playerStatus.maxHealth)
        {
            StartCoroutine(RegainHealthOverTime());
        }
    }
    private IEnumerator RegainHealthOverTime()
    {
        isRegenHealth = true;
        while (playerStatus.health < playerStatus.maxHealth)
        {
            Heal(lifeRegvalue);
            yield return new WaitForSeconds(regTime);
        }
        isRegenHealth = false;
    }
    //Damage over Time Funktion die von aus nach innen benutzt werden kann
    //Zeitabstand zwischen den Schadensereignissen in sekunden, betrag des Schadens; Gesamtdauer der Schadensphase in Sekunden
    public void DecreaseHealthOverTime(float damageIntervall, float damageAmount, float damageDuration)
    {
        StartCoroutine(DecreaseHealthOverTimeAttack( damageIntervall, damageAmount, damageDuration));
    }

    private IEnumerator DecreaseHealthOverTimeAttack(float damageIntervall, float damageAmount, float damageDuration)
    {
        Debug.Log("Lets go");
        float timer = 0f;
        while (timer < damageDuration)
        {
            Debug.Log(timer);
            yield return new WaitForSeconds(damageIntervall);

            playerStatus.health -= (playerStatus.maxHealth * (damageAmount / 100));
            Debug.Log((playerStatus.maxHealth * (damageAmount / 100)));

            timer += damageIntervall;
        }

    }

    public void HealChange(object sender, EventArgs e)
    {
        healthSliderPlayer.SetHealt(GetPlayerHelath(), playerStatus.maxHealth);
    }
}
