using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class OneShotSound : MonoBehaviour
{
    private AudioSource m_audioSource;

    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (m_audioSource && !m_audioSource.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
