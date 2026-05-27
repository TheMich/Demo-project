using UnityEngine;

public class Screenshake : MonoBehaviour
{
    public Vector3 joltVector;
    public float joltDecayFactor;
    public float maxMoveSpeed;

    private Vector3 m_normalPosition;

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
        transform.position = Vector3.MoveTowards(m_normalPosition, m_normalPosition + joltVector, maxMoveSpeed * Time.deltaTime);
        joltVector *= joltDecayFactor;
    }
}
