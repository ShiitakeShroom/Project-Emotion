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
    public ProjectilSpawnAbility ability;

    [Header("Interaktion")]
    public float interactionSphereRadius = 1f;
    public float maxRangeInteraktion = 5f;

    public SpawnManager spawnManager;

    public void Start()
    {
        if (LevelLoader.instance.spawnLoader)
        {
            this.transform.position = PlayerPosition.GetPosition();
        }

        spawnManager = FindObjectOfType<SpawnManager>();
        if (!ability)
        {
            ability = FindObjectOfType<ProjectilSpawnAbility>();
        }


        //Emotion Events
        status.emotionSystem.NearlyMorbingTime += NearlyMorbingTime;
        status.emotionSystem.OhNoItsMorbingTime += ItsMorbingTime;
    }

    public void Update()
    {
        InteractionPlayer();
        LoadLevel();
        LoadEnemies();
    }

    public void InteractionPlayer()
    {
        //Interacktion wird ausgelöst wenn E gedrückt wird
        if (Input.GetKeyDown(KeyCode.E))
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


    public void LoadEnemies()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            string enemyNameToSpawn = "Goblin";
            //SpawnEnemyCharacter(enemyNameToSpawn);

            bool enemyStatus = spawnManager.LoadEnemyStatus(enemyNameToSpawn);
            Debug.Log("Enemy " + enemyNameToSpawn + " status: " + enemyStatus);

            if (enemyStatus)
            {
                spawnManager.SpawnEnemyCharacter(enemyNameToSpawn);
            }
        }
    }

    public void LoadLevel()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            PlayerPosition.SavePosition(this.transform.position);
            Debug.Log(PlayerPosition.GetPosition());
            LevelLoader.instance.LoadSpanLevel("SampleScene", true);
        }
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

    public void FireAttacks(float duration)
    {
        ability.affektedByAbility = true;
        if (ability.affektedByAbility)
        {
            ability.fireAbility = true;
        }
        StartCoroutine(ReturnToNormalAbility(duration));
    }

    public void IceAttacks(float duration)
    {
        ability.affektedByAbility = true;
        if(ability.affektedByAbility)
        {
            ability.iceAbility = true;
        }
        StartCoroutine(ReturnToNormalAbility(duration));
    }

    IEnumerator ReturnToNormalAbility(float duration)
    {
        yield return new WaitForSeconds(duration);
        ability.affektedByAbility = false;
        ability.iceAbility = false;
        ability.fireAbility = false;
    }
}
