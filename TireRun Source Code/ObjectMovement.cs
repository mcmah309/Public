
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    private Rigidbody rb;
    public int move = 1;
    public Transform PlayerLocation;

    private float Speed = 30;
    private float BaseSpeed = 30;
    public int time = 0;
    public float timer = 5;
    private bool play = true;

    private void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        PlayerLocation = GameObject.Find("wheel").transform;
        //FindObjectOfType<AudioManager>().Play("Car");

    }
    void Update()
    {
        if(transform.position.y <-1 || transform.position.z < -10)
        {
                Destroy(gameObject);
        }
        if (time == 1)
        {
            if(timer < 0)
            {
                Destroy(gameObject);
            }
            timer -= Time.deltaTime;
        }
        /*if(Vector3.Distance(transform.position, PlayerLocation.position) < 2 && play)
        {
            FindObjectOfType<AudioManager>().Play("Swoosh");
            play = false;
        }*/
    }

    // Update is called once per frame
    void FixedUpdate()
    {

      transform.Translate(Vector3.back * Time.deltaTime * Speed);



     Speed = BaseSpeed + Mathf.Pow(1.5f * Mathf.Log(Time.timeSinceLevelLoad), 2);

        //int randomnumber = Random.Range(0, 10);
        /* if (randomnumber == 1)
         {
             rb.AddForce(new Vector3(-1, -0, -1) * Time.deltaTime * Speed*300 );
         }
         if (randomnumber == 2)
         {
             rb.AddForce(new Vector3(1, 0, -1) * Time.deltaTime * Speed*300 );
         }*/

    }
}
