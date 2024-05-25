using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// SceneTransition.cs
// This script handles scene transitions with an animation
public class SceneTransition : MonoBehaviour
{

    // Reference to the Animator component for the transition animation
    private Animator transitionAnim;

    // Start is called before the first frame update
    private void Start()
    {
        // Get the Animator component attached to this GameObject
        transitionAnim = GetComponent<Animator>();
    }

    // Load a new scene with a transition animation
    public void LoadScene(string sceneName)
    {
        // Start the coroutine to handle the transition
        StartCoroutine(Transition(sceneName));
    }

    // Coroutine to play the transition animation and load the new scene
    IEnumerator Transition(string sceneName)
    {
        // Trigger the "end" animation parameter
        transitionAnim.SetTrigger("end");
        // Wait for 1 second while the animation plays
        yield return new WaitForSeconds(1);
        // Load the new scene
        SceneManager.LoadScene(sceneName);
    }

    // Reload the current scene with a transition animation
    public void ReloadScene()
    {
        // Start the coroutine to handle the transition
        StartCoroutine(Transition(SceneManager.GetActiveScene().name));
    }

    // Quit the application
    public void QuitGame()
    {
        Application.Quit();
    }

}
