using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyControll : MonoBehaviour
{
    public Transform parentTransform;

    public void Start()
    {
        LevelLoader.instance.battleChange += BattleArenaChange;

    }

    void BattleArenaChange(object sender, EventArgs e)
    {
        if (parentTransform) { 
            UnityEngine.Debug.Log("Fuckyou");
            gameObject.SetActive(false);
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(DestroyParent());
        }
    }

    IEnumerator DestroyParent()
    {
        Debug.Log("start courururururu");

        yield return new WaitForSeconds(0.8f);

        Destroy(parentTransform.gameObject);

        Debug.Log("ende");
    }
}
