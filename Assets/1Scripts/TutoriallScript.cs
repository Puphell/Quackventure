using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public GameObject tutorialPanel;
    private bool panelActive = false;
    private int contactCount = 0; // Temas say�s�n� takip etmek i�in bir de�i�ken
    
    void Start()
    {
        // PlayerPrefs'ten temas say�s�n� al
        contactCount = PlayerPrefs.GetInt("TutorialContactCount", 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !panelActive && contactCount < 3) // 5 kere temas edilmi� mi kontrol edilir
        {
            // Oyuncu ile temasa ge�ildi�inde, panel aktif de�ilse ve 5 temas hakk� varsa
            tutorialPanel.SetActive(true);
            panelActive = true;
            Time.timeScale = 0f;

            // Temas say�s�n� artt�r ve PlayerPrefs'e kaydet
            contactCount++;
            PlayerPrefs.SetInt("TutorialContactCount", contactCount);
        }
    }

    public void CloseTutorialPanel()
    {
        tutorialPanel.SetActive(false);
        panelActive = false;
        Time.timeScale = 1f;
    }
}
