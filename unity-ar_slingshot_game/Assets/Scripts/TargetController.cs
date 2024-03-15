using UnityEngine;

public class TargetController : MonoBehaviour
{
    public GameObject plane; // Reference to the selected plane
    public int numberOfTargets = 5; // Number of targets to instantiate

    private float minScale = 0.1f; // Minimum scale of the target
    private float maxScale = 1.0f; // Maximum scale of the target
    private float maxDistance = 10f; // Maximum distance for scaling
    private float movementSpeed = 1.0f; // Speed of movement

    private void Start()
    {
        // Instantiate targets only if a plane is selected
        if (plane != null)
        {
            for (int i = 0; i < numberOfTargets; i++)
            {
                InstantiateTarget();
            }
        }
    }

    private void InstantiateTarget()
    {
        // Random position on the plane
        Vector3 randomPosition = new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f));

        // Instantiate target on the plane
        GameObject target = Instantiate(gameObject, randomPosition, Quaternion.identity);
        
        // Ensure targets are on top of the plane
        target.transform.position = new Vector3(target.transform.position.x, plane.transform.position.y, target.transform.position.z);
        
        // Random scale based on distance from the camera/device
        float distance = Vector3.Distance(target.transform.position, Camera.main.transform.position);
        float scaleFactor = Mathf.Clamp(distance / maxDistance, minScale, maxScale);
        target.transform.localScale *= scaleFactor;

        // Add collider to the target
        target.AddComponent<BoxCollider>();

        // Random initial movement direction
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;

        // Start moving the target
        MoveTarget(target, randomDirection);
    }

    private void MoveTarget(GameObject target, Vector3 direction)
    {
        // Move the target in its direction
        target.transform.Translate(direction * movementSpeed * Time.deltaTime, Space.World);

        // Clamp target within plane boundaries
        Vector3 targetPosition = target.transform.position;
        targetPosition.x = Mathf.Clamp(targetPosition.x, -5f, 5f);
        targetPosition.z = Mathf.Clamp(targetPosition.z, -5f, 5f);
        target.transform.position = targetPosition;

        // Schedule next movement
        Invoke("ChangeDirection", Random.Range(1f, 3f));
    }

    private void ChangeDirection()
    {
        // Pick a new random direction
        Vector3 newDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;

        // Select a random target to change direction
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
        if (targets.Length > 0)
        {
            GameObject randomTarget = targets[Random.Range(0, targets.Length)];
            MoveTarget(randomTarget, newDirection);
        }
    }
}
