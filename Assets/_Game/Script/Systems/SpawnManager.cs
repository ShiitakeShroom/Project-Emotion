using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    public EnemyCharacterStatus enemyCharacterStatus;
    public Transform[] spawnPoint;


    private void Start()
    {
        /*string enemyNameToSpawn = "Goblin";
        //SpawnEnemyCharacter(enemyNameToSpawn);

        bool enemyStatus = LoadEnemyStatus(enemyNameToSpawn);
        Debug.Log("Enemy " + enemyNameToSpawn + " status: " + enemyStatus);

        if (enemyStatus)
        {
            SpawnEnemyCharacter(enemyNameToSpawn);
        }
        */
    }

    public void SpawnEnemyCharacter(string enemyName)
    {
        EnemyCharacterStatus.EnemyInfo selecteEnemy = FindEnemyByName(enemyName);
        if (selecteEnemy != null)
        {
            foreach (Transform spawnPoints in spawnPoint)
            {
                GameObject spawnedEnemy = enemyCharacterStatus.enemies[0].enemyPrefab;
                Instantiate(selecteEnemy.enemyPrefab, spawnPoints.position, Quaternion.identity);

                //Speichert den Staus des gespawnten Gegners in PlayerPref
                SafeEnemyStatus(enemyName, true); // true steht für Aktiviert
            }
        }
        else
        {
            Debug.Log("Enemy not fount : " + enemyName);
        }
    }

    public EnemyCharacterStatus.EnemyInfo FindEnemyByName(string name)
    {
        foreach(var enemy in enemyCharacterStatus.enemies)
        {
            if(enemy.enemyName == name)
            {
                return enemy;
            } 
        }
        return null;
    }

    public void SafeEnemyStatus(string enemyName, bool isActive)
    {
        int statusValue = isActive ? 1 : 0;
        PlayerPrefs.SetInt("EnemyStatus" + enemyName, statusValue);
        PlayerPrefs.Save();
    }

    public bool LoadEnemyStatus(string enemyName)
    {
        //der gegner der den Statusvalue 0 hat wird bekommt den StatusValue 1
        int statusValue = PlayerPrefs.GetInt("EnemyStatus" + enemyName, 0);
        return statusValue == 1;
    }

    public void DeactivateEnemy(string enemyName)
    {
        SafeEnemyStatus(enemyName, false);
    }
}
