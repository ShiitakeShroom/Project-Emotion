using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDataBaseManager : MonoBehaviour
{
    public static EnemyDataBaseManager Instance { get; private set; }
    public EnemyDatabase enemyDatabase = new EnemyDatabase();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveDefeatedEnemy(string enemyID)
    {
        enemyDatabase.AddDefeatedEniemes(enemyID);
        SaveEnemyDatabase();
    }

    public bool IsEnemyDefeated(string enemyID)
    {
        return enemyDatabase.IsEnemyDefeated(enemyID);
    }

    public void ClearDefeatedEnemies()
    {
        enemyDatabase.ClearDefeatedEnemies();
        SaveEnemyDatabase();
    }

    public void SaveSpawnedEnemies(string enemyID)
    {
        enemyDatabase.AddSpawnedEniemes(enemyID);
        SaveEnemyDatabase();
    }

    public bool IsEnemySpawned(string enemyID)
    {
        return enemyDatabase.IsEnemySpawned(enemyID);
    }

    public void ClearSpawnedEnemies()
    {
        enemyDatabase.ClearSpawnedEnemies();
        SaveEnemyDatabase();
    }

    public void SaveEnemyDatabase()
    {
        string serializedData = JsonUtility.ToJson(enemyDatabase);
        PlayerPrefs.SetString("EnemyDatabase", serializedData);
        PlayerPrefs.Save();
    }

    public void LoadEnemyDatabase()
    {
        if (PlayerPrefs.HasKey("EnemyDatabase"))
        {
            string serializedData = PlayerPrefs.GetString("EnemyDatabase");
            enemyDatabase = JsonUtility.FromJson<EnemyDatabase>(serializedData);
        }
    }

}
