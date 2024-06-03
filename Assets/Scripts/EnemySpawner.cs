using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // The enemy prefab to spawn
    public Transform spawnPoint; // The point where enemies will spawn
    private bool playerInZone = false; // Flag to check if player is in the trigger zone

    // When something enters the trigger zone
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the object is the player
        {
            playerInZone = true;
            SpawnEnemy();
        }
    }

    // When something exits the trigger zone
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
        }
    }

    // Method to spawn an enemy
    private void SpawnEnemy()
    {
        if (enemyPrefab != null && playerInZone)
        {
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
