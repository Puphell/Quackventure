using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviour
{
    private bool isFirstTime = true; // Ýlk kez mi oyuna giriliyor?

    public float miniGameHour = 24f;
    public Button miniGameButton;
    public Animator miniGameAnimator;
    public Text miniGameHourText; // Text objesi

    private DateTime lastOpenTime; // Son talep edilen tarih ve saat

    void Start()
    {
        // PlayerPrefs'ten isFirstTime deðerini kontrol et
        if (PlayerPrefs.HasKey("isFirstTimeForMiniGame"))
        {
            isFirstTime = PlayerPrefs.GetInt("isFirstTimeForMiniGame") == 1;
        }

        // Ýlk kez giriþ yapýlýyorsa ve introPanel aktif deðilse IntroPanel'i aktif hale getir
        if (isFirstTime)
        {
            miniGameButton.interactable = true;
            PlayerPrefs.SetInt("isFirstTimeForMiniGame", 0);
            PlayerPrefs.Save();
        }

        // Son talep edilen zamaný kontrol et
        if (PlayerPrefs.HasKey("LastOpenTime"))
        {
            string lastClaimTimeString = PlayerPrefs.GetString("LastOpenTime");
            lastOpenTime = DateTime.Parse(lastClaimTimeString);
        }
        else
        {
            // Ýlk kez oynuyorsa, baþlangýç zamanýný kaydet
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
        // Son talep edilen tarihin üzerinden geçen süreyi kontrol et
        TimeSpan timeSinceLastClaim = DateTime.Now - lastOpenTime;
        if (timeSinceLastClaim.TotalHours >= miniGameHour)
        {
            miniGameButton.interactable = true;
        }
        else
        {
            miniGameButton.interactable = false;
            UpdateTimerText(timeSinceLastClaim); // Süreyi güncelle
        }
    }

    void UpdateTimerText(TimeSpan remainingTime)
    {
        TimeSpan timeLeft = TimeSpan.FromHours(miniGameHour) - remainingTime;
        miniGameHourText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", timeLeft.Hours, timeLeft.Minutes, timeLeft.Seconds);
    }

    public void miniGame()
    {
        // Son talep edilen tarih ve saati güncelle
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
