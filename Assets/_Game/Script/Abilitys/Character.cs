using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    public enum CharacterStates
    {
        Idle,
        Walking,
        Patroling,
        Attacking,
        Dead,
        UsingSpell
    }


    [SerializeField] private CharacterStates _currentCharacterState = CharacterStates.Idle;

    public CharacterStates CurrentCharacterStates
    {
        get => _currentCharacterState;
        private set => _currentCharacterState = value;
    }

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Hier kannst du �berpr�fen, in welcher Szene der Spieler sich befindet und den Zustand entsprechend anpassen.
        if (scene.name == "OverworldScene")
        {
            SetCharacterState(CharacterStates.UsingSpell);
        }
        else if (scene.name == "BattleScene")
        {
            SetCharacterState(CharacterStates.Attacking);
        }
        else
        {
            SetCharacterState(CharacterStates.Idle); // Standardzustand f�r andere Szenen
        }
    }

    public void SetCharacterState(CharacterStates newState)
    {
        _currentCharacterState = newState;
        // Hier kannst du zus�tzliche Aktionen ausf�hren, die mit dem neuen Zustand verbunden sind.
    }
}

