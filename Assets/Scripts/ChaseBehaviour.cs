using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ChaseBehaviour.cs
// This class controls the chase behavior of an enemy character

public class ChaseBehaviour : StateMachineBehaviour
{

    // Reference to the player GameObject
    private GameObject player;
    // Speed at which the enemy moves towards the player
    public float speed;


    // Called when the chase state is entered
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Find the GameObject with the "Player" tag
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Called every frame while in the chase state
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Check if the player reference is not null
        if (player != null)
        {
            // Move the enemy towards the player's position
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    // Called when the chase state is exited
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
