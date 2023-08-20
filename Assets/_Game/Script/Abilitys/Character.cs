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

}
