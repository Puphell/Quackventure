using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using Interstitial;
using GoogleMobileAds.Api;
using System;

namespace Interstitial
{
    public class ShowAdForPuzzle : MonoBehaviour
    {
        public GameObject removeAdsButton;

        public bool canShowAdBool;

        private string _adInterstitialUnitId = "ca-app-pub-3304006097050661/9944681897";

        bool isInterstitialAdLoaded = false;

        AdsManager adsManager;

        private static AdsManager _instance;

        private InterstitialAd _interstitialAd;

        public delegate void AdClosedDelegate();

        public static event AdClosedDelegate OnAdClosed;

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
        void Awake()
        {
            LoadInterstitialAd();

            // PlayerPrefs'ten "RemoveAds" anahtarının değerini kontrol et
            int removeAdsValue = PlayerPrefs.GetInt("RemoveAds", 0);

            // Eğer değer 1 ise, canShowAdBool'u false olarak ayarla
            // Değer 0 ise, canShowAdBool'u true olarak ayarla
            canShowAdBool = removeAdsValue == 1 ? false : true;

            if (canShowAdBool == false)
            {
                removeAdsButton.SetActive(false);
            }
        }

        void Start()
        {
            Invoke("ShowAdFor1sn", 0.1f);
        }

        private void ShowAdFor1sn()
        {
            ShowGameOverPanel();
        }

        public void ShowGameOverPanel()
        {
            if (canShowAdBool)
            {
                ShowInterstitialAd();
            }
            else
            {
                Time.timeScale = 1f;
            }


            if (!canShowAdBool)
            {
                Time.timeScale = 1f;
            }
        }

        #region Geci�Reklam�
        public void LoadInterstitialAd()
        {
            if (!isInterstitialAdLoaded)
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
                isInterstitialAdLoaded = true;
            }
        }

        public void ShowInterstitialAd()
        {
            if (_interstitialAd != null && _interstitialAd.CanShowAd())
            {
                Debug.Log("Showing interstitial ad.");

                // Oyunu durdur
                Time.timeScale = 0;

                // Register a callback to be notified when the interstitial ad is closed
                _interstitialAd.OnAdFullScreenContentClosed += OnInterstitialAdClosed;

                // Show the interstitial ad
                _interstitialAd.Show();

                Invoke("TimeScale1", 1f);
            }
            else
            {
                Debug.LogError("Interstitial ad is not ready yet.");
            }
        }

        private void TimeScale1()
        {
            Time.timeScale = 1;
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

        void OnDisable()
        {
            // Oyun kapandığında PlayerPrefs'e durumu kaydetme
            PlayerPrefs.SetInt("RemoveAds", canShowAdBool ? 0 : 1);
            PlayerPrefs.Save();
        }
    }

}