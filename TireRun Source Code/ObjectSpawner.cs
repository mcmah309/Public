using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;

    public Transform[] SidespawnPoints;

    public GameObject blockPrefab;
    public GameObject blockPrefab2;
    public GameObject blockPrefab3;
    public GameObject blockPrefab4;
    public GameObject blockPrefab5;
    public GameObject blockPrefab6;
    public GameObject blockPrefab7;
    public GameObject blockPrefab8;
    public GameObject blockPrefab9;
    public GameObject blockPrefab10;
    public GameObject blockPrefab11;
    public GameObject blockPrefab12;

    private float timeBetweenWaves=0.5f;

    private float timeToSpawn = 1f;
    private void Start()
    {
        //Vector3 x = new Vector3(90, 0, 0);
        //blockPrefab2.transform.Rotate(x);
    }
    void Update()
    {

        if (Time.time >= timeToSpawn)
        {
            SpawnBlocks();
            timeToSpawn = Time.time + timeBetweenWaves;
        }

    }

    void SpawnBlocks()
    {
        //timeBetweenWaves = 0.5f;
        int randomIndex = Random.Range(1, 13);
        int randomspawn = Random.Range(0, spawnPoints.Length);
        switch (randomIndex) {
            case 1:
                Instantiate(blockPrefab, spawnPoints[randomspawn].position, Quaternion.identity);
                break;
            case 2:
                Instantiate(blockPrefab2, spawnPoints[randomspawn].position, Quaternion.identity);
                break;
            case 3:
                Instantiate(blockPrefab3, spawnPoints[randomspawn].position, Quaternion.identity);
                break;
            case 4:
                Instantiate(blockPrefab4, spawnPoints[randomspawn].position, Quaternion.identity);
                break;
            case 5:
                Instantiate(blockPrefab5, spawnPoints[randomspawn].position, Quaternion.identity);
                break;
            case 6:
                Instantiate(blockPrefab6, spawnPoints[randomspawn].position, Quaternion.identity);
                break;
            case 7:
                Instantiate(blockPrefab7, spawnPoints[randomspawn].position, Quaternion.identity);
                break;
            case 8:
                Instantiate(blockPrefab8, spawnPoints[randomspawn].position, Quaternion.identity);
                break;
            case 9:
                Instantiate(blockPrefab9, spawnPoints[randomspawn].position, Quaternion.identity);
                break;
            case 10:
                Instantiate(blockPrefab10, spawnPoints[randomspawn].position, Quaternion.identity);
                break;
            case 11:
                Instantiate(blockPrefab11, spawnPoints[randomspawn].position, Quaternion.identity);
                break;
            case 12:
                Instantiate(blockPrefab12, spawnPoints[randomspawn].position, Quaternion.identity);
                break;
        }
        int randomIndex2 = Random.Range(1, 7);
        int randomspawn2 = Random.Range(0, 2);
        switch (randomIndex2)
        {
            case 1:
                Instantiate(blockPrefab, SidespawnPoints[randomspawn2].position, Quaternion.identity);
                break;
            case 2:
                Instantiate(blockPrefab2, SidespawnPoints[randomspawn2].position, Quaternion.identity);
                break;
            case 3:
                Instantiate(blockPrefab3, SidespawnPoints[randomspawn2].position, Quaternion.identity);
                break;
            case 4:
                Instantiate(blockPrefab4, SidespawnPoints[randomspawn2].position, Quaternion.identity);
                break;
            case 5:
                Instantiate(blockPrefab5, SidespawnPoints[randomspawn2].position, Quaternion.identity);
                break;
            case 6:
                Instantiate(blockPrefab6, SidespawnPoints[randomspawn2].position, Quaternion.identity);
                break;
        }

    }
}
