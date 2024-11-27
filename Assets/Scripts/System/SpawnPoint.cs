using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    public bool active = true;

    public void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}
