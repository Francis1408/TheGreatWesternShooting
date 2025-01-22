using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{

    private Transform m_transform;
    private GameObject aimingObject;
    private float maxMouseDistance = 5.0f;
    public float angle;
    private PlayerOrientation playerOrientation;
    
    public float minMouseDistance = 1.0f; // Minimum radius from the player

    
    [SerializeField]
    private Vector3 gunOffset;

    private Weapon equippedWeapon;

    private void Start()
    {
        m_transform = transform.Find("Aim");
        equippedWeapon = PlayerWeaponController.Instance.currentWeapon;
      
        
        playerOrientation = GetComponent<PlayerOrientation>();
    }
    public void HandleAiming()
    {
        
        
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Get direction between mouse and player
        Vector2 direction = (mousePos - transform.position).normalized;
        
        
        // angle between mouse and player
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        
        playerOrientation.PlayerLookAt(angle);
       // Debug.Log("Arma = " + angleGun);
        
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        m_transform.rotation = rotation;
      
        
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
    
}
