using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScript : MonoBehaviour
{
    public delegate void LevelUnlockedAction(int levelIndex);
    public static event LevelUnlockedAction OnLevelUnlocked;

    public GameObject objectToDisable; // Disable edilecek GameObject
    private bool hasPlayerPassed = false; // Oyuncunun geçip geçmediğini kontrol etmek için

    public void Pass()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;

        // Bir sonraki seviyenin kilidini aç
        UnlockNextLevel(currentLevel);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasPlayerPassed)
        {
            // SceneController'dan sonraki seviyeye geç
            SceneController.instance.NextLevel();
            // Eğer objectToDisable null değilse, disable hale getir
            if (objectToDisable != null)
            {
                objectToDisable.SetActive(false);
            }
            hasPlayerPassed = true; // Oyuncu geçtiğinde işaretle
        }
    }

    void UnlockNextLevel(int levelIndex)
    {
        PlayerPrefs.SetInt("LevelUnlocked_" + (levelIndex + 1), 1); // Unlock index sistemi 1'den başlaması için 1 eklen
        PlayerPrefs.Save();

        Debug.Log("LEVEL " + (levelIndex + 1) + " UNLOCKED");

        // Level unlocked eventini tetikle
        OnLevelUnlocked?.Invoke(levelIndex + 1); // Unlock index sistemi 1'den başladığı için 1 ekle
    }
}
