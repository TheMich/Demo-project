using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyBehaviour : MonoBehaviour
{
    public float speed;

    protected Rigidbody m_thisRigidBody;
    protected NavMeshAgent m_navAgent;

    protected void OnEnable()
    {
        References.allEnemies.Add(this);
    }

    protected void OnDisable()
    {
        References.allEnemies.Remove(this);
    }

    protected virtual void Start()
    {
        m_thisRigidBody = GetComponent<Rigidbody>();
        m_navAgent = GetComponent<NavMeshAgent>();
        m_navAgent.speed = speed;
    }

    protected virtual void Update()
    {
        ChasePlayer();
    }

    protected void ChasePlayer()
    {
        if (GetPlayerPositionAndDirection() is var (playerPosition, _)) 
        {
            m_navAgent.destination = playerPosition;
        }
    }

    protected (Vector3, Vector3)? GetPlayerPositionAndDirection()
    {
        if (References.thePlayer)
        {
            var playerPosition = References.thePlayer.transform.position;
            var directionToPlayer = playerPosition - transform.position;
            return (playerPosition, directionToPlayer);
        } else
        {
            return null;
        }
    }

    
}
