using UnityEngine;

public class RespawnOnCollision : MonoBehaviour
{
    private Vector3 originalPosition;

    private void Start()
    {
        // Store the original starting position
        originalPosition = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with an object tagged as "Obstacle"
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Reset the position to the original starting position
            transform.position = originalPosition;
        }
    }
}
