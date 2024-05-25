using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{

    // Distance at which the enemy will stop moving towards the player
    public float stopDistance;
    // Prefab for the bullet that the enemy will shoot
    public GameObject enemyBullet;
    // Transform component for the point from which the bullet will be shot
    public Transform shotPoint;

    float attackTime;
    Animator anim;

    public override void Start()
    {
        base.Start();

        // Get the Animator component attached to this GameObject
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // Check if the player reference is not null
        if (player != null)
        {
            // If the distance between the enemy and the player is greater than the stop distance
            if (Vector2.Distance(transform.position, player.position) > stopDistance)
            {
                // Move the enemy towards the player's position
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }

            // If the current time is greater than or equal to the attack time
            if (Time.time >= attackTime)
            {
                // Set the next attack time
                attackTime = Time.time + timeBetweenAttacks;
                // Trigger the "attack" animation
                anim.SetTrigger("attack");
            }
        }
    }

    public void RangedAttack()
    {
        // Check if the player reference is not null
        if (player != null)
        {
            // Calculate the direction from the shot point to the player's position
            Vector2 direction = player.position - shotPoint.position;
            // Calculate the angle in degrees based on the direction
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            // Create a rotation quaternion based on the calculated angle
            Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            // Set the rotation of the shot point
            shotPoint.rotation = rotation;

            // Instantiate the enemy bullet at the shot point's position and rotation
            Instantiate(enemyBullet, shotPoint.position, shotPoint.rotation);
        }
    }
}
