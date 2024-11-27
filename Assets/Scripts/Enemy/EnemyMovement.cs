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
        EnemyLookAt(); // Updates enemy facing the player
        
        
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

    private void EnemyLookAt()
    {
        Vector3 direction = Player.Instance.transform.position - transform.position;
        
        float playerAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = playerAngle;

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
    }

}
