using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject DogeCoin;
    private float timeToSpawn=5;
    private float randomtime=2;
    public float frequencyOfSpawn = 7f;
    void Update()
    {

        if (Time.time >= timeToSpawn)
        {
            Instantiate(DogeCoin, spawnPoint.position, Quaternion.identity);
            timeToSpawn = Time.time + randomtime;
            randomtime = Random.Range(0.5f, frequencyOfSpawn);
        }
    }
}
