using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatusManager : MonoBehaviour
{
    //Singelton script 
    private static CharacterStatusManager _instance;
    public static CharacterStatusManager Instance { get { return _instance; } }

    //Die N�tigen Status der Characktere werden hier gespeichert
    public CharacterStatus enemyCharacterStatus; 
    public CharacterStatus playerCharacterStatus;

    public static bool isInitialized = false;

    private void Awake()
    {       
        //Singelton Script das es maximal einmal ausgef�hrt wird 
        if(_instance != this && _instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        if (!isInitialized)
        {
            isInitialized = true;
            playerCharacterStatus.isHealedMax = false;
        }
        else
        {
            playerCharacterStatus.isHealedMax = true;
        }
    }
}
