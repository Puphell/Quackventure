using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScriptForSahne7 : MonoBehaviour
{
    public GameObject objectToDisable; // Disable edilecek GameObject
    private bool hasPlayerPassed = false; // Oyuncunun ge�ip ge�medi�ini kontrol etmek i�in
    private bool nextLevelUnlocked = false; // Bir sonraki seviyenin kilidinin a��l�p a��lmad���n� kontrol etmek i�in
    private const string NextLevelUnlockedKey = "NextLevelUnlocked";

    public void Pass()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;

        // Bir sonraki seviyenin kilidini a�
        UnlockNextLevel();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasPlayerPassed)
        {
            // E�er bir sonraki seviye hen�z kilidi a��lmad�ysa ve objectToDisable null de�ilse, disable hale getir
            if (!nextLevelUnlocked && objectToDisable != null)
            {
                objectToDisable.SetActive(false);
            }

            // SceneController'dan sonraki seviyeye ge�
            SceneController.instance.NextLevel();
            hasPlayerPassed = true; // Oyuncu ge�ti�inde i�aretle
        }
    }

    void UnlockNextLevel()
    {
        if (!nextLevelUnlocked)
        {
            int currentLevel = SceneManager.GetActiveScene().buildIndex;
            PlayerPrefs.SetInt(NextLevelUnlockedKey, 1); // Bir sonraki seviyenin kilidini a��k olarak i�aretle
            PlayerPrefs.Save();
            Debug.Log("LEVEL " + (currentLevel + 1) + " UNLOCKED");
            nextLevelUnlocked = true; // Bir sonraki seviyenin kilidini a��k olarak i�aretle
        }
    }

    private void Start()
    {
        // PlayerPrefs'ten bir sonraki seviyenin kilidinin a��l�p a��lmad���n� kontrol et
        nextLevelUnlocked = PlayerPrefs.GetInt(NextLevelUnlockedKey, 0) == 1;
    }
}
