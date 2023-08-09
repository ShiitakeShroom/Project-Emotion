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


    public void ReturnToOverWorld(string levelName, bool victory)
    {
        playerWins = victory;
        StartCoroutine(LoadOverworldLevel(levelName));
    }

    IEnumerator LoadOverworldLevel(string levelName)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);

        //Den Gespeicherte Position wiedergeben

        SceneManager.LoadScene(levelName);

        transition.SetTrigger("End");
    }
}
