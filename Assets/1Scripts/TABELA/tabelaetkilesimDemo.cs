using UnityEngine;
using UnityEngine.SceneManagement;

public class TabelaEtkilesimForSahne3Demo : MonoBehaviour
{
    public GameObject objectToDisable; // Disable edilecek GameObject

    void OnTriggerEnter2D(Collider2D other)
    {
        // Eğer Player Tabela ile etkileşime girerse
        if (other.CompareTag("Player"))
        {
            // Sahneyi değiştir (3. sahne adınızı belirtin)
            SceneManager.LoadScene("Thanks");
            // Eğer objectToDisable null değilse, disable hale getir
            if (objectToDisable != null)
            {
                objectToDisable.SetActive(false);
            }
        }
    }
}