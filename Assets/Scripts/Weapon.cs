using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{  
    
    [Header("Weapon Sizes")]
    public float size_x;
    public float size_y;
    public float size_z;

    [Header("Weapon Properties")] 
    public Sprite weaponSprite;

    public int maxAmmo;
    protected int currentAmmo;
    
    public float reloadTime;
    public float coolDownTime;
    public float damage;

    [Header("Weapon Universal Flags")] public bool isReloading;
    public bool canFire;


    public abstract void Shoot();
    
    //Reload function
    public abstract IEnumerator Reload();
    
    public Sprite GetWeaponSprite() => weaponSprite;

}