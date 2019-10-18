using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Rotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.eulerAngles= new Vector3(90, 0, 90);
        //Quaternion rotation = Quaternion.Euler(90, 0, 90);
        //transform.Quaternion
    }


}
