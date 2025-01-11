using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController1 : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject gameOverCanvas; // Yeni ekledik: Game Over Canvas
    public Button restartButton; // Yeni ekledik: Restart Button
    public GameObject mutePanel;
    private bool isMutePanelActive = false;
    public Animator MainMenuAnimator;
    private bool allowAnimation = true;
    private bool isGameOver = false;

    public List<Rigidbody2D> playerRigidbody2D;
    void Start()
    {
        Animator MainMenuAnimator = GetComponent<Animator>();
        HidePauseMenu();
    }
    void Update()
    {
        // Zaman durduğunda da animasyonların çalışmasını sağlamak için
        if (Time.timeScale == 0)
        {
            // Animasyonlara elle zaman bilgisi sağlamak
            MainMenuAnimator.speed = 1;
        }
        else
        {
            // Normal zaman akışında animasyonların çalışmasını sağlamak
            MainMenuAnimator.speed = Time.timeScale;
        }
    }

    private void HidePauseMenu()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    // Yeni ekledik: Oyunu durdur ve Game Over ekranını göster
    public void ShowGameOver()
    {
        isGameOver = true;
        gameOverCanvas.SetActive(true);

        // Restart butonunu aktif et ve dinleyici ekle
        restartButton.gameObject.SetActive(true);
        restartButton.onClick.AddListener(RestartGame);
    }

    // Yeni ekledik: Oyunu yeniden başlat
    private void RestartGame()
    {
        isGameOver = false;

        // Restart butonunu tıklanabilir yap
        restartButton.onClick.RemoveListener(RestartGame);
        restartButton.gameObject.SetActive(false);

        // Sahneyi yeniden yükle
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void ResumeGame()
    {
        HidePauseMenu();
    }

    public void LoadMainMenu()
    {
        // Eğer playerRigidbody2D listesi henüz oluşturulmadıysa, yeni bir liste oluştur
        if (playerRigidbody2D == null)
        {
            playerRigidbody2D = new List<Rigidbody2D>();
        }

        // Tüm Player GameObject'lerini bul
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");

        // Her bir Player GameObject'inden Rigidbody2D bileşenlerini al ve listeye ekle
        foreach (GameObject playerObject in playerObjects)
        {
            Rigidbody2D rb = playerObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                playerRigidbody2D.Add(rb);
            }
        }

        // Liste içindeki her Rigidbody2D bileşenini devre dışı bırak
        foreach (Rigidbody2D rb in playerRigidbody2D)
        {
            rb.simulated = false;
        }


        // AITrackingPlayer bileşenine eriş
        AITrackingPlayer aiTracker = FindObjectOfType<AITrackingPlayer>();

        // Eğer AITrackingPlayer bileşeni bulunursa ve moveSpeed değişkenine erişim sağlanabilirse
        if (aiTracker != null)
        {
            aiTracker.moveSpeed = 0f; // moveSpeed'i 0 olarak ayarla
        }
        Time.timeScale = 1f;
        Invoke("ChangeSceneToMainMenu", 1f);
        MainMenuAnimator.SetTrigger("Close");
    }
    public void ChangeSceneToMainMenu()
    {
        // AITrackingPlayer bileşenine eriş
        AITrackingPlayer aiTracker = FindObjectOfType<AITrackingPlayer>();
        
        if (aiTracker != null)
        {
            aiTracker.moveSpeed = 0f; // moveSpeed'i 0 olarak ayarla
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene("MAIN MENU");
    }

    public void LoadOptions()
    {
        // MutePanel'i toggle et
        if (mutePanel != null)
        {
            isMutePanelActive = !isMutePanelActive;
            mutePanel.SetActive(isMutePanelActive);
        }
        else
        {
            Debug.LogError("MutePanel reference is not set!");
        }
    }

    public void LoadLevel1()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SAHNE 1");
    }

    // Yeni ekledik: Section1 scenindeyken özel bir Options1 sahnesine git
    public void LoadOnPlayedOptions()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("OnPlayedOptions");
    }
}
