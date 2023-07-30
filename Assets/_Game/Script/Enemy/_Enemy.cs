using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Enemy : MonoBehaviour
{
    [Header("Attack")]
    private float damageInterval = 5f;
    private float nextDamageTime;
    public float attackRange = 100f;

    [Header("Sriptsverwaltung")]
    private Enemy_Main enemyMain;


    private void Awake()
    {   
        enemyMain = GetComponent<Enemy_Main>();

        nextDamageTime = Time.time + damageInterval;
        //healthSystem.OnDamage += HandleDamage;
        //healthSystem.OnDeath += HandleDeath;
    }

    private void HandleDamage(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void HandleDeath(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }


    /*private void Start()
    {
        //_enemy.healthSystem.HealthChanged += HealthSystem_HealtChanged;
        //_enemy.healthSystem.HealthMaxChanged += HealthSystem_HealthMaxChanged;
    }*/


    // Zeichne den Angriffsbereich als Drahtgitter-Sphere im Editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    #region Unity HealthSystem
    private void OnHealthChanged(object sender, System.EventArgs e)
    {
        // Hier kannst du entsprechende Aktionen ausführen, wenn sich die Gesundheit des Spielers ändert.
        // Zum Beispiel die Anzeige der Gesundheit aktualisieren.
    }

    private void OnDamage(object sender, System.EventArgs e)
    {
        // Hier kannst du entsprechende Aktionen ausführen, wenn der Spieler Schaden erleidet.
    }

    private void OnDeath(object sender, System.EventArgs e)
    {
        // Hier kannst du entsprechende Aktionen ausführen, wenn der Spieler stirbt.
        Destroy(gameObject);
    }
    #endregion

}

