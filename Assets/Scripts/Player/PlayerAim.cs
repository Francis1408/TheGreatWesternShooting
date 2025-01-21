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

    private Weapon equippedWeapon;

    private void Start()
    {
        m_transform = transform.Find("Aim");
        equippedWeapon = PlayerWeaponController.Instance.currentWeapon;
        
        if (equippedWeapon!= null) // If player is carrying a gun, the aiming will be based on it
        { 
            aimingObject = equippedWeapon.gameObject;
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
        
        if (equippedWeapon != null)
        {
            // Shooting action
            if(Input.GetMouseButtonDown(0)) equippedWeapon.Shoot();
            
            // Reloading action
            if (Input.GetKeyDown(KeyCode.R) && !equippedWeapon.isReloading) StartCoroutine(equippedWeapon.Reload());

        }
        else
        {
            Debug.Log("Weapon not equipped");
        }
    }

    public void DisableAim()
    {
        m_transform.gameObject.SetActive(false);
    }
    
    public void EnableAim()
    {
        m_transform.gameObject.SetActive(true);
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
