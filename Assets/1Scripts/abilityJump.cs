using UnityEngine;
using UnityEngine.UI;

public class AbilityJump : MonoBehaviour
{
    public float jumpIncreaseAmount = 100f; // Art�� miktar�
    public float abilityDuration = 5f; // Yetenek s�resi
    public AudioClip abilitySound; // Yetenek sesi

    private Move playerController; // Player hareket scripti referans�
    private AudioSource audioSource; // Ses kayna��
    private float normalJumpForce; // Normal z�plama kuvveti
    private bool isActive = false; // Yetenek etkin mi?

    private SpriteRenderer spriteRenderer; // Sprite renderer bile�eni

    public Text abilityDurationText; // S�re metni
    private float currentAbilityTime; // Ge�erli yetenek zaman�

    private float previousJumpForce; // �nceki z�plama kuvveti

    void Start()
    {
        // Player hareket scriptine eri�im
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<Move>();

        // Normal z�plama kuvvetini al
        normalJumpForce = playerController.jumpForce;

        // Ses kayna��n� al
        audioSource = GetComponent<AudioSource>();

        // Yetenek s�resi metnini ba�lang��ta devre d��� b�rak
        abilityDurationText.gameObject.SetActive(false);

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // E�er Player ile temas ederse ve etkin de�ilse
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

        // �nceki z�plama kuvvetini sakla
        previousJumpForce = playerController.jumpForce;

        // Z�plama kuvvetini artt�r
        playerController.jumpForce += jumpIncreaseAmount;

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
        abilityDurationText.text = " " + Mathf.Round(currentAbilityTime).ToString();
    }

    void DeactivateAbility()
    {
        // Yetenek devre d��� b�rak�ld�
        isActive = false;

        // Z�plama kuvvetini �nceki de�ere d�nd�r
        playerController.jumpForce = previousJumpForce;

        // Objeyi devre d��� b�rak (sahnedeki g�r�n�rl���n� kapat)
        gameObject.SetActive(false);

        // S�re metnini devre d��� b�rak
        abilityDurationText.gameObject.SetActive(false);
    }
}
