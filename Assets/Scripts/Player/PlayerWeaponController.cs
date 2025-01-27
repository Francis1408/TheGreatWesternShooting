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
    public int currentIndex;
    
    
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
        // Disable all weapons
        foreach (Weapon weapon in GetComponentsInChildren<Weapon>())
        {
            weaponsOwned.Add(weapon);
            weapon.gameObject.SetActive(false);
            weapon.enabled = false;
        }
        
        // Set current index to 0 and enable first weapon in the list
        currentIndex = 0;
        currentWeapon = weaponsOwned[0];
        currentWeapon.gameObject.SetActive(true);
        currentWeapon.enabled = true;
    }

    public void SwitchWeapon()
    {
        for (int i = 0; i < weaponsOwned.Count; i++) // Disable all the weapons unused 
        {
            if (i != currentIndex)
            {
                weaponsOwned[i].gameObject.SetActive(false);
                weaponsOwned[i].enabled = false;
            }
        }
        // Enable the current weapon
        currentWeapon = weaponsOwned[currentIndex];
        currentWeapon.gameObject.SetActive(true);
        currentWeapon.enabled = true;
        
        // Update the weapon display 
        WeaponUIManager.Instance.InitializeWeaponDisplay();
    }

    private void Update()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f && !currentWeapon.isReloading)  // ScrollUp
        {
            if (currentIndex + 1 > weaponsOwned.Count - 1)
            {
                currentIndex = 0;
            }
            else
            {
                currentIndex++;
            }
            SwitchWeapon();
        }

        if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f && !currentWeapon.isReloading) // ScrollDown
        {
            if (currentIndex <= 0)
            {
                currentIndex =  weaponsOwned.Count - 1;
            }
            else
            {
                currentIndex--;
            }
            SwitchWeapon();
        } 
    }
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
    
    

