using UnityEngine;
using UnityEngine.UI;

public class FreezeAbility : MonoBehaviour
{
    public float freezeDuration = 3f; // Dondurma süresi
    public MonoBehaviour[] scriptsToDisable; // Dondurulacak düþman objesinin scriptleri
    public Animator[] animatorsToTrigger;
    private bool hasTriggered = false;
    public AudioSource audioSource;
    public Text freezeDurationText; // Süre metni

    private bool isFreezing = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasTriggered && collision.CompareTag("Player") && !isFreezing)
        {
            hasTriggered = true;

            if (audioSource != null)
            {
                audioSource.Play();
            }
            if (GetComponent<SpriteRenderer>() != null)
            {
                Color currentColor = GetComponent<SpriteRenderer>().color;
                currentColor.a = 0f;
                GetComponent<SpriteRenderer>().color = currentColor;
            }
            foreach (var animator in animatorsToTrigger)
            {
                if (animator != null)
                {
                    animator.SetTrigger("isFreezingTrigger");
                }
            }
            Freeze();
        }
    }

    private void Update()
    {
        if (isFreezing)
        {
            freezeDuration -= Time.deltaTime; // Zamaný azalt

            if (freezeDuration <= 0)
            {
                Unfreeze();
            }

            // Süre metnini güncelle
            UpdateFreezeDurationText();
        }
    }

    private void Freeze()
    {
        foreach (MonoBehaviour script in scriptsToDisable)
        {
            script.enabled = false; // Düþmanýn scriptlerini devre dýþý býrak
        }
        isFreezing = true;
        freezeDurationText.gameObject.SetActive(true); // Metni etkinleþtir
        UpdateFreezeDurationText(); // Metni güncelle
    }

    private void Unfreeze()
    {
        foreach (MonoBehaviour script in scriptsToDisable)
        {
            script.enabled = true; // Düþmanýn scriptlerini yeniden etkinleþtir
        }
        isFreezing = false;
        freezeDuration = 3f; // Baþlangýç donma süresine geri dön
        freezeDurationText.gameObject.SetActive(false); // Metni devre dýþý býrak

        Destroy(gameObject);
    }

    private void UpdateFreezeDurationText()
    {
        freezeDurationText.text = " " + Mathf.Round(freezeDuration).ToString();
    }
}
