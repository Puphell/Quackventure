using UnityEngine;
using UnityEngine.UI;

public class MuteSound : MonoBehaviour
{
    public Sprite soundOnSprite; // Ses a��kken g�r�necek sprite
    public Sprite soundOffSprite; // Ses kapal�yken g�r�necek sprite

    private bool isMuted = false; // Ba�lang��ta sesin a��k oldu�unu varsayal�m

    private AudioManager audioManager; // AudioManager ile etkile�im i�in referans

    private Image buttonImage; // Butonun Image bile�eni

    void Start()
    {
        // AudioManager'a eri�im sa�layal�m
        audioManager = FindObjectOfType<AudioManager>();

        // Butonun Image bile�enini alal�m
        buttonImage = GetComponent<Image>();

        // Ba�lang��ta sesin durumuna g�re butonun g�r�n�m�n� ayarlayal�m
        UpdateButtonSprite();
    }

    // Butona t�kland���nda �al��acak fonksiyon
    public void ToggleSound()
    {
        // Ses durumunu tersine �evir
        isMuted = !isMuted;

        // AudioManager'da ses durumunu g�ncelle
        if (audioManager != null)
        {
            audioManager.ToggleMute(isMuted);
        }

        // Butonun g�r�n�m�n� g�ncelle
        UpdateButtonSprite();
    }

    // Butonun g�r�n�m�n� g�ncelleyen yard�mc� fonksiyon
    private void UpdateButtonSprite()
    {
        // Ses kapal�ysa, ses kapal� sprite'�n� kullan; aksi halde ses a��k sprite'�n� kullan
        buttonImage.sprite = isMuted ? soundOffSprite : soundOnSprite;
    }
}
