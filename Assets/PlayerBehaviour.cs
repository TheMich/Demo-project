using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBehaviour : MonoBehaviour
{

    public float playerSpeed;
    public float secondsBetweenShots;
    public GameObject bulletPrefab;

    private Rigidbody thisRigidBody;

    float secondsSinceLastShot;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        secondsSinceLastShot = secondsBetweenShots;
        thisRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        Vector3 inputVector = new(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        thisRigidBody.linearVelocity = playerSpeed * inputVector;

        Vector3 cursorPosition = getCursorPosition(transform.position);

        // Face the new position
        Vector3 lookAtPosition = cursorPosition;
        transform.LookAt(lookAtPosition);

        // Firing
        secondsSinceLastShot += Time.deltaTime;

        if (Input.GetButton("Fire1") && secondsSinceLastShot >= secondsBetweenShots)
        {
            secondsSinceLastShot = 0;
            Instantiate(bulletPrefab, transform.position + transform.forward, transform.rotation); 
        }
    }

    private Vector3 getCursorPosition(Vector3 relativePosition)
    {
        Ray rayFromCameraToCursor = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane playerPlane = new(Vector3.up, relativePosition);
        playerPlane.Raycast(rayFromCameraToCursor, out float distanceFromCamera);
        return rayFromCameraToCursor.GetPoint(distanceFromCamera);
    }
}
