using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterCoin : MonoBehaviour
{
    void Awake()
    {
        var boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = true;
    }
    void OnTriggerEnter(Collider info)
    {
        if (info.tag == "Player")
        {
            FindObjectOfType<AudioManager>().Play("CoinCollected");
            GameObject.FindGameObjectWithTag("PointsToSCreen").GetComponent<AddPoints>().points = 5;
            GameObject.FindGameObjectWithTag("PointsToSCreen").GetComponent<AddPoints>().show = 1;
            GameObject.FindGameObjectWithTag("PointsToSCreen").GetComponent<AddPoints>().time = 1;
            GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().x += 5;
            Destroy(gameObject);
        }
    }

}
