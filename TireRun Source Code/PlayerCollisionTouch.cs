using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionTouch : MonoBehaviour
{
    private PlayerMovementTouch playerMovementTouch;
    private PitchRollYaw pitchRollYaw; 

    void Start()
    {
        playerMovementTouch = GetComponent<PlayerMovementTouch>();
        pitchRollYaw = GetComponent<PitchRollYaw>();
    }
    void OnCollisionEnter(Collision collisioninfo)
    {
        if(collisioninfo.collider.tag == "Obstacle" || collisioninfo.collider.tag =="Ground")
        {
            /*
            Vector3 explosionPosition = new Vector3 (transform.position.x, transform.position.y+1, transform.position.z+1f);
 
    
            float explosionRadius = 2f;
            float upwardsModifier = 0.0f;
            ForceMode mode = ForceMode.Force;
            collisioninfo.rigidbody.AddExplosionForce(000, explosionPosition, explosionRadius, upwardsModifier, mode);
            */


            playerMovementTouch.CanJump = true;

          /*  GameObject.FindGameObjectWithTag("PointsToSCreen").GetComponent<AddPoints>().points = (int)collisioninfo.collider.attachedRigidbody.mass -2;
            GameObject.FindGameObjectWithTag("PointsToSCreen").GetComponent<AddPoints>().show = 1;
            GameObject.FindGameObjectWithTag("PointsToSCreen").GetComponent<AddPoints>().time = 1;
            GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().x += (int)collisioninfo.collider.attachedRigidbody.mass -2;*/

        }
        else if(collisioninfo.collider.tag == "Grass")
        {
            playerMovementTouch.CanJump = true;
            pitchRollYaw.maxangle += 9;
        }
        else if (collisioninfo.collider.tag == "Mountain")
        {
            playerMovementTouch.CanJump = true;
            pitchRollYaw.maxangle += 14;
        }
    }
}
