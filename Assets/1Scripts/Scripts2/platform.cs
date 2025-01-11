using UnityEngine;
using UnityEngine.UI;

public class PlatformController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    private float fadeDuration = 2f;
    private float elapsedTime = 0f;
    private bool shouldFade = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Platforma bir kere temas edildiyse ve alpha de�eri 0'dan b�y�kse azalt
        if (shouldFade && spriteRenderer.color.a > 0)
        {
            elapsedTime += Time.deltaTime;
            float alphaValue = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            Color newColor = spriteRenderer.color;
            newColor.a = alphaValue;
            spriteRenderer.color = newColor;
        }

        // Alpha de�eri 0'a ula�t���nda ve daha fazla azaltmaya gerek kalmad���nda platformu yok et
        if (shouldFade && spriteRenderer.color.a <= 0)
        {
            DestroyPlatform();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Player tagine sahip obje platforma temas ettiyse ve platform daha �nce temas edilmediyse
        if (collision.gameObject.CompareTag("Player"))
        {
            shouldFade = true;
            if (collision.relativeVelocity.y <= 0f)
            {
                collision.gameObject.GetComponent<JumpGameMove>().Jump();
                Debug.Log("Player platforma girdi.");
            }
        }
    }

    void DestroyPlatform()
    {
        // Platformu yok et
        Destroy(gameObject);
    }
}
