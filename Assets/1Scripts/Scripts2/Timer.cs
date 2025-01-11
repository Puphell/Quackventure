using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float delay = 60f; // Timer'�n s�resi (saniye cinsinden)
    public Text delayText; // Kalan s�reyi g�sterecek metin nesnesi
    public GameObject panelToShow; // G�sterilecek panel
    public GameObject[] objectsToDisable; // Devre d��� b�rak�lacak GameObject'lerin listesi
    public Button mainMenuButton; // Ana men� butonu
    public Animator mainMenuAnimator; // Ana men� animator�
    public string mainMenuSceneName = "MAIN MENU"; // Ana men� sahne ad�

    private bool timerStarted = false; // Timer ba�lad� m�?

    void Start()
    {
        panelToShow.SetActive(false);

        // Ana men� butonuna t�klan�nca mainMenu fonksiyonunu ekle
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
        // Kalan s�reyi g�stermek i�in ba�lang��ta text'i g�ncelle
        if (delayText != null)
        {
            delayText.text = Mathf.CeilToInt(delay).ToString();
        }

        StartCoroutine(Countdown());
    }
    // Timer'� ba�lat
    public void StartTimer()
    {
        if (!timerStarted)
        {
            StartCoroutine(Countdown());
            timerStarted = true;
        }
    }

    // Geri say�m� ger�ekle�tiren Coroutine
    private System.Collections.IEnumerator Countdown()
    {
        float timeElapsed = 0f;

        while (timeElapsed < delay)
        {
            timeElapsed += Time.deltaTime;

            // Kalan s�reyi g�ncelle
            if (delayText != null)
            {
                delayText.text = Mathf.CeilToInt(delay - timeElapsed).ToString();
            }

            yield return null;
        }

        // Paneli aktifle�tir
        if (panelToShow != null)
        {
            panelToShow.SetActive(true);
        }

        // Devre d��� b�rak�lacak GameObject'leri devre d��� b�rak
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(false);
        }
    }

    // Ana men� fonksiyonu
    public void mainMenu()
    {
        // Ana men� animasyonunu �al��t�r
        if (mainMenuAnimator != null)
        {
            mainMenuAnimator.SetTrigger("Close");
        }
        Invoke("LoadMainMenuScene",1f);
    }

    private void LoadMainMenuScene()
    {
        Time.timeScale = 1f;
        // Ana men� sahnesine ge�i� yap
        UnityEngine.SceneManagement.SceneManager.LoadScene(mainMenuSceneName);
    }

    public void BackToMiniGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MiniGameLevelScene");
    }
}
