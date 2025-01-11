using UnityEngine;
using UnityEngine.UI;

public class UrlManager : MonoBehaviour
{
    public GameObject prizeObject;
    private string urlInstagram = "https://www.instagram.com/pereffectgames/?next=%2Fzilaa.ia%2F";
    private string urlX = "https://twitter.com/PerEffect0";
    private string urlTikTok = "https://www.tiktok.com/@quackventuregame?_t=8l8hW7CYaPO&_r=1";
    private string urlPrivatePolicy = "https://doc-hosting.flycricket.io/quackventure-privacy-policy/d1348378-8407-4140-a6af-cece263214fd/privacy";
    private string urlGooglePlay = "https://play.google.com/store/apps/dev?id=8638426958129596034";


    private const string InstagramKey = "InstagramClicked";
    private const string XKey = "XClicked";
    private const string TikTokKey = "TikTokClicked";

    public TotalCoinTextUpdater coinUpdater;
    public Text totalCoinText; // Toplam coin miktar�n� g�stermek i�in text objesi

    private void Start()
    {
        prizeObject.SetActive(false);
        // T�m sahnelerde toplanan coin miktar�n� alarak total coin miktar�n� g�ncelle
        int totalCoins = PlayerPrefs.GetInt("TotalCollectedCoins", 0); // Toplanan coin miktar�n� al, yoksa varsay�lan de�eri kullan
        totalCoinText.text = totalCoins.ToString(); // Toplam coin miktar�n� g�ster

        // E�er daha �nce tetiklenmi�se, PlayerPrefs'ten durumlar� y�kle
        if (PlayerPrefs.HasKey(InstagramKey))
        {
            if (PlayerPrefs.GetInt(InstagramKey) == 1)
                GetComponent<Button>().interactable = false;
        }
        if (PlayerPrefs.HasKey(XKey))
        {
            if (PlayerPrefs.GetInt(XKey) == 1)
                GetComponent<Button>().interactable = false;
        }
        if (PlayerPrefs.HasKey(TikTokKey))
        {
            if (PlayerPrefs.GetInt(TikTokKey) == 1)
                GetComponent<Button>().interactable = false;
        }
    }

    public void Instagram()
    {
        if (PlayerPrefs.HasKey(InstagramKey))
        {
            Application.OpenURL(urlInstagram);
        }
        if (!PlayerPrefs.HasKey(InstagramKey))
        {
            prizeObject.SetActive(true);
            Application.OpenURL(urlInstagram);
            PlayerPrefs.SetInt(InstagramKey, 1);
            PlayerPrefs.Save();
            coinUpdater.GiveCoinReward(100);
        }
    }

    public void X()
    {
        if (PlayerPrefs.HasKey(XKey))
        {
            Application.OpenURL(urlX);
        }
        if (!PlayerPrefs.HasKey(XKey))
        {
            prizeObject.SetActive(true);
            Application.OpenURL(urlX);
            PlayerPrefs.SetInt(XKey, 1);
            PlayerPrefs.Save();
            coinUpdater.GiveCoinReward(100);
        }
    }

    public void TikTok()
    {
        if (PlayerPrefs.HasKey(TikTokKey))
        {
            Application.OpenURL(urlTikTok);
        }
        if (!PlayerPrefs.HasKey(TikTokKey))
        {
            prizeObject.SetActive(true);
            Application.OpenURL(urlTikTok);
            PlayerPrefs.SetInt(TikTokKey, 1);
            PlayerPrefs.Save();
            coinUpdater.GiveCoinReward(100);
        }
    }

    public void PrivatePolicy()
    {
        Application.OpenURL(urlPrivatePolicy);
    }

    public void GooglePlay()
    {
        Application.OpenURL(urlGooglePlay);
    }

    public void ExitPrizeObject()
    {
        prizeObject.SetActive(false);
    }
}