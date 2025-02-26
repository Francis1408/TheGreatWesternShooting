using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    // Start is called before the first frame update
    
    // Animations States
    // ====== IDLE ANIMATIONS ======
    const string IDLE_RIGHT = "Idle_Right";
    const string IDLE_LEFT = "Idle_Left";
    const string IDLE_FRONT_LEFT = "Idle_Front_Left";
    const string IDLE_FRONT_RIGHT = "Idle_Front_Right";
    const string IDLE_DIAGONAL_LEFT = "Idle_Diagonal_Left";
    const string IDLE_DIAGONAL_RIGHT = "Idle_Diagonal_Right";
    const string IDLE_TOP_LEFT = "Idle_Top_Left";
    const string IDLE_TOP_RIGHT = "Idle_Top_Right";
    
    // ====== SHOOTING ANIMATIONS ======
    
    const string SHOOTING_RIGHT = "Shooting_Right";
    const string SHOOTING_LEFT = "Shooting_Left";
    const string SHOOTING_FRONT_LEFT = "Shooting_Front_Left";
    const string SHOOTING_FRONT_RIGHT = "Shooting_Front_Right";
    const string SHOOTING_DIAGONAL_LEFT = "Shooting_Diagonal_Left";
    const string SHOOTING_DIAGONAL_RIGHT = "Shooting_Diagonal_Right";
    const string SHOOTING_TOP_LEFT = "Shooting_Top_Left";
    const string SHOOTING_TOP_RIGHT = "Shooting_Top_Right";
    
    // ====== Walking ANIMATIONS =======
    
    const string WALKING_RIGHT = "Walking_Right";
    const string WALKING_LEFT = "Walking_Left";
    const string WALKING_FRONT_LEFT = "Walking_Front_Left";
    const string WALKING_FRONT_RIGHT = "Walking_Front_Right";
    const string WALKING_DIAGONAL_LEFT = "Walking_Diagonal_Left";
    const string WALKING_DIAGONAL_RIGHT = "Walking_Diagonal_Right";
    const string WALKING_TOP_LEFT = "Walking_Top_Left";
    const string WALKING_TOP_RIGHT = "Walking_Top_Right";
    
    public Animator animator;
    private PlayerOrientation playerOrientation;
    private string current_state;

    public float axisX, axisY;
    public float player_angle;
    void Start()
    {
       // animator = GetComponent<Animator>();
       playerOrientation = GetComponent<PlayerOrientation>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (playerOrientation.isRightHanded)
        {
            // Check initially in which angle the player is aiming to define the correct sprite sheet
            /*
                         90
                         |
             180/-180 --- --- 0
                         |
                        -90
            Right = 30 -> -60
            Diagonal Right = 30 -> 60
            Top Right = 60 -> 90
            Top Left = 90 -> 120
            Diagonal Left = 120 -> 150
            Left  = 150 -> -120
            Front Left = -120 -> -90
            Front Right = -90 -> -60

         */
            case true:
                switch (player_angle)
                {
                    
                    // Right
                    case <= 30f and > -60f when axisX == 0 && axisY == 0:
                        ChangeAnimationState(IDLE_RIGHT);
                        break;
                    case <= 30f and > -60f:
                        ChangeAnimationState(WALKING_RIGHT);
                        break;
                    // Front Right
                    // Diagonal Right
                    case <= 60f and > 30f when axisX == 0 && axisY == 0:
                        ChangeAnimationState(IDLE_DIAGONAL_RIGHT);
                        break;
                    case <= 60f and > 30f:
                        ChangeAnimationState(WALKING_DIAGONAL_RIGHT);
                        break;
                    // Top Right
                    case > 60f when axisX == 0 && axisY == 0:
                        ChangeAnimationState(IDLE_TOP_RIGHT);
                        break;
                    case > 60f:
                        ChangeAnimationState(WALKING_TOP_RIGHT);
                        break;
                    case <= -60f when axisX == 0 && axisY == 0:
                        ChangeAnimationState(IDLE_FRONT_RIGHT);
                        break;
                    case <= -60f:
                        ChangeAnimationState(WALKING_FRONT_LEFT); // NOMECLATURE IS SWITCHED
                        break;
                }
                break;
            case false:
                switch (player_angle)
                {
                    // Left
                    case <= -120f and >= -180f:
                    case >= 150f and <= 180f:
                    {
                        if (axisX == 0 && axisY == 0)
                        {
                            ChangeAnimationState(IDLE_LEFT);

                        }
                        else
                        {
                            ChangeAnimationState(WALKING_LEFT);
                        }

                        break;
                    }
                    // Diagonal Left
                    case < 150f and > 120f when axisX == 0 && axisY == 0:
                        ChangeAnimationState(IDLE_DIAGONAL_LEFT);
                        break;
                    case < 150f and > 120f:
                        ChangeAnimationState(WALKING_DIAGONAL_LEFT);
                        break;
                    // Front Left
                    case > -120f and < 0 when axisX == 0 && axisY == 0:
                        ChangeAnimationState(IDLE_FRONT_LEFT);
                        break;
                    case > -120f and < 0:
                        ChangeAnimationState(WALKING_FRONT_RIGHT); // NOMECLATURE IS SWITCHED
                        break;
                    // Top Left
                    case <= 120f and > 0 when axisX == 0 && axisY == 0:
                        ChangeAnimationState(IDLE_TOP_LEFT);
                        break;
                    case <= 120f and > 0 :
                        ChangeAnimationState(WALKING_TOP_LEFT);
                        break;
                }
                break;
        }
 
    }

    void ChangeAnimationState(string newState)
    {
        //stop the same animation from interrupting itself
        if (current_state == newState) return;
        
        //play the animation
        animator.Play(newState);
        
        // reasign the current state
        current_state = newState;
    }
}
