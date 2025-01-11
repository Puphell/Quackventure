using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Move playerMoveScript;
    public AITrackingPlayer aiTrackingPlayer; // AITrackingPlayer scriptine referans
    public Text coinText;
    public Text speedCostText; // Speed upgrade maliyetini g�stermek i�in text objesi
    public Text jumpCostText; // Jump upgrade maliyetini g�stermek i�in text objesi
    public Text trackingSpeedCostText; // Tracking Speed upgrade maliyetini g�stermek i�in text objesi
    public float speedUpgradeAmount = 1f;
    public float jumpUpgradeAmount = 1;
    public float trackingSpeedDecreaseAmount = 1f; // Hedef takip h�z�n� azaltma miktar�
    public int speedUpgradeCost = 100; // Speed upgrade i�in gereken coin miktar�
    public int jumpUpgradeCost = 100; // Jump upgrade i�in gereken coin miktar�
    public int trackingSpeedDecreaseCost = 100; // Tracking Speed upgrade i�in gereken coin miktar�
    public AudioSource buttonClickSound; // Butona bas�ld���nda �al�nacak ses

    private void Start()
    {
        // Oyun ba�lad���nda PlayerPrefs'ten h�z ve z�plama de�erlerini alarak g�ncelleyin
        LoadPlayerPrefs();
        UpdateCostTexts(); // Maliyet metinlerini g�ncelle

       
        // T�m sahnelerde toplanan coin miktar�n� alarak total coin miktar�n� g�ncelle
        int totalCoins = PlayerPrefs.GetInt("TotalCollectedCoins", 0);
        coinText.text = totalCoins.ToString();
    }
    void Update()
    {
        UpdateCostTexts();
    }
    public void UpgradeSpeed()
    {
        // Yeterli coin varsa ve upgrade yap�ld�ysa g�ncelle
        if (HasEnoughCoins(speedUpgradeCost))
        {
            playerMoveScript.speed += speedUpgradeAmount;
            SavePlayerPrefs();
            SubtractCoins(speedUpgradeCost);
            UpdateCostTexts(); // Maliyet metinlerini g�ncelle
            if (buttonClickSound != null)
            {
                buttonClickSound.Play(); // Butona bas�ld���nda ses �al
            }
        }
    }

    public void UpgradeJump()
    {
        // Yeterli coin varsa ve upgrade yap�ld�ysa g�ncelle
        if (HasEnoughCoins(jumpUpgradeCost))
        {
            playerMoveScript.jumpForce += jumpUpgradeAmount;
            SavePlayerPrefs();
            SubtractCoins(jumpUpgradeCost);
            UpdateCostTexts(); // Maliyet metinlerini g�ncelle
            if (buttonClickSound != null)
            {
                buttonClickSound.Play(); // Butona bas�ld���nda ses �al
            }
        }
    }

    public void DecreaseTrackingSpeed()
    {
        // Yeterli coin varsa ve upgrade yap�ld�ysa g�ncelle
        if (HasEnoughCoins(trackingSpeedDecreaseCost))
        {
            aiTrackingPlayer.moveSpeed -= trackingSpeedDecreaseAmount; // AITrackingPlayer scriptine eri�im ve h�z� azaltma
            SavePlayerPrefs();
            SubtractCoins(trackingSpeedDecreaseCost);
            UpdateCostTexts(); // Maliyet metinlerini g�ncelle
            if (buttonClickSound != null)
            {
                buttonClickSound.Play(); // Butona bas�ld���nda ses �al
            }
        }
    }

    private void LoadPlayerPrefs()
    {
        // PlayerPrefs'ten kaydedilmi� speed ve jumpForce de�erlerini al
        if (PlayerPrefs.HasKey("Speed"))
        {
            playerMoveScript.speed = PlayerPrefs.GetFloat("Speed");
        }
        if (PlayerPrefs.HasKey("JumpForce"))
        {
            playerMoveScript.jumpForce = PlayerPrefs.GetFloat("JumpForce");
        }
        if (PlayerPrefs.HasKey("TrackingSpeed"))
        {
            aiTrackingPlayer.moveSpeed = PlayerPrefs.GetFloat("TrackingSpeed"); // AITrackingPlayer scriptine eri�im ve h�z� y�kleme
        }
    }

    private void SavePlayerPrefs()
    {
        // G�ncellenen speed ve jumpForce de�erlerini PlayerPrefs'e kaydet
        PlayerPrefs.SetFloat("Speed", playerMoveScript.speed);
        PlayerPrefs.SetFloat("JumpForce", playerMoveScript.jumpForce);
        PlayerPrefs.SetFloat("TrackingSpeed", aiTrackingPlayer.moveSpeed);
        PlayerPrefs.Save();
    }

    // Move s�n�f�ndan speed de�erine eri�im sa�lar
    public float GetPlayerSpeed()
    {
        return playerMoveScript.speed;
    }

    // Move s�n�f�ndan jumpForce de�erine eri�im sa�lar
    public float GetPlayerJumpForce()
    {
        return playerMoveScript.jumpForce;
    }

    // Coin kontrol� yapar
    private bool HasEnoughCoins(int cost)
    {
        int totalCoins = PlayerPrefs.GetInt("TotalCollectedCoins", 0);
        return totalCoins >= cost;
    }

    // Coinleri azalt�r
    private void SubtractCoins(int amount)
    {
        int totalCoins = PlayerPrefs.GetInt("TotalCollectedCoins", 0);
        totalCoins -= amount;
        PlayerPrefs.SetInt("TotalCollectedCoins", totalCoins);
        PlayerPrefs.Save();
        UpdateCoinText();
    }

    // Coin textini g�nceller
    private void UpdateCoinText()
    {
        coinText.text = PlayerPrefs.GetInt("TotalCollectedCoins", 0).ToString();
    }

    // Maliyet metinlerini g�nceller
    private void UpdateCostTexts()
    {
        speedCostText.text = speedUpgradeCost.ToString(); // Speed upgrade maliyetini g�ncelle
        jumpCostText.text = jumpUpgradeCost.ToString(); // Jump upgrade maliyetini g�ncelle
        trackingSpeedCostText.text = trackingSpeedDecreaseCost.ToString(); // Tracking Speed upgrade maliyetini g�ncelle

        // Yeteri kadar coin yoksa metin renklerini k�rm�z� yap
        if (!HasEnoughCoins(speedUpgradeCost))
        {
            speedCostText.color = Color.red;
        }
        else
        {
            speedCostText.color = Color.yellow;
        }

        if (!HasEnoughCoins(jumpUpgradeCost))
        {
            jumpCostText.color = Color.red;
        }
        else
        {
            jumpCostText.color = Color.yellow;
        }

        if (!HasEnoughCoins(trackingSpeedDecreaseCost))
        {
            trackingSpeedCostText.color = Color.red;
        }
        else
        {
            trackingSpeedCostText.color = Color.yellow;
        }
    }
}
