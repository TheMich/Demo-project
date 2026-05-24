using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyBehaviour : MonoBehaviour
{
    public float speed;

    protected Rigidbody m_thisRigidBody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        m_thisRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        ChasePlayer();
    }

    protected void ChasePlayer()
    {
        if (GetPlayerPositionAndDirection() is var (playerPosition, directionToPlayer)) 
        {
            //var playerPosition = References.thePlayer.transform.position;
            //var directionToPlayer = playerPosition - transform.position;

            // Follow the player

            m_thisRigidBody.linearVelocity = speed * directionToPlayer.normalized;
            Vector3 playerPositionAtOurHeight = new(playerPosition.x, transform.position.y, playerPosition.z);
            transform.LookAt(playerPositionAtOurHeight);

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

    protected void OnCollisionEnter(Collision thisCollision)
    {
        var theirGameObject = thisCollision.gameObject;
        if (theirGameObject.GetComponent<PlayerBehaviour>())
        {
            if (theirGameObject.GetComponent<HealthSystem>() is var theirHealth && theirHealth)
            {
                theirHealth.TakeDamage(1); //TODO de-magic
            }
        }
    }
}
