using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : Weapon
{
    private float timer;
    public Transform bulletTransform;
    public GameObject bulletTrail;
    public Animator muzzleFlashAnimator;

    public LayerMask ignoreLayerMask;


    private void Start()
    {
        // Setting up the flags at the beginning 
        canFire = true;
        isReloading = false;
        
        // Starts loaded 
        currentAmmo = maxAmmo;
    }


    // Update is called once per frame
    void Update()
    {
        // Reloads automatically when ammo reaches zero 
        if (currentAmmo == 0 && !isReloading)
        {
            StartCoroutine(Reload());
        }
        
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

        if (canFire)
        {
            canFire = false;
            //muzzleFlashAnimator.SetTrigger("Shoot"); // For futher animation
            
            // Decreases ammo
            currentAmmo--;
            
            // Calculate the distance between the bulletTransform and the mousePointer
           
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
            // Calculate the direction from the bullet transform to the mouse position
            Vector2 direction = (mouseWorldPosition - bulletTransform.position).normalized;
            
            // Calculate the distance between the gun and the mouse
            float distance = Vector2.Distance(bulletTransform.position, mouseWorldPosition);
            
            // Perform the raycast in the calculated direction
            var hit = Physics2D.Raycast(bulletTransform.position, direction, distance , ignoreLayerMask);
            
            if (hit.collider != null)
            {
                Debug.Log($"Hit {hit.collider.name} at {hit.point}");
            }
            else
            {
                Debug.Log("Missed");
            }

            var trail = Instantiate(
                bulletTrail,
                bulletTransform.position,
                Quaternion.FromToRotation(Vector3.up, direction));

            BulletTrail trailScript = trail.GetComponent<BulletTrail>();

            if (hit.collider != null)
            {
                trailScript.SetTargetPosition(hit.point);
                hitManager(hit);
               
            }
            else
            {
                Vector3 endPosition = bulletTransform.position +
                                      (Vector3)direction * Vector3.Distance(mouseWorldPosition ,bulletTransform.position);
                
                trailScript.SetTargetPosition(endPosition);
              
            }
            
        }
    }

    public override IEnumerator Reload()
    {
        // No need to reload
        if (currentAmmo == maxAmmo)
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
        currentAmmo = maxAmmo;
        isReloading = false;
        canFire = true; // Can fire immediately after the reload

    }

    private void hitManager(RaycastHit2D hit)
    {
        Enemy enemy = hit.transform.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        
    }
}
