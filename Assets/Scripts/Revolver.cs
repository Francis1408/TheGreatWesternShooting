using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : Weapon
{
    private bool canFire = true;
    private float timer;
    public Transform bulletTransform;
    public GameObject bulletTrail;
    public Animator muzzleFlashAnimator;
    

    // Update is called once per frame
    void Update()
    {
        if (!canFire)
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
            //muzzleFlashAnimator.SetTrigger("Shoot");
            
            // Calculate the distance between the bulletTransform and the mousePointer
           
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
            // Calculate the direction from the bullet transform to the mouse position
            Vector2 direction = (mouseWorldPosition - bulletTransform.position).normalized;
            
            // Perform the raycast in the calculated direction
            var hit = Physics2D.Raycast(bulletTransform.position, direction);
            
            float distance = Vector3.Distance(bulletTransform.position, mouseWorldPosition);
            Debug.Log(distance);
            
            Debug.DrawRay(bulletTransform.position, direction * 10, Color.red, 1f);
            
            
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

           // BulletTrail trailScript = trail.GetComponent<BulletTrail>();
        }
    }
}
