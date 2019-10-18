using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTouch : MonoBehaviour
{
    public Rigidbody rb;
    public PitchRollYaw w;
    public float speed = 1f;
    public bool canUseLife = true;


    private int SidewaysForce = 0;
    private int JumpForce = 220;
    public bool CanJump = true;

    private float halfScreenWidth;

    private AudioManager audioManger;

    private void Start()
    {
        audioManger =FindObjectOfType<AudioManager>();
        halfScreenWidth = Screen.width / 2;
    }
    void Update()
    {
        //Debug.Log(Input.touchCount);
        if (Input.touchCount > 1)
        {
            if (CanJump)
            {
                audioManger.Play("Bounce");
                rb.AddForce(0, JumpForce * Time.deltaTime, 0, ForceMode.VelocityChange);
                CanJump = false;
            }
        }
        else
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (Input.GetTouch(i).position.x > halfScreenWidth)
                {
                    if (w.right < 15)
                    {
                        w.right += 0.5f;
                    }
                    SidewaysForce = 25;
                }
                else if (Input.GetTouch(i).position.x < halfScreenWidth)
                {
                    if (w.left < 15)
                    {
                        w.left += 0.5f;
                    }
                    SidewaysForce = -25;
                }
            }
        }
 
         if (w.right > 0)
             w.right -= 0.5f;
        if (w.left > 0)
           w.left -= 0.5f;
        if (Input.touchCount == 0)
        {
            SidewaysForce = 0;

        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (SidewaysForce !=0)
        {
            rb.AddForce(SidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }
        if(rb.position.y < 0)
        {
            //Debug.Log("PlayerMovement postion <0");
            FindObjectOfType<Game_Manager>().EndGame();
        }

        /*float step = speed * Time.deltaTime*2; // calculate distance to move
        float momentum = Mathf.Abs(transform.position.z)/100;
        //Debug.Log("fire");
        Vector3 TargetPosition = new Vector3(transform.position.x, transform.position.y, 0f);
        transform.position = Vector3.MoveTowards(transform.position, TargetPosition, step*momentum);*/
    }
}
