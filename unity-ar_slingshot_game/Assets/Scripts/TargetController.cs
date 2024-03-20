using UnityEngine;

public class TargetController : MonoBehaviour
{
    public GameObject targetprefab;
    public int numberOfTargets = 5; // Number of targets to instantiate

    private float minScale = 0.1f; // Minimum scale of the target
    private float maxScale = 1.0f; // Maximum scale of the target
    private float maxDistance = 10f; // Maximum distance for scaling
    private float movementSpeed = 1.0f; // Speed of movement

    public void StartGame()
    {
        // Instantiate targets only if a plane is selected
        if (PlaneSelector.selectedPlane != null)
        {
            Debug.Log("Starting game...");
            for (int i = 0; i < numberOfTargets; i++)
            {
                InstantiateTarget();


            }
        }
        else
        {
            Debug.LogWarning("No plane selected. Cannot start the game.");
        }
    }

   private void InstantiateTarget()
{
    // Get the bounds of the plane
    Renderer planeRenderer = PlaneSelector.selectedPlane.GetComponent<Renderer>();
    Bounds planeBounds = planeRenderer.bounds;

    // Random position within the bounds of the plane
    float randomX = Random.Range(planeBounds.min.x, planeBounds.max.x);
    float randomZ = Random.Range(planeBounds.min.z, planeBounds.max.z);
    Vector3 randomPosition = new Vector3(randomX, planeBounds.min.y, randomZ); // Set y-coordinate to the plane's bottom edge

    Debug.Log("Instantiating target at position: " + randomPosition);

    // Instantiate target prefab on the plane
    GameObject target = Instantiate(targetprefab, randomPosition, Quaternion.identity);

    // Random scale based on distance from the camera/device
    float distance = Vector3.Distance(target.transform.position, Camera.main.transform.position);
    float scaleFactor = Mathf.Clamp(distance / maxDistance, minScale, maxScale);
    target.transform.localScale *= scaleFactor;
    Debug.Log("Target scaled to: " + target.transform.localScale);

    // Ensure targets are on top of the plane
    target.transform.position = new Vector3(target.transform.position.x, planeBounds.max.y, target.transform.position.z);

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
        float randomInterval = Random.Range(1f, 3f);
        Debug.Log("Next movement direction change in: " + randomInterval + " seconds");
        Invoke("ChangeDirection", randomInterval);
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
            Debug.Log("Changing direction of target: " + randomTarget.name);
            MoveTarget(randomTarget, newDirection);
        }
        else
        {
            Debug.LogWarning("No targets found to change direction.");
        }
    }
}
