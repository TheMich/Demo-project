using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemyPrefab;
    public float secondsBetweenSpawns;
    public GameObject spawnPoint;
    public bool activated;

    private float m_secondsSinceLastSpawn;

    private void Awake()
    {
        References.spawner = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_secondsSinceLastSpawn = 0;
    }

    // Fixed update happens the same number of times for all
    // players, so it's good for gameplay critical things.
    void FixedUpdate()
    {
        m_secondsSinceLastSpawn += Time.fixedDeltaTime;
        if (activated && m_secondsSinceLastSpawn >= secondsBetweenSpawns)
        {
            SpawnEnemy();
            m_secondsSinceLastSpawn = 0;
        }
    }

    // Helper function in case we want extra behavior upon spawning
    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
    }
}
