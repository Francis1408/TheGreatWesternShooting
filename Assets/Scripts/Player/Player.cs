using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    // Attributes 
    
      public int lifes = 3;
   // private float currentHealth;
   // public float damage = 10f;
   // public Weapon weapon;
    
    // Components
    public PlayerMovement playerMovement;
    public PlayerAim playerAim;
    public PlayerAnimationManager playerAnimationManager;
    
    public static Player Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple Player instances detected! Destroying extra instance.");
            Destroy(gameObject);
        }
    }

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAim = GetComponent<PlayerAim>();
        playerAnimationManager = GetComponent<PlayerAnimationManager>();
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement.ProcessInputs();
        playerAim.HandleAiming();
        playerAim.HandleShooting();
        
        // Getting data for the animations 
        playerAnimationManager.axisX = playerMovement.GetMoveX();
        playerAnimationManager.axisY = playerMovement.GetMoveY();
        playerAnimationManager.player_angle = playerAim.angle;
    }

    private void FixedUpdate()
    {
        playerMovement.Move();
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void Die()
    {
        // Play death animation
        lifes--;
        GameManager.Instance.PlayerOnDeath();
    }
}