using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBattleMovementAi : MonoBehaviour
{
    /*public float moveSpeed = 3f;
    public BattleSystemManager battleSystemManager;

    public Transform[] arenaGrid;
    public LayerMask whatsStopsMovement;

    [Header("Raycast")]
    public float viewDistance = 10f;
    public float raycastDistance = 2f;
    public LayerMask obstacleMask;

    private int currentWayPointIndex = 0;
    private Transform movePoint;
    public Transform player;

    private Vector3 originalPosition;
    private bool hasSeenPlayer = false;
    /*private bool isReturningToPosition = false;

    private Vector3 orignialMovePoint;

    // Start is called before the first frame update
    void Start()
    {
        battleSystemManager = FindObjectOfType<BattleSystemManager>();
        movePoint = new GameObject("MovePoint").transform;
        movePoint.position = transform.position;
        /*
        wayPoints = battleSystemManager.spawnPoints.ToArray();
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
        originalPosition = transform.position;//saves original position
    }

    // Update is called once per frame
    void Update()
    {
        if (CanSeePlayer() == true)
        {
            hasSeenPlayer = true;//has seen player
            movePoint.position = player.position;
        }

        if(!hasSeenPlayer)
        {
            MoveToNextWayPoint();
        }

        Movement();
    }

    bool CanSeePlayer()
    {
        Vector3 leftDirection = -transform.right;//blickrichtung des Gegners
        Vector3 shortRaycast = transform.position + leftDirection * raycastDistance;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, leftDirection, out hit, viewDistance, obstacleMask))
        {
            if (hit.collider.CompareTag("Player"))
            {
                Debug.DrawLine(transform.position, player.position, Color.green);
                return true;// sieht den Spieler
            }
        }
        Debug.DrawLine(transform.position, player.position, Color.red);
        return false;//Sieht dn Spieler nicht 
    }

    private void RetunrToClosestPointToPlayer()
    {
        //if player wasnt seen
        if (!hasSeenPlayer)
        {
            hasSeenPlayer = false;
            isReturningToPosition = false;
            return;
        }

        Transform closestPoint = arenaGrid[0];
        float closestDistance = Vector3.Distance(player.position, arenaGrid[0].position);

        foreach (Transform point in arenaGrid)
        {
            float distance = Vector3.Distance(player.position, point.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPoint = point;
            }
        }

        //move enemy to the next point
        transform.position = closestPoint.position;

        isReturningToPosition = false;
    }


    private void JumpToNextPoint()
    {
        //ifPlayer was seen, setze die Bewegung zurück
        if (hasSeenPlayer)
        {
            hasSeenPlayer = false;
            return;
        }

        //Jump to next Point in the arena
        currentWayPointIndex = (currentWayPointIndex + 1) % arenaGrid.Length;
        Vector3 nextWayPoint = arenaGrid[currentWayPointIndex].position;

        movePoint.position = nextWayPoint;

        if (!hasSeenPlayer)
        {
            movePoint.position = orignialMovePoint;
        }
    }

    private void Movement()
    {
        //Look if the Object can move nichta uf dem hindernisLayer
        Vector3 movemntDirection = movePoint.position - transform.forward;
        Ray movementRay = new Ray(transform.position, movemntDirection);
        RaycastHit hitharder;

        if (Physics.Raycast(movementRay, out hitharder, movemntDirection.magnitude, whatsStopsMovement))
        {
            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        }
        if(Vector3.Distance(transform.position, movePoint.position) <= 0.05f)
        {
            MoveToNextWayPoint();
        }
    }

    public void MoveToNextWayPoint()
    {
        if (arenaGrid.Length == 0)
        {
            Debug.LogWarningFormat("EnemyAI: no wayPoints defined!");
            return;
        }

        currentWayPointIndex = (currentWayPointIndex + 1) % arenaGrid.Length;
        Vector3 nextWayPoint = arenaGrid[currentWayPointIndex].position;

        if (!Physics.CheckSphere(nextWayPoint, 0.2f, whatsStopsMovement))
        {
            movePoint.position = nextWayPoint;
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 rayStart = transform.position;
        Vector3 rayEnd = transform.position + -transform.right * raycastDistance;

        // Zeichne den kurzen Raycast im Editor
        Gizmos.color = Color.red;
        Gizmos.DrawLine(rayStart, rayEnd);
    }*/

    [Header("Movement")]
    public Transform[] gridArea;
    public float teleportDealy = 0.5f;//jumpverzögerung
    private Transform currentMovePoint;
    private bool isTeleporting = false;

    [Header("RayCast")]
    public float detectionRange = 5f;
    public float viewDistance = 5f;
    public Transform player;
    public LayerMask playerMask;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        //teleportiert den gegner zum ersten BewegungsFeld
        if (gridArea.Length > 0)
        {
            currentMovePoint = gridArea[0];
            transform.position = currentMovePoint.position;
            StartCoroutine(TeleportWithDelay());
        }
    }

    private void Update()
    {

        if (!isTeleporting && !CanSeePlayer())
        {
            StartCoroutine(TeleportWithDelay());
        }

        if (CanSeePlayer())
        {
            Vector3 nearstPointToPlayer = GetNearestPointToPlayer();
            TeleportToNearestMovePoint(nearstPointToPlayer);
            //do Something
        }
    }

    private IEnumerator TeleportWithDelay()
    {

        isTeleporting = true;

        yield return new WaitForSeconds(teleportDealy);

        TeleportToRandomAdjacentMovePoint();

        isTeleporting = false;
    }


    private bool CanSeePlayer()
    {
        Vector3 leftDirection = -transform.right;//blickrichtung des Gegners
        Vector3 shortRaycast = transform.position + leftDirection * detectionRange;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, leftDirection, out hit, viewDistance, playerMask))
        {
            if (hit.collider.CompareTag("Player"))
            {
                Debug.DrawLine(transform.position, player.position, Color.green);
                return true;
            }
        }
        Debug.DrawLine(transform.position, player.position, Color.red);
        return false;
    }

    private Vector3 GetNearestPointToPlayer()
    {
        Vector3 nearestPoint = transform.position;
        float nearestDistance = float.MaxValue;

        foreach (Transform point in gridArea)
        {
            float distance = Vector3.Distance(player.position, point.position);
            if(distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestPoint = point.position;
            }
        }
        return nearestPoint;
    }

    private void TeleportToNearestMovePoint(Vector3 targetPosition)
    {
        Transform nearestPoint = null;
        float nearesDistanceFunc = float.MaxValue;

        foreach (Transform point in gridArea)
        {
            float distance = Vector3.Distance(targetPosition, point.position);
            if (distance < nearesDistanceFunc)
            {
                nearesDistanceFunc = distance;
                nearestPoint = point;
            }
        }

        if (nearestPoint != null)
        {
            currentMovePoint = nearestPoint;
            transform.position = currentMovePoint.position;
        }
    }

    private void TeleportToRandomAdjacentMovePoint()
    {
        List<Transform> validMovePoints = new List<Transform>();

        foreach (Transform point in gridArea)
        {
            if (Vector3.Distance(point.position, currentMovePoint.position) <= 1.1f)
            {
                validMovePoints.Add(point);
            }
        }

        if (validMovePoints.Count > 0)
        {
            int randomIndex = Random.Range(0, validMovePoints.Count);
            currentMovePoint = validMovePoints[randomIndex];
            transform.position = currentMovePoint.position;
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 rayStart = transform.position;
        Vector3 rayEnd = transform.position + -transform.right * detectionRange;

        // Zeichne den kurzen Raycast im Editor
        Gizmos.color = Color.red;
        Gizmos.DrawLine(rayStart, rayEnd);
    }
}