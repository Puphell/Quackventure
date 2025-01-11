using UnityEngine;
using UnityEngine.UI;

public class CoinButForMiniGame : MonoBehaviour
{
    public Text coinTextForMiniGame;
    private int coinCount = 0;

    void Start()
    {
        UpdateCoinText();
    }

    public void IncreaseCoins(int amount)
    {
        coinCount += amount;
        UpdateCoinText();
    }

    void UpdateCoinText()
    {
        coinTextForMiniGame.text = coinCount.ToString();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            IncreaseCoins(1);
        }
    }
}
