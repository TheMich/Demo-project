using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyBehaviour : MonoBehaviour
{
    public float speed;
    public float visionRange;
    public float visionConeAngle;
    public bool alerted;
    public float turnSpeed;
    public Light myLight;

    private Rigidbody m_thisRigidBody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_thisRigidBody = GetComponent<Rigidbody>();
        SetAlert(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (References.thePlayer)
        {
            var playerPosition = References.thePlayer.transform.position;
            var directionToPlayer = playerPosition - transform.position;
            //if (myLight)
            //{
            //    myLight.color = Color.white;
            //}

            // Follow the player
            if (alerted)
            {
                m_thisRigidBody.linearVelocity = speed * directionToPlayer.normalized;
                Vector3 playerPositionAtOurHeight = new(playerPosition.x, transform.position.y, playerPosition.z);
                transform.LookAt(playerPositionAtOurHeight); 
            } else
            {
                // Rotate and patrol
                var lateralOffset = Time.deltaTime * turnSpeed * transform.right;
                transform.LookAt(transform.position + transform.forward + lateralOffset);
                m_thisRigidBody.linearVelocity = speed * transform.forward;

                // Check if we can see the player
                if(Vector3.Distance(transform.position, playerPosition) <= visionRange
                    && Vector3.Angle(transform.forward, directionToPlayer) <= visionConeAngle)
                {
                    SetAlert(true);
                }
            }
        }
    }

    private void SetAlert(bool alert)
    {
        alerted = alert;
        if (myLight)
        {
            myLight.color = alert ? Color.red : Color.white;
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
