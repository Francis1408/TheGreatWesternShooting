using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{

    private Transform m_transform;
    public GameObject bullet;
    public Transform bulletTransform;
    public bool canFire, isFiring;
    private float timer;
    private float maxMouseDistance = 2.0f;
    public float timeBetweenFiring;
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
        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0;
            }
        }

        if (Input.GetMouseButton(0) && canFire)
        {
            canFire = false;
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
        }
    }

    private void HandleDistanceCampling()
    {

        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

    }
}
