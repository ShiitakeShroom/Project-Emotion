using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

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

    public void SetCharacterState(CharacterStates newState) => CurrentCharacterStates = newState;

    public enum Scenes
    {
        SampleScene,
        BattleArena
    }

    [SerializeField] private Scenes _scenes = Scenes.SampleScene;

    public Scenes CurrentScene
    {
        get => _scenes; 
        private set => _scenes = value;
    }

    public void SetSceneState(Scenes newSceneState) => CurrentScene = newSceneState;
}
