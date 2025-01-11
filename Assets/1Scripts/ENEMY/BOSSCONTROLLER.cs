using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using Interstitial;
public class BossController : MonoBehaviour
{
    private bool gameIsOver = false;
    public GameObject gameOverPanel;
    public GameObject objectToDisable; // Disable edilecek game object
    public GameObject objectToDisable2; // Disable edilecek game object
    public GameObject loadingPanel;
    public Slider loadingSlider;

    void Start()
    {
        // Başlangıçta GameOverPanel'i gizle
        gameOverPanel.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !gameIsOver)
        {
            // Kamerayı küçült
            Camera.main.GetComponent<CameraFollowForSkins>().ZoomOut();

            GameOver();
        }
    }

    void GameOver()
    {
        // Belirtilen game object'u disable et
        if (objectToDisable != null)
        {
            objectToDisable.SetActive(false);
        }
        // Belirtilen game object'u disable et
        if (objectToDisable2 != null)
        {
            objectToDisable2.SetActive(false);
        }
        Invoke("OpenGameOverPanel", 0.1f);
    }
    private void OpenGameOverPanel()
    {
        // Oyunu durdur
        Time.timeScale = 0;
        gameIsOver = true;

        // GameOverPanel'i göster
        gameOverPanel.SetActive(true);
    }

    // Oyunu tekrar başlatmak için kullanılacak fonksiyon
    public void RestartGame()
    {
        // Loading paneli aktif hale getir
        loadingPanel.SetActive(true);

        // Yüklenme işlemi başladığında slider'ı sıfırla
        loadingSlider.value = 0f;

        // Asenkron sahne yükleme işlevini başlat
        StartCoroutine(LoadSceneAsync());
    }
    IEnumerator LoadSceneAsync()
    {
        // FadeController gameobjesine bağlı Animator bileşenini al
        Animator fadeAnimator = GameObject.Find("Fade31Controller").GetComponent<Animator>();

        gameIsOver = false;

        // Oyunu tekrar başlat
        Time.timeScale = 1;

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
