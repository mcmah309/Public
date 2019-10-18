using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWheel : MonoBehaviour
{
    public Transform wheel;
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = wheel.position;
        transform.Rotate(0f, -10f, 0f);
    }
}
