using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PItchYawRollCollision : MonoBehaviour
{
    public PitchRollYaw x;
    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.contactCount);
        //Debug.Log(collision.impulse);
        //Debug.Log(collision.impulse.magnitude);
       // Debug.Log(collision.collider.tag);
        if (collision.collider.tag == "Obstacle")
        {
            //Debug.Log("go");
            //x.maxangle = x.maxangle + (collision.impulse.magnitude*collision.relativeVelocity.magnitude)/2;
            //float w = Random.Range(1f, 2f);
            x.maxangle = x.maxangle + (collision.rigidbody.mass*collision.relativeVelocity.magnitude);
           // if (x.maxangle > 30f)
           // {
           //     x.maxangle = 25f;
           //}
            x.speed = Random.Range(0.5f, x.maxangle/7 + 0.5f);
            
        }

    }

}
