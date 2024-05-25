using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class represents an enemy that can summon other enemies
public class Summoner : Enemy
{

    public float minX; // Minimum X coordinate for the summoner's target position
    public float maxX; // Maximum X coordinate for the summoner's target position
    public float minY; // Minimum Y coordinate for the summoner's target position
    public float maxY; // Maximum Y coordinate for the summoner's target position

    Vector2 targetPosition; // The target position for the summoner to move towards

    Animator anim; // Reference to the Animator component attached to the summoner

    public float stopDistance; // Distance at which the summoner stops moving and starts attacking

    private float attackTime; // Time when the summoner can attack again

    public float attackSpeed; // Speed at which the summoner attacks

    public Enemy enemyToSummon; // Prefab of the enemy to be summoned
    public float timeBetweenSummons; // Time between summoning new enemies

    private float summonTime; // Time when the summoner can summon a new enemy


    public override void Start() // Overridden Start method from the base Enemy class
    {
        base.Start(); // Call the Start method of the base class
        float randomX = Random.Range(minX, maxX); // Generate a random X coordinate within the specified range
        float randomY = Random.Range(minY, maxY); // Generate a random Y coordinate within the specified range
        targetPosition = new Vector2(randomX, randomY); // Set the target position using the random coordinates
        anim = GetComponent<Animator>(); // Get a reference to the Animator component attached to the summoner
    }


    private void Update() // Update method called every frame
    {
        if (player != null) // Check if the player reference is not null
        {
            if ((Vector2)transform.position != targetPosition) // Check if the summoner is not at the target position
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime); // Move the summoner towards the target position
                anim.SetBool("isRunning", true); // Set the "isRunning" parameter in the Animator to true
            }

            else // If the summoner is at the target position
            {
                anim.SetBool("isRunning", false); // Set the "isRunning" parameter in the Animator to false

                if (Time.time >= summonTime) // Check if it's time to summon a new enemy
                {
                    summonTime = Time.time + timeBetweenSummons; // Set the next summon time
                    anim.SetTrigger("summon"); // Trigger the "summon" animation
                }

            }

            if (Vector2.Distance(transform.position, player.position) <= stopDistance) // Check if the player is within the stop distance
            {
                if (Time.time >= attackTime) // Check if it's time to attack
                {
                    attackTime = Time.time + timeBetweenAttacks; // Set the next attack time
                    StartCoroutine(Attack()); // Start the attack coroutine
                }
            }

        }
    }

    public void Summon() // Method called when the summoner summons a new enemy
    {
        if (player != null) // Check if the player reference is not null
        {
            Instantiate(enemyToSummon, transform.position, transform.rotation); // Instantiate the enemy prefab at the summoner's position and rotation
        }
    }

    IEnumerator Attack() // Coroutine for attacking the player
    {

        player.GetComponent<Player>().TakeDamage(damage); // Deal damage to the player    

        Vector2 originalPosition = transform.position; // Store the original position of the summoner
        Vector2 targetPosition = player.position; // Set the target position to the player's position

        float percent = 0f; // Initialize the percent variable to 0
        while (percent <= 1) // Loop until percent reaches 1
        {
            percent += Time.deltaTime * attackSpeed; // Increment percent based on the attack speed
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4; // Calculate the interpolation value
            transform.position = Vector2.Lerp(originalPosition, targetPosition, interpolation); // Interpolate the summoner's position between the original and target positions
            yield return null; // Wait for the next frame
        }

    }

}
