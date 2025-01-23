using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyMovement : MonoBehaviour, IEnemyMovement
{
    [SerializeField] Transform target;
    NavMeshAgent agent;
    private Rigidbody2D rb;
    
    private Vector3 roamPosition;
    public float reachedPositionDistance = 2f;
    public float stopRoamingDistance = 12f;
    public float targetRange = 10f;
    
    public float enemyAngle;
    public bool isRightHanded = true;
    public bool isMoving = true;

    private enum State
    {
        Roaming,
        Chasing,
    }

    private State state;
        
    // Start is called before the first frame update

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody2D>();
        state = State.Chasing;
    }

    void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;

    }
    public void Move(Enemy enemy)
    {

        if (Player.Instance.lifes > 0 && isMoving) // While the player still alive and the enemy is not dead
        {
            agent.isStopped = false;
            isMoving = true;
            switch (state)
            {
                default:
                case State.Chasing:
                   // Debug.Log(state);
                    agent.speed = 3f;
                    agent.SetDestination(Player.Instance.GetPosition());
                    ApproachRoamingArea();
                    break;
                case State.Roaming:
                    //Debug.Log(state);
                    agent.speed = 1f;
                    agent.SetDestination(roamPosition);
                    if (Vector3.Distance(transform.position, roamPosition) < reachedPositionDistance)
                    {
                        roamPosition = GetRoamingPosition();
                    }

                    if (Vector3.Distance(transform.position, Player.Instance.GetPosition()) > stopRoamingDistance)
                    {
                        // Target is too far, start chasing
                        state = State.Chasing;
                    }
                    break;
            }
        }
    }

    public void Stop()
    {
        isMoving = false;
        agent.isStopped = true;
    }

    private Vector3 GetRoamingPosition() {
        return transform.position + GetRandomDir() * Random.Range(3f, 3f);
    } 

    private static Vector3 GetRandomDir() {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    private void  ApproachRoamingArea()
    {

        if (Vector3.Distance(transform.position, Player.Instance.GetPosition()) < targetRange)
        {
            // Enemy Within the target range
            roamPosition = GetRoamingPosition();
            state = State.Roaming;
        }
    }
    
    
    // Draw Gizmos in the scene to visualize the distances
    private void OnDrawGizmosSelected()
    {
        // Draw a red sphere to represent the reachedPositionDistance
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, reachedPositionDistance);

        // Draw a blue sphere to represent the stopRoamingDistance
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, stopRoamingDistance);

        // Draw a green sphere to represent the targetRange (chase range)
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, targetRange);
        
        // Draw enemy angle

        if (Player.Instance == null) return;
        
        Vector3 enemyPosition = transform.position;

        // Direction to the player
        Vector3 directionToPlayer = Player.Instance.transform.position - enemyPosition;

        // Draw a line from the enemy to the player
        Gizmos.color = Color.red;
        Gizmos.DrawLine(enemyPosition, Player.Instance.transform.position);

        // Draw an angle visualization
        Gizmos.color = Color.yellow;
        Vector3 forward = Vector3.right; // Assuming the enemy's "forward" direction is along the x-axis
        Vector3 directionNormalized = directionToPlayer.normalized;

        Gizmos.DrawRay(enemyPosition, forward * 2); // Draw the "forward" direction
        Gizmos.DrawRay(enemyPosition, directionNormalized * 2); // Draw the direction to the player

        // Optional: Draw arc for better visualization
       // DrawAngleArc(enemyPosition, forward, directionNormalized, 2.0f);
    }
    
    private void DrawAngleArc(Vector3 origin, Vector3 forward, Vector3 targetDir, float radius)
    {
        int segments = 20; // Number of segments in the arc
        float angle = Vector3.SignedAngle(forward, targetDir, Vector3.forward);

        for (int i = 0; i <= segments; i++)
        {
            float step = angle / segments;
            float currentAngle = step * i;
            Quaternion rotation = Quaternion.Euler(0, 0, currentAngle);
            Vector3 point = rotation * forward * radius;
            Gizmos.DrawLine(origin, origin + point);
        }
    }

}
