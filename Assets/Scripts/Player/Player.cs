using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public float speed; // Player movement speed

    private Rigidbody2D rb; // Reference to the player's Rigidbody2D component

    private Vector2 moveAmount; // Amount to move the player
    private Animator anim; // Reference to the player's Animator component

    [Header("Health Configuration")]
    public int health; // Player's current health    
    public GameObject[] hearts; // Array of heart UI elements
    public Sprite fullHeart; // Sprite for a full heart
    public Sprite emptyHeart; // Sprite for an empty heart

    [Header("Hurt Configuration")]
    public Animator hurtAnim; // Reference to the hurt animation    
    public GameObject hurtSound; // Prefab for the hurt sound effect

    private SceneTransition sceneTransitions; // Reference to the SceneTransition script

    [Header("Trail")]
    public GameObject trail; // Prefab for the trail effect
    private float timeBtwTrail; // Timer for spawning trail effects
    public float startTimeBtwTrail; // Initial value for the trail timer
    public Transform groundPos; // Position to spawn trail effects
    public GameObject pausePanel; // Reference to the pause panel UI element


    [Header("Pause")]
    public bool paused = false; // Flag to track if the game is paused

    private void Start()
    {
        /*----------------------------------------------------*/
        anim = GetComponent<Animator>(); // Get the Animator component
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
        sceneTransitions = FindObjectOfType<SceneTransition>(); // Find the SceneTransition script
    }

    void Update()
    {
        /*-----------------PAUSE-----------------*/

        // Toggle pause state when the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
        }
        // If the game is paused
        if (paused == true)
        {
            Time.timeScale = 0f; // Freeze time
            pausePanel.SetActive(true); // Show the pause panel            
        }
        else // If the game is not paused
        {
            Time.timeScale = 1f; // Unfreeze time
            pausePanel.SetActive(false); // Hide the pause panel            
        }
        /*-----------------PAUSE-----------------*/

        /*-----------------MOVEMENT--------------*/
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); // Get the raw movement input
        moveAmount = moveInput.normalized * speed; // Calculate the movement amount
        if (moveInput != Vector2.zero) // If the player is moving
        {

            if (timeBtwTrail <= 0) // If it's time to spawn a trail effect
            {
                Instantiate(trail, groundPos.position, Quaternion.identity); // Spawn a trail effect
                timeBtwTrail = startTimeBtwTrail; // Reset the trail timer
            }
            else
            {
                timeBtwTrail -= Time.deltaTime; // Decrement the trail timer
            }
            anim.SetBool("isRunning", true); // Set the "isRunning" animation parameter to true
        }
        else // If the player is not moving
        {
            anim.SetBool("isRunning", false); // Set the "isRunning" animation parameter to false
        }
        /*-----------------MOVEMENT--------------*/
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime); // Move the player
    }

    public void TakeDamage(int amount) // Function to handle taking damage
    {
        Instantiate(hurtSound, transform.position, Quaternion.identity); // Spawn the hurt sound effect
        health -= amount; // Decrease the player's health
        UpdateHealthUI(health); // Update the health UI
        hurtAnim.SetTrigger("hurt"); // Trigger the hurt animation
        if (health <= 0) // If the player's health is 0 or less
        {
            Destroy(this); // Destroy the player object
            sceneTransitions.LoadScene("Lose"); // Load the "Lose" scene
        }
    }

    void UpdateHealthUI(int currentHealth) // Function to update the health UI
    {
        for (int i = 0; i < hearts.Length; i++) // Loop through the heart UI elements
        {
            if (i < currentHealth) // If the current index is less than the current health
            {
                hearts[i].GetComponent<Image>().sprite = fullHeart; // Set the heart sprite to the full heart sprite
            }
            else
            {
                hearts[i].GetComponent<Image>().sprite = emptyHeart; // Set the heart sprite to the empty heart sprite
            }

        }
    }

    public void Heal(int healAmount) // Function to heal the player
    {
        if (health + healAmount > 5) // If the new health would be greater than 5
        {
            health = 5; // Set the health to 5
        }
        else // If the new health is less than or equal to 5
        {
            health += healAmount; // Increase the health by the heal amount
        }
        UpdateHealthUI(health); // Update the health UI
    }

}