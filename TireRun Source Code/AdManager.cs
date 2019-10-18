using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;
using UnityEngine.Advertisements;
public class AdManager : MonoBehaviour
{
    private AdManager instance;

    //private string appleStoreID = "3276305";
    private string googleStoreID = "3276304";

    private string video_ad = "video";
    private string rewardedVideo_Ad = "rewardedVideo";
    private string banner_Ad = "banner";

    private bool testMode = false;

   private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance=this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        Monetization.Initialize(googleStoreID, testMode);
        try
        {
            Advertisement.Initialize(googleStoreID, testMode);
        }
        catch { }

        //Advertisement.Initialize(googleStoreID, testMode);
        //Monetization.Initialize(googleStoreID, testMode);

        //Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
        StartCoroutine(PlayBanner());
    }
    public void PlayVideo()
    {
        if (Monetization.IsReady(video_ad))
        {
            ShowAdPlacementContent ad = Monetization.GetPlacementContent(video_ad) as ShowAdPlacementContent;
            if(ad != null)
            {
                ad.Show();
            }
        }
    }
    public void PlayRewardedVideo()
    {
        if (Monetization.IsReady(rewardedVideo_Ad))
        {
            ShowAdPlacementContent ad = Monetization.GetPlacementContent(rewardedVideo_Ad) as ShowAdPlacementContent;
            if (ad != null)
            {
                ad.Show();
            }
        }
    }
    public IEnumerator PlayBanner()
    {
            while (!Advertisement.IsReady(banner_Ad))
            {
                yield return new WaitForSeconds(0.5f);
            }
            Advertisement.Banner.Show(banner_Ad);
    
    }
}
