using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static EmotionSystem;

public class Player_Base : MonoBehaviour
{
    [Header("Scrips")]
    public bool IsAlive = true;
    public StatusManager status;

    [Header("Interaktion")]
    public float interactionSphereRadius = 1f;
    public float maxRangeInteraktion = 5f;


    public void Start()
    {
        //Emotion Events
        status.emotionSystem.NearlyMorbingTime += NearlyMorbingTime;
        status.emotionSystem.OhNoItsMorbingTime += ItsMorbingTime;
    }

    public void Update()
    {
        InteractionPlayer();
    }

    public void InteractionPlayer()
    {
        //Interacktion wird ausgelöst wenn E gedrückt wird
        if(Input.GetKeyDown(KeyCode.E)) 
        {
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, interactionSphereRadius, transform.forward, out hit, maxRangeInteraktion))
            {
                NPCInteraktion npc = hit.collider.GetComponent<NPCInteraktion>();
                if (npc != null)
                {
                    Debug.Log("Its Possible");
                    
                        npc.Interact();
                }
            }
        }
    }

    private void HealthSystem_HandleDamage(object sender, EventArgs e)
    {
        if (IsAlive)
        {

        }
    }

    private void HealthSystem_OnHealed(object sender, EventArgs e)
    {
        if (IsAlive == true)
        {
        }
    }

    private void OnDeathHandler(object sender, EventArgs e)
    {
        if (IsAlive == true)
        {
            Debug.Log("I´m dead");
            Application.Quit();
            Destroy(gameObject);
        }
    }

    private void HealthChanged(object sender, EventArgs e)
    {
    }


    private void NearlyMorbingTime(object sender, EventArgs e)
    {
        Debug.Log("just one more. Come to the Darkside");
    }

    private void ItsMorbingTime(object sender, EventArgs e)
    {
        Debug.Log("*show teeths* It´s morbing Time");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionSphereRadius);
    }
}
