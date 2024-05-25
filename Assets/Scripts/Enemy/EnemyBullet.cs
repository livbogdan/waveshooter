using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// EnemyBullet class
public class EnemyBullet : MonoBehaviour
{

    // Reference to the Player script
    Player playerScript;
    // Target position for the bullet to move towards
    Vector2 targetPosition;

    // Speed of the bullet
    public float speed;
    // Damage dealt by the bullet
    public int damage;

    // Effect to be instantiated when the bullet reaches the target
    public GameObject effect;

    // Health of the bullet
    int health = 1;

    // Start is called before the first frame update
    private void Start()
    {
        // Find the GameObject with the "Player" tag and get its Player component
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        // Set the target position to the player's position
        targetPosition = playerScript.transform.position;
    }


    // Update is called once per frame
    private void Update()
    {

        // If the bullet has reached the target position
        if ((Vector2)transform.position == targetPosition)
        {
            // Instantiate the effect at the bullet's position
            Instantiate(effect, transform.position, Quaternion.identity);
            // Destroy the bullet
            Destroy(gameObject);
        }
        else
        {
            // Move the bullet towards the target position
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }

    }


    // Called when the bullet enters a trigger collider
    private void OnTriggerEnter2D(Collider2D other)
    {

        // If the collider has the "Player" tag
        if (other.tag == "Player")
        {
            // Deal damage to the player
            playerScript.TakeDamage(damage);
            // Destroy the bullet
            Destroy(gameObject);
        }

    }

    // Method to handle taking damage
    public void TakeDamage(int amount)
    {
        // Reduce the bullet's health
        health -= amount;
        // If the bullet's health is 0 or less
        if (health <= 0)
        {
            // Destroy the bullet
            Destroy(this.gameObject);
        }

    }
}
