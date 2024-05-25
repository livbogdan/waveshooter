using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Boss class that manages the boss's health, enemy spawning, and damage dealing
public class Boss : MonoBehaviour
{

    // Health value of the boss, ranging from 1 to 100
    [Tooltip("Healt from 1 to 100")]
    public int health;

    // Array of enemies that will spawn when the player fights the boss
    [Tooltip("How much and what type of enemys gonna span when player fight with Boss")]
    public Enemy[] enemies;

    // Time offset for enemy spawning
    [Tooltip("Enemy spawn time")]
    public float spawnOffset;

    // Half of the boss's health value
    private int halfHealth;

    // Reference to the boss's Animator component
    private Animator anim;

    // Amount of damage the boss deals to the player on collision
    [Tooltip("Amount of dealing damage to player when boss collide with player")]
    public int damage;

    // Visual effects for blood and other effects
    [Header("VFX")]
    public GameObject blood;
    public GameObject effect;

    // Reference to the health bar UI element
    private Slider healthBar;

    // Reference to the SceneTransition component for loading scenes
    private SceneTransition sceneTransitions;

    // Initialization method
    private void Start()
    {
        // Calculate half of the boss's health
        halfHealth = health / 2;

        // Get the Animator component attached to the boss
        anim = GetComponent<Animator>();

        // Find the Slider component for the health bar
        healthBar = FindObjectOfType<Slider>();

        // Set the maximum value of the health bar to the boss's health
        healthBar.maxValue = health;

        // Set the initial value of the health bar to the boss's health
        healthBar.value = health;

        // Find the SceneTransition component
        sceneTransitions = FindObjectOfType<SceneTransition>();
    }

    // Method for handling damage taken by the boss
    public void TakeDamage(int amount)
    {
        // Reduce the boss's health by the specified amount
        health -= amount;

        // Update the health bar value
        healthBar.value = health;

        // Check if the boss's health is depleted
        if (health <= 0)
        {
            // Instantiate the effect and blood visual effects
            Instantiate(effect, transform.position, Quaternion.identity);
            Instantiate(blood, transform.position, Quaternion.identity);

            // Destroy the boss game object
            Destroy(this.gameObject);

            // Disable the health bar UI element
            healthBar.gameObject.SetActive(false);

            // Load the "Win" scene
            sceneTransitions.LoadScene("Win");
        }

        // Check if the boss's health has reached or fallen below half health
        if (health <= halfHealth)
        {
            // Trigger the "stage2" animation
            anim.SetTrigger("stage2");
        }

        // Spawn a random enemy from the enemies array
        Enemy randomEnemy = enemies[Random.Range(0, enemies.Length)];
        Instantiate(randomEnemy, transform.position + new Vector3(spawnOffset, spawnOffset, 0), transform.rotation);
    }

    // Method for handling collisions with the player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collision is with the player
        if (collision.tag == "Player")
        {
            // Deal damage to the player
            collision.GetComponent<Player>().TakeDamage(damage);
        }
    }
}
