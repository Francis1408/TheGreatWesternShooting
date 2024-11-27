using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public float FollowSpeed = 2f;
    public Transform target;

    

    // Update is called once per frame
    private void Update()
    {
        Vector3 newPos = new Vector3(target.position.x, target.position.y, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed*Time.deltaTime);
    }
}
