using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    // Speed of the projectile
    public float speed;
    // Lifetime of the projectile before it is destroyed
    public float lifeTime;
    // Damage dealt by the projectile
    public int damage;

    // Explosion effect to be instantiated when the projectile is destroyed
    public GameObject explosion;

    // Sound effect to be instantiated when the projectile is fired
    public GameObject soundObject;

    // Trail effect to be instantiated behind the projectile
    public GameObject trail;
    private float timeBtwTrail;
    public float startTimeBtwTrail;

    private void Start()
    {
        // Invoke the DestroyProjectile method after the specified lifeTime
        Invoke("DestroyProjectile", lifeTime);
        // Instantiate the sound effect at the projectile's position and rotation
        Instantiate(soundObject, transform.position, transform.rotation);
        // Instantiate the explosion effect at the projectile's position
        Instantiate(explosion, transform.position, Quaternion.identity);
    }

    private void Update()
    {
        // Check if it's time to instantiate a new trail effect
        if (timeBtwTrail <= 0)
        {
            // Instantiate the trail effect at the projectile's position
            Instantiate(trail, transform.position, Quaternion.identity);
            // Reset the timer for the next trail effect
            timeBtwTrail = startTimeBtwTrail;
        }
        else
        {
            // Decrement the timer for the next trail effect
            timeBtwTrail -= Time.deltaTime;
        }

        // Move the projectile upwards based on its speed and the time since the last frame
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void DestroyProjectile()
    {
        // Instantiate the explosion effect at the projectile's position
        Instantiate(explosion, transform.position, Quaternion.identity);
        // Destroy the projectile game object
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the projectile collided with an enemy
        if (other.tag == "Enemy")
        {
            // Deal damage to the enemy
            other.GetComponent<Enemy>().TakeDamage(damage);
            // Destroy the projectile
            DestroyProjectile();
        }

        // Check if the projectile collided with a boss
        if (other.tag == "boss")
        {
            // Deal damage to the boss
            other.GetComponent<Boss>().TakeDamage(damage);
            // Destroy the projectile
            DestroyProjectile();
        }

        // Check if the projectile collided with an enemy bullet
        if (other.tag == "EnemyBullet")
        {
            // Deal damage to the enemy bullet
            other.GetComponent<EnemyBullet>().TakeDamage(damage);
            // Destroy the projectile
            DestroyProjectile();
        }
    }
}
