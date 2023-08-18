using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattleMovementAi : MonoBehaviour
{
    public float moveSpeed = 3f;
    public BattleSystemManager battleSystemManager;

    public Transform[] wayPoints;
    public LayerMask whatsStopsMovement;

    private int currentWayPointIndex = 0;
    private Transform movePoint;
   
    // Start is called before the first frame update
    void Start()
    {
        battleSystemManager = FindObjectOfType<BattleSystemManager>();
        movePoint = new GameObject("MovePoint").transform;
        movePoint.position = transform.position;
        
        wayPoints = battleSystemManager.spawnPoints.ToArray();

        MoveToNextWayPoint();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f)
        {
            MoveToNextWayPoint();
        }
    }

    public void MoveToNextWayPoint()
    {
        if(wayPoints.Length == 0)
        {
            Debug.LogWarningFormat("EnemyAI: no wayPoints defined!");
            return;
        }

        currentWayPointIndex = (currentWayPointIndex + 1) % wayPoints.Length;
        Vector3 nextWayPoint = wayPoints[currentWayPointIndex].position;

        if(!Physics.CheckSphere(nextWayPoint, 0.2f, whatsStopsMovement))
        {
            movePoint.position = nextWayPoint;
        }
    }
}

