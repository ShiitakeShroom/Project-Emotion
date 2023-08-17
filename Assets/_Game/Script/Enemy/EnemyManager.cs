using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    private Vector3[] enemyPositions;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SafeEnemiePosition(Transform[] enemies)
    {
        enemyPositions = new Vector3[enemies.Length];
        for(int i = 0; i < enemies.Length; i++) 
        { 
            enemyPositions[i] = enemies[i].position; 
        }
    }

    public void GetEnemyPosition(Transform[] enemies)
    {   
        if(enemyPositions == null && enemyPositions.Length != enemyPositions.Length) 
        { 
            return;
        }
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].position = enemyPositions[i];
        }
    }
}

