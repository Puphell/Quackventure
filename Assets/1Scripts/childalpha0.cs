using UnityEngine;
using UnityEngine.UI;

public class childalpha0 : MonoBehaviour
{   
   private SpriteRenderer spriteRenderer; // Sprite renderer bile�eni

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // E�er Player ile temas ederse ve etkin de�ilse
        if (other.CompareTag("Player"))
        {
            // Yetene�i etkinle�tir
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
