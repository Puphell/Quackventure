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

        // Player kamera dýþýnda mý?
        if (playerViewportPosition.x < 0 || playerViewportPosition.x > 1 || playerViewportPosition.y < 0 || playerViewportPosition.y > 1)
        {
            // Kamera dýþýna çýktý, istenen eylemleri gerçekleþtir
            OnPlayerOutOfBounds();
            isPlayerOutOfBounds = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.CompareTag("Finish"))
       {
            int rewardAmount = 500; // Ödül miktarý
                                     // TotalCoinTextUpdater script'ine eriþim saðlayarak ödül miktarýný güncelleyebilirsiniz
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
        // Player kamera dýþýna çýktýðýnda yapýlacak iþlemler burada gerçekleþtirilir
        Debug.Log("Oyun Bitti");

        gameOverPanelForminiGame.SetActive(true);

        if (gameOverPanelForminiGame.activeSelf)
        {
            Time.timeScale = 0f;
        }
        // Diðer istenen iþlemleri buraya ekleyebilirsin
        // Örneðin: Ekrana bir mesaj gösterme, bir ses çalma, oyunu yeniden baþlatma vs.
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
