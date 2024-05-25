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
    [SerializeField] public GameObject[] weapons, sprites, switchWeaponSound;

    void Start()
    {
        // Set initial weapon, sprite, and sound
        SwapWeapon(0);
        SwapSprite(0);
        SwapSound(0);
    }

    void Update()
    {
        // Check for key input and switch weapon if a different key is pressed
        for (int i = 0; i < weaponInput.Length; i++)
        {
            if (Input.GetKeyDown(weaponInput[i]) && weaponSelected != i)
            {
                SwapWeapon(i);
                SwapSprite(i);
                SwapSound(i);
            }
        }
    }

    // Activate the weapon GameObject at the given index and deactivate the rest
    void SwapWeapon(int weaponType)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(i == weaponType);
        }
        weaponSelected = weaponType;
    }

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
}
