using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Health of the enemy
    public int health;
    [HideInInspector]
    public Transform player;

    // Movement speed of the enemy
    public float speed;
    // Time between attacks
    public float timeBetweenAttacks;
    // Damage dealt by the enemy
    public int damage;

    // Score awarded for destroying the enemy
    public int scoreForDestroy;

    [Header("Health Pickup")]

    // Chance of dropping a health pickup and weapon
    [Range(0f, 100f)]
    public int healthPickupChance;
    // Health pickup prefab
    public GameObject healthPickup;

    [Header("[Ammo]")]

    public GameObject ammoPickup;

    [Range(0, 100)]
    public int ammoPickupChance;

    // Death effect prefab
    public GameObject deathEffect;

    // Initialize the player transform
    public virtual void Start()
    {
        // Find the GameObject with the "Player" tag and get its Transform component
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Handle damage taken by the enemy
    public void TakeDamage(int amount)
    {
        // Reduce the enemy's health by the specified amount
        health -= amount;
        if (health <= 0)
        {
            // Drop a health pickup if chance is met
            DropHealthPickup();

            // Drop an ammo pickup if chance is met
            AmmoPickup();

            // Instantiate the death effect and destroy the enemy
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            // Increment the score when the enemy is destroyed
            Weapon.score++;
        }
    }

    // Drop a health pickup based on the given chance
    private void DropHealthPickup()
    {
        // Generate a random number between 0 and 100
        int random = Random.Range(0, 101);
        // If the random number is less than the healthPickupChance, instantiate the health pickup
        if (random < healthPickupChance)
        {
            Instantiate(healthPickup, transform.position, transform.rotation);
        }
    }

    private void AmmoPickup()
    {
        // Generate a random number between 0 and 100
        int random = Random.Range(0, 101);
        // If the random number is less than the ammoPickupChance, instantiate the ammo pickup
        if (random < ammoPickupChance)
        {
            Instantiate(ammoPickup, transform.position, transform.rotation);
        }
    }
}
