/// <summary>
/// The WaveSpawner class is responsible for spawning waves of enemies in a game. It manages the configuration of waves, including the enemies to spawn, the count of enemies, and the time between spawns. It also handles the transition between waves and the spawning of a boss enemy.
/// </summary>
/// 

// This script manages the spawning of enemy waves and the boss
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    // Serializable class to store wave data
    [System.Serializable]
    public class Wave
    {
        [Header("Enemies")]

        public GameObject[] enemies; // Array of enemy prefabs
        public int count; // Number of enemies to spawn
        public float timeBetweenSpawns; // Time between spawning each enemy

    }

    [Header("Enemy Spawn Configuration")]

    public Wave[] waves; // Array of wave configurations
    [Space]
    public Transform[] spawnPoints; // Array of spawn points for enemies
    [Space]
    public float timeBetweenWaves; // Time between each wave


    private Wave currentWave; // Current wave being spawned
    private int currentWaveIndex; // Index of the current wave
    private Transform player; // Reference to the player's transform

    private bool spawningFinished; // Flag to indicate if spawning is finished

    [Header("Boss Configuration")]
    public GameObject boss; // Boss prefab
    public Transform bossSpawnPoint; // Spawn point for the boss
    public GameObject healthBar; // Health bar UI element

    [Header("Text")]
    public TextMeshProUGUI waveText; // UI text for displaying wave number
    public TextMeshProUGUI countdownText; // UI text for displaying countdown

    public float countdownDuration = 10f; // Duration of the countdown before spawning the next wave


    private void Start()
    {
        countdownDuration = 10f; // Reset countdown duration
        player = GameObject.FindWithTag("Player").transform; // Get the player's transform
        StartCoroutine(CallNextWave(currentWaveIndex)); // Start spawning the first wave
    }

    private void Update()
    {
        countdownDuration -= Time.deltaTime; // Update countdown

        countdownText.text = "Countdown: " + Mathf.Ceil(countdownDuration).ToString(); // Update countdown text

        if (countdownDuration <= 0)
        {
            if (spawningFinished == true && GameObject.FindGameObjectsWithTag("Enemy").Length == 1)
            {
                spawningFinished = false;
                if (currentWaveIndex + 1 < waves.Length)
                {
                    currentWaveIndex++;
                    StartCoroutine(CallNextWave(currentWaveIndex)); // Start spawning the next wave

                }
                else
                {
                    Instantiate(boss, bossSpawnPoint.position, bossSpawnPoint.rotation); // Spawn the boss
                    healthBar.SetActive(true); // Show the health bar

                }

            }

            waveText.text = "Wave " + currentWaveIndex.ToString(); // Update wave text
        }

    }

    #region Spawn Methods
    IEnumerator CallNextWave(int waveIndex)
    {
        yield return new WaitForSeconds(timeBetweenWaves); // Wait before spawning the next wave
        StartCoroutine(SpawnWave(waveIndex)); // Start spawning the wave
    }

    IEnumerator SpawnWave(int waveIndex)
    {
        currentWave = waves[waveIndex]; // Get the current wave configuration

        for (int i = 0; i < currentWave.count; i++)
        {

            if (player == null)
            {
                yield break; // Exit if the player is null
            }
            GameObject randomEnemy = currentWave.enemies[Random.Range(0, currentWave.enemies.Length)]; // Get a random enemy prefab
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)]; // Get a random spawn point
            Instantiate(randomEnemy, randomSpawnPoint.position, transform.rotation); // Spawn the enemy

            if (i == currentWave.count - 1)
            {
                spawningFinished = true; // Set spawning finished flag if this is the last enemy in the wave

            }
            else
            {
                spawningFinished = false; // Reset spawning finished flag

            }

            yield return new WaitForSeconds(currentWave.timeBetweenSpawns); // Wait before spawning the next enemy

        }


    }
    #endregion

}
