using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class ItemPageLoad : MonoBehaviour
{
    void Start()
    {
        int numberOfItems = transform.GetChild(0).childCount;
        //Debug.Log(transform.GetChild(0).childCount);
        GameObject ItemHolder = transform.GetChild(0).gameObject;
        for (int i = 0; i < numberOfItems; i++)
        {
            string name = ItemHolder.transform.GetChild(i).GetChild(1).GetChild(1).GetComponent<TMP_Text>().text;
            string[] splitName = name.Split(' ');
            //name = "HasBought" + splitName[0];
            //Debug.Log(name);
            if (PlayerPrefs.GetInt(name) == 1)
            {
                //Debug.Log("hi1");
                ItemHolder.transform.GetChild(i).GetChild(1).GetChild(0).GetComponent<Text>().text = "";

            }
        }
    }

}
