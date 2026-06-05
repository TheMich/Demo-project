using UnityEngine;

public class SwarmerBehaviour : EnemyBehaviour
{
    public GameObject explosionPrefab;

    protected void OnCollisionEnter(Collision thisCollision)
    {
        var theirGameObject = thisCollision.gameObject;
        var potentialPlayer = theirGameObject.GetComponent<PlayerBehaviour>();
        if (potentialPlayer == References.thePlayer)
        {
            Instantiate(explosionPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
