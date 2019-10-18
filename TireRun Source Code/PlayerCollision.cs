using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collisioninfo)
    {
        if(collisioninfo.collider.tag == "Obstacle")
        {
            /*
            Vector3 explosionPosition = new Vector3 (transform.position.x, transform.position.y+1, transform.position.z+1f);
 
    
            float explosionRadius = 2f;
            float upwardsModifier = 0.0f;
            ForceMode mode = ForceMode.Force;
            collisioninfo.rigidbody.AddExplosionForce(000, explosionPosition, explosionRadius, upwardsModifier, mode);
            */


            GetComponent<PlayerMovement>().CanJump = 1;

          /*  GameObject.FindGameObjectWithTag("PointsToSCreen").GetComponent<AddPoints>().points = (int)collisioninfo.collider.attachedRigidbody.mass -2;
            GameObject.FindGameObjectWithTag("PointsToSCreen").GetComponent<AddPoints>().show = 1;
            GameObject.FindGameObjectWithTag("PointsToSCreen").GetComponent<AddPoints>().time = 1;
            GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().x += (int)collisioninfo.collider.attachedRigidbody.mass -2;*/

        }
        else if(collisioninfo.collider.tag == "Grass")
        {
            GetComponent<PlayerMovement>().CanJump = 1;
            GetComponent<PitchRollYaw>().maxangle += 10;
        }
        else if (collisioninfo.collider.tag == "Mountain")
        {
            GetComponent<PlayerMovement>().CanJump = 1;
            GetComponent<PitchRollYaw>().maxangle += 15;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Obstacle")
        {
            //Debug.Log(collision.collider.tag);
            GetComponent<PlayerMovement>().CanJump = 0;
        }
    }
}
