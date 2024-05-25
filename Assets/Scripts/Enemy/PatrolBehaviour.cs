using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehaviour : StateMachineBehaviour
{

    // Speed at which the object moves
    public float speed;

    // Array to store patrol points
    private GameObject[] patrolPoints;

    // Index of the current patrol point
    int randomPoint;

    // Called when the state is entered
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Find all game objects with the tag "point" and store them in the patrolPoints array
        patrolPoints = GameObject.FindGameObjectsWithTag("point");
        // Randomly select a patrol point from the array
        randomPoint = Random.Range(0, patrolPoints.Length);
    }

    // Called on each frame while the state is active
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Move the object towards the current patrol point
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, patrolPoints[randomPoint].transform.position, speed * Time.deltaTime);

        // If the object is close enough to the current patrol point
        if (Vector2.Distance(animator.transform.position, patrolPoints[randomPoint].transform.position) < 0.1f)
        {
            // Select a new random patrol point
            randomPoint = Random.Range(0, patrolPoints.Length);
        }

    }

    // Called when the state is exited
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
