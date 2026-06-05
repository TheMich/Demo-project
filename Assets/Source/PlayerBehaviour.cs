using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBehaviour : MonoBehaviour
{

    public float playerSpeed;
    public List<WeaponBehaviour> weapons = new();
    
    private int m_selectedWeaponIndex = 0;
    private Rigidbody m_thisRigidBody;


    private void Awake()
    {
        References.thePlayer = this;
    }

    void Start()
    {
        m_thisRigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Movement
        Vector3 inputVector = new(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        m_thisRigidBody.linearVelocity = playerSpeed * inputVector;

        Vector3 cursorPosition = GetCursorPosition(transform.position);

        // Face the new position
        Vector3 lookAtPosition = cursorPosition;
        transform.LookAt(lookAtPosition);

        // Useables
        if (Input.GetButtonDown("Use"))
        {
            // Use the nearest useable
            // TODO think if it is more efficient to instead maintain order of list at each update,
            // so that action here will be O(1) instead of O(n)
            // Might not be worth it though since list should be small in size anyway
            Useable nearest = null;
            float nearestDistance = 2; // TODO de-magic
            foreach (var item in References.useables)
            {
                var distance = Vector3.Distance(transform.position, item.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearest = item;
                }
            }
            if (nearest)
            {
                nearest.Use();
            }
        }

        // Firing
        // need to check we have at least one weapon
        if (weapons.Count > 0)
        {
            if (Input.GetButton("Fire1"))
            {
                weapons[m_selectedWeaponIndex].Fire(cursorPosition);
            }

            // Change selected weapon
            if (Input.GetButtonDown("Fire2"))
            {
                ChangeWeaponIndex(m_selectedWeaponIndex + 1);
            } 
        }
    }

    public void SelectLatestWeapon()
    {
        ChangeWeaponIndex(weapons.Count - 1);
    }
    
    private void ChangeWeaponIndex(int index)
    {
        // original tutorial code used a for-loop to check for activation,
        // but we can simply use an additional index instead
        var oldIndex = m_selectedWeaponIndex;
        m_selectedWeaponIndex = index;
        if (m_selectedWeaponIndex >= weapons.Count)
        {
            m_selectedWeaponIndex = 0;
        }
        weapons[oldIndex].gameObject.SetActive(false);
        weapons[m_selectedWeaponIndex].gameObject.SetActive(true);
    }

    private Vector3 GetCursorPosition(Vector3 relativePosition)
    {
        Ray rayFromCameraToCursor = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane playerPlane = new(Vector3.up, relativePosition);
        playerPlane.Raycast(rayFromCameraToCursor, out float distanceFromCamera);
        return rayFromCameraToCursor.GetPoint(distanceFromCamera);
    }
}
