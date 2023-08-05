using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatusManager : MonoBehaviour
{
    //Singelton script 
    private static CharacterStatusManager _instance;
    public static CharacterStatusManager Instance { get { return _instance; } }

    //Die Nötigen Status der Characktere werden hier gespeichert
    public CharacterStatus enemyCharacterStatus; 
    public CharacterStatus playerCharacterStatus;

    private void Awake()
    {       
        //Singelton Script das es maximal einmal ausgeführt wird 
        if(_instance != this && _instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
