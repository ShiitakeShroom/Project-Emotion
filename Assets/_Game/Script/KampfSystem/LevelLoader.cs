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


    public Transform[] enemiesTransform;
    public Animator transition;
    public float transitionTime = 3f;
    public bool playerWins;
    public bool spawnLoader;

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
        
        StartCoroutine(LoadOverworldLevel(levelName));
    }

    IEnumerator LoadOverworldLevel(string levelName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelName);

        Debug.Log("Please;(");

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

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelName);

        transition.SetTrigger("End");
    }
}
