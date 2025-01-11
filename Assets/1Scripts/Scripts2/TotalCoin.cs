using UnityEngine;
using UnityEngine.UI;

public class TotalCoinTextUpdater : MonoBehaviour
{
    public Text totalCoinText; // Toplam coin miktar�n� g�stermek i�in text objesi

    void Start()
    {
        // T�m sahnelerde toplanan coin miktar�n� alarak total coin miktar�n� g�ncelle
        int totalCoins = PlayerPrefs.GetInt("TotalCollectedCoins", 0); // Toplanan coin miktar�n� al, yoksa varsay�lan de�eri kullan
        totalCoinText.text = totalCoins.ToString(); // Toplam coin miktar�n� g�ster
    }
    void Update()
    {
        // T�m sahnelerde toplanan coin miktar�n� alarak total coin miktar�n� g�ncelle
        int totalCoins = PlayerPrefs.GetInt("TotalCollectedCoins", 0); // Toplanan coin miktar�n� al, yoksa varsay�lan de�eri kullan
        totalCoinText.text = totalCoins.ToString(); // Toplam coin miktar�n� g�ster
    }

    // �d�l vermek i�in bu fonksiyonu �a��rabiliriz
    public void GiveCoinReward(int amount)
    {
        // �d�l miktar�n� toplam coine ekleyerek g�ncelle
        int totalCoins = PlayerPrefs.GetInt("TotalCollectedCoins", 0); // Toplanan coin miktar�n� al
        totalCoins += amount; // �d�l miktar�n� toplam coine ekle
        PlayerPrefs.SetInt("TotalCollectedCoins", totalCoins); // Toplanan coin miktar�n� g�ncelle
        PlayerPrefs.Save(); // De�i�iklikleri kaydet
        totalCoinText.text = totalCoins.ToString(); // Toplam coin miktar�n� g�ncelle
    }
}
