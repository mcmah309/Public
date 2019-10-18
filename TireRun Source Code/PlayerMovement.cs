using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public PitchRollYaw w;
    public float speed = 1f;
    public bool canUseLife = true;


    private int SidewaysForce = 0;
    private bool jump=false;
    private int JumpForce = 350;
    public int CanJump = 0;
    private AudioManager audioManger;

    private void Start()
    {
       audioManger=FindObjectOfType<AudioManager>();
    }
    void Update()
    {
        if (Input.GetKey("d") )
        {
            if (w.right < 15) { 
            w.right += 0.5f; }
            SidewaysForce = 50;
        }
        else
        {
            if (w.right > 0)
                w.right -= 0.5f; ;
        }
        if (Input.GetKey("a"))
        {
            if (w.left < 15)
            {
                w.left += 0.5f;
            }
            SidewaysForce = -50;
            
        }
        else
        {
            if (w.left > 0)
                w.left -= 0.5f; ;
        }
        if (!(Input.GetKey("a") || Input.GetKey("d")))
        {
            SidewaysForce = 0;

        }
        if (Input.GetKey("w") && CanJump==1)
        {
            audioManger.Play("Bounce");
            jump = true;
            CanJump = 0;
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
        if (jump)
        {
            rb.AddForce(0, JumpForce * Time.deltaTime, 0, ForceMode.VelocityChange);
            jump = false;
        }

        /*float step = speed * Time.deltaTime*2; // calculate distance to move
        float momentum = Mathf.Abs(transform.position.z)/100;
        //Debug.Log("fire");
        Vector3 TargetPosition = new Vector3(transform.position.x, transform.position.y, 0f);
        transform.position = Vector3.MoveTowards(transform.position, TargetPosition, step*momentum);*/
    }
}
