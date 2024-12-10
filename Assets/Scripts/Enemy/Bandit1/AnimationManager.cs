using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimationManager: MonoBehaviour
{
    
    public string current_state;
    public Animator animator;

    public abstract string ConcatenatePrefixes(string movementType);
   

    public void ChangeAnimationState(string animationType)
    {
        string newState = ConcatenatePrefixes(animationType);
        
        //stop the same animation from interrupting itself
        if (current_state == newState) return;
        
        //play the animation
        animator.Play(newState);
        
        // reasign the current state
        current_state = newState;
    }

}
