using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialSceneManager : MonoBehaviour
{
    private const string FirstTimeKey = "FirstTimePlaying";

    // Devre dýþý býrakýlacak script'in referansý
    public MonoBehaviour scriptToDisable;

    public void CheckFirstTime()
    {
        // Oyuncunun oyuna ilk defa girip girmediðini kontrol et
        if (!PlayerPrefs.HasKey(FirstTimeKey))
        {
            // Ýlk defa giriyorsa, anahtar oluþtur ve deðeri 1 olarak ayarla
            PlayerPrefs.SetInt(FirstTimeKey, 1);
            PlayerPrefs.Save();

            // Tutorial sahnesine geçiþ yap
            SceneManager.LoadScene("TutorialScene");
        }
        else
        {
            // Oyuncu daha önce oyuna girmiþse, burada baþka bir iþlem yapýlabilir
            Debug.Log("Oyuncu daha önce oyuna girmiþ.");
        }

        // Devre dýþý býrakýlacak script'i disable yap
        if (scriptToDisable != null)
        {
            scriptToDisable.enabled = false;
        }
    }
}
