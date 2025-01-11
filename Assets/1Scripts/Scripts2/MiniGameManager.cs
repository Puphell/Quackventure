using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviour
{
    private bool isFirstTime = true; // �lk kez mi oyuna giriliyor?

    public float miniGameHour = 24f;
    public Button miniGameButton;
    public Animator miniGameAnimator;
    public Text miniGameHourText; // Text objesi

    private DateTime lastOpenTime; // Son talep edilen tarih ve saat

    void Start()
    {
        // PlayerPrefs'ten isFirstTime de�erini kontrol et
        if (PlayerPrefs.HasKey("isFirstTimeForMiniGame"))
        {
            isFirstTime = PlayerPrefs.GetInt("isFirstTimeForMiniGame") == 1;
        }

        // �lk kez giri� yap�l�yorsa ve introPanel aktif de�ilse IntroPanel'i aktif hale getir
        if (isFirstTime)
        {
            miniGameButton.interactable = true;
            PlayerPrefs.SetInt("isFirstTimeForMiniGame", 0);
            PlayerPrefs.Save();
        }

        // Son talep edilen zaman� kontrol et
        if (PlayerPrefs.HasKey("LastOpenTime"))
        {
            string lastClaimTimeString = PlayerPrefs.GetString("LastOpenTime");
            lastOpenTime = DateTime.Parse(lastClaimTimeString);
        }
        else
        {
            // �lk kez oynuyorsa, ba�lang�� zaman�n� kaydet
            lastOpenTime = DateTime.Now;
            SaveLastClaimTime();
        }
        CheckDailyPanel();
    }

    void Update()
    {
        CheckDailyPanel();
    }

    void CheckDailyPanel()
    {
        // Son talep edilen tarihin �zerinden ge�en s�reyi kontrol et
        TimeSpan timeSinceLastClaim = DateTime.Now - lastOpenTime;
        if (timeSinceLastClaim.TotalHours >= miniGameHour)
        {
            miniGameButton.interactable = true;
        }
        else
        {
            miniGameButton.interactable = false;
            UpdateTimerText(timeSinceLastClaim); // S�reyi g�ncelle
        }
    }

    void UpdateTimerText(TimeSpan remainingTime)
    {
        TimeSpan timeLeft = TimeSpan.FromHours(miniGameHour) - remainingTime;
        miniGameHourText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", timeLeft.Hours, timeLeft.Minutes, timeLeft.Seconds);
    }

    public void miniGame()
    {
        // Son talep edilen tarih ve saati g�ncelle
        lastOpenTime = DateTime.Now;

        miniGameAnimator.SetTrigger("Close");
        Invoke("miniGameScene", 1f);
    }

    private void miniGameScene()
    {
        SaveLastClaimTime();
        SceneManager.LoadScene("MiniGameScene");
    }

    void SaveLastClaimTime()
    {
        PlayerPrefs.SetString("LastOpenTime", lastOpenTime.ToString());
        PlayerPrefs.Save();
    }
}
