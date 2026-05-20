using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBehaviour : MonoBehaviour
{

    public float playerSpeed;
    public float secondsBetweenShots;
    public GameObject bulletPrefab;

    private Rigidbody m_thisRigidBody;

    private float m_secondsSinceLastShot;

    private void Awake()
    {
        References.thePlayer = gameObject;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_secondsSinceLastShot = secondsBetweenShots;
        m_thisRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        Vector3 inputVector = new(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        m_thisRigidBody.linearVelocity = playerSpeed * inputVector;

        Vector3 cursorPosition = GetCursorPosition(transform.position);

        // Face the new position
        Vector3 lookAtPosition = cursorPosition;
        transform.LookAt(lookAtPosition);

        // Firing
        m_secondsSinceLastShot += Time.deltaTime;

        if (Input.GetButton("Fire1") && m_secondsSinceLastShot >= secondsBetweenShots)
        {
            m_secondsSinceLastShot = 0;
            Instantiate(bulletPrefab, transform.position + transform.forward, transform.rotation); 
        }
    }

    private Vector3 GetCursorPosition(Vector3 relativePosition)
    {
        Ray rayFromCameraToCursor = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane playerPlane = new(Vector3.up, relativePosition);
        playerPlane.Raycast(rayFromCameraToCursor, out float distanceFromCamera);
        return rayFromCameraToCursor.GetPoint(distanceFromCamera);
    }
}
