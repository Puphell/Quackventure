using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour
{
    public float speedIncreaseAmount = 5f; // Art�� miktar�
    public float abilityDuration = 5f; // Yetenek s�resi
    public AudioClip abilitySound; // Yetenek sesi
    public List<GameObject> targetObjects; // �arp��may� kontrol etmek istedi�iniz objelerin listesi

    private Move playerController; // Player hareket scripti referans�
    private AudioSource audioSource; // Ses kayna��
    private float normalSpeed; // Normal h�z de�eri
    private bool isActive = false; // Yetenek etkin mi?

    public Text abilityDurationText; // S�re metni
    private float currentAbilityTime; // Ge�erli yetenek zaman�
    private SpriteRenderer spriteRenderer; // Sprite renderer bile�eni
    private float previousSpeed; // �nceki h�z de�eri

    void Start()
    {
        // Player hareket scriptine eri�im
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<Move>();

        // Normal h�z de�erini al
        normalSpeed = playerController.speed;

        // Ses kayna��n� al
        audioSource = GetComponent<AudioSource>();

        // Yetenek s�resi metnini ba�lang��ta devre d��� b�rak
        abilityDurationText.gameObject.SetActive(false);

        // Sprite renderer bile�enini al
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // E�er �arp��an obje targetObjects listesinde varsa ve etkin de�ilse
        if (targetObjects.Contains(other.gameObject) && !isActive)
        {
            // Yetene�i etkinle�tir
            ActivateAbility();
        }
        // E�er player ile temas ederse ve etkin de�ilse
        if (other.CompareTag("Player") && !isActive)
        {
            // Yetene�i etkinle�tir
            ActivateAbility();
        }
    }

    void ActivateAbility()
    {
        // Yetenek etkinle�tirildi
        isActive = true;

        // �nceki h�z� sakla
        previousSpeed = playerController.speed;

        // H�z� artt�r
        playerController.speed += speedIncreaseAmount;

        // Belirli bir s�re sonra yetene�i devre d��� b�rak
        Invoke("DeactivateAbility", abilityDuration);

        // Yetenek sesini �al
        if (audioSource != null && abilitySound != null)
        {
            audioSource.PlayOneShot(abilitySound);
        }

        // S�re metnini etkinle�tir ve s�reyi ayarla
        abilityDurationText.gameObject.SetActive(true);
        currentAbilityTime = abilityDuration;
        UpdateAbilityDurationText();

        // Player ile temas edildi�inde sprite renderer'�n alfa de�erini 0 yap
        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);
        }
    }

    void Update()
    {
        // E�er yetenek aktifse s�reyi azalt
        if (isActive)
        {
            currentAbilityTime -= Time.deltaTime;
            UpdateAbilityDurationText();

            // S�re bitti�inde yetene�i devre d��� b�rak
            if (currentAbilityTime <= 0)
            {
                DeactivateAbility();
            }
        }
    }

    void UpdateAbilityDurationText()
    {
        // S�re metnini g�ncelle
        abilityDurationText.text = Mathf.Round(currentAbilityTime).ToString();
    }

    void DeactivateAbility()
    {
        // Yetenek devre d��� b�rak�ld�
        isActive = false;

        // H�z� normale d�nd�r
        playerController.speed = previousSpeed;

        // Objeyi devre d��� b�rak (sahnedeki g�r�n�rl���n� kapat)
        gameObject.SetActive(false);

        // S�re metnini devre d��� b�rak
        abilityDurationText.gameObject.SetActive(false);
    }
}
