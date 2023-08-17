using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "EnemyDatabase", menuName = "Game/Enemy Database")]
public class EnemyCharacterStatus : ScriptableObject
{
    [System.Serializable]
    public class EnemyInfo
    {
        public string enemyName;
        public GameObject enemyPrefab;
        //weiter eigenschaften wie HP, usw;
    }

    public EnemyInfo[] enemies;

    //zuzsätzliche Methoden um bestimmte gegner zu suchen usw.
}
