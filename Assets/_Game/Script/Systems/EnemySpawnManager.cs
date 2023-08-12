using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySpawnInfo 
{ 
    public GameObject enemyStatus;
    public Transform spawnPoint;
}
public class EnemySpawnManager : MonoBehaviour
{
    public List<EnemySpawnInfo> spawnInfoList = new List<EnemySpawnInfo>();
    // Start is called before the first frame update
    public void Start()
    {
        SpawnEnemyStart();
    }

    public void SpawnEnemyStart()
    {
        foreach (var spawnInfo in spawnInfoList) 
        { 
            Instantiate(spawnInfo.enemyStatus, spawnInfo.spawnPoint.position, spawnInfo.spawnPoint.rotation);
            //spawnInfo.enemyStatus.SetActive(true);
        }
    }

    public void SpawnEnemyReset()
    {
        foreach (var spawnInfo in spawnInfoList)
        {
            Instantiate(spawnInfo.enemyStatus, spawnInfo.spawnPoint.position, spawnInfo.spawnPoint.rotation);
            spawnInfo.enemyStatus.SetActive(true);
        }
    }
}
