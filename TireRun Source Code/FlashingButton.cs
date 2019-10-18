using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashingButton : MonoBehaviour
{
    public Image image;
    private float y = 0.2f;
    private bool w = false;//add
    void Update()
    {
        image.color = new Color(0, y, 0, 1);
        if (y < 0.3)
        {
                w = false;
        }
        else if(y>0.7)
        {
            w = true;
        }
        if (w)
        {
            y -= 0.1f;
        }
        else
        {
            y += 0.1f;
        }
    }
}
