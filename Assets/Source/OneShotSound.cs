using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class OneShotSound : MonoBehaviour
{
    private AudioSource m_audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_audioSource && !m_audioSource.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
