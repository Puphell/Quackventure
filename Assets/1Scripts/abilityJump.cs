using UnityEngine;
using UnityEngine.UI;

public class AbilityJump : MonoBehaviour
{
    public float jumpIncreaseAmount = 100f; // Artýþ miktarý
    public float abilityDuration = 5f; // Yetenek süresi
    public AudioClip abilitySound; // Yetenek sesi

    private Move playerController; // Player hareket scripti referansý
    private AudioSource audioSource; // Ses kaynaðý
    private float normalJumpForce; // Normal zýplama kuvveti
    private bool isActive = false; // Yetenek etkin mi?

    private SpriteRenderer spriteRenderer; // Sprite renderer bileþeni

    public Text abilityDurationText; // Süre metni
    private float currentAbilityTime; // Geçerli yetenek zamaný

    private float previousJumpForce; // Önceki zýplama kuvveti

    void Start()
    {
        // Player hareket scriptine eriþim
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<Move>();

        // Normal zýplama kuvvetini al
        normalJumpForce = playerController.jumpForce;

        // Ses kaynaðýný al
        audioSource = GetComponent<AudioSource>();

        // Yetenek süresi metnini baþlangýçta devre dýþý býrak
        abilityDurationText.gameObject.SetActive(false);

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Eðer Player ile temas ederse ve etkin deðilse
        if (other.CompareTag("Player") && !isActive)
        {
            // Yeteneði etkinleþtir
            ActivateAbility();
        }
    }

    void ActivateAbility()
    {
        // Yetenek etkinleþtirildi
        isActive = true;

        // Önceki zýplama kuvvetini sakla
        previousJumpForce = playerController.jumpForce;

        // Zýplama kuvvetini arttýr
        playerController.jumpForce += jumpIncreaseAmount;

        // Belirli bir süre sonra yeteneði devre dýþý býrak
        Invoke("DeactivateAbility", abilityDuration);

        // Yetenek sesini çal
        if (audioSource != null && abilitySound != null)
        {
            audioSource.PlayOneShot(abilitySound);
        }

        // Süre metnini etkinleþtir ve süreyi ayarla
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
        // Eðer yetenek aktifse süreyi azalt
        if (isActive)
        {
            currentAbilityTime -= Time.deltaTime;
            UpdateAbilityDurationText();

            // Süre bittiðinde yeteneði devre dýþý býrak
            if (currentAbilityTime <= 0)
            {
                DeactivateAbility();
            }
        }
    }

    void UpdateAbilityDurationText()
    {
        // Süre metnini güncelle
        abilityDurationText.text = " " + Mathf.Round(currentAbilityTime).ToString();
    }

    void DeactivateAbility()
    {
        // Yetenek devre dýþý býrakýldý
        isActive = false;

        // Zýplama kuvvetini önceki deðere döndür
        playerController.jumpForce = previousJumpForce;

        // Objeyi devre dýþý býrak (sahnedeki görünürlüðünü kapat)
        gameObject.SetActive(false);

        // Süre metnini devre dýþý býrak
        abilityDurationText.gameObject.SetActive(false);
    }
}
