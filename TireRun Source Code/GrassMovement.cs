using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassMovement : MonoBehaviour
{

    private float Speed = 30;
    private float BaseSpeed = 30;

 

    void Update()
    {
        if (transform.position.y < -1 || transform.position.z < -10)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        transform.Translate(Vector3.back * Time.deltaTime * Speed);




    }
}
