using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Interstitial
{
    public class AdsManager : MonoBehaviour
    {
        private InterstitialAd _interstitialAd;
        private string _adInterstitialUnitId = "ca-app-pub-3304006097050661/9944681897";

        private static AdsManager _instance;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            MobileAds.Initialize(initStatus => { });
            LoadInterstitialAd();
        }

        public static AdsManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject adsManagerObject = new GameObject("AdsManager");
                    _instance = adsManagerObject.AddComponent<AdsManager>();
                }
                return _instance;
            }
        }

        public bool IsAdShowing()
        {
            if (_interstitialAd != null)
            {
                return _interstitialAd.CanShowAd();
            }
            else
            {
                return false;
            }
        }

        public delegate void AdClosedDelegate();

        public static event AdClosedDelegate OnAdClosed;

        void Start()
        {
            // Initialize the Google Mobile Ads SDK.
            MobileAds.Initialize((InitializationStatus initStatus) =>
            {
                // This callback is called once the MobileAds SDK is initialized.
            });

            LoadInterstitialAd();
        }


        #region Geci�Reklam�
        public void LoadInterstitialAd()
        {
            // Clean up the old ad before loading a new one.
            if (_interstitialAd != null)
            {
                _interstitialAd.Destroy();
                _interstitialAd = null;
            }

            Debug.Log("Loading the interstitial ad.");

            // create our request used to load the ad.
            var adRequest = new AdRequest();

            // send the request to load the ad.
            InterstitialAd.Load(_adInterstitialUnitId, adRequest,
                (InterstitialAd ad, LoadAdError error) =>
                {
                    // if error is not null, the load request failed.
                    if (error != null || ad == null)
                    {
                        Debug.LogError("interstitial ad failed to load an ad " +
                                       "with error : " + error);
                        return;
                    }

                    Debug.Log("Interstitial ad loaded with response : "
                              + ad.GetResponseInfo());

                    _interstitialAd = ad;
                });
        }

        public void ShowInterstitialAd()
        {
            if (_interstitialAd != null && _interstitialAd.CanShowAd())
            {
                Debug.Log("Showing interstitial ad.");

                // Register a callback to be notified when the interstitial ad is closed
                _interstitialAd.OnAdFullScreenContentClosed += OnInterstitialAdClosed;

                // Show the interstitial ad
                _interstitialAd.Show();
            }
            else
            {
                Debug.LogError("Interstitial ad is not ready yet.");
            }
        }


        private void OnInterstitialAdClosed()
        {
            Debug.Log("Interstitial ad closed.");

            // Unregister the callback
            _interstitialAd.OnAdFullScreenContentClosed -= OnInterstitialAdClosed;

            // Invoke the OnAdClosed event
            if (OnAdClosed != null)
            {
                OnAdClosed();
            }
            Time.timeScale = 0f;
            // Load the next scene after the interstitial ad is closed
        }


        private void OnInterstitialAdEvent()
        {
            Debug.Log("Interstitial ad closed.");

            // Unregister the callback
            _interstitialAd.OnAdFullScreenContentClosed -= OnInterstitialAdEvent;

            // Invoke the OnAdClosed event
            if (OnAdClosed != null)
            {
                OnAdClosed();
            };
            Time.timeScale = 0f;
        }

        private void RegisterEventHandlers(InterstitialAd interstitialAd)
        {
            // Raised when the ad is estimated to have earned money.
            interstitialAd.OnAdPaid += (AdValue adValue) =>
            {
                Debug.Log(String.Format("Interstitial ad paid {0} {1}.",
                    adValue.Value,
                    adValue.CurrencyCode));
            };
            // Raised when an impression is recorded for an ad.
            interstitialAd.OnAdImpressionRecorded += () =>
            {
                Debug.Log("Interstitial ad recorded an impression.");
            };
            // Raised when a click is recorded for an ad.
            interstitialAd.OnAdClicked += () =>
            {
                Debug.Log("Interstitial ad was clicked.");
            };
            // Raised when an ad opened full screen content.
            interstitialAd.OnAdFullScreenContentOpened += () =>
            {
                Debug.Log("Interstitial ad full screen content opened.");
            };
            // Raised when the ad closed full screen content.
            interstitialAd.OnAdFullScreenContentClosed += () =>
            {
                Debug.Log("Interstitial ad full screen content closed.");
            };
            // Raised when the ad failed to open full screen content.
            interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
            {
                Debug.LogError("Interstitial ad failed to open full screen content " +
                               "with error : " + error);
            };
        }
        private void RegisterReloadHandler(InterstitialAd interstitialAd)
        {
            // Raised when the ad closed full screen content.
            interstitialAd.OnAdFullScreenContentClosed += () =>
            {
                Debug.Log("Interstitial Ad full screen content closed.");

                // Reload the ad so that we can show another as soon as possible.
                LoadInterstitialAd();
            };
            // Raised when the ad failed to open full screen content.
            interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
            {
                Debug.LogError("Interstitial ad failed to open full screen content " +
                               "with error : " + error);

                // Reload the ad so that we can show another as soon as possible.
                LoadInterstitialAd();
            };
        }
        #endregion
    }
}
