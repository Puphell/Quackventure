using UnityEngine;
using UnityEngine.SceneManagement;

public class TabelaEtkilesim4 : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // Eğer Player Tabela ile etkileşime girerse
        if (other.CompareTag("Player"))
        {
            // Sahneyi değiştir (3. sahne adınızı belirtin)
            SceneManager.LoadScene("Sahne 4");
        }
    }
}