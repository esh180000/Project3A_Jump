using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateGameObject : MonoBehaviour
{
    [SerializeField] GameObject objectToSpawn;
    [SerializeField] Transform[] spawnPoints;
    private float timer = 0.0f;
    private float waitTime = 3.0f;

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > waitTime)
        {
            SpawnRandom();
            timer = timer - waitTime;
        }
    }

    void SpawnRandom()
    {
        int newSpawnIndex = Random.Range(0,spawnPoints.Length);
        Instantiate(objectToSpawn, spawnPoints[newSpawnIndex].position, spawnPoints[newSpawnIndex].rotation);
    }

}
