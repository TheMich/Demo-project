using UnityEngine;

public class SwarmerBehaviour : EnemyBehaviour
{
    public GameObject explosionPrefab;

    protected void OnCollisionEnter(Collision thisCollision)
    {
        var theirGameObject = thisCollision.gameObject;
        if (References.thePlayer && theirGameObject == References.thePlayer.gameObject)
        {
            Instantiate(explosionPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
