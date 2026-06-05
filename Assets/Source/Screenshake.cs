using UnityEngine;

public class Screenshake : MonoBehaviour
{
    public Vector3 joltVector;
    public float shakeAmount;

    public float joltDecayFactor;
    public float shakeDecayFactor;
    
    public float maxMoveSpeed;

    private Vector3 m_normalPosition;
    private Vector3 m_desiredPosition;

    private void Awake()
    {
        References.screenshake = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_normalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        var shakeVector = new Vector3(GetRandomShakeAmount(), GetRandomShakeAmount(), GetRandomShakeAmount());
        m_desiredPosition = m_normalPosition + joltVector + shakeVector;

        // set our position to jolted position
        transform.position = Vector3.MoveTowards(m_normalPosition, m_desiredPosition, maxMoveSpeed * Time.deltaTime);
        joltVector *= joltDecayFactor;
        shakeAmount *= shakeDecayFactor;
    }

    float GetRandomShakeAmount()
    {
        return Random.Range(-shakeAmount, shakeAmount);
    }
}
