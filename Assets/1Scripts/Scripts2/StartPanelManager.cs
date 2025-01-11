using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPanelManager : MonoBehaviour
{
    public Animator startPanelAnimator;
    public GameObject startPanel;

    private bool isFirstTime = true;

    public GameObject ObjectToDisable;

    void Start()
    {
        // ��aretleme kontrol�n� burada yapar�z
        if (PlayerPrefs.GetInt("StartPanelActive") == 1)
        {
            isFirstTime = false;
        }
        else
        {
            // ��aretleme kaydetme
            PlayerPrefs.SetInt("StartPanelActive", 1);
            PlayerPrefs.Save();
            isFirstTime = true;
        }

        // StartPanel'i ilk kez aktif etme
        if (isFirstTime)
        {
            startPanel.SetActive(true);
        }
        Animator startPanelAnimator = GetComponent<Animator>();
    }

    public void StartPanelClose()
    {
        startPanelAnimator.SetTrigger("Close");
        Invoke("StartPanelCloseAfterAnim", 0.5f);
    }
    private void StartPanelCloseAfterAnim()
    {
        startPanel.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
        // Oyun kapat�ld���nda i�aretleme s�f�rlan�r
        PlayerPrefs.SetInt("StartPanelActive", 0);
        PlayerPrefs.Save();
    }
}

