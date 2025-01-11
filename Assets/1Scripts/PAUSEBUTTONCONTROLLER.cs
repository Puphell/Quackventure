using UnityEngine;

public class PauseButtonController : MonoBehaviour
{
    public GameObject pausePanel;
    private bool isPaused = false;

    void Start()
    {
        // Başlangıçta PausePanel'i gizle
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
    }

    public void OnPauseButtonClicked()
    {
        if (!isPaused)
        {
            // Oyunu durdur
            Time.timeScale = 0;
            isPaused = true;

            // PausePanel'i göster
            if (pausePanel != null)
            {
                pausePanel.SetActive(true);
            }
        }
    }

    public void OnContinueButtonClicked()
    {
        if (isPaused)
        {
            // Oyunu devam ettir
            Time.timeScale = 1;
            isPaused = false;

            // PausePanel'i gizle
            if (pausePanel != null)
            {
                pausePanel.SetActive(false);
            }
        }
    }
}
