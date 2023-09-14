using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class BattleControllerPlayer : MonoBehaviour
{
    public static BattleControllerPlayer Instance;

    //basic infos über Speed, der bewegungspunkt und wieviel spielraum zw. dem Sprite und der Punkt sein sollte
    [Header("Basis Stats Battle")]
    public float moveSpeedBattle = 5f;
    public Character owner;

    [Header("AreaMovePoints")]
    public Transform movePoint;
    public float jumpTime = 0.05f;
    private Vector3 targetPosition;

    [Header("PlayerAbilitys")]
    public AbilityHolder _holderArea;
    public AbilityHolder holderProjectil;
    public Animator animator;


    //Collider ansprache
    [Header("Movement")]
    public LayerMask whatStopsMovement;

    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {   
        owner = GetComponent<Character>();
        animator = GetComponent<Animator>();
        SetMovePointParentToNull();
        owner.SetCharacterState(Character.CharacterStates.Attacking);
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Space))
        {
            _holderArea.TriggerAbility();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            holderProjectil.TriggerAbility();
        }

        Movement();
    }

    public void Movement()
    {
        //Player Sprite bewegt sich zu movePoint/ der movePoint wird Accelerated durch den moveSpeed
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeedBattle * Time.deltaTime);

        //GridMovement  	^^

        if (Vector3.Distance(transform.position, movePoint.position) <= jumpTime)
        {
            {
                //Check if we fully to one direction/ Controller
                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
                {   //CheckSphere(Position + bewgungsrichtung auf der XAchse)
                    if (!Physics.CheckSphere(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), 0.2f, whatStopsMovement))
                    {
                        movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                    }
                }
                else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f) //Verhindert diagonale Bewegung ^^
                {
                    if (!Physics.CheckSphere(movePoint.position + new Vector3(0f, 0f, Input.GetAxisRaw("Vertical")), 0.2f, whatStopsMovement))
                    {
                        movePoint.position += new Vector3(0f, 0f, Input.GetAxisRaw("Vertical"));
                    }
                }
            }
        }
    }

    public void SetMovePointParentToNull()
    {
        movePoint.parent = null;
    }
}
