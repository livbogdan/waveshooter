using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// HealthPickup.cs
// This script controls the health pickup behavior

public class HealthPickup : MonoBehaviour
{

    // Reference to the Player script
    Player playerScript;

    // Amount of health to be added when picked up
    public int healAmount;

    // Effect to be instantiated when picked up
    public GameObject effect;

    // Find the Player GameObject and get its Player component
    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // When the player collides with the health pickup
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collider is the Player
        if (collision.tag == "Player")
        {
            // Instantiate the effect at the pickup's position
            Instantiate(effect, transform.position, Quaternion.identity);

            // Call the Heal method on the Player script and pass the healAmount
            playerScript.Heal(healAmount);

            // Destroy the health pickup GameObject
            Destroy(gameObject);
        }
    }

}
