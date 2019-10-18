using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollision : MonoBehaviour
{
    private ObjectMovement movement;
    void OnCollisionEnter(Collision collisioninfo)
    {
        if (collisioninfo.collider.tag == "Obstacle" || collisioninfo.collider.tag == "Player")
        {
            FindObjectOfType<AudioManager>().Play("HittingCar");
            if (gameObject.GetComponent<ObjectMovement>())
            {
                GetComponent<ObjectMovement>().time = 1;
            }
            else
            {
                GetComponentInParent<ObjectMovement>().time = 1;
            }

        }
    }
}
