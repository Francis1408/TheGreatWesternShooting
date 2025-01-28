using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : Weapon
{
    private float timer;
    public Transform bulletTransform;
    public GameObject bulletTrail;
    public Animator gunAnimator;

    public LayerMask ignoreLayerMask;


    private void Start()
    {
        // Setting up the flags at the beginning 
        canFire = true;
        isReloading = false;
        
        // Starts loaded 
        currentAmmo = ammoCapacity;
        totalAmmo = -1;
    }


    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetMouseButtonDown(0)) Shoot(); // Handles shooting
        
        // Cant reload while dashing 
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && !Player.Instance.isDashing) StartCoroutine(Reload()); // Handles reloading
        
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
            if (!canFire) return;
            
            
            canFire = false;
            gunAnimator.SetTrigger("Shoot"); // Plays the revolver shootanimation
                
            CameraShake.Instance.ShakeCamera(3f, .1f); // Shakes the camera 
                
            // Decreases ammo
            currentAmmo--;
            NotifyCurrentAmmoChange(); // Callback to notify UI's that the currentAmmoHasChanged
                
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
            // Calculate the direction from the bullet transform to the mouse position
            Vector2 direction = (mouseWorldPosition - bulletTransform.position).normalized;
                
               
            // Calculate the distance between the bulletTransform and the mousePointer
            float distance = Vector2.Distance(bulletTransform.position, mouseWorldPosition);
                
            // Perform the raycast in the calculated direction
            var hit = Physics2D.Raycast(bulletTransform.position, direction, distance , ignoreLayerMask);
            

            var trail = Instantiate(
                bulletTrail,
                bulletTransform.position,
                Quaternion.FromToRotation(Vector3.up, direction));

            BulletTrail trailScript = trail.GetComponent<BulletTrail>();

            if (hit.collider != null)
            {
                trailScript.SetTargetPosition(hit.point);
                HitManager(hit);
               // Debug.Log($"Hit {hit.collider.name} at {hit.point}");
                   
            }
            else
            {
                Vector3 endPosition = bulletTransform.position +
                                      (Vector3)direction * distance;
                    
                trailScript.SetTargetPosition(endPosition);
              //  Debug.Log("Missed");
                  
            }

        }
        else
        {
            // Make empty magazine animation
           // Debug.Log("Magazine is empty! Reload");
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
       // Debug.Log("Is reloading");
        yield return new WaitForSeconds(reloadTime);
        
        // Reload complete
        //Debug.Log("Reload Complete");
        currentAmmo = ammoCapacity;
        NotifyCurrentAmmoChange(); // Callback to notify UI's that the currentAmmoHasChanged
        isReloading = false;
        canFire = true; // Can fire immediately after the reload

    }

    private void HitManager(RaycastHit2D hit)
    {
        if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
            
        }
    }
}
