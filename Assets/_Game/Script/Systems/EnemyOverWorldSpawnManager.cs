using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class EnemyOverWorldSpawnManager : MonoBehaviour
{
    //List der gegner
    public Transform[] spawnPoints;
    public GameObject spawnEnemy;

    // Start is called before the first frame update
    public void Start()
    {
        LoadSpawnedEnemies();
        SpawnEnemyStart();
    }

    public void SpawnEnemyStart()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            if (!EnemyDataBaseManager.Instance.enemyDatabase.IsEnemyDefeated(spawnEnemy.name))
            {
                GameObject enemyInstance = Instantiate(spawnEnemy, spawnPoint.position, spawnPoint.rotation);
                AssignUniqueName(enemyInstance, spawnEnemy.name);
                EnemyDataBaseManager.Instance.enemyDatabase.AddSpawnedEniemes(enemyInstance.name);
            }
        }
        SaveSpawnedEnemies();
    }

    public void LoadSpawnedEnemies()
    {
        foreach(string enemyName in EnemyDataBaseManager.Instance.enemyDatabase.spawnedEnemies)
        {

            GameObject enemyPrefab = Resources.Load<GameObject>("EnemyOverworld");
            if (enemyPrefab != null)
            {
                GameObject enemyInstance = Instantiate(enemyPrefab);
            }
        }
    }

    private void OnDestroy()
    {
        UpdateSpawnedEnemies();
        SaveSpawnedEnemies();
    }

    private void UpdateSpawnedEnemies()
    {
        List<string> enemiesToRemove = new List<string>();

        foreach (var enemy in EnemyDataBaseManager.Instance.enemyDatabase.spawnedEnemies)
        {

            if (EnemyDataBaseManager.Instance.enemyDatabase.IsEnemyDefeated(enemy))
            {
                enemiesToRemove.Add(enemy);
            }
        }

        foreach (var enemyToRemove in enemiesToRemove)
        {

            EnemyDataBaseManager.Instance.enemyDatabase.MarkSpawnedEnmeyDestroyed(enemyToRemove);
        }
    }

    private void SaveSpawnedEnemies()
    {
        Debug.Log("fuck?");
        EnemyDataBaseManager.Instance.SaveEnemyDatabase();
    }


    public void RemoveEnemyFromList(GameObject enemy)
    {
        Debug.Log("damn?");
        Destroy(enemy);
        Debug.Log("nein?");
        EnemyDataBaseManager.Instance.SaveDefeatedEnemy(enemy.name);
    }

    public void AssignUniqueName(GameObject obj, string baseName) 
    {
        int index = 1;
        string uniqueName = baseName + "_" + index;

        while(GameObject.Find(uniqueName) != null)
        {
            index++;
            uniqueName = baseName + "_" + index;
        } 
        obj.name = uniqueName;
    }
}
