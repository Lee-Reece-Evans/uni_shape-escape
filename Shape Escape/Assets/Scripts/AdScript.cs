using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;

// Ads Youtube PpTheBest Monetize Your Game https://www.youtube.com/watch?v=lNDG6E3etdw&list=PLPL9_77MOo2L3BSNQgraIOByxsdEwE0sp&index=5&t=0s  18/11/2019

public class AdScript : MonoBehaviour
{

    private string AdID = "3317613";

    private string VideoID = "video";


    bool HasPlayedAlready = false;

    // Start is called before the first frame update
    void Start()
    {

        Monetization.Initialize(AdID, true);
    }

    public void ShowAd()
    {
        if (Monetization.IsReady(VideoID) && HasPlayedAlready == false)
        {
            ShowAdPlacementContent ad = null;
            ad = Monetization.GetPlacementContent(VideoID) as ShowAdPlacementContent;
            HasPlayedAlready = true;
            if (ad != null)
            {
                ad.Show();



            }
        }
    }


    void Update()
    {
        if (!HasPlayedAlready)
        {
            ShowAd();
        }
    }


}