using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOrientation : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    private Transform m_transform;
   

    public Weapon weapon;
    private float offset = 0.55f;

    public bool isInBuffer;
    
    private float bufferAngle = 20f;
    private float initial_x_pos;
    private float initial_y_pos;
    private float initial_z_pos;

    public bool isRightHanded = true;

    void Start()
    {
        m_transform = transform.Find("Aim");
        initial_x_pos = m_transform.localPosition.x;
        initial_y_pos = m_transform.localPosition.y;
        initial_z_pos = m_transform.localPosition.z;

    }
    public void PlayerLookAt(float angle)
    {
        if (angle > -90f + bufferAngle && angle < 90f - bufferAngle)
        {
            m_transform.localPosition = new Vector3(initial_x_pos, initial_y_pos, initial_z_pos);
            m_transform.localScale = new Vector3(weapon.size_x, weapon.size_y, 0);
            isRightHanded = true;
        }
        else if (angle < -90f - bufferAngle || angle > 90f + bufferAngle)
        {

            m_transform.localPosition = new Vector3(-initial_x_pos - offset, initial_y_pos, initial_z_pos);
            m_transform.localScale = new Vector3(weapon.size_x, -weapon.size_y, 0);
            isRightHanded = false;
        }
    }

}
