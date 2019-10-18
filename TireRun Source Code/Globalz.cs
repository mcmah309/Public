using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globalz : MonoBehaviour
{
    public int globalz = 0;

    public GameObject Grass;
    private void Update()
    {
        var x = Instantiate(Grass, transform.position, Quaternion.identity);
        x.transform.parent = gameObject.transform;
    }
}
