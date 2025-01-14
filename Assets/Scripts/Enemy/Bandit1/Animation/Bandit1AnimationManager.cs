using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit1AnimationManager : MonoBehaviour, IEnemyAnimation
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

    // ====== Death ANIMATIONS ========
    private const string DEATH_RIGHT = "Death_Right";
    private const string DEATH_LEFT = "Death_Left";


    // ====== Aux Variables =======
    private EnemyMovement enemyMovement;
    private EnemyAttack enemyAttack;
    private string current_state;
    private bool isDead = false;
   
    
    // ===== Meshes parts animators =====
    public AnimationManager hairAnimator;
    public AnimationManager robeAnimator;
    public AnimationManager pantsAnimator;
    public AnimationManager skinAnimator;
    public AnimationManager scarfAnimator;
    public AnimationManager shirtAnimator;
    public AnimationManager eyeAnimator;
    
    void Start()
    {
       // animator = GetComponent<Animator>();
       // Get the enemy references
       enemyAttack = GetComponent<EnemyAttack>();
       enemyMovement = GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    public void HandleAnimation(Enemy enemy)
    {
        if (!isDead)
        {
            
            switch (enemyAttack.isRightHanded)
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
                    switch (enemyAttack.aimAngle)
                    {
                        
                        // Right
                        case <= 30f and > -60f when enemyMovement.isMoving == false:
                            ModifyAnimationStates(IDLE_RIGHT);
                            break;
                        case <= 30f and > -60f:
                            ModifyAnimationStates(WALKING_RIGHT);
                            break;
                        // Front Right
                        case <= -60f and > -90f when enemyMovement.isMoving == false:
                            ModifyAnimationStates(IDLE_FRONT_RIGHT);
                            break;
                        case <= -60f and > -90f:
                            ModifyAnimationStates(WALKING_FRONT_RIGHT);
                            break;
                        // Top Right
                        case <= 90f and > 60f when enemyMovement.isMoving == false:
                            ModifyAnimationStates(IDLE_TOP_RIGHT);
                            break;
                        case <= 90f and > 60f:
                            ModifyAnimationStates(WALKING_TOP_RIGHT);
                            break;
                        // Diagonal Right
                        case <= 60f and > 30f when enemyMovement.isMoving == false:
                            ModifyAnimationStates(IDLE_DIAGONAL_RIGHT);
                            break;
                        case <= 60f and > 30f:
                            ModifyAnimationStates(WALKING_DIAGONAL_RIGHT);
                            break;
                    }
                    break;
                case false:
                    switch (enemyAttack.aimAngle)
                    {
                        // Front Left
                        case <= -90f and > -120f when enemyMovement.isMoving == false:
                            ModifyAnimationStates(IDLE_FRONT_LEFT);
                            break;
                        case <= -90f and > -120f:
                            ModifyAnimationStates(WALKING_FRONT_LEFT);
                            break;
                        case <= -120f and >= -180f:
                        // Left
                        case >= 150f and <= 180f:
                        {
                            if (enemyMovement.isMoving == false)
                            {
                                ModifyAnimationStates(IDLE_LEFT);

                            }
                            else
                            {
                                ModifyAnimationStates(WALKING_LEFT);
                            }

                            break;
                        }
                        // Diagonal Left
                        case < 150f and > 120f when enemyMovement.isMoving == false:
                            ModifyAnimationStates(IDLE_DIAGONAL_LEFT);
                            break;
                        case < 150f and > 120f:
                            ModifyAnimationStates(WALKING_DIAGONAL_LEFT);
                            break;
                        // Top Left
                        case <= 120f and > 90f when enemyMovement.isMoving == false:
                            ModifyAnimationStates(IDLE_TOP_LEFT);
                            break;
                        case <= 120f and > 90f:
                            ModifyAnimationStates(WALKING_TOP_LEFT);
                            break;
                    }
                    break;
            }
        }
        else
        {
            ModifyAnimationStates(DEATH_RIGHT);
        }
    }

    public void HandleDeathAnimation()
    {
        isDead = true;
    }


    void ModifyAnimationStates(string newState)
    {
     hairAnimator.ChangeAnimationState(newState);
     robeAnimator.ChangeAnimationState(newState);
     pantsAnimator.ChangeAnimationState(newState);
     skinAnimator.ChangeAnimationState(newState);
     scarfAnimator.ChangeAnimationState(newState);
     shirtAnimator.ChangeAnimationState(newState);
     eyeAnimator.ChangeAnimationState(newState);
    }

}
    
