using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddPoints : MonoBehaviour
{
    public int show =0;
    public Text AddText;
    public int points = 0;
    public float time = 1;
    void Update()
    {
        if(show == 1)
        {
            AddText.text = "+" + points.ToString("0");
            time -= Time.deltaTime;
            if(time < 0)
            {
                AddText.text = "";
                show = 0;
                time = 1;
            }
        }
    }
}
