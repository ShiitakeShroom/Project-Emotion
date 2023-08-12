using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;


public class LevelLoader : MonoBehaviour
{
    #region Singelton
    public static LevelLoader instance;
    public CharacterStatus charaStatus;
    public Animator transition;
    public float transitionTime = 3f;
    public bool playerWins;
    public bool spawnLoader;
    public EnemySpawnManager enemySpawnManager;

    private void Awake()
    {   
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    #endregion
    #region BattleLaden
    public void LoadLevel(string levelName)
    {
        StartCoroutine(LoadNamedLevel(levelName));
    }

    IEnumerator LoadNamedLevel(string levelName)
    {

        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelName);
        
        transition.SetTrigger("End");
    }
    #endregion

    #region Win or Lose Loader
    public void ReturnToOverWorld(string levelName, bool victory)
    {
        playerWins = true;
        
        if(playerWins && charaStatus != null && charaStatus.health > 0 /*&& !DestroyObjectTracker.IsDestroyed(charaStatus.characterGameObject)*/)
        {
            StartCoroutine(LoadOverworldLevel(levelName));
        }
        else
        {
            StartCoroutine(LoadOverworldWithoutBattle(levelName));
        }

    }

    IEnumerator LoadOverworldLevel(string levelName)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelName);

        transition.SetTrigger("End");
    }

    IEnumerator LoadOverworldWithoutBattle(string levelName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelName);

        transition.SetTrigger("End");
    }
    #endregion
    public void LoadSpanLevel(string levelName, bool spawn)
    {
            StartCoroutine(LoadSpawnLevel(levelName));
    }

    IEnumerator LoadSpawnLevel(string levelName)
    {
        transition.SetTrigger("Start");

        ResetRespawn();

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelName);

        transition.SetTrigger("End");
    }


    private void ResetRespawn()
    {
        if(enemySpawnManager != null)
        {
            enemySpawnManager.SpawnEnemyReset();
        }
    }
}
