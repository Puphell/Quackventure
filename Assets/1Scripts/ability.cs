using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour
{
    public float speedIncreaseAmount = 5f; // Artýþ miktarý
    public float abilityDuration = 5f; // Yetenek süresi
    public AudioClip abilitySound; // Yetenek sesi
    public List<GameObject> targetObjects; // Çarpýþmayý kontrol etmek istediðiniz objelerin listesi

    private Move playerController; // Player hareket scripti referansý
    private AudioSource audioSource; // Ses kaynaðý
    private float normalSpeed; // Normal hýz deðeri
    private bool isActive = false; // Yetenek etkin mi?

    public Text abilityDurationText; // Süre metni
    private float currentAbilityTime; // Geçerli yetenek zamaný
    private SpriteRenderer spriteRenderer; // Sprite renderer bileþeni
    private float previousSpeed; // Önceki hýz deðeri

    void Start()
    {
        // Player hareket scriptine eriþim
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<Move>();

        // Normal hýz deðerini al
        normalSpeed = playerController.speed;

        // Ses kaynaðýný al
        audioSource = GetComponent<AudioSource>();

        // Yetenek süresi metnini baþlangýçta devre dýþý býrak
        abilityDurationText.gameObject.SetActive(false);

        // Sprite renderer bileþenini al
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Eðer çarpýþan obje targetObjects listesinde varsa ve etkin deðilse
        if (targetObjects.Contains(other.gameObject) && !isActive)
        {
            // Yeteneði etkinleþtir
            ActivateAbility();
        }
        // Eðer player ile temas ederse ve etkin deðilse
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

        // Önceki hýzý sakla
        previousSpeed = playerController.speed;

        // Hýzý arttýr
        playerController.speed += speedIncreaseAmount;

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

        // Player ile temas edildiðinde sprite renderer'ýn alfa deðerini 0 yap
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
        abilityDurationText.text = Mathf.Round(currentAbilityTime).ToString();
    }

    void DeactivateAbility()
    {
        // Yetenek devre dýþý býrakýldý
        isActive = false;

        // Hýzý normale döndür
        playerController.speed = previousSpeed;

        // Objeyi devre dýþý býrak (sahnedeki görünürlüðünü kapat)
        gameObject.SetActive(false);

        // Süre metnini devre dýþý býrak
        abilityDurationText.gameObject.SetActive(false);
    }
}
