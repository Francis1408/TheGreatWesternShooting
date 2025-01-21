using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    // Attributes 
    
    public int lifes = 3;
   // private float currentHealth;
   // public Weapon weapon;
   
   // Flags
    public bool isInvunerable;
    public bool isDashing;
    
    // Components
    public PlayerMovement playerMovement;
    public PlayerAim playerAim;
    public PlayerAnimationManager playerAnimationManager;
    
    public static Player Instance { get; private set; }
    
    
    // It subscribes to playerMovement.OnPlayerDashing event 
    private void OnEnable()
    {
        playerMovement.OnPlayerDashing += ManageDashBehaviour;
    }
    // It unsubscribes from the currentWeapon.OnAmmoChanged event
    private void OnDisable()
    {
        playerMovement.OnPlayerDashing -= ManageDashBehaviour;
    }

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
        

        isInvunerable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDashing)
        {
            playerAim.HandleAiming();
            playerAim.HandleShooting();
        }
        
        playerMovement.ProcessInputs();
        
        // Getting data for the animations 
        playerAnimationManager.axisX = playerMovement.GetMoveX();
        playerAnimationManager.axisY = playerMovement.GetMoveY();
        playerAnimationManager.player_angle = playerAim.angle;
    }

    private void FixedUpdate()
    {
        if (!playerMovement.isDashing)
        {
            playerMovement.Move();
        }
    }

    private void ManageDashBehaviour(bool isDashing)
    {
        if (isDashing)
        {
            this.isDashing = true;
            isInvunerable = true;
            playerAim.DisableAim(); // Hide the aim sprites
        }
        else
        {
            this.isDashing = false;
            isInvunerable = false;
            playerAim.EnableAim(); // Show the aim sprites
        }
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