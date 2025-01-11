using GoogleMobileAds.Api;
using Interstitial;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RewardedAdScript : MonoBehaviour
{
    public GameObject RewardAdPanel;

    public Button RewardAdButton;

    private RewardedAd _rewardedAd;
    private string _adRewardUnitId = "ca-app-pub-3304006097050661/1238095860";

    private void Start()
    {
        LoadRewardedAd();

        if (RewardAdButton == null)
        {
            GameObject rewardAdGameObject = GameObject.Find("RewardAdButton");
            if (rewardAdGameObject != null)
            {
                RewardAdButton = rewardAdGameObject.GetComponent<Button>();
                Debug.Log("RewardAdButton baþarýyla bulundu!");
            }
            else
            {
                Debug.LogError("RewardAdButton GameObject bulunamadý!");
                return;
            }
        }

        RewardAdButton.onClick.AddListener(ShowRewardedAd);
    }

    public void ShowRewardedAd()
    {
        const string rewardMsg =
            "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

        if (_rewardedAd != null && _rewardedAd.CanShowAd())
        {
            _rewardedAd.Show((Reward reward) =>
            {
                int rewardAmount = 100; // Ödül miktarý
                                        // TotalCoinTextUpdater script'ine eriþim saðlayarak ödül miktarýný güncelleyebilirsiniz
                TotalCoinTextUpdater coinUpdater = FindObjectOfType<TotalCoinTextUpdater>();
                if (coinUpdater != null)
                {
                    RewardAdPanel.SetActive(true);

                    coinUpdater.GiveCoinReward(rewardAmount);
                }
                Debug.Log(String.Format(rewardMsg, reward.Type, reward.Amount));
            });
            RegisterReloadHandler(_rewardedAd);
        }
    }

    private void RegisterReloadHandler(RewardedAd ad)
    {
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Rewarded Ad full screen content closed.");

            // Reload the ad so that we can show another as soon as possible.
            LoadRewardedAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed to open full screen content " +
                           "with error : " + error);

            // Reload the ad so that we can show another as soon as possible.
            LoadRewardedAd();
        };
    }
    public void LoadRewardedAd()
    {
        // Clean up the old ad before loading a new one.
        if (_rewardedAd != null)
        {
            _rewardedAd.Destroy();
            _rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        RewardedAd.Load(_adRewardUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                _rewardedAd = ad;
            });
    }
    public void CloseRewardAdPanel()
    {
        RewardAdPanel.SetActive(false);
    }
}