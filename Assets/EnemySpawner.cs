using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemyPrefab;
    public float secondsBetweenSpawns;
    public GameObject spawnPoint;
    public int enemiesToSpawn;

    private float m_secondsSinceLastSpawn;
    private int m_spawnCount;

    private void OnEnable()
    {
        References.spawners.Add(this);
    }

    private void OnDisable()
    {
        References.spawners.Remove(this);
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
        if (References.levelManager.alarmSounded && m_secondsSinceLastSpawn >= secondsBetweenSpawns && m_spawnCount < enemiesToSpawn)
        {
            SpawnEnemy();
        }
    }

    // Helper function in case we want extra behavior upon spawning
    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        m_spawnCount++;
        m_secondsSinceLastSpawn = 0;
    }
}
