using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

        playerHealth.ValueHealthChanged += ValueHealthChanged;
        characterStatusManager = CharacterStatusManager.Instance;
        enemyStatus = characterStatusManager.enemyCharacterStatus;
        //Enemy
        enemyHealthChange.ValueHealthEnemyChanged += EnemyHPChange;
        enemyHealthChange.OnDeath += DeathEnemy;
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
        battleState = BattleState.Battle;
        Debug.Log("its a " + battleState);
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
            LevelLoader.instance.LoadLevel("SampleScene");
        }

        else if (battleState == BattleState.Lose)
        {
            Debug.Log("Fatality! JErry Wins");

            yield return new WaitForSeconds(1);
            LevelLoader.instance.LoadLevel("SampleScene");
        }
    }

    public void ValueHealthChanged(object sender, PlayerHealth.HealthChangeEventArgs e)
    {

    }

    public void EnemyHPChange(object sender, EnemyHealth.HealthChangeEnemyEventArgs e)
    {

    }


    public void DeathEnemy(object sender, EventArgs e)
    {
        Debug.Log("Enemy is under zero");
            battleState = BattleState.Win;
            StartCoroutine(EndBattle());
    }
}

//player = Instantiate(playerStatus.characterGameObject.transform.GetChild(0).gameObject, playerTransform); player.SetActive(true)
