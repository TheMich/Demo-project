using UnityEngine;

public class ExplosionBehaviour : MonoBehaviour
{
    public float secondsToExist;
    public GameObject soundObject;
    public float damage;

    private float m_secondsWeHaveBeenAlive;
    private readonly Vector3 m_maxScale = Vector3.one * 5;

    void Start()
    {
        m_secondsWeHaveBeenAlive = 0;
        Instantiate(soundObject, transform.position, transform.rotation);
    }

    void FixedUpdate()
    {
        m_secondsWeHaveBeenAlive += Time.fixedDeltaTime;

        float lifeRatio = m_secondsWeHaveBeenAlive / secondsToExist;
        transform.localScale = Vector3.Lerp(Vector3.zero, m_maxScale, lifeRatio);

        if (m_secondsWeHaveBeenAlive >= secondsToExist)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider victim)
    {
        if (victim.gameObject.GetComponent<HealthSystem>() is var theirHealth and not null)
        {
            theirHealth.TakeDamage(damage);
        }
    }
}
