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
   
   // Flags
    public bool alive = true;

    // Movement behavior placeholder
    private IEnemyMovement movementBehavior;
    
    // Attack behavior placeholder
    private IEnemyAttack attackBehavior;
    
    // Animation behavior placeholder
    private IEnemyAnimation animationBehaviour;
    
    // Enemy Hitbox
    private BoxCollider2D enemyBoxCollider;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        // Initialize movement behavior (can be set differently for each type of enemy)
        movementBehavior = GetComponent<IEnemyMovement>();
        attackBehavior = GetComponent<IEnemyAttack>();
        animationBehaviour = GetComponent<IEnemyAnimation>();
        enemyBoxCollider = GetComponent<BoxCollider2D>();


        // Custom initialization for health, damage, etc. can be done here
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // Handle movement
        movementBehavior?.Move(this);

        // Handle attack
        if (attackBehavior != null && health > 0)
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

        if (!(health <= 0) || !alive) return;
        alive = false;
        Die();
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
        // Stop enemy attack
        attackBehavior.Halt();
        // Disable hitbox
        enemyBoxCollider.enabled = false;
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