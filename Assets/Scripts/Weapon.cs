using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    
    // Callbacks when the ammo change
    public delegate void AmmoChanged(int currentAmmo);
    public event AmmoChanged OnAmmoChanged;
    
    
    [Header("Weapon Sizes")]
    public float size_x;
    public float size_y;
    public float size_z;
    
    [Header("Weapon Sprites")]
    public Sprite weaponSprite;
    public GameObject weaponAmmoSprite;


    [Header("Weapon Properties")] 
    public string gunName;
    
    public int ammoCapacity;
    public int currentAmmo;
    public int totalAmmo;
    
    public float reloadTime;
    public float coolDownTime;
    public float damage;
    

    [Header("Weapon Universal Flags")] public bool isReloading;
    public bool canFire;

    
    public abstract void Shoot();
    
    //Reload function
    public abstract IEnumerator Reload();
    
    public Sprite GetWeaponSprite() => weaponSprite;
    
    // Method to notify the bullet change
    protected void NotifyCurrentAmmoChange()
    {
        OnAmmoChanged?.Invoke(currentAmmo);
    }
    
}