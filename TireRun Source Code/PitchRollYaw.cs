using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PitchRollYaw : MonoBehaviour
{
    public GameObject EndGameScore;


    public float speed = 0.5f;
    public float maxangle = 10f;
    public float left = 0f;
    public float right = 0f;
    private AudioManager audioManger;

    private void Start()
    {
        audioManger = FindObjectOfType<AudioManager>();
        audioManger.Play("WarbleOfWheel");
    }
    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, maxangle * Mathf.Sin(Time.time * speed) + right - left);
        audioManger.Volume("WarbleOfWheel", speed/30);
        if (maxangle > 55f)
        {
            audioManger.Volume("HittingCar", 0);
            audioManger.Stop("WarbleOfWheel");
            //GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerMovementTouch>().enabled = false;
            GetComponent<PitchRollYaw>().enabled = false;
            GetComponentInChildren<RotateWheel>().enabled = false;
            GameObject.Find("score").GetComponent<Score>().on = false;
            Invoke("EndTheGame", 2);
        }
        else if (maxangle > 0)
        {
            maxangle -= maxangle / 200;
            
        }
        if(speed > 2)
        {
            speed -= speed / 100;
        }

    }
    void EndTheGame()
    {
        int ran = Random.Range(1, 5);
        if (ran ==2)
        {
            FindObjectOfType<AdManager>().PlayVideo();
        }
        EndGameScore.SetActive(true);
        //if (!(GameObject.Find("wheel").GetComponent<PlayerMovement>().canUseLife))
        if (!(GameObject.Find("wheel").GetComponent<PlayerMovementTouch>().canUseLife))
        {
            GameObject.Find("UseExtraLife").SetActive(false);
        }
        Time.timeScale = 0;
    }
}
