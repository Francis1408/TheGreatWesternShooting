using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAttack : MonoBehaviour, IEnemyAttack
{
    
    // Transform to manage the weapon position
    private Transform m_transform;
    public Transform bulletTransform;
    private float bufferAngle = 20f;
    public bool isRightHanded = true;
    private float initial_x_pos;
    private float initial_y_pos;
    private float initial_z_pos;
    private float offset = 0.1f;

    private Rigidbody2D rb;
    
    // Aiming Variables
    public float aimAngle;
    private Vector2 aimDirection;
    
    // Weapon type
    public Weapon weapon;
    
    public GameObject bulletPrefab;
    public float bulletSpeed = 5f; // Speed of the bullet
    public float timer;
    public bool canFire;
    public float timeBetweenFiring;
    public float lowerCooldown = 4f;
    public float upperCooldown = 6f;

    private void Start()
    {
        m_transform = transform.Find("Aim");
        initial_x_pos = m_transform.localPosition.x;
        initial_y_pos = m_transform.localPosition.y;
        initial_z_pos = m_transform.localPosition.z;
    }

    public void Attack(Enemy enemy)
    {
        
        // Gets the aimingDirection 
        aimDirection = (Player.Instance.GetPosition() - transform.position).normalized;
        
        // Angle from the aimDirection
        aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        
        //Handles the Gun position to always aim at the player position
        Quaternion rotation = Quaternion.AngleAxis(aimAngle, Vector3.forward);

        m_transform.rotation = rotation;
        
        EnemyLookAt(aimAngle);
        
     

        if (!canFire)
        {
            timeBetweenFiring = Random.Range(lowerCooldown, upperCooldown);
            canFire = true;
           // Debug.Log("The cooldown is: " + timeBetweenFiring);
        }   
        
        timer += Time.deltaTime;
        
        if(canFire && timer > timeBetweenFiring )
        {
            // ------------------------ IMPLEMENT LATER THE OWN ENEMY BULLET SCRIPT --------------------------
            timer = 0;
                canFire = false;
             
                GameObject bullet = Instantiate(bulletPrefab, bulletTransform.position, Quaternion.identity);
                
                // Set bullet rotation based on the direction
                bullet.transform.rotation = Quaternion.Euler(0, 0, aimAngle);
                
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.velocity = aimDirection * bulletSpeed;
                
            // ------------------------ IMPLEMENT LATER THE OWN ENEMY BULLET SCRIPT --------------------------
        }
    }

    public void Halt()
    {
        // Destroy Enemy Aim
        Destroy(m_transform.gameObject);
    }

    // Applies transformation to the Weapon based on the player location
    
    //  Manages weapon flipping from one hand to another
    private void EnemyLookAt(float weaponAngle)
    {

        if (weaponAngle > -90f + bufferAngle && weaponAngle < 90f - bufferAngle)
        {
            m_transform.localPosition = new Vector3(initial_x_pos, initial_y_pos, initial_z_pos);
            m_transform.localScale = new Vector3(weapon.size_x, weapon.size_y, 0);
            isRightHanded = true;
        }
        else if (weaponAngle < -90f - bufferAngle || weaponAngle > 90f + bufferAngle)
        {
            m_transform.localPosition = new Vector3(-initial_x_pos - offset, initial_y_pos, initial_z_pos);
            m_transform.localScale = new Vector3(weapon.size_x, -weapon.size_y, 0);
            isRightHanded = false;
        }
    }
    
}
