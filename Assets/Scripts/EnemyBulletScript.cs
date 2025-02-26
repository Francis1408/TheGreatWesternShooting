using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    
    private void OnTriggerStay2D(Collider2D other)
    {
        Player player = other.GetComponentInParent<Player>();
        
        //check if is the player
        if (player != null && !player.isInvunerable)
        {
       
            player.Die();
            Destroy(gameObject);
        }
    }
    
}
