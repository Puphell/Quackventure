using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public GameObject tutorialPanel;
    private bool panelActive = false;
    private int contactCount = 0; // Temas sayýsýný takip etmek için bir deðiþken
    
    void Start()
    {
        // PlayerPrefs'ten temas sayýsýný al
        contactCount = PlayerPrefs.GetInt("TutorialContactCount", 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !panelActive && contactCount < 3) // 5 kere temas edilmiþ mi kontrol edilir
        {
            // Oyuncu ile temasa geçildiðinde, panel aktif deðilse ve 5 temas hakký varsa
            tutorialPanel.SetActive(true);
            panelActive = true;
            Time.timeScale = 0f;

            // Temas sayýsýný arttýr ve PlayerPrefs'e kaydet
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
