using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DeathEffect : MonoBehaviour
{

    public float shakeAmount;
    public Light myLight;
    public float duration;

    private float m_maxLightIntensity;
    private float m_secondsLeft;
    private AudioSource m_audioSource;

    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        References.screenshake.shakeAmount = shakeAmount;
        m_maxLightIntensity = myLight.intensity;
        m_secondsLeft = duration;
    }

    void Update()
    {
        m_secondsLeft -= Time.deltaTime;
        if (myLight)
        {
            myLight.intensity = (m_secondsLeft / duration) * m_maxLightIntensity; 
        }

        if (m_secondsLeft <= 0 && m_audioSource && !m_audioSource.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
