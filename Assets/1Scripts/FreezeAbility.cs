using UnityEngine;
using UnityEngine.UI;

public class FreezeAbility : MonoBehaviour
{
    public float freezeDuration = 3f; // Dondurma s�resi
    public MonoBehaviour[] scriptsToDisable; // Dondurulacak d��man objesinin scriptleri
    public Animator[] animatorsToTrigger;
    private bool hasTriggered = false;
    public AudioSource audioSource;
    public Text freezeDurationText; // S�re metni

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
            freezeDuration -= Time.deltaTime; // Zaman� azalt

            if (freezeDuration <= 0)
            {
                Unfreeze();
            }

            // S�re metnini g�ncelle
            UpdateFreezeDurationText();
        }
    }

    private void Freeze()
    {
        foreach (MonoBehaviour script in scriptsToDisable)
        {
            script.enabled = false; // D��man�n scriptlerini devre d��� b�rak
        }
        isFreezing = true;
        freezeDurationText.gameObject.SetActive(true); // Metni etkinle�tir
        UpdateFreezeDurationText(); // Metni g�ncelle
    }

    private void Unfreeze()
    {
        foreach (MonoBehaviour script in scriptsToDisable)
        {
            script.enabled = true; // D��man�n scriptlerini yeniden etkinle�tir
        }
        isFreezing = false;
        freezeDuration = 3f; // Ba�lang�� donma s�resine geri d�n
        freezeDurationText.gameObject.SetActive(false); // Metni devre d��� b�rak

        Destroy(gameObject);
    }

    private void UpdateFreezeDurationText()
    {
        freezeDurationText.text = " " + Mathf.Round(freezeDuration).ToString();
    }
}
