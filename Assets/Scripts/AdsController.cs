using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsController : MonoBehaviour
{
    private string GooglePlay_ID = "3734973";
    [SerializeField] private bool testMode;

    void Start()
    {
        Advertisement.Initialize(GooglePlay_ID, testMode);
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
