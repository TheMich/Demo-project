using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyBehaviour : MonoBehaviour
{
    public float speed;

    private Rigidbody m_thisRigidBody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_thisRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //var rigidBody = GetComponent<Rigidbody>();
        // rigidBody.velocity = vector going towards the player
        if (References.thePlayer)
        {
            var directionToPlayer = References.thePlayer.transform.position - transform.position;
            m_thisRigidBody.linearVelocity = speed * directionToPlayer.normalized; 
        }

    }
    private void OnCollisionEnter(Collision thisCollision)
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
