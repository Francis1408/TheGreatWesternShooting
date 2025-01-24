using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    // Start is called before the first frame update
    private float timer;
    void Start()
    {
        // Setting up the flags at the beginning 
        canFire = true;
        isReloading = false;
        
        // Starts loaded 
        currentAmmo = ammoCapacity;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetMouseButtonDown(0)) Shoot(); // Handles shooting
        
        if (Input.GetKeyDown(KeyCode.R) && !isReloading) StartCoroutine(Reload()); // Handles reloading
        
        if (!canFire && !isReloading)
        {
            timer += Time.deltaTime;
            if (timer > coolDownTime)
            {
                canFire = true;
                timer = 0;
            }
        }
        
    }

    public override void Shoot()
    {
       if (currentAmmo != 0) // If magazine is not empty
       {
            if (canFire)
            {
                Debug.Log("Pew");
                canFire = false;
                CameraShake.Instance.ShakeCamera(3f, .1f); // Shakes the camera 
                
                // Decreases ammo
                currentAmmo--;
                NotifyCurrentAmmoChange(); // Callback to notify UI's that the currentAmmoHasChanged
                
                // Calculate the distance between the bulletTransform and the mousePointer
                
            }
            
       }
       else
       {
            // Make empty magazine animation
            Debug.Log("Magazine is empty! Reload");
       }
    }

    public override IEnumerator Reload()
    {
        // No need to reload
        if (currentAmmo == ammoCapacity)
        {
            Debug.Log("Ammo full no need to reload");
            yield break;
        }
        
        // Makes the reload action
        canFire = false; // Interrupts firing 
        isReloading = true;
        Debug.Log("Is reloading");
        yield return new WaitForSeconds(reloadTime);
        
        // Reload complete
        Debug.Log("Reload Complete");
        currentAmmo = ammoCapacity;
        NotifyCurrentAmmoChange(); // Callback to notify UI's that the currentAmmoHasChanged
        isReloading = false;
        canFire = true; // Can fire immediately after the reload
    }
}
