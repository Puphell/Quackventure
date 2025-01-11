using System.Collections;
using UnityEngine;

public class RatePanelManager : MonoBehaviour
{
    private string GooglePlayQuackventure = "https://play.google.com/store/apps/details?id=com.PerEffect.Quackventure";
    public GameObject panel; // Panelin referans�n� ata

    void Start()
    {
        // Sonraki panel g�sterim zaman�n� al veya rastgele bir zaman belirle
        if (PlayerPrefs.HasKey("NextPanelTime"))
        {
            float nextPanelTime = PlayerPrefs.GetFloat("NextPanelTime");
            float currentTime = Time.realtimeSinceStartup;

            // E�er belirlenen zaman ge�mi�se paneli hemen g�ster
            if (currentTime >= nextPanelTime)
            {
                panel.SetActive(true);
            }
            else
            {
                StartCoroutine(ActivatePanelAfterDelay(nextPanelTime - currentTime));
            }
        }
        else
        {
            float randomTime = Random.Range(2 * 3600, 120 * 3600); // 2 saat - 120 saat
            float nextPanelTime = Time.realtimeSinceStartup + randomTime;

            PlayerPrefs.SetFloat("NextPanelTime", nextPanelTime);
            StartCoroutine(ActivatePanelAfterDelay(randomTime));
        }

        // Seviye kontrol� yap
        int unlockedLevels = PlayerPrefs.GetInt("TotalUnlockedLevels", 0);
        if (unlockedLevels >= 5)
        {
            panel.SetActive(true);
        }
    }

    IEnumerator ActivatePanelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        panel.SetActive(true);
    }

    public void openQuackventure()
    {
        Application.OpenURL(GooglePlayQuackventure);
    }

    public void closeQuackventure()
    {
        panel.SetActive(false);

        // Gelecek g�sterim i�in yeni bir rastgele zaman belirle
        float randomTime = Random.Range(2 * 3600, 120 * 3600); // 2 saat - 120 saat
        float nextPanelTime = Time.realtimeSinceStartup + randomTime;
        PlayerPrefs.SetFloat("NextPanelTime", nextPanelTime);
    }
}
