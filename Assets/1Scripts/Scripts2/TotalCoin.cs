using UnityEngine;
using UnityEngine.UI;

public class TotalCoinTextUpdater : MonoBehaviour
{
    public Text totalCoinText; // Toplam coin miktarýný göstermek için text objesi

    void Start()
    {
        // Tüm sahnelerde toplanan coin miktarýný alarak total coin miktarýný güncelle
        int totalCoins = PlayerPrefs.GetInt("TotalCollectedCoins", 0); // Toplanan coin miktarýný al, yoksa varsayýlan deðeri kullan
        totalCoinText.text = totalCoins.ToString(); // Toplam coin miktarýný göster
    }
    void Update()
    {
        // Tüm sahnelerde toplanan coin miktarýný alarak total coin miktarýný güncelle
        int totalCoins = PlayerPrefs.GetInt("TotalCollectedCoins", 0); // Toplanan coin miktarýný al, yoksa varsayýlan deðeri kullan
        totalCoinText.text = totalCoins.ToString(); // Toplam coin miktarýný göster
    }

    // Ödül vermek için bu fonksiyonu çaðýrabiliriz
    public void GiveCoinReward(int amount)
    {
        // Ödül miktarýný toplam coine ekleyerek güncelle
        int totalCoins = PlayerPrefs.GetInt("TotalCollectedCoins", 0); // Toplanan coin miktarýný al
        totalCoins += amount; // Ödül miktarýný toplam coine ekle
        PlayerPrefs.SetInt("TotalCollectedCoins", totalCoins); // Toplanan coin miktarýný güncelle
        PlayerPrefs.Save(); // Deðiþiklikleri kaydet
        totalCoinText.text = totalCoins.ToString(); // Toplam coin miktarýný güncelle
    }
}
