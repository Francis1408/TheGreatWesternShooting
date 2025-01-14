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
    public float reloadTime;
    public float coolDownTime;

    protected int currentAmmo;

    public abstract void Shoot();
    
    public Sprite GetWeaponSprite() => weaponSprite;
   

    //Reload function
    public virtual void Reload()
    {
        currentAmmo = maxAmmo;
    }
    
}