using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialSceneManager : MonoBehaviour
{
    private const string FirstTimeKey = "FirstTimePlaying";

    // Devre d��� b�rak�lacak script'in referans�
    public MonoBehaviour scriptToDisable;

    public void CheckFirstTime()
    {
        // Oyuncunun oyuna ilk defa girip girmedi�ini kontrol et
        if (!PlayerPrefs.HasKey(FirstTimeKey))
        {
            // �lk defa giriyorsa, anahtar olu�tur ve de�eri 1 olarak ayarla
            PlayerPrefs.SetInt(FirstTimeKey, 1);
            PlayerPrefs.Save();

            // Tutorial sahnesine ge�i� yap
            SceneManager.LoadScene("TutorialScene");
        }
        else
        {
            // Oyuncu daha �nce oyuna girmi�se, burada ba�ka bir i�lem yap�labilir
            Debug.Log("Oyuncu daha �nce oyuna girmi�.");
        }

        // Devre d��� b�rak�lacak script'i disable yap
        if (scriptToDisable != null)
        {
            scriptToDisable.enabled = false;
        }
    }
}
