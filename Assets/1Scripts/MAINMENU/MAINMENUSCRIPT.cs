using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MAINMENUSCRIPT : MonoBehaviour
{
    public GameObject upgradePanel;
    public GameObject introPanel;

    private bool isFirstTime = true; // �lk kez mi oyuna giriliyor?
    private DateTime lastClaimTime; // Son talep edilen tarih ve saat
    public GameObject dailyPanel; // Daily �d�l paneli
    public int rewardAmount = 100; // �d�l miktar�
    public float claimCooldownHours = 24f; // �d�l talep etme aral���
    public AudioClip claimSound;
    public Animator UpgradeAnimator;
    public GameObject MoreCoinPanel;
    public Animator moreCoinAnimator;
    void Start()
    {
        Animator UpgradeAnimator = GetComponent<Animator>();
        // PlayerPrefs'ten isFirstTime de�erini kontrol et
        if (PlayerPrefs.HasKey("isFirstTime"))
        {
            isFirstTime = PlayerPrefs.GetInt("isFirstTime") == 1;
        }

        // �lk kez giri� yap�l�yorsa ve introPanel aktif de�ilse IntroPanel'i aktif hale getir
        if (isFirstTime && !introPanel.activeSelf && !dailyPanel.activeSelf)
        {
            introPanel.SetActive(true);
            dailyPanel.SetActive(true);
            PlayerPrefs.Save();
        }

        // Son talep edilen zaman� kontrol et
        if (PlayerPrefs.HasKey("LastClaimTime"))
        {
            string lastClaimTimeString = PlayerPrefs.GetString("LastClaimTime");
            lastClaimTime = DateTime.Parse(lastClaimTimeString);
        }
        else
        {
            // �lk kez oynuyorsa, ba�lang�� zaman�n� kaydet
            lastClaimTime = DateTime.Now;
            SaveLastClaimTime();
        }
        // T�m sahnelerde toplanan coin miktar�n� alarak total coin miktar�n� g�ncelle
        int totalCoins = PlayerPrefs.GetInt("TotalCollectedCoins", 0);

        // Daily paneli kontrol et
        CheckDailyPanel();
    }
    void CheckDailyPanel()
    {
        // Son talep edilen tarihin �zerinden ge�en s�reyi kontrol et
        TimeSpan timeSinceLastClaim = DateTime.Now - lastClaimTime;
        if (timeSinceLastClaim.TotalHours >= claimCooldownHours)
        {
            // �d�l talep edilebilir durumda, paneli etkinle�tir
            dailyPanel.SetActive(true);
        }
        else
        {
            // �d�l talep edilemez durumda, paneli devre d��� b�rak
            dailyPanel.SetActive(false);
        }
    }
    public void ClaimButton()
    {
        // Toplam coin miktar�n� g�ncelle
        int totalCoins = PlayerPrefs.GetInt("TotalCollectedCoins", 0);
        totalCoins += rewardAmount;
        PlayerPrefs.SetInt("TotalCollectedCoins", totalCoins);

        // Son talep edilen tarih ve saati g�ncelle
        lastClaimTime = DateTime.Now;
        SaveLastClaimTime();

        PlayClaimSound();

        // Daily paneli devre d��� b�rak
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

    public void CREDITSCL�CKED()
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
        // Upgrade panelini devre d��� b�rak
        upgradePanel.SetActive(false);
    }
    public void BackIntro()
    {
        // isFirstTime de�erini g�ncelle
        PlayerPrefs.SetInt("isFirstTime", 0);
        PlayerPrefs.Save();

        // Daily Panel'i aktif hale getir
        dailyPanel.SetActive(true);

        // Intro panelini devre d��� b�rak
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
