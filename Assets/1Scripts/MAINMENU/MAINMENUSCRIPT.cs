using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MAINMENUSCRIPT : MonoBehaviour
{
    public GameObject upgradePanel;
    public GameObject introPanel;

    private bool isFirstTime = true; // Ýlk kez mi oyuna giriliyor?
    private DateTime lastClaimTime; // Son talep edilen tarih ve saat
    public GameObject dailyPanel; // Daily ödül paneli
    public int rewardAmount = 100; // Ödül miktarý
    public float claimCooldownHours = 24f; // Ödül talep etme aralýðý
    public AudioClip claimSound;
    public Animator UpgradeAnimator;
    public GameObject MoreCoinPanel;
    public Animator moreCoinAnimator;
    void Start()
    {
        Animator UpgradeAnimator = GetComponent<Animator>();
        // PlayerPrefs'ten isFirstTime deðerini kontrol et
        if (PlayerPrefs.HasKey("isFirstTime"))
        {
            isFirstTime = PlayerPrefs.GetInt("isFirstTime") == 1;
        }

        // Ýlk kez giriþ yapýlýyorsa ve introPanel aktif deðilse IntroPanel'i aktif hale getir
        if (isFirstTime && !introPanel.activeSelf && !dailyPanel.activeSelf)
        {
            introPanel.SetActive(true);
            dailyPanel.SetActive(true);
            PlayerPrefs.Save();
        }

        // Son talep edilen zamaný kontrol et
        if (PlayerPrefs.HasKey("LastClaimTime"))
        {
            string lastClaimTimeString = PlayerPrefs.GetString("LastClaimTime");
            lastClaimTime = DateTime.Parse(lastClaimTimeString);
        }
        else
        {
            // Ýlk kez oynuyorsa, baþlangýç zamanýný kaydet
            lastClaimTime = DateTime.Now;
            SaveLastClaimTime();
        }
        // Tüm sahnelerde toplanan coin miktarýný alarak total coin miktarýný güncelle
        int totalCoins = PlayerPrefs.GetInt("TotalCollectedCoins", 0);

        // Daily paneli kontrol et
        CheckDailyPanel();
    }
    void CheckDailyPanel()
    {
        // Son talep edilen tarihin üzerinden geçen süreyi kontrol et
        TimeSpan timeSinceLastClaim = DateTime.Now - lastClaimTime;
        if (timeSinceLastClaim.TotalHours >= claimCooldownHours)
        {
            // Ödül talep edilebilir durumda, paneli etkinleþtir
            dailyPanel.SetActive(true);
        }
        else
        {
            // Ödül talep edilemez durumda, paneli devre dýþý býrak
            dailyPanel.SetActive(false);
        }
    }
    public void ClaimButton()
    {
        // Toplam coin miktarýný güncelle
        int totalCoins = PlayerPrefs.GetInt("TotalCollectedCoins", 0);
        totalCoins += rewardAmount;
        PlayerPrefs.SetInt("TotalCollectedCoins", totalCoins);

        // Son talep edilen tarih ve saati güncelle
        lastClaimTime = DateTime.Now;
        SaveLastClaimTime();

        PlayClaimSound();

        // Daily paneli devre dýþý býrak
        dailyPanel.SetActive(false);
    }
    public void PlayClaimSound()
    {
        AudioSource.PlayClipAtPoint(claimSound, Camera.main.transform.position);
    }
    void SaveLastClaimTime()
    {
        PlayerPrefs.SetString("LastClaimTime", lastClaimTime.ToString());
        PlayerPrefs.Save();
    }
    public void PLAYClicked()
    {
        Invoke("LevelsScene", 0.5f);
    }
    private void LevelsScene()
    {
        SceneManager.LoadScene("LEVELS");
    }

    public void OPTIONSClicked()
    {
        Invoke("OptionsInvoke", 0.5f);
    }
    private void OptionsInvoke()
    {
        SceneManager.LoadScene("OPTIONS");
    }

    public void CREDITSCLÝCKED()
    {
        Invoke("CreditsInvoke", 0.5f);
    }
    private void CreditsInvoke()
    {
        SceneManager.LoadScene("CREDIT");
    }

    public void Upgrade()
    {
        UpgradeAnimator.SetTrigger("Open");
        // Upgrade panelini aktif hale getir
        upgradePanel.SetActive(true);
    }
    public void MiniGamesLevel()
    {
        SceneManager.LoadScene("MiniGameLevelScene");
    }

    public void Back()
    {
        UpgradeAnimator.SetTrigger("Close");
        Invoke("CloseUpgradePanel", 0.5f);
    }
    private void CloseUpgradePanel()
    {
        // Upgrade panelini devre dýþý býrak
        upgradePanel.SetActive(false);
    }
    public void BackIntro()
    {
        // isFirstTime deðerini güncelle
        PlayerPrefs.SetInt("isFirstTime", 0);
        PlayerPrefs.Save();

        // Daily Panel'i aktif hale getir
        dailyPanel.SetActive(true);

        // Intro panelini devre dýþý býrak
        introPanel.SetActive(false);
    }
    public void OpenMoreCoin()
    {
        moreCoinAnimator.SetTrigger("Open");
        MoreCoinPanel.SetActive(true);
    }

    public void CloseMoreCoin()
    {
        moreCoinAnimator.SetTrigger("Close");
        Invoke("CloseMoreCoinPanel", 0.5f);
    }
    private void CloseMoreCoinPanel()
    {
        MoreCoinPanel.SetActive(false);
    }
    public void SKINCLICKED()
    {
        Invoke("SkinsOpen", 0.5f);
    }
    private void SkinsOpen()
    {
        SceneManager.LoadScene("SKINS");
    }
    public void QUITClicked()
    {
        Application.Quit();
    }
}
