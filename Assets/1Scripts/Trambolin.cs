using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float bounceForce = 10f; // S��rama kuvveti
    public AudioClip bounceSound; // S��rama sesi

    private AudioSource audioSource; // Ses kayna��

    void Start()
    {
        // Ses kayna��n� al
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // E�er ses kayna�� yoksa, yeni bir tane olu�tur
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // E�er temas eden obje bir Player ise
        if (other.CompareTag("Player"))
        {
            // Player'a yukar� do�ru bir kuvvet uygula
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = new Vector2(rb.velocity.x, bounceForce);

                // Ses �al
                if (bounceSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(bounceSound);
                }
            }
        }
    }
}
