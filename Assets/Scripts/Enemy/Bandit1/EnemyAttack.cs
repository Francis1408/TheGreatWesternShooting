using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour, IEnemyAttack
{
    // Start is called before the first frame update
    public GameObject bulletPrefab;
    public float bulletSpeed = 5f; // Speed of the bullet
    public float timer;
    public bool canFire;
    public float timeBetweenFiring;
    public float lowerCooldown = 4f;
    public float upperCooldown = 6f;

    public void Attack(Enemy enemy)
    {

        if (!canFire)
        {
            timeBetweenFiring = Random.Range(lowerCooldown, upperCooldown);
            canFire = true;
           // Debug.Log("The cooldown is: " + timeBetweenFiring);
        }   
        
        timer += Time.deltaTime;
        
        if(canFire && timer > timeBetweenFiring )
        {
                timer = 0;
                canFire = false;
                Vector2 direction = (Player.Instance.GetPosition() - transform.position).normalized;
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                
                // Set bullet rotation based on the direction
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
                
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.velocity = direction * bulletSpeed;
        }
    }
    
}
