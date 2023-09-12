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
    public bool isTouched = false;
    private SphereCollider sphere;

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTouched = true;
        }
    }

    public void Start()
    {
        sphere = GetComponent<SphereCollider>();
        //Das GAmeobjekt wird in der im event erwähnt und das event ausgeführt
        LevelLoader.instance.overworldChange += OverworldChange;
        LevelLoader.instance.battleChange += Instance_battleChange;
    }

    private void Instance_battleChange(object sender, EventArgs e)
    {
        //Sphere wird disabled because rechenleistung oder so
        sphere.enabled = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        //wenn das GameObject zerstört werden die Events nicht mehr ausgeführt., da es sonst zu einem Fehler kommt beim Laden der jeweiligen Szene
        LevelLoader.instance.overworldChange -= OverworldChange;
        LevelLoader.instance.battleChange -= Instance_battleChange;
    }

    void OverworldChange(object sender, EventArgs e)
    {
        if (isTouched)
        {
            //this means, dass nur das Gameobjekt zerstört wird mitden man interagiert hat
            Destroy(this.gameObject);
        }
        else
        {
            sphere.enabled = true;
            //beim laden in die OverworldScene wird das child im Gameobjekt auf active gestellt
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}

