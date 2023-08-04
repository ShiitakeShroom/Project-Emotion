using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BattleSystemManager : MonoBehaviour
{
    public enum BattleState { Start, Battle, Pause, Win, Lose }

    public PlayerStatus playerHealth;
    public EnemyHealth enemyHealthChange;

    public CharacterStatus playerStatus;
    public CharacterStatus enemyStatus;

    private GameObject player;
    private GameObject enemy;

    public Transform playerTransform;
    public Transform enemyTransform;

    public StatusHUD playerStatusHUD;
    public StatusHUD enemyStatusHUD;

    private BattleState battleState;



    public void Start()
    {   //Player
        playerHealth.ValueHealthChanged += ValueHealthChanged;
        playerHealth.OnDeath += OnDeath;
        //Enemy
        enemyHealthChange.ValueHealthEnemyChanged += EnemyHPChange;
        enemyHealthChange.OnDeath += EnemyDeath;
        battleState = BattleState.Start;
        StartCoroutine(BeginBattle());
    }

    IEnumerator BeginBattle()
    {
        //Spawn Characters
        enemy = Instantiate(enemyStatus.characterGameObject.transform.GetChild(0).gameObject, enemyTransform); enemy.SetActive(true);
        player = Instantiate(playerStatus.characterGameObject.transform.GetChild(0).gameObject, playerTransform); player.SetActive(true);


        //Sprites Invisible in the Beginning 
        enemy.GetComponent<SpriteRenderer>().color = new Color(1 ,1 , 1 , 0);
        player.GetComponent<SpriteRenderer>().color = new Color(01 ,01 ,01 ,0);

        //Set the Character Stats in the HUD
        playerStatusHUD.SetStatusHUD(playerStatus);
        enemyStatusHUD.SetStatusHUD(enemyStatus);

        yield return new WaitForSeconds(1);

        // fade in our Characters sprites
        yield return StartCoroutine(FadeInOpponents());

        yield return new WaitForSeconds(2);

        //start Battle
        battleState = BattleState.Battle;
    }

    IEnumerator FadeInOpponents(int steps = 10)
    {
        float totalTransparencyPerStep = 1 / (float) steps;

        for(int i = 0; i < steps; i++)
        {
            steSpriteOpacity(player, totalTransparencyPerStep);
            steSpriteOpacity(enemy, totalTransparencyPerStep);
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void steSpriteOpacity(GameObject ob, float transPerStep)
    {
        Color currColor = ob.GetComponent<SpriteRenderer>().color;
        float alpha = currColor.a;
        alpha += transPerStep;
        ob.GetComponent<SpriteRenderer>().color = new Color(currColor.r, currColor.g, currColor.b, alpha);
    }

    IEnumerator Battle()
    {
        if(enemyStatus.health <= 0 && playerStatus.health > 0)
        {
            battleState = BattleState.Win;
            yield return StartCoroutine(EndBattle());
        }
        else if(enemyStatus.health > 0 && playerStatus.health <= 0)
        {
            battleState = BattleState.Lose;
            yield return StartCoroutine(EndBattle());
        }
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
            LevelLoader.instance.LoadLevel("SampleScene");
        }

        else if(battleState == BattleState.Lose)    
        {
            yield return new WaitForSeconds(1);
            LevelLoader.instance.LoadLevel("SampleScene");
        }
    }

    public void ValueHealthChanged(object sender, PlayerStatus.HealthChangeEventArgs e)
    {
        playerStatusHUD.SetHP(playerStatus, e.amount);
        Debug.Log("Damge" + e.amount);
    }

    public void EnemyHPChange(object sender, EnemyHealth.HealthChangeEnemyEventArgs e)
    {
        enemyStatusHUD.SetHP(enemyStatus, e.amount);
    }

    public void OnDeath(object sender, EventArgs e)
    {
        battleState = BattleState.Lose;
        StartCoroutine(EndBattle());
    }

    public void EnemyDeath(object sender, EventArgs e)
    {
        battleState = BattleState.Win;
        StartCoroutine(EndBattle());
    }
}


//player = Instantiate(playerStatus.characterGameObject.transform.GetChild(0).gameObject, playerTransform); player.SetActive(true)
