using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class UnityMonetization : MonoBehaviour, IUnityAdsListener
{
    public static UnityMonetization instance;
    string GooglPlayId = "4083307";
    bool TestMode = true;

    //[SerializeField] Text scoreText;

    string myPlacementId = "rewardedVideo";

    void Awake()
    {
        instance = new UnityMonetization();
        //If we don't currently have a GameManager...
        if (instance == null)
            //...set this one to be it...
            instance = this;
        //...otherwise...
        else if (instance != this)
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        Advertisement.AddListener(this);

        Advertisement.Initialize(GooglPlayId, TestMode);
    }

    public void DisplayInterstitialAd()
    {
        //GameManager.instance.ButtonAudio();
        //GameManager.instance.isGameOver = true;
        //GameManager.instance.isPaused = true;
        Advertisement.Show();
    }

    public void DisplayRewardedVideoAd()
    {
        GameManager.instance.ButtonAudio();
        GameManager.instance.isGameOver = true;
        GameManager.instance.isPaused = true;
        Advertisement.Show(myPlacementId);
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            //GameManager.instance.Restart();
            if (placementId == myPlacementId)
            {
                // Reward the user for watching the ad to completion.
                GameManager.instance.RewardAd();
            }
            else if (placementId != myPlacementId)
            {
                Debug.Log("Reward NOT granted");
                GameManager.instance.GameOver();
            }
        }
        else if (showResult == ShowResult.Skipped)
        {
            GameManager.instance.GameOver();
            //GameManager.instance.Restart();
            //Do not reward the user for skipping the ad.
            //if (placementId != myPlacementId)
            //{
            //    Debug.Log("Reward NOT granted");
            //}
        }
        else if (showResult == ShowResult.Failed)
        {
            GameManager.instance.GameOver();
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        //GameManager.instance.Restart();
        // If the ready Placement is rewarded, show the ad:
        //if (placementId == myPlacementId)
        //{

        //}
    }

    public void OnUnityAdsDidError(string message)
    {
        //GameManager.instance.Restart();
        // Log the error.
    }

    public void OnUnityAdsDidStart(string surfacingId)
    {
        //GameManager.instance.Restart();
        // Optional actions to take when the end-users triggers an ad.
    }
}
