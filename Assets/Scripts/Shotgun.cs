using System.Collections;

using UnityEngine;

public class Shotgun : Weapon
{
    // Start is called before the first frame update
    private float timer;
    public Transform bulletTransform;
    
    // Shot variables
    public int spreadCount = 3;
    public float spreadAngle = 30f;
    public float spreadRadius = 0.2f;
    private float angleStep;
    public float bulletDistance = 15f;
    private int circleSegments = 10;
    
    // Effect variables
    public GameObject bulletTrail;
    public LayerMask ignoreLayerMask;
 
    void Start()
    {
        // Setting up the flags at the beginning 
        canFire = true;
        isReloading = false;
        
        // Starts loaded 
        currentAmmo = ammoCapacity;
        totalAmmo = 3;
        
        CalculateTheBulletSpread();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetMouseButtonDown(0)) Shoot(); // Handles shooting
        
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
           
           //var hits = new List<RaycastHit2D>();

           canFire = false;
           CameraShake.Instance.ShakeCamera(3f, .1f); // Shakes the camera 
           // Decreases ammo
           currentAmmo--;
                
           NotifyCurrentAmmoChange(); // Callback to notify UI's that the currentAmmoHasChanged
                
           // Calculate the distance between the bulletTransform and the mousePointer
           Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
           
           // Set the initial position from the spread as the last ray on the left
           Vector2 startPos = Quaternion.AngleAxis(angleStep, Vector3.forward) * transform.right;
           
           // Iterates over the rays from left to the right
           for (int i = 0; i < spreadCount; i++)
           {

               Vector2 direction = Quaternion.AngleAxis(-angleStep * i, Vector3.forward) * startPos;
               
               // Perform the CircleCastAll
               var hit = Physics2D.CircleCast(bulletTransform.position, spreadRadius, direction , bulletDistance, ignoreLayerMask);
               // Instantiate the bullet trail
               var trail = Instantiate(
                   bulletTrail,
                   bulletTransform.position,
                   Quaternion.FromToRotation(Vector3.up, direction));
               
              //Debug.DrawRay(bulletTransform.position, direction * distance, Color.red, 1f);
               BulletTrail trailScript = trail.GetComponent<BulletTrail>();
               
               // Checking for hits
               if (hit.collider != null)
               {
                   trailScript.SetTargetPosition(hit.point);
                   HitManager(hit);
                   // Add damage or effects here
               }
               else
               {
                   Vector3 endPosition =
                       bulletTransform.position +
                           (Vector3)direction * bulletDistance;
                    
                   trailScript.SetTargetPosition(endPosition);
                  
               }
               
               
               // Visualize the cast in the Scene view
               for (int j = 0; j <= circleSegments; j++)
               {
                   float t = (float)j / circleSegments;
                   Vector3 position = bulletTransform.position + (Vector3)(direction * bulletDistance * t);
                   DrawCircle(position, spreadRadius, Color.green);
               }
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
           // Debug.Log("Ammo full no need to reload");
            yield break;
        }

        if (totalAmmo <= 0)
        {
           // Debug.Log("No ammo left");
            yield break;
        } 
        
        // Makes the reload action
        canFire = false; // Interrupts firing 
        isReloading = true;
        Debug.Log("Is reloading");
        yield return new WaitForSeconds(reloadTime);
        
        // Reload complete
        Debug.Log("Reload Complete");
        if (totalAmmo / ammoCapacity >= 1) // Enough ammo for a full cartridge 
        {
            currentAmmo = ammoCapacity;
            totalAmmo -= ammoCapacity;

        }
        else // Reload with what is left 
        {
            currentAmmo = totalAmmo % ammoCapacity;
            totalAmmo = 0;
        }
        NotifyCurrentAmmoChange(); // Callback to notify UI's that the currentAmmoHasChanged
        isReloading = false;
        canFire = true; // Can fire immediately after the reload
    }

    private void CalculateTheBulletSpread() // Calculate the bullets spread based on the defined spread variables
    {
        angleStep = (2 * spreadAngle) / spreadCount;
       
    }

    private void HitManager(RaycastHit2D hit)
    {
        if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
            
        }
    }
    
    void DrawCircle(Vector3 position, float radius, Color color)
    {
        int segments = 36;
        float angleStep = 360f / segments;
        Vector3 prevPoint = position + new Vector3(Mathf.Cos(0) * radius, Mathf.Sin(0) * radius, 0);

        for (int i = 1; i <= segments; i++)
        {
            float angle = angleStep * i * Mathf.Deg2Rad;
            Vector3 nextPoint = position + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
            Debug.DrawLine(prevPoint, nextPoint, color, 1f);
            prevPoint = nextPoint;
        }
    }
}
