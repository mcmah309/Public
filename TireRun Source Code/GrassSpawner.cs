using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassSpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject[] Grass;


    void Update()
    {
        if (Time.timeScale != 0)
        {
            int rand = Random.Range(0, 3);
            Instantiate(Grass[rand], spawnPoint.position, Quaternion.identity);
        }
    }
}
