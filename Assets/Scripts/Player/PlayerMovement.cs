using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    private Vector2 moveDirection;
    private float moveX;
    private float moveY;
    
    public void ProcessInputs() {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
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

}
