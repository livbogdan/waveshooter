using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// RandomSound.cs
// This script plays a random audio clip from an array of clips

public class RandomSound : MonoBehaviour
{
    // Reference to the AudioSource component
    private AudioSource source;

    // Array of audio clips to choose from
    public AudioClip[] clips;

    private void Start()
    {
        // Get the AudioSource component attached to this GameObject
        source = GetComponent<AudioSource>();

        // Generate a random index within the range of the clips array
        int randomNumber = Random.Range(0, clips.Length);

        // Assign the randomly selected clip to the AudioSource
        source.clip = clips[randomNumber];

        // Play the audio clip
        source.Play();
    }
}
