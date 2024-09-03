using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// WeaponChanger.cs
// This script handles the switching of weapons, sprites, and sounds based on key input

public class WeaponChanger : MonoBehaviour
{
    // Array of keycodes for weapon switching
    public KeyCode[] weaponInput;
    // Index of the currently selected weapon
    int weaponSelected = 0;
    // Arrays of GameObjects for weapons, sprites, and sounds
    public GameObject[] weapons, sprites, switchWeaponSound;

    void Start()
    {
        // Set initial weapon, sprite, and sound
        SwapWeapon(0,0,0);
        //SwapSprite(0);
        //SwapSound(0);
    }

    void Update()
    {
        // Check for key input and switch weapon if a different key is pressed
        for (int i = 0; i < weaponInput.Length; i++)
        {
            if (Input.GetKeyDown(weaponInput[i]) && weaponSelected != i)
            {
                SwapWeapon(i, i, i);
            }
        }
    }

    // Activate the weapon GameObject at the given index and deactivate the rest
    void SwapWeapon(int weaponType, int weaponSprite, int swapSound)
    {
        // Set active weapon
        SetActiveAtIndex(weapons, weaponType);
        // Set active sprite
        SetActiveAtIndex(sprites, weaponSprite);
        // Set active sound
        SetActiveAtIndex(switchWeaponSound, swapSound);
        // Update the selected weapon index
        weaponSelected = swapSound;
    }

    // Helper method to set active GameObject at given index and deactivate others
    private void SetActiveAtIndex(GameObject[] objects, int index)
    {
        // Loop through all objects in the array
        for (int i = 0; i < objects.Length; i++)
        {
            // Set active state based on whether the current index matches the given index
            objects[i].SetActive(i == index);
        }
    }
}


    /* // Activate the weapon Sprite&Sound at the given index and deactivate the rest
    // Activate the sprite GameObject at the given index and deactivate the rest
    void SwapSprite(int weaponSprite)
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].SetActive(i == weaponSprite);
        }
        weaponSelected = weaponSprite;
    }

    // Activate the sound GameObject at the given index and deactivate the rest
    void SwapSound(int swapSound)
    {
        for (int i = 0; i < switchWeaponSound.Length; i++)
        {
            switchWeaponSound[i].SetActive(i == swapSound);
        }
        weaponSelected = swapSound;
    }
    */
