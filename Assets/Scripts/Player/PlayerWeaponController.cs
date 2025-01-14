using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public Weapon currentWeapon;
    public SpriteRenderer weaponSpriteRenderer;

    public void EquipWeapon(Weapon newWeapon)
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject); // Remove the old weapon
        }

        currentWeapon = Instantiate(newWeapon, transform); // Equip the new weapon
        weaponSpriteRenderer.sprite = currentWeapon.GetWeaponSprite(); // Update sprite
    }    
    
    
    
}
