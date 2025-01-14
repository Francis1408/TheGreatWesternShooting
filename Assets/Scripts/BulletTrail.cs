using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrail : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float progress;

    [SerializeField] private float speed = 40f;
    void Start()
    {
        startPosition = new Vector3(transform.position.x, transform.position.y, -1);
        
    }
    void Update()
    {
        progress += Time.deltaTime + speed;
        transform.position = Vector3.Lerp(startPosition, targetPosition, progress);
        
    }

    public void SetTargerPosition(Vector3 targetPosition)
    {
        targetPosition = new Vector3(targetPosition.x, targetPosition.y, -1);
    }
    
}
