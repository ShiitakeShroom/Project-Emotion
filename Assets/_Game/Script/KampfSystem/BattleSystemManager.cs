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

    public PlayerHealth playerHealth;
    public EnemyHealth enemyHealthChange;

    public CharacterStatus playerStatus;
    public CharacterStatus enemyStatus;

    private GameObject player;
    private GameObject enemy;

    public Transform playerTransform;
    public Transform enemyTransform;

    public StatusHUDSliderPlayer playerStatusHUD;
    public StatusHUDSilderEnemy enemyStatusHUD;

    private BattleState battleState;

    public void Start()
    {   //Player
        characterStatusManager = CharacterStatusManager.Instance;
        enemyStatus = characterStatusManager.enemyCharacterStatus;
        

        //Enemy
        battleState = BattleState.Start;
        StartCoroutine(BeginBattle());
    }



    IEnumerator BeginBattle()
    {
        //Spawn Characters
        enemy = Instantiate(enemyStatus.characterGameObject.transform.GetChild(0).gameObject, enemyTransform); enemy.SetActive(true);
        player = Instantiate(playerStatus.characterGameObject.transform.GetChild(0).gameObject, playerTransform); player.SetActive(true);


        //Sprites Invisible in the Beginning 
        enemy.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);

        //Set the Character Stats in the HUD
        playerStatusHUD.SetStatusHUD(playerStatus);
        Debug.Log("Playerhealth" + playerHealth.GetPlayerHelath());
        enemyStatusHUD.SetStatusHUD(enemyStatus);
        Debug.Log("Enemy health" + enemyStatus.health);

        yield return new WaitForSeconds(1);

        // fade in our Characters sprites
        yield return StartCoroutine(FadeInOpponents());

        yield return new WaitForSeconds(1.5f);

        //start Battle
        StartCoroutine(ManageBattle());
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

            if (enemyStatus.health <= 0)
            {
                Debug.Log("UWU there is Damage");
                battleState = BattleState.Win;
                StartCoroutine(EndBattle());
            }

            yield return null;//Warten auf die n�chste Frame aktualisierung 
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
                yield return new WaitForSeconds(1);
                Debug.Log("Back to Level 1");
                LevelLoader.instance.ReturnToOverWorld("SampleScene", true);
            }

            else if (battleState == BattleState.Lose)
            {
                Debug.Log("Fatality! JErry Wins");

                yield return new WaitForSeconds(1);
                LevelLoader.instance.ReturnToOverWorld("SampleScene", true);
            }
        }
    }
}

//player = Instantiate(playerStatus.characterGameObject.transform.GetChild(0).gameObject, playerTransform); player.SetActive(true)
