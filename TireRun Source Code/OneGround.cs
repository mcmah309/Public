using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneGround : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if(transform.position.z < -150)
        {
            Destroy(gameObject);
        }
    }
}
