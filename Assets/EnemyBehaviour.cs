using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyBehaviour : MonoBehaviour
{
    public float speed;
    public GameObject player;

    private Rigidbody thisRigidBody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thisRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //var rigidBody = GetComponent<Rigidbody>();
        // rigidBody.velocity = vector going towards the player
        var directionToPlayer = player.transform.position - transform.position;
        thisRigidBody.linearVelocity = speed * directionToPlayer.normalized;

    }
}
