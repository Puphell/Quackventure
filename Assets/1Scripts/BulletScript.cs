using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;

    private GameObject player; // Player objesini referans almak için

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // Dart'ýn hedefini Player'ýn pozisyonu olarak ayarla
            Vector2 direction = player.transform.position - transform.position;
            rb.velocity = direction.normalized * speed;
        }
        else
        {
            Debug.LogWarning("Player objesi bulunamadý!");
            // Player objesi bulunamazsa, default olarak saða doðru hareket et
            rb.velocity = -transform.right * speed;
        }

        Destroy(gameObject, 4f);
    }

    private void Update()
    {
        // Dart'ýn yönünü deðiþtirmeniz gerekiyorsa, burada gerekli kontrolleri yapabilirsiniz.
    }
}