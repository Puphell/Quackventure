using UnityEngine;
using UnityEngine.UI;

public class MuteSound : MonoBehaviour
{
    public Sprite soundOnSprite; // Ses açýkken görünecek sprite
    public Sprite soundOffSprite; // Ses kapalýyken görünecek sprite

    private bool isMuted = false; // Baþlangýçta sesin açýk olduðunu varsayalým

    private AudioManager audioManager; // AudioManager ile etkileþim için referans

    private Image buttonImage; // Butonun Image bileþeni

    void Start()
    {
        // AudioManager'a eriþim saðlayalým
        audioManager = FindObjectOfType<AudioManager>();

        // Butonun Image bileþenini alalým
        buttonImage = GetComponent<Image>();

        // Baþlangýçta sesin durumuna göre butonun görünümünü ayarlayalým
        UpdateButtonSprite();
    }

    // Butona týklandýðýnda çalýþacak fonksiyon
    public void ToggleSound()
    {
        // Ses durumunu tersine çevir
        isMuted = !isMuted;

        // AudioManager'da ses durumunu güncelle
        if (audioManager != null)
        {
            audioManager.ToggleMute(isMuted);
        }

        // Butonun görünümünü güncelle
        UpdateButtonSprite();
    }

    // Butonun görünümünü güncelleyen yardýmcý fonksiyon
    private void UpdateButtonSprite()
    {
        // Ses kapalýysa, ses kapalý sprite'ýný kullan; aksi halde ses açýk sprite'ýný kullan
        buttonImage.sprite = isMuted ? soundOffSprite : soundOnSprite;
    }
}
