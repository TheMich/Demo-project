using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class RailgunBeamBehaviour : BulletBehaviour
{
    public LineRenderer myBeam;
    public float beamDamageWidth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Fire laser to check how far we can go before hitting a wall
        Physics.Raycast(transform.position, transform.forward, out RaycastHit wallHitInfo, References.maxDistanceInALevel, References.wallsLayer);

        // Fire a new laser to check for enemies
        var enemiesHit = Physics.SphereCastAll(transform.position, beamDamageWidth, transform.forward, wallHitInfo.distance, References.enemiesLayer);
        foreach (var enemy in enemiesHit) {
            var healthSystem = enemy.collider.GetComponentInParent<HealthSystem>();
            if (healthSystem)
            {
                healthSystem.TakeDamage(damage);
            }
        }
        

        // Draw beam
        myBeam.SetPosition(0, transform.position);
        myBeam.SetPosition(1, wallHitInfo.point);

    }

    // Update is called once per frame
    override protected void Update()
    {
        myBeam.widthMultiplier *= 1 - Time.deltaTime;
        base.Update();
    }
}
