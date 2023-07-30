using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthSystem: MonoBehaviour
{
    public event EventHandler HealthChanged;
    public event EventHandler HealthMaxChanged;
    public event EventHandler OnDamage;
    public event EventHandler OnHealed;
    public event EventHandler OnDeath;

    private int healthMax;
    private int health;


    public HealthSystem(int healthMax)
    {
        this.healthMax = healthMax;
        health = healthMax;
    }


    //Die Methode "GetHealthNormalized()" gibt den aktuellen 
    //Gesundheitszustand eines Objekts als normalisierten Wert zwischen 0 und 1 zurück.
    public float GetHealthNormalized()
    {
        return (float)health / healthMax;
    }
    
    public int GetHealth()
    {
        return health;
    }
    public int GetHealthMax() {
        return healthMax;
    }

    public void DealDamage(int amount)
    {
        health -= amount;

        if(health < 0){
            health = 0;
        }

        //löst das Event aus und benachrichtigt andere Teile des Codes darüber, dass sich 
        //die Gesundheit des Objektes geändert hat
        HealthChanged?.Invoke(this, EventArgs.Empty);
        OnDamage?.Invoke(this, EventArgs.Empty);

        if(health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //Löst Event aus und benachrichtigt andere Teile des Codes über den Tod des Objektes
        OnDeath?.Invoke(this, EventArgs.Empty);
    }

    public bool IsDead() {
        return health <= 0;
    }

    public void Heal(int amount)
    {
        health += amount;
        if(health > healthMax)
        {
            health = healthMax;
        }

        HealthChanged?.Invoke(this, EventArgs.Empty);
        OnHealed?.Invoke(this, EventArgs.Empty);
    }

    public void SetHealthMax(int healthMax , bool fullHealth)
    {
        this.healthMax = healthMax;
        if (fullHealth) health = healthMax;
        HealthMaxChanged?.Invoke(this, EventArgs.Empty);
        HealthChanged?.Invoke(this, EventArgs.Empty);
    }
}
