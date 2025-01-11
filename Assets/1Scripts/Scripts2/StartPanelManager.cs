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
        // Ýþaretleme kontrolünü burada yaparýz
        if (PlayerPrefs.GetInt("StartPanelActive") == 1)
        {
            isFirstTime = false;
        }
        else
        {
            // Ýþaretleme kaydetme
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
        // Oyun kapatýldýðýnda iþaretleme sýfýrlanýr
        PlayerPrefs.SetInt("StartPanelActive", 0);
        PlayerPrefs.Save();
    }
}

