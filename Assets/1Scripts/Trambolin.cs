using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float bounceForce = 10f; // Sýçrama kuvveti
    public AudioClip bounceSound; // Sýçrama sesi

    private AudioSource audioSource; // Ses kaynaðý

    void Start()
    {
        // Ses kaynaðýný al
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Eðer ses kaynaðý yoksa, yeni bir tane oluþtur
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Eðer temas eden obje bir Player ise
        if (other.CompareTag("Player"))
        {
            // Player'a yukarý doðru bir kuvvet uygula
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = new Vector2(rb.velocity.x, bounceForce);

                // Ses çal
                if (bounceSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(bounceSound);
                }
            }
        }
    }
}
