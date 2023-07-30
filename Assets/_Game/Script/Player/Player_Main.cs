using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Main : MonoBehaviour
{   //Händelt PlayerClass references
    // Start is called before the first frame update
    public Player_Base Player;
    //public PlayerMovmentHandler PlayerMovmentHandler { get; private set; }
    public Rigidbody Rigidbody { get; private set; }



    private void Awake()
    {
        Player = GetComponent<Player_Base>();
       // PlayerMovmentHandler = GetComponent<PlayerMovementHandler>();
        Rigidbody = GetComponent<Rigidbody>();
    }
}
