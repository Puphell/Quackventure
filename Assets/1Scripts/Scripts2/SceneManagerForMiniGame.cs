using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerForMiniGame : MonoBehaviour
{
    private string[] miniGameScenes = { "MiniGamePuzzle"};

    public void miniGameJumpGame()
    {
        SceneManager.LoadScene("MiniGameJumpGame");
    }

    public void miniGameMemoryGame()
    {
        SceneManager.LoadScene("MemoryGameScene");
    }

    public void miniGameCubGame()
    {
        SceneManager.LoadScene("MiniGameCub");
    }

    public void miniGamePuzzleGame()
    {
        int randomIndex = Random.Range(0, miniGameScenes.Length);
        SceneManager.LoadScene(miniGameScenes[randomIndex]);
    }

    public void BackToTheMainMenu()
    {
        SceneManager.LoadScene("MAIN MENU");
    }

}