using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusHUDSilderEnemy : MonoBehaviour
{
    public Slider enemyHealthBarSlider;
    public Transform tragetEnemy; //Der Gegner, über dem die Healthbar erscheinen soll
    public Vector3 offset = new Vector3(0f, 2f, 0f);
    
    //setzt das Leben des Gegners in die Healthbar
    public void SetStatusHUD(CharacterStatus status)
    {
        enemyHealthBarSlider.maxValue = status.maxHealth;
        enemyHealthBarSlider.value = status.health;
    }
    //Aktualisiert die die Healthbar
    public void SetHealt(float currentValue)
    {
        if (enemyHealthBarSlider != null)
        {
            enemyHealthBarSlider.value = currentValue;
        }
    }

    //der LebensBalken verfolgt den Gegner bei der Bewegung
    public void Update()
    {
        if (tragetEnemy != null)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(tragetEnemy.position + offset);
            enemyHealthBarSlider.transform.position = screenPos;
        }
    }
}
