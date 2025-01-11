using UnityEngine;

public class Portal : MonoBehaviour
{
    // I��nlanma yap�lacak hedef nokta
    public Transform targetPoint;

    // I��nlanma s�ras�nda �al�nacak ses
    public AudioClip teleportSound;

    private AudioSource audioSource;

    private void Start()
    {
        // AudioSource bile�enini al
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // E�er Player tagine sahip bir nesne portal�n i�ine girerse
        if (other.CompareTag("Player"))
        {
            // Player'� hedef noktaya ���nla
            other.transform.position = targetPoint.position;

            // I��nlanma s�ras�nda ses �al
            if (teleportSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(teleportSound);
            }
        }
    }
}
