using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEditor;

public class LevelManager : MonoBehaviour
{
    int maxLevels = 10; // Toplam seviye sayısı
    int[] levelsUnlocked; // Seviyelerin unlock durumu

    public GameObject loadingPanel;
    public Slider loadingSlider;

    public Button[] buttons = new Button[10]; // Button dizisinin boyutunu belirle

    void Start()
    {
        loadingPanel.SetActive(false);
        levelsUnlocked = new int[maxLevels];

        // Element 0'ın butonu her zaman unlock olsun ve interactable true olsun
        buttons[0].interactable = true;
        levelsUnlocked[0] = 1;

        for (int i = 1; i < buttons.Length; i++) // 1. indexten itibaren kontrol et
        {
            buttons[i].interactable = i < levelsUnlocked.Length && levelsUnlocked[i] == 1;
        }

        for (int i = 1; i < maxLevels; i++)
        {
            levelsUnlocked[i] = PlayerPrefs.GetInt("LevelUnlocked_" + (i + 1), 0); // Unlock index sistemi 1'den başlasın
            if (i < buttons.Length) // Diziyi aşmamak için kontrol ekle
            {
                buttons[i].interactable = i < levelsUnlocked.Length && levelsUnlocked[i] == 1;
            }
        }

        // LevelScript'ten gelen eventi dinle
        LevelScript.OnLevelUnlocked += HandleLevelUnlocked;
    }



    void OnDestroy()
    {
        // Event dinlemeyi kaldır
        LevelScript.OnLevelUnlocked -= HandleLevelUnlocked;
    }

    void HandleLevelUnlocked(int levelIndex)
    {
        // Eğer bir seviye kilidi açıldıysa, butonları güncelle
        levelsUnlocked[levelIndex - 1] = 1; // Unlock index sistemini 1'den başlatmak için 1 çıkar
        PlayerPrefs.SetInt("LevelUnlocked_" + levelIndex, 1);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = i < levelsUnlocked.Length && levelsUnlocked[i] == 1;
        }
    }

    public void LoadLevel(int levelIndex)
    {
        {
            StartCoroutine(LoadAsyncScene(levelIndex));
        }
    }

    IEnumerator LoadAsyncScene(int levelIndex)
    {
        float progress = 0;
        loadingPanel.SetActive(true);
        // Sahne yüklenirken AsyncOperation nesnesi oluşturulur
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(levelIndex);

        // Sahne tamamen yüklenene kadar döngü
        while (!asyncOperation.isDone)
        {
            // Yükleme yüzdesini hesapla ve slider değerini güncelle
            progress = Mathf.Clamp01(asyncOperation.progress / 0.9f); // Yüzde 0.9'a kadar (tam yükleme)
            loadingSlider.value = progress;

            // Bir sonraki frame'i bekle
            yield return null;
        }

        loadingPanel.SetActive(false);
        // Asenkron yükleme tamamlandıktan sonra, sahne yüklenir
        SceneManager.LoadScene(levelIndex);
    }
}