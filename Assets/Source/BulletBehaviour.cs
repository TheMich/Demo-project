using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletBehaviour : MonoBehaviour
{
    public float bulletSpeed;
    public float lifetimeInSeconds;
    public float damage;

    private float m_secondsUntilDestroyed;

    protected float LifetimeNormalized()
    {
        return lifetimeInSeconds != 0 ? m_secondsUntilDestroyed / lifetimeInSeconds : 0;
    }

    private void Awake()
    {

        m_secondsUntilDestroyed = lifetimeInSeconds;
    }

    void Start()
    {
        var rigidBody = GetComponent<Rigidbody>();
        if (rigidBody)
        {
            rigidBody.linearVelocity = bulletSpeed * transform.forward;
        }
    }
    protected virtual void Update()
    {
        m_secondsUntilDestroyed -= Time.deltaTime;


        if (m_secondsUntilDestroyed < 1)
        {
            transform.localScale *= m_secondsUntilDestroyed;
        }


        if (m_secondsUntilDestroyed < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision thisCollision)
    {
        var theirGameObject = thisCollision.gameObject;

        if (theirGameObject.GetComponent<HealthSystem>() is var theirHealth && theirHealth)
        {
            theirHealth.TakeDamage(damage);
        }
        Destroy(gameObject);

    }
}
