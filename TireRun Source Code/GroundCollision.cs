using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collisioninfo)
    {
        //Debug.Log("GroundCollisions");
        if (collisioninfo.collider.tag == "Ground")
        {
            //Debug.Log(collisioninfo.collider.tag);
            GetComponent<PlayerMovement>().CanJump = 1;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            //Debug.Log(collision.collider.tag);
            GetComponent<PlayerMovement>().CanJump = 0;
        }
    }
}
