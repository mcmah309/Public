using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRewardedVideo : MonoBehaviour
{
public void Play()
    {
        FindObjectOfType<AdManager>().PlayRewardedVideo();
        int x = PlayerPrefs.GetInt("NumberOfCoins");
        x += 30;
        PlayerPrefs.SetInt("NumberOfCoins", x);
    }
}
