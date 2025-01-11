using UnityEngine;
using UnityEngine.UI;

public class COINCOUNTERForSahne1 : MonoBehaviour
{
    public static COINCOUNTERForSahne1 instance;
    public Text coinText;
    private int coinCount = 0;
    public AudioSource coinSound; // Ses �almak i�in bir AudioSource ekledik.

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        coinCount = PlayerPrefs.GetInt("TotalCollectedCoins", 0); // Sahne 1 i�in toplanan coin miktar�n� al
        UpdateCoinText();
    }

    public void IncreaseCoins(int amount)
    {
        coinCount += amount;
        UpdateCoinText();
        PlayerPrefs.SetInt("TotalCollectedCoins", coinCount); // Sahne 1 i�in toplanan coin miktar�n� kaydet
    }

    void UpdateCoinText()
    {
        coinText.text = coinCount.ToString();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            IncreaseCoins(1);

            // Coin temas�nda ses �al
            if (coinSound != null)
            {
                coinSound.Play();
            }
        }
    }
}
