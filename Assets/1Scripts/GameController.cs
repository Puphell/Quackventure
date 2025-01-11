using UnityEngine;

public class GameController : MonoBehaviour
{
    private bool isGamePaused = false;
    private bool isGameOver = false;

    public void TogglePause()
    {
        isGamePaused = !isGamePaused;

        if (isGamePaused)
        {
            Debug.Log("Game Paused");
            Time.timeScale = 0f;
        }
        else
        {
            Debug.Log("Game Resumed");
            Time.timeScale = 1f;
        }
    }

    public void ShowGameOver()
    {
        isGameOver = true;
        Debug.Log("Game Over");
    }

    public bool IsGamePaused => isGamePaused;
    public bool IsGameOver => isGameOver;
}
