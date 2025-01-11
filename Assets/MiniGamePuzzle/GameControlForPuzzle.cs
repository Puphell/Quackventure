using Interstitial;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControlForPuzzle : MonoBehaviour
{
    public GameObject GameEndPanel;

    bool gameEnd = true;

    [SerializeField]
    private Transform[] pictures;

    [SerializeField]
    private GameObject winText;

    public static bool youWin;


    void Start()
    {
        GameEndPanel.SetActive(false);
        gameEnd = true;
        winText.SetActive(false);
        youWin = false;
    }

    void Update()
    {
        if ((pictures[0].rotation.z == 0 &&
      pictures[1].rotation.z == 0 &&
      pictures[2].rotation.z == 0 &&
      pictures[3].rotation.z == 0 &&
      pictures[4].rotation.z == 0 &&
      pictures[5].rotation.z == 0 &&
      pictures[6].rotation.z == 0 &&
      pictures[7].rotation.z == 0 &&
      pictures[8].rotation.z == 0) && gameEnd)
        {
            int rewardAmount = 10; // Ödül miktarý
                                   // TotalCoinTextUpdater script'ine eriþim saðlayarak ödül miktarýný güncelleyebilirsiniz
            TotalCoinTextUpdater coinUpdater = FindObjectOfType<TotalCoinTextUpdater>();
            if (coinUpdater != null)
            {
                coinUpdater.GiveCoinReward(rewardAmount);
                Debug.Log("Para Verildi");
            }

            GameEndPanel.SetActive(true);
            gameEnd = false;
            youWin = true;
            winText.SetActive(true);

            Time.timeScale = 0f;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MiniGamePuzzle");
    }

    public void MainMenuClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MAIN MENU");
    }

    public void BackToMiniGamesScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MiniGameLevelScene");
    }
}
