using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float delay = 60f; // Timer'ýn süresi (saniye cinsinden)
    public Text delayText; // Kalan süreyi gösterecek metin nesnesi
    public GameObject panelToShow; // Gösterilecek panel
    public GameObject[] objectsToDisable; // Devre dýþý býrakýlacak GameObject'lerin listesi
    public Button mainMenuButton; // Ana menü butonu
    public Animator mainMenuAnimator; // Ana menü animatorü
    public string mainMenuSceneName = "MAIN MENU"; // Ana menü sahne adý

    private bool timerStarted = false; // Timer baþladý mý?

    void Start()
    {
        panelToShow.SetActive(false);

        // Ana menü butonuna týklanýnca mainMenu fonksiyonunu ekle
        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.AddListener(mainMenu);
        }

        if (!timerStarted)
        {
            timerStarted = true;

        }

        StartCoroutine(Countdown());
    }
    void Update()
    {
        // Kalan süreyi göstermek için baþlangýçta text'i güncelle
        if (delayText != null)
        {
            delayText.text = Mathf.CeilToInt(delay).ToString();
        }

        StartCoroutine(Countdown());
    }
    // Timer'ý baþlat
    public void StartTimer()
    {
        if (!timerStarted)
        {
            StartCoroutine(Countdown());
            timerStarted = true;
        }
    }

    // Geri sayýmý gerçekleþtiren Coroutine
    private System.Collections.IEnumerator Countdown()
    {
        float timeElapsed = 0f;

        while (timeElapsed < delay)
        {
            timeElapsed += Time.deltaTime;

            // Kalan süreyi güncelle
            if (delayText != null)
            {
                delayText.text = Mathf.CeilToInt(delay - timeElapsed).ToString();
            }

            yield return null;
        }

        // Paneli aktifleþtir
        if (panelToShow != null)
        {
            panelToShow.SetActive(true);
        }

        // Devre dýþý býrakýlacak GameObject'leri devre dýþý býrak
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(false);
        }
    }

    // Ana menü fonksiyonu
    public void mainMenu()
    {
        // Ana menü animasyonunu çalýþtýr
        if (mainMenuAnimator != null)
        {
            mainMenuAnimator.SetTrigger("Close");
        }
        Invoke("LoadMainMenuScene",1f);
    }

    private void LoadMainMenuScene()
    {
        Time.timeScale = 1f;
        // Ana menü sahnesine geçiþ yap
        UnityEngine.SceneManagement.SceneManager.LoadScene(mainMenuSceneName);
    }

    public void BackToMiniGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MiniGameLevelScene");
    }
}
