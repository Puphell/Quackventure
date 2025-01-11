using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject gameOverCanvas; // Yeni ekledik: Game Over Canvas
    public Button restartButton; // Yeni ekledik: Restart Button

    private bool isGamePaused = false;
    private bool isGameOver = false;

    void Start()
    {
        HidePauseMenu();
    }

    void Update()
    {
        if (!isGameOver && Input.GetButtonDown("PauseButton"))
        {
            TogglePauseMenu();
        }
    }

    private void HidePauseMenu()
    {
        isGamePaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    // Bu metodunu 'public' yap
    public void TogglePauseMenu()
    {
        isGamePaused = !isGamePaused;

        if (isGamePaused)
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
        }
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
        UnityEngine.SceneManagement.SceneManager.LoadScene("MAIN MENU");
    }

    public void LoadOptions()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("OPTIONS");
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
