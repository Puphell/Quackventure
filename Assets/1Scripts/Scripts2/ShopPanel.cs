using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Move playerMoveScript;
    public AITrackingPlayer aiTrackingPlayer; // AITrackingPlayer scriptine referans
    public Text coinText;
    public Text speedCostText; // Speed upgrade maliyetini göstermek için text objesi
    public Text jumpCostText; // Jump upgrade maliyetini göstermek için text objesi
    public Text trackingSpeedCostText; // Tracking Speed upgrade maliyetini göstermek için text objesi
    public float speedUpgradeAmount = 1f;
    public float jumpUpgradeAmount = 1;
    public float trackingSpeedDecreaseAmount = 1f; // Hedef takip hýzýný azaltma miktarý
    public int speedUpgradeCost = 100; // Speed upgrade için gereken coin miktarý
    public int jumpUpgradeCost = 100; // Jump upgrade için gereken coin miktarý
    public int trackingSpeedDecreaseCost = 100; // Tracking Speed upgrade için gereken coin miktarý
    public AudioSource buttonClickSound; // Butona basýldýðýnda çalýnacak ses

    private void Start()
    {
        // Oyun baþladýðýnda PlayerPrefs'ten hýz ve zýplama deðerlerini alarak güncelleyin
        LoadPlayerPrefs();
        UpdateCostTexts(); // Maliyet metinlerini güncelle

       
        // Tüm sahnelerde toplanan coin miktarýný alarak total coin miktarýný güncelle
        int totalCoins = PlayerPrefs.GetInt("TotalCollectedCoins", 0);
        coinText.text = totalCoins.ToString();
    }
    void Update()
    {
        UpdateCostTexts();
    }
    public void UpgradeSpeed()
    {
        // Yeterli coin varsa ve upgrade yapýldýysa güncelle
        if (HasEnoughCoins(speedUpgradeCost))
        {
            playerMoveScript.speed += speedUpgradeAmount;
            SavePlayerPrefs();
            SubtractCoins(speedUpgradeCost);
            UpdateCostTexts(); // Maliyet metinlerini güncelle
            if (buttonClickSound != null)
            {
                buttonClickSound.Play(); // Butona basýldýðýnda ses çal
            }
        }
    }

    public void UpgradeJump()
    {
        // Yeterli coin varsa ve upgrade yapýldýysa güncelle
        if (HasEnoughCoins(jumpUpgradeCost))
        {
            playerMoveScript.jumpForce += jumpUpgradeAmount;
            SavePlayerPrefs();
            SubtractCoins(jumpUpgradeCost);
            UpdateCostTexts(); // Maliyet metinlerini güncelle
            if (buttonClickSound != null)
            {
                buttonClickSound.Play(); // Butona basýldýðýnda ses çal
            }
        }
    }

    public void DecreaseTrackingSpeed()
    {
        // Yeterli coin varsa ve upgrade yapýldýysa güncelle
        if (HasEnoughCoins(trackingSpeedDecreaseCost))
        {
            aiTrackingPlayer.moveSpeed -= trackingSpeedDecreaseAmount; // AITrackingPlayer scriptine eriþim ve hýzý azaltma
            SavePlayerPrefs();
            SubtractCoins(trackingSpeedDecreaseCost);
            UpdateCostTexts(); // Maliyet metinlerini güncelle
            if (buttonClickSound != null)
            {
                buttonClickSound.Play(); // Butona basýldýðýnda ses çal
            }
        }
    }

    private void LoadPlayerPrefs()
    {
        // PlayerPrefs'ten kaydedilmiþ speed ve jumpForce deðerlerini al
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
            aiTrackingPlayer.moveSpeed = PlayerPrefs.GetFloat("TrackingSpeed"); // AITrackingPlayer scriptine eriþim ve hýzý yükleme
        }
    }

    private void SavePlayerPrefs()
    {
        // Güncellenen speed ve jumpForce deðerlerini PlayerPrefs'e kaydet
        PlayerPrefs.SetFloat("Speed", playerMoveScript.speed);
        PlayerPrefs.SetFloat("JumpForce", playerMoveScript.jumpForce);
        PlayerPrefs.SetFloat("TrackingSpeed", aiTrackingPlayer.moveSpeed);
        PlayerPrefs.Save();
    }

    // Move sýnýfýndan speed deðerine eriþim saðlar
    public float GetPlayerSpeed()
    {
        return playerMoveScript.speed;
    }

    // Move sýnýfýndan jumpForce deðerine eriþim saðlar
    public float GetPlayerJumpForce()
    {
        return playerMoveScript.jumpForce;
    }

    // Coin kontrolü yapar
    private bool HasEnoughCoins(int cost)
    {
        int totalCoins = PlayerPrefs.GetInt("TotalCollectedCoins", 0);
        return totalCoins >= cost;
    }

    // Coinleri azaltýr
    private void SubtractCoins(int amount)
    {
        int totalCoins = PlayerPrefs.GetInt("TotalCollectedCoins", 0);
        totalCoins -= amount;
        PlayerPrefs.SetInt("TotalCollectedCoins", totalCoins);
        PlayerPrefs.Save();
        UpdateCoinText();
    }

    // Coin textini günceller
    private void UpdateCoinText()
    {
        coinText.text = PlayerPrefs.GetInt("TotalCollectedCoins", 0).ToString();
    }

    // Maliyet metinlerini günceller
    private void UpdateCostTexts()
    {
        speedCostText.text = speedUpgradeCost.ToString(); // Speed upgrade maliyetini güncelle
        jumpCostText.text = jumpUpgradeCost.ToString(); // Jump upgrade maliyetini güncelle
        trackingSpeedCostText.text = trackingSpeedDecreaseCost.ToString(); // Tracking Speed upgrade maliyetini güncelle

        // Yeteri kadar coin yoksa metin renklerini kýrmýzý yap
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
