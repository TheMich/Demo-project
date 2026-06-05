using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WeaponBehaviour : MonoBehaviour
{
    public float secondsBetweenShots;
    public float accuracy;
    public int numberOfProjectiles;
    public GameObject bulletPrefab;
    public float kickAmount;

    private float m_secondsSinceLastShot;
    private AudioSource m_audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_secondsSinceLastShot = secondsBetweenShots;
        m_audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        m_secondsSinceLastShot += Time.deltaTime;
    }

    public void BePickedUpByPlayer()
    {
        if (References.thePlayer is var player and not null)
        {
            player.weapons.Add(this);
            transform.SetPositionAndRotation(player.transform.position, player.transform.rotation);
            transform.SetParent(player.transform);
            player.SelectLatestWeapon();
        }

    }

    public void Fire(Vector3 targetPosition)
    {

        if (m_secondsSinceLastShot >= secondsBetweenShots)
        {
            m_secondsSinceLastShot = 0;
            m_audioSource.Play();
            References.screenshake.joltVector = kickAmount * transform.forward;

            if (References.levelManager)
            {
                References.levelManager.alarmSounded = true;
            }
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
