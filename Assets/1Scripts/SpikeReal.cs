using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using Interstitial;
using GoogleMobileAds.Api;
using System;

public class Spike : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject loadingPanel;
    public Slider loadingSlider;
    public GameObject objectToDisable; // Disable edilecek game object
    void Start()
    {
        Time.timeScale = 1;
        // Başlangıçta GameOverPanel'i gizle
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Belirtilen game object'u disable et
            if (objectToDisable != null)
            {
                objectToDisable.SetActive(false);
            }

            // Kamerayı küçült
            Camera.main.GetComponent<CameraFollowForSkins>().ZoomOut();

            Invoke("ShowGameOverPanel", 0.1f);
        }
    }
    private void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }
    public void RestartGame()
    {
        // Loading paneli aktif hale getir
        loadingPanel.SetActive(true);

        // Yüklenme işlemi başladığında slider'ı sıfırla
        loadingSlider.value = 0f;

        Time.timeScale = 1;

        // Asenkron sahne yükleme işlevini başlat
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        // Aktif sahnenin adını al
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Sahne yükleme işlemi
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(currentSceneName);

        // Sahne yüklenirken loading panelindeki slider'ı güncelle
        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f); // Yüzde 0.9'a kadar (tam yükleme)
            loadingSlider.value = progress;
            yield return null;
        }

        // Yükleme tamamlandığında loading panelini gizle
        loadingPanel.SetActive(false);
    }
}