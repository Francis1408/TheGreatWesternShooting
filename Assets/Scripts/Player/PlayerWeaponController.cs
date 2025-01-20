using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    
    // Singleton instance of GameManager
    public static PlayerWeaponController Instance { get; private set; }
    
    public Weapon currentWeapon;
    public List<Weapon> weaponsOwned;
    
    
    private void Awake()
    {
        // Ensure there is only one instance of the WeaponController
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object between scene loads
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentWeapon = GetComponentInChildren<Weapon>();
    }


    /*
    public void EquipWeapon(Weapon newWeapon)
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject); // Remove the old weapon
        }

        currentWeapon = Instantiate(newWeapon, transform); // Equip the new weapon
        weaponSpriteRenderer.sprite = currentWeapon.GetWeaponSprite(); // Update sprite
    }    
    */
    
    
}
