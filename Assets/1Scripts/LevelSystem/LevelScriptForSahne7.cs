using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScriptForSahne7 : MonoBehaviour
{
    public GameObject objectToDisable; // Disable edilecek GameObject
    private bool hasPlayerPassed = false; // Oyuncunun geçip geçmediðini kontrol etmek için
    private bool nextLevelUnlocked = false; // Bir sonraki seviyenin kilidinin açýlýp açýlmadýðýný kontrol etmek için
    private const string NextLevelUnlockedKey = "NextLevelUnlocked";

    public void Pass()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;

        // Bir sonraki seviyenin kilidini aç
        UnlockNextLevel();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasPlayerPassed)
        {
            // Eðer bir sonraki seviye henüz kilidi açýlmadýysa ve objectToDisable null deðilse, disable hale getir
            if (!nextLevelUnlocked && objectToDisable != null)
            {
                objectToDisable.SetActive(false);
            }

            // SceneController'dan sonraki seviyeye geç
            SceneController.instance.NextLevel();
            hasPlayerPassed = true; // Oyuncu geçtiðinde iþaretle
        }
    }

    void UnlockNextLevel()
    {
        if (!nextLevelUnlocked)
        {
            int currentLevel = SceneManager.GetActiveScene().buildIndex;
            PlayerPrefs.SetInt(NextLevelUnlockedKey, 1); // Bir sonraki seviyenin kilidini açýk olarak iþaretle
            PlayerPrefs.Save();
            Debug.Log("LEVEL " + (currentLevel + 1) + " UNLOCKED");
            nextLevelUnlocked = true; // Bir sonraki seviyenin kilidini açýk olarak iþaretle
        }
    }

    private void Start()
    {
        // PlayerPrefs'ten bir sonraki seviyenin kilidinin açýlýp açýlmadýðýný kontrol et
        nextLevelUnlocked = PlayerPrefs.GetInt(NextLevelUnlockedKey, 0) == 1;
    }
}
