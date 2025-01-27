using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    // Callbacks when the player is dashing
    public delegate void PlayerDashing(bool isDashing);

    public event PlayerDashing OnPlayerDashing;
    
    // Movement Variables
    public float moveSpeed;
    public Rigidbody2D rb;
    private Vector2 moveDirection;
    private float moveX;
    private float moveY;
    
    // Flags
    public bool isDashing = false;
    public bool canDash = true;
    public float dashSpeed;
    public float dashDuration = 0.5f;
    public float dashCooldown = 1f;
    
    public void ProcessInputs() {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

        // Ensures that the dash is only enabled if the player is in momentum and not reloading
        if (Input.GetMouseButtonDown(1) && moveDirection.magnitude > 0 && canDash 
            && !PlayerWeaponController.Instance.currentWeapon.isReloading)
        {
            StartCoroutine(Dash());
        }
        
    }

    public void Move() {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    public float GetMoveX()
    {
        return this.moveX;
    }
    
    public float GetMoveY()
    {
        return this.moveY;
    }

    // Handles the dash mechanic
    public IEnumerator Dash()
    {
        canDash = false; // Is already dashing
        isDashing = true;
        OnPlayerDashing?.Invoke(isDashing); // Informs that the dash animation has started
        
        rb.velocity = new Vector2(moveDirection.x * dashSpeed, moveDirection.y * dashSpeed);
        yield return new WaitForSeconds(dashDuration);
        
        isDashing = false;
        OnPlayerDashing?.Invoke(isDashing); // Informs that the dash animation has ended
        
        yield return new WaitForSeconds(dashCooldown);
     
        canDash = true;
    }

}
