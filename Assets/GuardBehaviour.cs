using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GuardBehaviour : EnemyBehaviour
{
    public float visionRange;
    public float visionConeAngle;
    public bool alerted;
    public float turnSpeed;
    public float reactionTime;
    public Light myLight;
    public WeaponBehaviour myWeapon;

    private float m_secondsSeeingPlayer = 0;

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

    protected bool CanSeeVector(Vector3 directionToPlayer)
    {
        return !Physics.Raycast(transform.position, directionToPlayer, directionToPlayer.magnitude, References.wallsLayer);
    }

    override protected void Update()
    {
        if (GetPlayerPositionAndDirection() is var (playerPosition, directionToPlayer))
        {
            // Follow the player
            if (alerted)
            {
                ChasePlayer();
                if (CanSeeVector(directionToPlayer))
                {
                    m_secondsSeeingPlayer += Time.deltaTime;
                    transform.LookAt(playerPosition);
                    if (m_secondsSeeingPlayer > reactionTime)
                    { 
                        myWeapon.Fire(playerPosition);
                    }
                } else
                {
                    m_secondsSeeingPlayer = 0;
                }
            
            }
            else
            {
                if (References.levelManager is var manager and not null && manager.alarmSounded)
                {
                    SetAlert(true);
                }

                if (m_navAgent.remainingDistance < 0.5f)
                {
                    GoToRandomNavPoint();
                }

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
        if (References.levelManager is var manager and not null)
        {
            manager.alarmSounded = alert;
        }
    }
}
