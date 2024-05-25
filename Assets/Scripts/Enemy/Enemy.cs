using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    // Health of the enemy
    public int health;
    [HideInInspector]
    public Transform player;

    // Speed of the enemy
    public float speed;
    // Time between enemy attacks
    public float timeBetweenAttacks;
    // Damage dealt by the enemy
    public int damage;

    // Score awarded for destroying the enemy
    public int scoreForDestroy;

    [Header("Weapon")]
    // Chance of dropping weapon01 when destroyed
    public int pickupChanceWeapon01;
    public GameObject weapon01;
    // Chance of dropping weapon02 when destroyed
    public int pickupChanceWeapon02;
    public GameObject weapon02;
    // Chance of dropping weapon03 when destroyed
    public int pickupChanceWeapon03;
    public GameObject weapon03;
    // Chance of dropping weapon04 when destroyed
    public int pickupChanceWeapon04;
    public GameObject weapon04;

    // Chance of dropping health pickup when destroyed
    public int healthPickupChance;
    public GameObject healthPickup;

    // Effect to spawn when enemy is destroyed
    public GameObject deathEffect;

    public virtual void Start()
    {
        // Find the player game object
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void TakeDamage(int amount)
    {
        // Reduce enemy health by the amount of damage taken
        health -= amount;
        if (health <= 0)
        {
            // Random chance to drop weapon01
            int weapon1 = Random.Range(0, 101);
            if (weapon1 < pickupChanceWeapon01)
            {
                Instantiate(weapon01, transform.position, transform.rotation);
            }

            // Random chance to drop weapon02
            int weapon2 = Random.Range(0, 101);
            if (weapon2 < pickupChanceWeapon02)
            {
                Instantiate(weapon02, transform.position, transform.rotation);
            }

            // Random chance to drop weapon03
            int weapon3 = Random.Range(0, 101);
            if (weapon3 < pickupChanceWeapon03)
            {
                Instantiate(weapon03, transform.position, transform.rotation);
            }

            // Random chance to drop weapon04
            int weapon4 = Random.Range(0, 101);
            if (weapon4 < pickupChanceWeapon04)
            {
                Instantiate(weapon04, transform.position, transform.rotation);
            }

            // Random chance to drop health pickup
            int randHealth = Random.Range(0, 101);
            if (randHealth < healthPickupChance)
            {
                Instantiate(healthPickup, transform.position, transform.rotation);
            }

            // Spawn death effect and destroy the enemy game object
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            Weapon.score++;
        }
    }

}
