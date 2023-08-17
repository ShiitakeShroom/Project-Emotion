using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyScriptSpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public List<Transform> spawnPoints = new List<Transform>();

    private void Start()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            if (!EnemyDataBaseManager.Instance.IsEnemyDefeated(enemyPrefab.name))
            {
                GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            }
        }
    }

    public void OnCombatSceneExit()
    {
        Debug.Log("Get Psoition");

        foreach (Transform spawnPoint in spawnPoints)
        {
            Debug.Log("Get Psoition");

            if (!EnemyDataBaseManager.Instance.IsEnemyDefeated(enemyPrefab.name) && !EnemyDataBaseManager.Instance.IsEnemySpawned(enemyPrefab.name))
            {
                Debug.Log("Get Psoition");

                Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            }
        }
    }
}

