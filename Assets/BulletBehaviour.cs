using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletBehaviour : MonoBehaviour
{
    public float bulletSpeed;
    public float secondsUntilDestroyed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var rigidBody = GetComponent<Rigidbody>();
        rigidBody.linearVelocity = bulletSpeed * transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        secondsUntilDestroyed -= Time.deltaTime;


        if (secondsUntilDestroyed < 1)
        {
            transform.localScale *= secondsUntilDestroyed;
        }
            

        if (secondsUntilDestroyed < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision thisCollision)
    {
        var theirGameObject = thisCollision.gameObject;
        if (theirGameObject.GetComponent<EnemyBehaviour>())
        {
            Destroy(theirGameObject);
            Destroy(gameObject);
        }
    }
}
