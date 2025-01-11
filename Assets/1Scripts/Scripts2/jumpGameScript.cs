using Interstitial;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerOutOfBounds : MonoBehaviour
{
    public Camera mainCamera;
    private bool isPlayerOutOfBounds = false;
    public GameObject gameOverPanelForminiGame;
    public GameObject finishPanel;

    void Start()
    {
        gameOverPanelForminiGame.SetActive(false);
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (!isPlayerOutOfBounds)
        {
            CheckOutOfBounds();
        }
    }

    void CheckOutOfBounds()
    {
        Vector3 playerViewportPosition = mainCamera.WorldToViewportPoint(transform.position);

        // Player kamera d���nda m�?
        if (playerViewportPosition.x < 0 || playerViewportPosition.x > 1 || playerViewportPosition.y < 0 || playerViewportPosition.y > 1)
        {
            // Kamera d���na ��kt�, istenen eylemleri ger�ekle�tir
            OnPlayerOutOfBounds();
            isPlayerOutOfBounds = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.CompareTag("Finish"))
       {
            int rewardAmount = 500; // �d�l miktar�
                                     // TotalCoinTextUpdater script'ine eri�im sa�layarak �d�l miktar�n� g�ncelleyebilirsiniz
            TotalCoinTextUpdater coinUpdater = FindObjectOfType<TotalCoinTextUpdater>();
            if (coinUpdater != null)
            {
                coinUpdater.GiveCoinReward(rewardAmount);
            }

            finishPanel.SetActive(true);
            GetComponent<miniGameJumpShowAd>().ShowGameOverPanel();
            Debug.Log("Oyun Bitti");
       }
    }

    public void OnPlayerOutOfBounds()
    {
        GetComponent<miniGameJumpShowAd>().ShowGameOverPanel();
        // Player kamera d���na ��kt���nda yap�lacak i�lemler burada ger�ekle�tirilir
        Debug.Log("Oyun Bitti");

        gameOverPanelForminiGame.SetActive(true);

        if (gameOverPanelForminiGame.activeSelf)
        {
            Time.timeScale = 0f;
        }
        // Di�er istenen i�lemleri buraya ekleyebilirsin
        // �rne�in: Ekrana bir mesaj g�sterme, bir ses �alma, oyunu yeniden ba�latma vs.
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("MiniGameJumpGame");
        Time.timeScale = 1f;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MAIN MENU");
        Time.timeScale = 1f;
    }
  
    public void BackMiniGames()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MiniGameLevelScene");
    }
}
