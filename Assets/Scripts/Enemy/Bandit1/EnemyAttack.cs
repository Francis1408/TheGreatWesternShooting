using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAttack : MonoBehaviour, IEnemyAttack
{
    
    // Transform to manage the weapon position
    private Transform m_transform;
    private float bufferAngle = 20f;
    public bool isRightHanded = true;
    private float initial_x_pos;
    private float initial_y_pos;
    private float initial_z_pos;
    private float offset = 0.55f;
    public float aimAngle;
    
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
        //Handles the Gun position to always aim at the player position
        HandleAiming();

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
    
    // Applies transformation to the Weapon based on the player location
    private void HandleAiming()
    {
        // Gets the direction between the aiming and 
        Vector2 aimDirection = Player.Instance.transform.position - m_transform.position;
        
        // Angle from the aimDirection
        aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        
        Quaternion rotation = Quaternion.AngleAxis(aimAngle, Vector3.forward);

        m_transform.rotation = rotation;
        
        EnemyLookAt(aimAngle);
        
    }
    
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
