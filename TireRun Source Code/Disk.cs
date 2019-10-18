using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disk : MonoBehaviour
{

    public Transform wheel;
 
    void FixedUpdate()
    {
        transform.localPosition = wheel.localPosition;
        transform.localRotation = wheel.localRotation;
       /* transform.localScale = wheel.localScale;
        transform.rotation = wheel.rotation;
        if (!((-1 < transform.eulerAngles.y) && (transform.eulerAngles.y < 1)))
        {
            if (transform.eulerAngles.y > 0)
            {
                transform.Rotate(0, -0.1f, 0);
            }
            else
            {
                transform.Rotate(0, 0.1f, 0);
            }
        }*/


    }
}
