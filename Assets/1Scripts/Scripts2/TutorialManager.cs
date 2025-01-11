using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialPanel;
    private bool isFirstTime = true;

    void Start()
    {
        if (PlayerPrefs.HasKey("isFirstTimeForTutorialJump"))
        {
            isFirstTime = PlayerPrefs.GetInt("isFirstTimeForTutorialJump") == 1;
        }

        if (isFirstTime)
        {
            Time.timeScale = 0f;
            tutorialPanel.SetActive(true);
            PlayerPrefs.SetInt("isFirstTimeForTutorialJump", 0);
            PlayerPrefs.Save();
        }
    }

    public void CloseTutorial()
    {
        Time.timeScale = 1f;
        tutorialPanel.SetActive(false);
        isFirstTime = false;
    }
}
