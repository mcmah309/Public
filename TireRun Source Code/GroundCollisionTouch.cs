using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollisionTouch : MonoBehaviour
{
    private PlayerMovementTouch playerMovementTouch;

    private void Start()
    {
        playerMovementTouch = GetComponent<PlayerMovementTouch>();
    }
    void OnCollisionEnter(Collision collisioninfo)
    {
        //Debug.Log("GroundCollisions");
        if (collisioninfo.collider.tag == "Ground")
        {
            //Debug.Log(collisioninfo.collider.tag);
            playerMovementTouch.CanJump = true;
        }
    }
}
