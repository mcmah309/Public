using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMovement : MonoBehaviour
{
    //public Rigidbody rb;


    public int Speed = 20;
    void Update()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(Vector3.back);
        //rb.AddForce(0, 0, ForwardForce * Time.deltaTime);
        transform.Translate(Vector3.back * Time.deltaTime*Speed);
    }
}
