using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuControllerCupGame : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject mutePanel;
    private bool isMutePanelActive = false;
    public Animator MainMenuAnimator;
    void Start()
    {
        Animator MainMenuAnimator = GetComponent<Animator>();
        HidePauseMenu();
    }

    private void HidePauseMenu()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ResumeGame()
    {
        HidePauseMenu();
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        Invoke("ChangeSceneToMainMenu", 1f);
        MainMenuAnimator.SetTrigger("Close");
    }
    public void ChangeSceneToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MAIN MENU");
    }

    public void LoadOptions()
    {
        // MutePanel'i toggle et
        if (mutePanel != null)
        {
            isMutePanelActive = !isMutePanelActive;
            mutePanel.SetActive(isMutePanelActive);
        }
        else
        {
            Debug.LogError("MutePanel reference is not set!");
        }
    }
}
