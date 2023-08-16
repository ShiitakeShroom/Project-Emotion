using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyLoader : MonoBehaviour
{
    EnemyLoader enemyLoader;

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {   
            LevelLoader.instance.overworldChange += OverworldChange;
    }
    private void OnDestroy()
    {
        LevelLoader.instance.overworldChange -= OverworldChange;
    }

    void OverworldChange(object sender, EventArgs e)
    {
        UnityEngine.Debug.Log("Fuck");
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

}

