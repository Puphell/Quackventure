using UnityEngine;

public class Portal : MonoBehaviour
{
    // Iþýnlanma yapýlacak hedef nokta
    public Transform targetPoint;

    // Iþýnlanma sýrasýnda çalýnacak ses
    public AudioClip teleportSound;

    private AudioSource audioSource;

    private void Start()
    {
        // AudioSource bileþenini al
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Eðer Player tagine sahip bir nesne portalýn içine girerse
        if (other.CompareTag("Player"))
        {
            // Player'ý hedef noktaya ýþýnla
            other.transform.position = targetPoint.position;

            // Iþýnlanma sýrasýnda ses çal
            if (teleportSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(teleportSound);
            }
        }
    }
}
