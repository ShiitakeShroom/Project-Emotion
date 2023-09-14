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
        // Hier kannst du überprüfen, in welcher Szene der Spieler sich befindet und den Zustand entsprechend anpassen.
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
            SetCharacterState(CharacterStates.Idle); // Standardzustand für andere Szenen
        }
    }

    public void SetCharacterState(CharacterStates newState)
    {
        _currentCharacterState = newState;
        // Hier kannst du zusätzliche Aktionen ausführen, die mit dem neuen Zustand verbunden sind.
    }
}

