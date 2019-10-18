using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class StopBannerAd : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        Advertisement.Banner.Hide();
    }

}
