using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class BuyItem : MonoBehaviour
{
    GameObject f;
public void BuyThisItem(GameObject obj)
    {
        //PlayerPrefs.SetInt("NumberOfCoins", 10000000);
        //Debug.Log(transform.GetChildCount());

        f = obj;

        int? w;
        int z;
        string stringPrice = obj.transform.GetChild(0).gameObject.GetComponent<Text>().text;
        if(stringPrice.Length == 0)
        {
            string name = obj.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text;
            string[] splitName = name.Split(' ');
            switch (splitName[0])
            {
                case "Tire":
                    PlayerPrefs.SetInt("wheel", 0);
                    break;
                case "Bronze":
                    PlayerPrefs.SetInt("wheel", 1000);
                    break;
                case "Silver":
                    PlayerPrefs.SetInt("wheel", 10000);
                    break;
                case "Gold":
                    PlayerPrefs.SetInt("wheel", 100000);
                    break;
            }
            obj.GetComponent<Image>().color = new Color(0, 1, 0);
            Invoke("release", 0.25f);
            return;
        }
        int price = Int32.Parse(stringPrice);
        int x = PlayerPrefs.GetInt("NumberOfCoins");
        bool canBuy = (price < x) ? true : false;
        if (canBuy)
        {
            switch (price)
            {
                case 0:
                    PlayerPrefs.SetInt("wheel", 0);
                    break;
                case 100:
                    z = PlayerPrefs.GetInt("NumberOfLives");
                    z += 1;
                    PlayerPrefs.SetInt("NumberOfLives", z);
                    x -= price;
                    PlayerPrefs.SetInt("NumberOfCoins", x);
                    break;
                case 1000:
                    PlayerPrefs.SetInt("wheel", 1000);
                    w = PlayerPrefs.GetInt("HasBoughtBronze");
                    if (w != 1)//if PlayerPrefs int is not set it returns 0
                    {
                        PlayerPrefs.SetInt("HasBoughtBronze", 1);
                        obj.transform.GetChild(0).gameObject.GetComponent<Text>().text = "";
                        x -= price;
                        PlayerPrefs.SetInt("NumberOfCoins", x);
                        //Buy Object
                    }
                    break;
                case 10000:
                    PlayerPrefs.SetInt("wheel", 10000);
                    w = PlayerPrefs.GetInt("HasBoughtSilver");
                    //Debug.Log(w);
                    if (w !=1)
                    {
                        PlayerPrefs.SetInt("HasBoughtSilver", 1);
                        obj.transform.GetChild(0).gameObject.GetComponent<Text>().text = "";
                        x -= price;
                        PlayerPrefs.SetInt("NumberOfCoins", x);
                        //buy
                    }
                    break;
                case 100000:
                    PlayerPrefs.SetInt("wheel", 100000);
                    w = PlayerPrefs.GetInt("HasBoughtGold");
                    if (w !=1)
                    {
                        PlayerPrefs.SetInt("HasBoughtGold", 1);
                        obj.transform.GetChild(0).gameObject.GetComponent<Text>().text = "";
                        x -= price;
                        PlayerPrefs.SetInt("NumberOfCoins", x);
                        //buy
                    }
                    break;
            }
            obj.GetComponent<Image>().color = new Color(0, 1, 0);
            Invoke("release", 0.25f);
            return;
        }
        else
        {
            return;
        }
        //Debug.Log(obj.transform.GetChild(0).name);
    }
    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
    private void release()
    {
        f.GetComponent<Image>().color = Color.white;
    }
}
