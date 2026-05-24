using UnityEngine;

public class ExplosionBehaviour : MonoBehaviour
{
    public float secondsToExist;
    private float m_secondsWeHaveBeenAlive;

    private readonly Vector3 m_maxScale = Vector3.one * 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_secondsWeHaveBeenAlive = 0;
    }

    // Update is called once per frame
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
            theirHealth.TakeDamage(10); // TODO de-magic
        }
    }
}
