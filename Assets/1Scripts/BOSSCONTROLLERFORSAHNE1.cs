using UnityEngine;
using UnityEngine.SceneManagement;

public class BossControllerForSahne1 : MonoBehaviour
{
    private bool gameIsOver = false;
    public GameObject gameOverPanel;

    void Start()
    {
        // Ba�lang��ta GameOverPanel'i gizle
        gameOverPanel.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !gameIsOver)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        // Oyunu durdur
        Time.timeScale = 0;
        gameIsOver = true;

        // GameOverPanel'i g�ster
        gameOverPanel.SetActive(true);
    }

    // Oyunu tekrar ba�latmak i�in kullan�lacak fonksiyon
    public void RestartGame()
    {
        // Oyunu tekrar ba�lat
        Time.timeScale = 1;
        gameIsOver = false;
        // FadeController gameobjesine ba�l� Animator bile�enini al
        Animator fadeAnimator = GameObject.Find("FadeController").GetComponent<Animator>();
        // Aktif sahnenin ad�n� al ve tekrar y�kle
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
