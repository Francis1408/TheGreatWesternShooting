using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for enemies
public class Enemy : MonoBehaviour
{
    // Attributes
    public float health;
    public float damage;
    public int pointsOnKill;
    public float despawnTime;
   // public Weapon weapon; // Reference to a Weapon class

    // Movement behavior placeholder
    public IEnemyMovement movementBehavior;
    
    // Attack behavior placeholder
    public IEnemyAttack attackBehavior;
    
    // Animation behavior placeholder
    public IEnemyAnimation animationBehaviour;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        // Initialize movement behavior (can be set differently for each type of enemy)
        movementBehavior = GetComponent<IEnemyMovement>();
        attackBehavior = GetComponent<IEnemyAttack>();
        animationBehaviour = GetComponent<IEnemyAnimation>();

        // Custom initialization for health, damage, etc. can be done here
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // Handle movement
        if (movementBehavior != null)
        {
            movementBehavior.Move(this);
        }
        
        // Handle attack
        if (attackBehavior != null)
        {
            attackBehavior.Attack(this);
        }
        
        // Handle animation
        if (animationBehaviour != null)
        {
            animationBehaviour.HandleAnimation(this);
        }
    }

    // Function to take damage
    public virtual void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            Die();
        }
    }

    // When the enemy dies
    protected virtual void Die()
    {
        // Add points to player
       // GameManager.Instance.AddPoints(pointsOnKill);
        GameManager.Instance.AddPoints(pointsOnKill);
        if (animationBehaviour != null && movementBehavior != null)
        {
            StartCoroutine(HandleDeathProcess());
        }
        else
        {
            Destroy(gameObject);
        }
        // Destroy the enemy
    }

    private IEnumerator HandleDeathProcess()
    {
        // Stop enemy movement
        movementBehavior.Stop();
        // Triggers death animation
        animationBehaviour.HandleDeathAnimation();
        // Wait for the predefined despawn time
        yield return new WaitForSeconds(despawnTime);

        // Destroy the enemy
        Destroy(gameObject);
    }
    
    /*
    // Attack functionality, can be overridden
    public virtual void Attack()
    {
        if (weapon != null)
        {
            weapon.Use(); // Use the weapon's attack functionality
        }
    }
    */
}