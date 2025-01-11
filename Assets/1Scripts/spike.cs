using UnityEngine;

public class SpikeController : MonoBehaviour
{
    // ...

    // Delegate tanımla
    public delegate void PlayerDiedHandler();
    public static event PlayerDiedHandler OnPlayerDied;

    // ...

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Yeni ekledik: OnPlayerDied event'ini tetikle
            OnPlayerDied?.Invoke();
        }
    }
}
