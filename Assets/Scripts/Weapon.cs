using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{  
    public float size_x;
    public float size_y;
    public float size_z;
    public void BasicShoot() {
        Shoot();
    }

    protected virtual void Shoot() {
        // Template to be overriden by our sublcasses
    }
    
}