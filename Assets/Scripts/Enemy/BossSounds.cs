using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// BossSounds.cs
// This script is responsible for playing random sound effects for a boss enemy

public class BossSounds : MonoBehaviour
{

    // Reference to the AudioSource component attached to this GameObject
    private AudioSource source;

    // Array of AudioClips to choose from
    public AudioClip[] clips;

    // Time in seconds between playing sound effects
    public float timeBetweenSoundEffects;
    private float nextSoundEffectTime;


    private void Start()
    {
        // Get the AudioSource component attached to this GameObject
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Check if it's time to play a sound effect
        if (Time.time >= nextSoundEffectTime)
        {
            // Choose a random sound effect from the clips array
            int randomNumber = Random.Range(0, clips.Length);
            source.clip = clips[randomNumber];

            // Play the chosen sound effect
            source.Play();

            // Calculate the next time a sound effect should be played
            nextSoundEffectTime = Time.time + timeBetweenSoundEffects;
        }
    }
}
