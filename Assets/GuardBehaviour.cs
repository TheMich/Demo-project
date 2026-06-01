using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GuardBehaviour : EnemyBehaviour
{
    public float visionRange;
    public float visionConeAngle;
    public bool alerted;
    public float turnSpeed;
    public Light myLight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    override protected void Start()
    {
        base.Start();
        SetAlert(false);
        GoToRandomNavPoint();
    }

    void GoToRandomNavPoint()
    {
        int randomNavPointIndex = Random.Range(0, References.navPoints.Count);
        m_navAgent.destination = References.navPoints[randomNavPointIndex].transform.position;
    }

    // Update is called once per frame
    override protected void Update()
    {
        if (GetPlayerPositionAndDirection() is var (playerPosition, directionToPlayer))
        {
            // Follow the player
            if (alerted)
            {
                ChasePlayer();
            }
            else
            {

                if (m_navAgent.remainingDistance < 0.5f)
                {
                    GoToRandomNavPoint();
                }

                // Rotate and patrol
                var lateralOffset = Time.deltaTime * turnSpeed * transform.right;
                transform.LookAt(transform.position + transform.forward + lateralOffset);
                m_thisRigidBody.linearVelocity = speed * transform.forward;

                // Check if we can see the player
                if (Vector3.Distance(transform.position, playerPosition) <= visionRange
                    && Vector3.Angle(transform.forward, directionToPlayer) <= visionConeAngle
                    && !Physics.Raycast(transform.position, directionToPlayer, directionToPlayer.magnitude, References.wallsLayer)) // if we don't hit a wall
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
        if (References.spawner is var spawner and not null)
        {
            spawner.activated = alert;
        }
    }
}
