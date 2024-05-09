using UnityEngine;
using System.Collections.Generic;

public class ObstacleSpawnSystem : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public int maxObstacles = 10;
    public Transform[] spawnPoints; // Define spawn points within the alley
    public Quaternion fixedRotation = Quaternion.identity; // Set the fixed rotation for the obstacles
    public float rotationSpeed = 50f; // Speed at which the obstacles rotate
    public Vector3 rotationAxis = Vector3.up; // Axis around which the obstacles rotate
    public Vector3 obstacleScale = new Vector3(0.5f, 0.5f, 0.5f); // Scale of the obstacles

    private List<GameObject> obstacles = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            GameObject alley = GameObject.FindGameObjectWithTag("Alley");
            if (alley != null)
            {
                StartSpawning(alley);
            }
            else
            {
                Debug.LogWarning("No object tagged as 'Alley' found.");
            }
        }
    }

    private void StartSpawning(GameObject alley)
    {
        for (int i = 0; i < maxObstacles; i++)
        {
            SpawnObstacle(alley, i % spawnPoints.Length); // Loop through spawn points
        }
    }

    private void SpawnObstacle(GameObject alley, int spawnPointIndex)
    {
        Vector3 spawnPosition = spawnPoints[spawnPointIndex].position;
        Quaternion spawnRotation = fixedRotation;

        GameObject obstacle = Instantiate(obstaclePrefab, spawnPosition, spawnRotation);
        obstacle.transform.localScale = obstacleScale; // Set the scale of the obstacle
        obstacles.Add(obstacle);

        Debug.Log("Obstacle spawned at position: " + spawnPosition); // Log the spawn position
    }

    private void Update()
    {
        // Rotate each spawned obstacle around its local axis
        foreach (GameObject obstacle in obstacles)
        {
            obstacle.transform.RotateAround(obstacle.transform.position, rotationAxis, rotationSpeed * Time.deltaTime);
        }
    }
}
