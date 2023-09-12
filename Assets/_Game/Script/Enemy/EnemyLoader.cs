using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

//Script existiert um das Prefab beim Laden zu deaktivieren und zu zerstören 
public class EnemyLoader : MonoBehaviour
{
    EnemyLoader enemyLoader;

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        //Das GAmeobjekt wird in der im event erwähnt und das event ausgeführt
        LevelLoader.instance.overworldChange += OverworldChange;
        LevelLoader.instance.battleChange += Instance_battleChange;
    }

    private void Instance_battleChange(object sender, EventArgs e)
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        //wenn das GameObject zerstört wird wird das Event nicht mehr ausgeführt
        LevelLoader.instance.overworldChange -= OverworldChange;
    }

    void OverworldChange(object sender, EventArgs e)
    {
        //beim laden in die nOverworldScene wird das child im Gameobjekt auf active gestellt
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
}

