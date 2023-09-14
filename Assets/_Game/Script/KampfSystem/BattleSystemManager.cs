using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BattleSystemManager : MonoBehaviour
{
    public enum BattleState { Start, Battle, Pause, Win, Lose }
    private CharacterStatusManager characterStatusManager;

    //SpawnSystem
    public List<CharacterStatus> enemyStatus;
    public List<Transform> spawnPoints;
    public CharacterStatus selectedEnemyStatus;

    public PlayerHealth playerHealth;
    public EnemyHealth enemyHealthChange;

    public CharacterStatus playerStatus;

    [Header("Scripts")]
    private EnemyBattleMovementAi enemyBattleMovementAi;
    private BattleControllerPlayer battleControllerPlayer;

    private GameObject player;
    private GameObject enemy;

    public Transform playerTransform;
    public Transform enemyTransform;

    public StatusHUDSliderPlayer playerStatusHUD;
    public StatusHUDSilderEnemy enemyStatusHUD;

    private BattleState battleState;

    public void Start()
    {
        //Enemy
        battleState = BattleState.Start;
        StartCoroutine(BeginBattle());

        battleControllerPlayer = FindObjectOfType<BattleControllerPlayer>();
        enemyBattleMovementAi = FindObjectOfType<EnemyBattleMovementAi>();
    }



    IEnumerator BeginBattle()
    {
        //Spawn Characters
        //zufälliger EnemyStatus ausgewählt aus der List
        int randomIndex = UnityEngine.Random.Range(0, enemyStatus.Count);
        selectedEnemyStatus = enemyStatus[randomIndex];
        Debug.Log("Enemy is" + selectedEnemyStatus.name);

        SpawnEnemies();

        //enemy = Instantiate(enemyStatus.characterGameObject.transform.GetChild(0).gameObject, enemyTransform); enemy.SetActive(true);
        player = Instantiate(playerStatus.characterGameObject.transform.GetChild(0).gameObject, playerTransform); player.SetActive(true);


        //Sprites Invisible in the Beginning 
        enemy.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);

        //Set the Character Stats in the HUD
        playerStatusHUD.SetStatusHUD(playerStatus);
        Debug.Log("Playerhealth" + playerHealth.GetPlayerHelath());

        enemyStatusHUD.SetStatusHUD(selectedEnemyStatus);
        Debug.Log("Enemy health" + selectedEnemyStatus.health);
        Debug.Log(selectedEnemyStatus.name);

        yield return new WaitForSeconds(1);

        // fade in our Characters sprites
        yield return StartCoroutine(FadeInOpponents());

        yield return new WaitForSeconds(1.5f);

        //start Battle
        StartCoroutine(ManageBattle());
    }

    public void SpawnEnemies()
    {
        //Mische die List der SpawnPoints
        spawnPoints.Shuffle();

        //wähle zufällig einen SpawnPoint aus
        Transform selectedSpawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)];
        Debug.Log(selectedSpawnPoint);
        //Spüawend den zufällig ausgewählten Gegner an der Spawn position
        enemy = Instantiate(selectedEnemyStatus.characterGameObject.transform.GetChild(0).gameObject, selectedSpawnPoint.position, Quaternion.identity); enemy.SetActive(true);
        enemyStatusHUD.tragetEnemy = enemy.transform;
    }

    IEnumerator FadeInOpponents(int steps = 10)
    {
        float totalTransparencyPerStep = 1 / (float)steps;

        for (int i = 0; i < steps; i++)
        {
            StepSpriteOpacity(player, totalTransparencyPerStep);
            StepSpriteOpacity(enemy, totalTransparencyPerStep);
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void StepSpriteOpacity(GameObject ob, float transPerStep)
    {
        Color currColor = ob.GetComponent<SpriteRenderer>().color;
        float alpha = currColor.a;
        alpha += transPerStep;
        ob.GetComponent<SpriteRenderer>().color = new Color(currColor.r, currColor.g, currColor.b, alpha);
    }

    IEnumerator ManageBattle()
    {
        battleState = BattleState.Battle;
        Debug.Log("its a " + battleState);

        battleControllerPlayer.enabled = true;
        enemyBattleMovementAi.enabled = true;

        while (battleState == BattleState.Battle)
        {   //Pausieren des Kampfes
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                StartCoroutine(PauseBattle());
            }

            if (playerStatus.health <= 0)
            {
                Debug.Log("Player is Dead");
                battleState = BattleState.Lose;
                StartCoroutine(EndBattle());
            }

            if (selectedEnemyStatus.health <= 0)
            {
                Debug.Log("UWU there is Damage");
                battleState = BattleState.Win;
                StartCoroutine(EndBattle());
            }

            yield return null;//Warten auf die nächste Frame aktualisierung 
        }

        IEnumerator PauseBattle()
        {
            Debug.Log("Time is Frozen");
            Time.timeScale = 0.25f;
            yield return new WaitForSeconds(2f);
            Time.timeScale = 1f;
            Debug.Log("Return Battle");
            StartCoroutine(ManageBattle());
        }


        IEnumerator EndBattle()
        {

            //Check if won
            if (battleState == BattleState.Win)
            {
                // you may wish to display some kind
                // of message or play a victory fanfare
                // here
                LevelLoader.instance.playerWins = true;

                //Soll gegner in der Overworld deaktivieren
                /*if (enemyStatus.characterGameObject != null)
                {
                    enemyStatus.characterGameObject.SetActive(false);

                    if(enemyStatus.characterGameObject != null)
                    {
                        Debug.Log("Es ist weg");
                    }
                }*/

                yield return new WaitForSeconds(1);
                Debug.Log("Back to Level 1");
                LevelLoader.instance.ReturnToOverWorld("SampleScene");
            }

            else if (battleState == BattleState.Lose)
            {
                Debug.Log("Fatality! JErry Wins");
                LevelLoader.instance.playerWins = false;
                yield return new WaitForSeconds(1);
                LevelLoader.instance.ReturnToOverWorld("SampleScene");
            }
        }
    }
}

//player = Instantiate(playerStatus.characterGameObject.transform.GetChild(0).gameObject, playerTransform); player.SetActive(true)
