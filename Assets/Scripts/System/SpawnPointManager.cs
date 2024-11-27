using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnPointManager : MonoBehaviour
{ 
    public SpawnPoint[] spawnPoints;
    public Camera mainCamera;
    public List<SpawnPoint> activeSpawns;
 
    [SerializeField] private float spawnInteval = 6f;

    private void Start()
    {   
        AddSpawnPoints();
        StartCoroutine(CallSpawn(spawnInteval));
    }

    private void Update()
    {
        foreach (SpawnPoint spawnPoint in spawnPoints)
        {
            Vector3 viewportPosition = mainCamera.WorldToViewportPoint(spawnPoint.transform.position);
            spawnPoint.active = !IsInCameraRange(viewportPosition);
            if (spawnPoint.active)
            {
                if(!activeSpawns.Contains(spawnPoint)) activeSpawns.Add(spawnPoint);
            }
            else
            {
                if (activeSpawns.Contains(spawnPoint)) activeSpawns.Remove(spawnPoint);
            }
        }
    }

    private bool IsInCameraRange(Vector3 viewportPosition)
    {
        // Check if the spawn point is within the camera's viewport
        bool isVisible = viewportPosition.x > 0 && viewportPosition.x < 1 &&
                         viewportPosition.y > 0 && viewportPosition.y < 1 &&
                         viewportPosition.z > 0;
        return isVisible;
    }

    private IEnumerator CallSpawn(float interval)
    {
        yield return new WaitForSeconds(interval);
        ChooseSpawnPoint();
        StartCoroutine(CallSpawn(interval));
    }

    private void ChooseSpawnPoint()
    {
        if (activeSpawns.Count > 0)
        {
            int spawnPointNumber = Random.Range(0, activeSpawns.Count - 1);
            SpawnPoint spawnPointChoosed = activeSpawns[spawnPointNumber];
            spawnPointChoosed.SpawnEnemy();
        }
        else
        {
            Debug.Log("No spawnpoints active, waiting the " + spawnInteval + "s refresh");
        }
    }

    private void AddSpawnPoints()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            activeSpawns.Add(spawnPoints[i]);
        }
    }
}
