using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMovement : MonoBehaviour
{
    private float Speed = 20;
    void Update()
    {
        if (transform.position.z < -20)
        {
            Destroy(gameObject);
        }
    }


    void FixedUpdate()
    {
        transform.Translate(Vector3.back * Time.deltaTime * Speed);


    }
}
