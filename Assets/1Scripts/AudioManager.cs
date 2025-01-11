using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    private AudioSource audioSource; // Ses çalmak için AudioSource bileþeni

    private bool isMuted = false; // Sesin açýk veya kapalý olduðunu belirten bir bayrak

    // PlayerPrefs için anahtarlar
    private const string MUTE_KEY = "IsMuted";

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Müzik objesini sahneler arasý geçiþlerde koru
        }
        else
        {
            Destroy(gameObject); // Birden fazla instance varsa bunu yok et
        }

        // AudioSource bileþenini al
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


    // Sesin açýk/kapalý durumunu deðiþtiren yöntem
    // Sesin açýk/kapalý durumunu deðiþtiren yöntem
    public void ToggleMute(bool mute)
    {
        isMuted = mute;

        // Sesin açýk veya kapalý durumuna göre sesi aç veya kapat
        audioSource.volume = isMuted ? 0f : 1f; // Ses kapalýysa 0, açýksa 0.5

        // Ses durumunu PlayerPrefs'e kaydet
        PlayerPrefs.SetInt(MUTE_KEY, isMuted ? 1 : 0);
        PlayerPrefs.Save(); // PlayerPrefs verilerini kaydet
    }

}
