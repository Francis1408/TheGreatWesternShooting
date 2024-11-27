using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Vector3 mousePos;
    private float aimAngle;
    private Rigidbody2D rb;
    public float force;
    void Start()
    {
        aimAngle = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAim>().angle;
        rb = GetComponent<Rigidbody2D>();
        Vector2 direction = new Vector2(Mathf.Cos(aimAngle * Mathf.Deg2Rad), Mathf.Sin(aimAngle * Mathf.Deg2Rad));
        rb.velocity = direction.normalized * force;
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

    }

    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Enemy enemy = collider.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(1f);
            Destroy(gameObject);
        }
    }
}
