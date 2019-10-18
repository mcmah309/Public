using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberOfLives : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<Text>().text = PlayerPrefs.GetInt("NumberOfLives").ToString();
    }

}
