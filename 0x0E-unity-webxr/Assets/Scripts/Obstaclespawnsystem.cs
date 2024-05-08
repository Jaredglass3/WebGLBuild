using UnityEngine;
using System.Collections.Generic;

public class ObstacleSpawnSystem : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public int maxObstacles = 10;
    public Transform[] spawnPoints; // Define spawn points within the alley
    public Quaternion fixedRotation = Quaternion.identity; // Set the fixed rotation for the obstacles

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
        obstacles.Add(obstacle);

        Debug.Log("Obstacle spawned at position: " + spawnPosition); // Log the spawn position
    }
}
