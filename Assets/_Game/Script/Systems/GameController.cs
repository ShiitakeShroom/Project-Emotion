using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : MonoBehaviour
{
    public static GameController Instance {get; private set;}
    public enum GameState
    {
        Overworld,
    }

    // Wechsel zwischen Kampf und Overworld
    public OverworldControllerPlayer overworldController;
    public GameState currentState;
    public Transform movePoint;
    private Transform originalParent;
     
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {   
        //originalParent = movePoint.parent;
        // Starte im Overworld-Zustand

        SwitchToOverworld();
    }

    private void Update()
    {
        // Zustandsüberprüfung und Zustandswechsellogik können hier eingefügt werden
        if (currentState == GameState.Overworld)
        {
            // Füge hier die Logik für den Overworld-Zustand hinzu
        }


        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab))
        {
            PauseGame();
        }

        if (Input.GetKeyDown(KeyCode.Space)){
            ResumeGame();
        }
    }

    private void SwitchToOverworld()
    {
        currentState = GameState.Overworld;
        Debug.Log("Overworld aktive");
        BindMovePointToParent();
    }

    public void UnBindMovePointFromParent()
    {
        movePoint.parent = null;
    }

    public void BindMovePointToParent()
    {
        movePoint.parent = originalParent;
    }

    public void PauseGame()
    {
        Time.timeScale = 0.0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; 
    }
}

