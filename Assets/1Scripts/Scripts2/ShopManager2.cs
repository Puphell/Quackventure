using UnityEngine;
using UnityEngine.Purchasing;
using System;
using UnityEngine.Purchasing.Extension;
using UnityEngine.UI;
using Interstitial;

public class InAppPurchaseManager : MonoBehaviour, IDetailedStoreListener
{
    public Button noAdsButton;

    public GameObject removeAdsPanel;
    public Animator removeAdsAnimator;
    SpikeShowAd spikeShowAd;

    public GameObject ThankYouForBuy1;
    public GameObject ThankYouForBuy2;
    public GameObject ThankYouForBuy3;

    public AudioClip claimSound;
    public AudioClip removeAdsSound;

    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider;

    public string product1000Coins = "1000_coins";
    public string product2000Coins = "2000_coins";
    public string product3000Coins = "3000_coins";
    public string productremoveAds = "remove_ads";

    public Animator addCoinAnim1000;
    public Animator addCoinAnim2000;
    public Animator addCoinAnim3000;

    public GameObject addCoinAnimGameobject1000;
    public GameObject addCoinAnimGameobject2000;
    public GameObject addCoinAnimGameobject3000;

    void Start()
    {
        removeAdsPanel.SetActive(false);
        addCoinAnimGameobject1000.SetActive(false);
        addCoinAnimGameobject2000.SetActive(false);
        addCoinAnimGameobject3000.SetActive(false);

        spikeShowAd = FindObjectOfType<SpikeShowAd>();

        InitializePurchasing();
    }

    public void InitializePurchasing()
    {
        if (IsInitialized())
        {
            return;
        }

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(product1000Coins, ProductType.Consumable);
        builder.AddProduct(product2000Coins, ProductType.Consumable);
        builder.AddProduct(product3000Coins, ProductType.Consumable);
        builder.AddProduct(productremoveAds, ProductType.NonConsumable);

        UnityPurchasing.Initialize(this, builder);
    }

    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    public void Buy1000Coins()
    {
        BuyProductID(product1000Coins);
    }

    public void Buy2000Coins()
    {
        BuyProductID(product2000Coins);
    }

    public void Buy3000Coins()
    {
        BuyProductID(product3000Coins);
    }

    public void RemoveAds()
    {
        Debug.Log("RemoveAds function is called.");
        BuyProductID(productremoveAds);
    }

    private void BuyProductID(string productId)
    {
        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(productId);
            if (product != null && product.availableToPurchase)
            {
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("Product not found or not available for purchase: " + productId);
            }
        }
        else
        {
            Debug.Log("Purchase failed: Store not initialized.");
        }
    }

    public void PlayClaimSound()
    {
        AudioSource.PlayClipAtPoint(claimSound, Camera.main.transform.position);
    }

    public void PlayRemoveAdsSound()
    {
        AudioSource.PlayClipAtPoint(removeAdsSound, Camera.main.transform.position);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("Initialization failed: " + error);
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.Log("Initialization failed: " + error + ". Message: " + message);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log("Purchase of product " + product.definition.id + " failed due to " + failureReason);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {
        Debug.Log("Purchase of product " + product.definition.id + " failed due to " + failureDescription.reason + ". Message: " + failureDescription.message);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (String.Equals(args.purchasedProduct.definition.id, product1000Coins, StringComparison.Ordinal))
        {
            PlayClaimSound();
            ThankYouForBuy1.SetActive(true);
            int rewardAmount = 1000;
            TotalCoinTextUpdater coinUpdater = FindObjectOfType<TotalCoinTextUpdater>();
            if (coinUpdater != null)
            {
                coinUpdater.GiveCoinReward(rewardAmount);
            }
            Debug.Log("1000 coins purchased.");
        }
        else if (String.Equals(args.purchasedProduct.definition.id, product2000Coins, StringComparison.Ordinal))
        {
            PlayClaimSound();
            ThankYouForBuy2.SetActive(true);
            int rewardAmount = 2000;
            TotalCoinTextUpdater coinUpdater = FindObjectOfType<TotalCoinTextUpdater>();
            if (coinUpdater != null)
            {
                coinUpdater.GiveCoinReward(rewardAmount);
            }
            Debug.Log("2000 coins purchased.");
        }
        else if (String.Equals(args.purchasedProduct.definition.id, product3000Coins, StringComparison.Ordinal))
        {
            PlayClaimSound();
            ThankYouForBuy3.SetActive(true);
            int rewardAmount = 3000;
            TotalCoinTextUpdater coinUpdater = FindObjectOfType<TotalCoinTextUpdater>();
            if (coinUpdater != null)
            {
                coinUpdater.GiveCoinReward(rewardAmount);
            }
            Debug.Log("3000 coins purchased.");
        }
        else if (String.Equals(args.purchasedProduct.definition.id, productremoveAds, StringComparison.Ordinal))
        {
            spikeShowAd = FindObjectOfType<SpikeShowAd>();
            if (spikeShowAd != null)
            {
                Destroy(noAdsButton.gameObject);
                removeAdsPanel.SetActive(true);
                removeAdsAnimator.SetTrigger("Open");
                PlayRemoveAdsSound();
                spikeShowAd.canShowAdBool = false;
                PlayerPrefs.SetInt("RemoveAds", spikeShowAd.canShowAdBool ? 1 : 0);
                PlayerPrefs.Save();
            }
            Debug.Log("Ads removed.");
        }
        else
        {
            Debug.Log("Unrecognized product: " + args.purchasedProduct.definition.id);
        }

        return PurchaseProcessingResult.Complete;
    }

    public void ThankYouForBuy1Close()
    {
        Invoke("ClosePurchaseAnim", 1.2f);
        addCoinAnimGameobject1000.SetActive(true);
        addCoinAnim1000.SetTrigger("Purchase");
        ThankYouForBuy1.SetActive(false);
    }

    public void ThankYouForBuy2Close()
    {
        addCoinAnimGameobject2000.SetActive(true);
        addCoinAnim2000.SetTrigger("Purchase");
        Invoke("ClosePurchaseAnim", 1.2f);
        ThankYouForBuy2.SetActive(false);
    }

    public void ThankYouForBuy3Close()
    {
        Invoke("ClosePurchaseAnim", 1.2f);
        addCoinAnimGameobject3000.SetActive(true);
        addCoinAnim3000.SetTrigger("Purchase");
        ThankYouForBuy3.SetActive(false);
    }

    public void CloseRemoveAdsPanel()
    {
        removeAdsPanel.SetActive(false);
        removeAdsAnimator.SetTrigger("Close");
    }

    private void ClosePurchaseAnim()
    {
        addCoinAnimGameobject1000.SetActive(false);
        addCoinAnimGameobject2000.SetActive(false);
        addCoinAnimGameobject3000.SetActive(false);
    }
}
