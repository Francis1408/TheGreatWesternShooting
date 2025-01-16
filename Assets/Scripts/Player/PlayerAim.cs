using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{

    private Transform m_transform;
    private GameObject aimingObject;
    private float maxMouseDistance = 2.0f;
    public float angle;
    private PlayerOrientation playerOrientation;
    
    [SerializeField]
    private Vector3 gunOffset;

    private void Start()
    {
        m_transform = transform.Find("Aim");
        Weapon waepon = gameObject.GetComponentInChildren<Weapon>();
        
        if (waepon!= null) // If player is carrying a gun, the aiming will be based on it
        { 
            aimingObject = waepon.gameObject;
            aimingObject = aimingObject.transform.Find("BulletTransform").gameObject;
        }
        else // if not, the aiming object will be own aim object
        {
            aimingObject = m_transform.gameObject;
        }
        playerOrientation = GetComponent<PlayerOrientation>();
    }
    public void HandleAiming()
    {

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Get direction_aim between mouse and gun
        Vector2 direction = (mousePos - aimingObject.transform.position).normalized;

        
        // Clamp the distance to the maximum allowed distance
        if (direction.magnitude < maxMouseDistance)
        {
            direction = direction.normalized * maxMouseDistance;
        }

        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        
        playerOrientation.PlayerLookAt(angle);
       // Debug.Log("Arma = " + angleGun);
        
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        m_transform.rotation = rotation;
        
      //  UpdateGunPosition();
      
        
    }

    public void HandleShooting()
    {
        Weapon equippedWeapon = m_transform.GetComponentInChildren<Weapon>();
        if (m_transform.GetComponentInChildren<Weapon>())
        {
            // Shooting action
            if(Input.GetMouseButton(0)) equippedWeapon.Shoot();
            
            // Reloading action
            if (Input.GetKeyDown(KeyCode.R) && !equippedWeapon.isReloading) StartCoroutine(equippedWeapon.Reload());

        }
        else
        {
            Debug.Log("Weapon not equipped");
        }
    }

    /*
    private void HandleDistanceCampling()
    {

        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

    }
    
    private void OnDrawGizmos()
    {
        if (aimingObject.transform)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(weaponObject.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            
            Gizmos.color = Color.green;
            Gizmos.DrawLine(m_transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }
    */
}
