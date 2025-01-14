using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{

    private Transform m_transform;
    private float maxMouseDistance = 2.0f;
    public float angle;
    private PlayerOrientation playerOrientation;

    private void Start()
    {
        m_transform = transform.Find("Aim");
        playerOrientation = GetComponent<PlayerOrientation>();
    }
    public void HandleAiming()
    {

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Get direction_aim between mouse and gun
        Vector2 direction = mousePos - m_transform.position;

        
        // Clamp the distance to the maximum allowed distance
        if (direction.magnitude < maxMouseDistance)
        {
            direction = direction.normalized * maxMouseDistance;
        }

        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        playerOrientation.PlayerLookAt(angle);
        
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        m_transform.rotation = rotation;
        
    }

    public void HandleShooting()
    {
        Weapon equippedWeapon = m_transform.GetComponentInChildren<Weapon>();
        if (m_transform.GetComponentInChildren<Weapon>())
        {
            if(Input.GetMouseButton(0)) equippedWeapon.Shoot();
                
        }
        else
        {
            Debug.Log("Weapon not equipped");
        }
    }

    private void HandleDistanceCampling()
    {

        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

    }
}
