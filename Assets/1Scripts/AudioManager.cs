using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    private AudioSource audioSource; // Ses �almak i�in AudioSource bile�eni

    private bool isMuted = false; // Sesin a��k veya kapal� oldu�unu belirten bir bayrak

    // PlayerPrefs i�in anahtarlar
    private const string MUTE_KEY = "IsMuted";

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // M�zik objesini sahneler aras� ge�i�lerde koru
        }
        else
        {
            Destroy(gameObject); // Birden fazla instance varsa bunu yok et
        }

        // AudioSource bile�enini al
        audioSource = GetComponent<AudioSource>();

        // PlayerPrefs'ten ses durumunu kontrol et
        if (PlayerPrefs.HasKey(MUTE_KEY))
        {
            isMuted = PlayerPrefs.GetInt(MUTE_KEY) == 1 ? true : false;
            ToggleMute(isMuted);
        }

        // Ses seviyesini 0.5 olarak ayarla
        audioSource.volume = 1;
    }


    // Sesin a��k/kapal� durumunu de�i�tiren y�ntem
    // Sesin a��k/kapal� durumunu de�i�tiren y�ntem
    public void ToggleMute(bool mute)
    {
        isMuted = mute;

        // Sesin a��k veya kapal� durumuna g�re sesi a� veya kapat
        audioSource.volume = isMuted ? 0f : 1f; // Ses kapal�ysa 0, a��ksa 0.5

        // Ses durumunu PlayerPrefs'e kaydet
        PlayerPrefs.SetInt(MUTE_KEY, isMuted ? 1 : 0);
        PlayerPrefs.Save(); // PlayerPrefs verilerini kaydet
    }

}
