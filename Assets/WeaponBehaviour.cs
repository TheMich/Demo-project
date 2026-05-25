using Unity.VisualScripting;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    public float secondsBetweenShots;
    public float accuracy;
    public int numberOfProjectiles;
    
    public GameObject bulletPrefab;

    private float m_secondsSinceLastShot;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_secondsSinceLastShot = secondsBetweenShots;
    }

    // Update is called once per frame
    void Update()
    {
        m_secondsSinceLastShot += Time.deltaTime;
    }

    public void Fire(Vector3 targetPosition)
    {

        if (m_secondsSinceLastShot >= secondsBetweenShots)
        {
            m_secondsSinceLastShot = 0;

            References.spawner.activated = true;

            // offset depending on weapon accuracy
            var targetDistance = Vector3.Distance(transform.position, targetPosition);
            var bulletSpread = targetDistance / accuracy; // TODO safeguard against 0 accuracy

            for (int i = 0; i < numberOfProjectiles; i++)
            {
                //var alteredRotation = transform.rotation * Quaternion.Euler(0f, Random.Range(-accuracy, accuracy), 0f);
                var newBullet = Instantiate(bulletPrefab,
                transform.position + transform.forward, transform.rotation);
                newBullet.name = "Bullet " + i.ToString();

                Vector3 alteredPosition = targetPosition;
                alteredPosition.x += Random.Range(-bulletSpread, bulletSpread);
                alteredPosition.z += Random.Range(-bulletSpread, bulletSpread);

                newBullet.transform.LookAt(alteredPosition);
            }
        }
    }
}
