using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    // Header for Shoot variables
    [Header("Shoot")]
    public GameObject projectile; // Reference to the projectile prefab
    public Transform shotPoint; // Transform for the shot point
    public float timeBetweenShots; // Time between shots

    private float shotTime; // Time of the last shot

    Animator cameraAnim; // Reference to the camera animator

    // Header for Score variables
    [Header("Score")]
    public static int score; // Static variable to keep track of the score
    public TextMeshProUGUI scoreText; // Reference to the score text UI element

    // Header for Ammunition variables
    [Header("Ammunition")]
    public int ammo; // Current ammunition count
    public TextMeshProUGUI ammoText; // Reference to the ammo text UI element

    private void Start()
    {
        cameraAnim = Camera.main.GetComponent<Animator>(); // Get the camera animator component
        score = 0; // Initialize the score to 0
    }

    public void Update()
    {
        // Calculate the direction and angle based on the mouse position
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = rotation; // Rotate the weapon towards the mouse position

        // Shoot if the left mouse button is clicked and there is ammo
        if (Input.GetMouseButtonDown(0) && (ammo > 0))
        {
            if (Time.time >= shotTime)
            {
                Instantiate(projectile, GameObject.FindGameObjectWithTag("Weapon").transform.position, transform.rotation); // Instantiate the projectile
                cameraAnim.SetTrigger("shake"); // Trigger the camera shake animation
                shotTime = Time.time + timeBetweenShots; // Set the next shot time
            }
            ammo--; // Decrease the ammo count
        }

        ammoText.text = ammo.ToString(); // Update the ammo text UI element
        scoreText.text = "Killed " + score.ToString(); // Update the score text UI element
    }

    void OnTriggerEnter2D(Collider2D otherObject)
    {
        // Check for different weapon pickups and add ammo accordingly
        if (otherObject.tag == "Weapon1")
        {
            ammo += 20;
            Destroy(otherObject.gameObject);
        }

        if (otherObject.tag == "Weapon2")
        {
            ammo += 15;
            Destroy(otherObject.gameObject);
        }

        if (otherObject.tag == "Weapon3")
        {
            ammo += 10;
            Destroy(otherObject.gameObject);
        }

        if (otherObject.tag == "Weapon4")
        {
            ammo += 5;
            Destroy(otherObject.gameObject);
        }
    }

}