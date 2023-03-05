using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    public Transform[] spawnPoints;
    private float timeBetweenSpawns;
    public float startTimeBtwSpawns;

    private void Start()
    {
        timeBetweenSpawns = startTimeBtwSpawns;
    }
    private void Update()
    {
        if (timeBetweenSpawns <= 0)
        {
            int randPoint = Random.Range(0, spawnPoints.Length-1); //picks a random spawn point to instantiate an enemy
            Instantiate(enemy, spawnPoints[randPoint].position, Quaternion.identity);
            timeBetweenSpawns = startTimeBtwSpawns;
        }
        else {
            timeBetweenSpawns -= Time.deltaTime;
        }
    }
}
