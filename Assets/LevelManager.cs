using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public bool alarmSounded;
    public SceneAsset nextLevel;
    public float graceTimeAtEndOfLevel;
    
    private float m_secondsBeforeNextLevel;

    private void Awake()
    {
        References.levelManager = this;
        m_secondsBeforeNextLevel = graceTimeAtEndOfLevel;
    }

    void Start()
    {
        alarmSounded = false;
    }

    void Update()
    {
        // If all enemies are dead, go to next level
        if (References.allEnemies.Count < 1)
        {
            m_secondsBeforeNextLevel -= Time.deltaTime;
            if (m_secondsBeforeNextLevel <= 0)
            {
                SceneManager.LoadScene(nextLevel.name);
            }
        } else
        {
            // Reset the timer if an enemy spawns
            m_secondsBeforeNextLevel = graceTimeAtEndOfLevel;
        }
    }
}
