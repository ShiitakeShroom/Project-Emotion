using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class OverworldControllerPlayer : MonoBehaviour
{
    [Header("Basic Stats Overworld")]
    public float moveSpeedOw = 10f;
    private Player_Main playerMain;
    private Vector3 moveDir;
    private Vector3 lastMoveDir;
    public Rigidbody rb;
    private GameObject Player;
    public StatusHUDSliderPlayer statusHUDPlayer;
    public CharacterStatus playerStatus;
    public Animator animator;

    [SerializeField] private AbilityHolder _holder;
    [SerializeField] private AbilityHolder holderProjectil;
    // Start is called before the first frame update
    void Start()
    {
        statusHUDPlayer = FindObjectOfType<StatusHUDSliderPlayer>();
        animator = GetComponent<Animator>();
        GameObject Player = this.GameObject();
        rb = GetComponent<Rigidbody>();
        statusHUDPlayer.SetStatusHUD(playerStatus);
    }

    private void Awake()
    {
        Debug.Log($"OW");
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _holder.TriggerAbility();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            holderProjectil.TriggerAbility();
        }
    }

    private void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Bewegung des Spielers
        //rb.velocity = new Vector3(moveX * moveSpeedOw, rb.velocity.y, moveZ * moveSpeedOw);
        moveDir = new Vector3(moveX, 0, moveZ).normalized;
    }

    private void FixedUpdate()
    {
       rb.velocity = moveDir * moveSpeedOw;
    }
}
