using UnityEngine;
using UnityEngine.UI;

public class childalpha0 : MonoBehaviour
{   
   private SpriteRenderer spriteRenderer; // Sprite renderer bileþeni

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Eðer Player ile temas ederse ve etkin deðilse
        if (other.CompareTag("Player"))
        {
            // Yeteneði etkinleþtir
            ActivateAbility();
        }
    }

    void ActivateAbility()
    {

        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);
        }
    }
}
