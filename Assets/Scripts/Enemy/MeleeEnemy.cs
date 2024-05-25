using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeEnemy : Enemy {

    public float stopDistance; // The distance at which the enemy stops chasing the player

    private float attackTime; // The time when the enemy last attacked

    public float attackSpeed; // The speed at which the enemy moves during an attack

    private void Update()
    {
         
        if(player != null) { // Check if the player reference is not null

            if (Vector2.Distance(transform.position, player.position) > stopDistance)
            {
                // If the distance between the enemy and the player is greater than the stop distance
                // Move the enemy towards the player
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            } else {

                if (Time.time >= attackTime)
                {
                    // If the current time is greater than or equal to the attack time
                    // Set the next attack time and start the attack coroutine
                    attackTime = Time.time + timeBetweenAttacks;
                    StartCoroutine(Attack());
                }

            }

        }

    }

    IEnumerator Attack() {

        // Deal damage to the player
        player.GetComponent<Player>().TakeDamage(damage);

        Vector2 originalPosition = transform.position; // Store the enemy's original position
        Vector2 targetPosition = player.position; // Get the player's position

        float percent = 0f; // Initialize the interpolation percentage
        while(percent <= 1) { // Loop until the interpolation is complete

            percent += Time.deltaTime * attackSpeed; // Increment the interpolation percentage
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4; // Calculate the interpolation value
            transform.position = Vector2.Lerp(originalPosition, targetPosition, interpolation); // Move the enemy towards the player
            yield return null; // Wait for the next frame

        }

    }

}
