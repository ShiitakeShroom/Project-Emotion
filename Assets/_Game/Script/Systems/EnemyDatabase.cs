using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyDatabase
{
    public List<string> defeatedEnemies = new List<string>();
    public List<string> spawnedEnemies = new List<string>();

    public void AddDefeatedEniemes(string enemyID)
    {
        if(!defeatedEnemies.Contains(enemyID))
        {
            defeatedEnemies.Add(enemyID);
        }
    }

    public bool IsEnemyDefeated(string enemyID)
    {
        return defeatedEnemies.Contains(enemyID);
    }

    public void AddSpawnedEniemes(string enemyID)
    {
        if (!spawnedEnemies.Contains(enemyID))
        {
            spawnedEnemies.Add(enemyID);
        }
    }

    public void MarkSpawnedEnmeyDestroyed(string enemyID)
    {
        if (spawnedEnemies.Contains(enemyID))
        {
            spawnedEnemies.Remove(enemyID);
        }
    }

    public void ClearDefeatedEnemies()
    {
        defeatedEnemies.Clear();
    }

    public bool IsEnemySpawned(string enemyID)
    {
        return spawnedEnemies.Contains(enemyID);
    }

    public void ClearSpawnedEnemies()
    {
        spawnedEnemies.Clear();
    }
}
