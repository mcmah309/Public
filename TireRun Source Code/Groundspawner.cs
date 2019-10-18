using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Groundspawner : MonoBehaviour
{
    private Vector3 SpawnPoint;
    //public Transform PositionOfThisObject;
    private bool RespawnTrigger = true;

    public GameObject GroundPrefab;
    private float length;
    void Start()
    {
        SpawnPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        //Debug.Log(transform.localScale.z);
        length = transform.localScale.z;
    }
    void FixedUpdate()
    {
        //Debug.Log(SpawnPoint);
        //Debug.Log(transform.position.z);
        if (RespawnTrigger) {
            if (transform.position.z <= (SpawnPoint.z - (length -3.5)))
            {
                RespawnTrigger = false;
                GroundBlock();
            }
        }
        if(transform.position.z < -20)
        {
            Destroy(gameObject);
        }

    }

    void GroundBlock()
    {

      Instantiate(GroundPrefab, SpawnPoint, Quaternion.identity);

    }
}
