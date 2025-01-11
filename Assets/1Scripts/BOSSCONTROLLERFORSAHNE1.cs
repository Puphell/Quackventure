using UnityEngine;
using UnityEngine.SceneManagement;

public class BossControllerForSahne1 : MonoBehaviour
{
    private bool gameIsOver = false;
    public GameObject gameOverPanel;

    void Start()
    {
        // Baþlangýçta GameOverPanel'i gizle
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

        // GameOverPanel'i göster
        gameOverPanel.SetActive(true);
    }

    // Oyunu tekrar baþlatmak için kullanýlacak fonksiyon
    public void RestartGame()
    {
        // Oyunu tekrar baþlat
        Time.timeScale = 1;
        gameIsOver = false;
        // FadeController gameobjesine baðlý Animator bileþenini al
        Animator fadeAnimator = GameObject.Find("FadeController").GetComponent<Animator>();
        // Aktif sahnenin adýný al ve tekrar yükle
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
