using UnityEngine;
using UnityEngine.Serialization;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth;
    public GameObject healthBarPrefab;
    public GameObject deathEffectPrefab;

    private HealthBar m_healthBar;
    private static readonly float m_healthBarOffset = 2;
    private float m_currentHealth;

    public void TakeDamage(float damageAmount)
    {
        if (m_currentHealth > 0)
        {
            m_currentHealth -= damageAmount;
            if (m_currentHealth <= 0)
            {
                if (deathEffectPrefab)
                {
                    ShowDeathEffect(deathEffectPrefab);
                }
                Destroy(gameObject);
            } 
        }
    }

    private void ShowDeathEffect(GameObject deathEffect)
    {
        Instantiate(deathEffect, transform.position, transform.rotation);
    }

    private void OnDestroy()
    {
        if(m_healthBar)
        {
            Destroy(m_healthBar.gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Create our health panel on the canvas
        var healthBarObject = Instantiate(healthBarPrefab, References.canvas.transform);
        m_healthBar = healthBarObject.GetComponent<HealthBar>();
        m_currentHealth = maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        // make our healthbar reflect our health - myHealthBar.ShowHealth()
        m_healthBar.ApplyHealthFraction(m_currentHealth / maxHealth);

        // make healthbar follow us - move it to our current position
        m_healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + 
            m_healthBarOffset * Vector3.up);
    }
}
