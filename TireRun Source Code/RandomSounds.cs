using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSounds : MonoBehaviour
{
    private AudioManager audioManger;
    private void Start()
    {
        audioManger = FindObjectOfType<AudioManager>();

        InvokeRepeating("RandomHonk", 0, 1f);
    }
    void RandomHonk()
    {
        int rand = Random.Range(0, 10);
        if(rand == 1)
        {
            audioManger.Play("CartoonHonk1");
        }
        if (rand == 2)
        {
            audioManger.Play("CartoonHonk2");
        }
    }
}
