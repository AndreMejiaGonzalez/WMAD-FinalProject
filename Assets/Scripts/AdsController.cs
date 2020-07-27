using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsController : MonoBehaviour
{
    private string GooglePlay_ID = "3734973";

    void Start()
    {
        Advertisement.Initialize(GooglePlay_ID, false);
    }

    public void showInterstitial()
    {
        if(Advertisement.IsReady())
        {
            Advertisement.Show("video");
        } else {
            Debug.Log("Ads not ready.");
        }
    }
}
