using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMovement : MonoBehaviour
{
    public float randLeft = -6.5f;
    public float randRight = 6.5f;
    void Update()
    {
        float randomrange = Random.Range(randLeft, randRight);
        transform.localPosition = new Vector3(randomrange,0f,0f);
    }
}
